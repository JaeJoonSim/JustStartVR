//  
//

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JustStart.OcculusionCulling
{
    /// <summary>
    /// This class contains all the available API. Anything that is not part of this class is not considered to be part of the official API and thus is more likely to change in future versions.
    /// </summary>
    public static class OcculusionCullingAPI
    {
        /// <summary>
        /// Bake related API
        /// </summary>
        public static class Bake
        {
#if UNITY_EDITOR
            public static System.Action<OcculusionCullingBakingBehaviour> OnBakeFinished;
            public static System.Action OnAllBakesFinished;

            public static T[] FindAllBakingBehaviours<T>() 
                where T : OcculusionCullingBakingBehaviour
                => Object.FindObjectsOfType<T>();
            
            public static OcculusionCullingBakingBehaviour[] FindAllBakingBehaviours() 
                => FindAllBakingBehaviours<OcculusionCullingBakingBehaviour>();
            
            /// <summary>
            /// Schedules a bake but doesn't perform the bake yet. Call BakeAllScheduled to start the baking process for all scheduled bakes.
            /// </summary>
            /// <param name="bakeInformation">Information about the bake to schedule</param>
            public static void ScheduleBake(BakeInformation bakeInformation)
                => OcculusionCullingBakingManager.ScheduleBake(bakeInformation);

            /// <summary>
            /// Starts to bake multiple baking behaviours immediately.
            /// </summary>
            /// <param name="cullingBakingBehaviours">All baking behaviours to bake</param>
            /// <param name="additionalOccluders">Additional occluders</param>
            public static void BakeNow(OcculusionCullingBakingBehaviour[] cullingBakingBehaviours, HashSet<Renderer> additionalOccluders = null)
                => OcculusionCullingBakingManager.BakeNow(cullingBakingBehaviours, additionalOccluders);

            /// <summary>
            /// Starts to bake a single baking behaviour immediately.
            /// </summary>
            /// <param name="bakingBehaviour">Baking behaviour to bake</param>
            /// <param name="additionalOccluders">Additional occluders</param>
            public static void BakeNow(OcculusionCullingBakingBehaviour bakingBehaviour, HashSet<Renderer> additionalOccluders = null)
                => OcculusionCullingBakingManager.BakeNow(bakingBehaviour, additionalOccluders);

            /// <summary>
            /// Starts to bake all scheduled bakes.
            /// </summary>
            public static void BakeAllScheduled()
                => OcculusionCullingBakingManager.BakeAllScheduled();

            /// <summary>
            /// Bakes a multi-scene setup by temporarily merging all scenes.
            /// </summary>
            /// <param name="scenes">Scenes that should be included in this bake</param>
            public static void BakeMultiScene(List<Scene> scenes)
                => OcculusionCullingBakingManager.BakeMultiScene(scenes);

            /// <summary>
            /// Allows to bake a single view point. This might be handy for some editor utilities.
            /// WARNING: THIS CHANGES YOUR SCENE!!!
            /// </summary>
            public static HashSet<Renderer> BakeSingleViewPointNow(Vector3 position, List<Renderer> inputRenderers)
            {
                HashSet<Renderer> visibleRenderers = new HashSet<Renderer>();
                
                GameObject tmp = new GameObject();

                tmp.transform.position = position;

                OcculusionCullingVolume vol = tmp.AddComponent<OcculusionCullingVolume>();

                vol.volumeSize = Vector3.one;
                vol.bakeCellSize = Vector3.one;
                
                OcculusionCullingVolumeBakeData bakeData = ScriptableObject.CreateInstance<OcculusionCullingVolumeBakeData>();

                vol.SetBakeData(bakeData);
                
                vol.bakeGroups = OcculusionCullingUtil.CreateBakeGroupsForRenderers(inputRenderers, (x) => true).ToArray();

                var enumerator = vol.PerformBakeAsync(false, false, null);

                while (enumerator.MoveNext())
                {
                    // We block the main thread
                }
                
                OcculusionCullingTemp.ListUshort.Clear();
                vol.BakeData.SampleAtIndex(0, OcculusionCullingTemp.ListUshort);

                foreach (ushort index in OcculusionCullingTemp.ListUshort)
                {
                    foreach (Renderer renderer in vol.bakeGroups[index].renderers)
                    {
                        visibleRenderers.Add(renderer);
                    }
                }
                
                Object.DestroyImmediate(vol.BakeData);
                Object.DestroyImmediate(tmp);
                
                return visibleRenderers;
            }
#endif
        }

        /// <summary>
        /// BakeGroup related API
        /// </summary>
        public static class BakeGroup
        {
            /// <summary>
            /// Returns number of renderers that are part of OcculusionCullingBakeGroup
            /// </summary>
            public static int GetRendererCount(OcculusionCullingBakeGroup group)
                => group.GetRuntimeRendererCount();
            
            /// <summary>
            /// Returns whether specified renderer is part of OcculusionCullingBakeGroup
            /// </summary>
            public static bool ContainsRenderer(OcculusionCullingBakeGroup group, Renderer r)
                => group.ContainsRuntimeRenderer(r);
            
            /// <summary>
            /// Adds specified renderer from OcculusionCullingBakeGroup
            /// </summary>
            public static void AddRenderer(OcculusionCullingBakeGroup group, Renderer r)
                => group.PushRuntimeRenderer(r);
            
            /// <summary>
            /// Removes specified renderer from OcculusionCullingBakeGroup
            /// </summary>
            public static bool RemoveRenderer(OcculusionCullingBakeGroup group, Renderer r)
                => group.PopRuntimeRenderer(r);
            
            /// <summary>
            /// Clears all renderers in OcculusionCullingBakeGroup
            /// </summary>
            public static void Clear(OcculusionCullingBakeGroup group) 
                => group.ClearRuntimeRenderers();
        }

        /// <summary>
        /// BakingBehaviour (for instance OcculusionCullingVolume) API
        /// </summary>
        public static class BakingBehaviour
        {
            /// <summary>
            /// Returns List of OcculusionCullingBakeGroup that contain the given renderer
            /// </summary>
            public static void FindBakeGroupWithRenderer(OcculusionCullingBakingBehaviour behaviour, Renderer r, List<OcculusionCullingBakeGroup> result)
            {
                if (result == null)
                {
                    result = new List<OcculusionCullingBakeGroup>();
                }
                else
                {
                    result.Clear();
                }
                
                int length = behaviour.bakeGroups.Length;
                
                for (int i = 0; i < length; ++i)
                {
                    if (behaviour.bakeGroups[i].ContainsRuntimeRenderer(r))
                    {
                        result.Add(behaviour.bakeGroups[i]);
                    }
                }
            }
        }
    }
}