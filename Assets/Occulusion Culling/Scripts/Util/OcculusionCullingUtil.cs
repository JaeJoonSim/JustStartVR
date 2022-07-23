//  
//

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace JustStart.OcculusionCulling
{
    public static class OcculusionCullingUtil
    {
        public static string FormatSeconds(double seconds)
        {
            System.TimeSpan ts = System.TimeSpan.FromSeconds(seconds);

            return ts.ToString(@"hh\:mm\:ss");
        }
        
        public static HashSet<OcculusionCullingBakeGroup> CreateBakeGroupsForRenderers(List<Renderer> inputRenderers, System.Func<Renderer, bool> filter, OcculusionCullingBakingBehaviour attachedBakingBehaviour = null)
        {
            // Filter all unsupported renderers
            inputRenderers.RemoveAll(rend => filter != null && !filter.Invoke(rend));

            // We are going to manipulate this. So we clone it just in case. We also use a Hashset to prevent double insertions.
            HashSet<Renderer> sceneRenderers = new HashSet<Renderer>(inputRenderers);

            // Find all the objects in the scene
            HashSet<LODGroup> lodGroups = new HashSet<LODGroup>(UnityEngine.Object.FindObjectsOfType<LODGroup>());
            HashSet<OcculusionCullingMonoGroup> OcculusionCullingGroups = new HashSet<OcculusionCullingMonoGroup>(FindMonoGroupsForBakingBehaviour(attachedBakingBehaviour));

            lodGroups.RemoveWhere((lodGroup) =>
            {
                foreach (LOD lod in lodGroup.GetLODs())
                {
                    foreach (Renderer r in lod.renderers)
                    {
                        if (sceneRenderers.Contains(r))
                        {
                            // Keep this LODGroup. We selected its renderers.
                            return false;
                        }
                    }
                }
                
                return true;
            });
            
            // Our output list
            HashSet<OcculusionCullingBakeGroup> result = new HashSet<OcculusionCullingBakeGroup>(new OcculusionCullingBakeGroupComparer());

            // We are going to remove already processed elements from our renderers list.
            //
            // Priority goes as follow:
            // - OcculusionCullingGroup
            // - LODGroup
            // - Anything else

            // OcculusionCullingGroup
            foreach (OcculusionCullingMonoGroup OcculusionCullingGroup in OcculusionCullingGroups)
            {
                foreach (Renderer renderer in OcculusionCullingGroup.Renderers)
                {
                    sceneRenderers.Remove(renderer);
                }
                
                result.Add(new OcculusionCullingBakeGroup()
                {
                    renderers = OcculusionCullingGroup.Renderers.ToArray(), // Renderers filters null renderers automatically
                    unityBehaviours = OcculusionCullingGroup.UnityBehaviours.ToArray(), // UnityBehaviours filters null renderers automatically
                    groupType = OcculusionCullingBakeGroup.GroupType.User
                });
            }

            // LODGroup
            foreach (LODGroup lodGroup in lodGroups)
            {
                HashSet<Renderer> renderersInLodGroup = new HashSet<Renderer>();
                    
                foreach (LOD lod in lodGroup.GetLODs())
                {
                    foreach (Renderer renderer in lod.renderers)
                    {
                        if (renderer == null)
                        {
                            Debug.LogWarning($"Found null renderer in LODGroup: {lodGroup.name}. Selecting the LODGroup might remove the invalid renderer(s) for you.", lodGroup.gameObject);
                            
                            continue;
                        }
                        
                        renderersInLodGroup.Add(renderer);
                        sceneRenderers.Remove(renderer);
                    }
                }
                
                if (renderersInLodGroup.Count == 0)
                {
                    continue;
                }
                
                result.Add(new OcculusionCullingBakeGroup()
                {
                    renderers = renderersInLodGroup.ToArray(),
                    groupType = OcculusionCullingBakeGroup.GroupType.LOD
                });
            }
            
            // Remaining renderers
            foreach (Renderer renderer in sceneRenderers)
            {
                result.Add(new OcculusionCullingBakeGroup()
                {
                    renderers = new Renderer[] { renderer },
                    groupType = OcculusionCullingBakeGroup.GroupType.Other
                });
            }
            
            return result;
        }

        public static OcculusionCullingMonoGroup[] FindMonoGroupsForBakingBehaviour(OcculusionCullingBakingBehaviour attachedBakingBehaviour)
        {
            return UnityEngine.Object.FindObjectsOfType<OcculusionCullingMonoGroup>().Where((group) =>
            {
                if (group.restrictToBehaviours.Length == 0 || attachedBakingBehaviour == null)
                {
                    return true;
                }

                return System.Array.IndexOf(group.restrictToBehaviours, attachedBakingBehaviour) >= 0;
            }).ToArray();
        }

        public static string FormatNumber(int number)
        {
            if (number >= 100000000) {
                return (number / 1000000f).ToString("0.#M");
            }
            else if (number >= 1000000) {
                return (number / 1000000f).ToString("0.##M");
            }
            else if (number >= 100000) {
                return (number / 1000f).ToString("0.#k");
            }
            else if (number >= 10000) {
                return (number / 1000f).ToString("0.##k");
            }

            return number.ToString("#,0");
        }

        public static float FindValidDivisorCloseToUserProvided(float userProvidedDivisor, float volumeSize)
        {
            float bestFit = 0;
            
            for (float i = 0; i < volumeSize; i += 1f / 4f)
            {
                if (volumeSize % i == 0)
                {
                    if ((bestFit <= 0) ||
                        Mathf.Abs(i - userProvidedDivisor) < Mathf.Abs(bestFit - userProvidedDivisor))
                    {
                        bestFit = i;
                    }
                }
            }

            if (bestFit <= 0)
            {
                return volumeSize;
            }

            return bestFit;
        }

        // Keywords that we use to assume the material should be transparent.
        private static readonly string[] transparentShaderKeywordHints = new string[]
        {
            "_ALPHATEST_ON",
            "ALPHACLIPPING_ON"
        };

        public const byte OPAQUE_RENDER_COLOR = 0;
        public const byte TRANSPARENT_RENDER_COLOR = 128;

            
        public static bool IsMaterialTransparent(Material mat)
        {
#pragma warning disable 162
            if (mat == null)
            {
                return false;
            }
            
            if (!OcculusionCullingSettings.Instance.renderTransparency)
            {
                return false;
            }

            // Check whether this material is forced to render transparent or opaque.
            string nameLower = mat.name.ToLower();

            if (nameLower.Contains("pc_trans"))
            {
                return true;
            }
            
            if (nameLower.Contains("pc_opaque"))
            {
                return false;
            }
            
            // It's pretty hard to detect transparent materials. Lets use some shader keywords as a hint.

            foreach (var keyword in transparentShaderKeywordHints)
            {
                if (mat.IsKeywordEnabled(keyword))
                {
                    return true;
                }
            }
            
            return mat.renderQueue >= 2450;
#pragma warning restore 162
        }

        // Allows to customize the behaviour easily. For instance could assign it to a different layer, etc. instead.
        public static void ToggleRenderer(Renderer r, bool visible, bool forceNullCheck, ShadowCastingMode defaultShadowCastingMode)
        {
            if (forceNullCheck && r == null)
            {
                // We forced null checks for a reason and are aware. No need to output anything.
                return;
            }
            
#if UNITY_EDITOR
            if (r == null)
            {
                // This is an unexpected null renderer. We want to log an error.
                
                Debug.LogError("Encountered null renderer.");
                
                return;
            }
#endif
            
#pragma warning disable 162
            switch (OcculusionCullingConstants.ToggleRenderMode)
            {
                case OcculusionCullingRenderToggleMode.ToggleRendererComponent:
                    r.enabled = visible;
                    break;
                
                case OcculusionCullingRenderToggleMode.ToggleShadowcastMode:
                    if (defaultShadowCastingMode == ShadowCastingMode.Off)
                    {
                        // We don't care about shadows so we might as well disable the entire Renderer.
                        
#if UNITY_2019_3_OR_NEWER
                        goto case OcculusionCullingRenderToggleMode.ToggleForceRenderingOff;
#else
                        goto case OcculusionCullingRenderToggleMode.ToggleRendererComponent;
#endif
                    }

                    if (defaultShadowCastingMode == ShadowCastingMode.TwoSided)
                    {
                        // Looks like we care about the ShadowCastingMode so let's just toggle the Renderer itself.
                        goto case OcculusionCullingRenderToggleMode.ToggleRendererComponent;
                    }
                    
                    r.shadowCastingMode = visible ? defaultShadowCastingMode : ShadowCastingMode.ShadowsOnly;
                    break;

                case OcculusionCullingRenderToggleMode.ToggleForceRenderingOff:
#if !UNITY_2019_3_OR_NEWER
                    // Unsupported before Unity 2019. This is the next best thing we can do (performance wise).
                    // I don't like that this happens silently but printing a warning when it is unsupported doesn't help either.
                    goto case OcculusionCullingRenderToggleMode.ToggleShadowcastMode;
#else
                    r.forceRenderingOff = !visible;
#endif
                    break;
                
                default:
                    throw new System.InvalidOperationException();
                    break;
            }
#pragma warning restore 162
        }
    }
}