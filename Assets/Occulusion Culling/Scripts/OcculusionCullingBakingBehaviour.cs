//  
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace JustStart.OcculusionCulling
{
    /// <summary>
    /// This is the base class that provides an easy interface for baking.
    /// Hopefully the built-in baking scripts are sufficient but if you need something else you can inherit it and roll your own.
    /// </summary>
    public abstract class OcculusionCullingBakingBehaviour : MonoBehaviour
    {
        public enum EOutOfBoundsBehaviour
        {
            ClampToNearestCell,
            Cull,
            IgnoreDoNothing,
        }
        
        [Tooltip("Try to find a non-empty cell if we hit an empty one?")]
        public bool searchForNonEmptyCells = false;
        
        [Tooltip("What should happen if we encounter an empty cell? Cull everything or make everything visible?")]
        public EEmptyCellCullBehaviour emptyCellCullBehaviour = EEmptyCellCullBehaviour.CullEverything;
        
        [Tooltip("Should this volume be culled if the camera is not inside it or should the camera position be clamped to the nearest cell?")]
        public EOutOfBoundsBehaviour outOfBoundsBehaviour = EOutOfBoundsBehaviour.ClampToNearestCell;
        
        private static DefaultActiveSamplingProvider DefaultSamplingProvider = new DefaultActiveSamplingProvider();

        public HashSet<IActiveSamplingProvider> SamplingProviders = new HashSet<IActiveSamplingProvider>()
        {
            DefaultSamplingProvider
        };

        public void AddSamplingProvider(IActiveSamplingProvider samplingProvider) =>
            SamplingProviders.Add(samplingProvider);
        
        public void RemoveSamplingProvider(IActiveSamplingProvider samplingProvider) =>
            SamplingProviders.Remove(samplingProvider);

        
        public void InitializeAllSamplingProviders()
        {
            foreach (var provider in SamplingProviders)
            {
                provider.InitializeSamplingProvider();
            }
        }

        public bool SamplingProvidersIsPositionActive(Vector3 pos)
        {
            // OcculusionCullingAlwaysIncludeVolume takes over priority as it allows to pull cells back in.
            foreach (OcculusionCullingAlwaysIncludeVolume alwaysIncludeVolume in DefaultSamplingProvider.AlwaysIncludeVolumes)
            {
                if (alwaysIncludeVolume.IsPositionActive(this, pos))
                {
                    return true;
                }
            }
            
            foreach (IActiveSamplingProvider provider in SamplingProviders)
            {
                if (!provider.IsSamplingPositionActive(this, pos))
                {
                    return false;
                }
            }

            return true;
        }
        
        [SerializeField] public OcculusionCullingBakeGroup[] bakeGroups = System.Array.Empty<OcculusionCullingBakeGroup>(); // Important to initialize or AddRange will fail

        [SerializeField] public List<Renderer> additionalOccluders = new List<Renderer>();
        
        public virtual OcculusionCullingBakeData BakeData { get; } = null;

        [System.NonSerialized] public int TotalVertexCount = 0;

        private bool[] m_renderersState;
        
        public virtual void Start()
        {
            TotalVertexCount = 0;
            m_renderersState = new bool[bakeGroups.Length];
            
            foreach (OcculusionCullingBakeGroup group in bakeGroups)
            {
                group.Init();
                
#if UNITY_EDITOR
                TotalVertexCount += group.vertexCount;
#endif
            }
        }
        
        public IEnumerator PerformBakeAsync(bool sceneReload, bool saveScene, HashSet<Renderer> additionalOccludersHashset)
        {
#if !UNITY_EDITOR
                yield break;
#else
            bool needsSceneReload = false;
            
            try
            {
                if (bakeGroups.Length <= 0)
                {
                    UnityEditor.EditorUtility.DisplayDialog("No renderers", "No renderers. Nothing to bake", "OK");
                    
                    yield return new OcculusionCullingBakeNotStartedYieldInstruction();
                }

                if (bakeGroups.Length == 1)
                {
                    Debug.LogError($"{nameof(bakeGroups)} contains only one element thus no occlusion is possible. Each Renderer should go into it's own {nameof(OcculusionCullingBakeGroup)}. Consider using {nameof(OcculusionCullingUtil.CreateBakeGroupsForRenderers)}!");
                    
                    yield return new OcculusionCullingBakeNotStartedYieldInstruction();
                }

                /*
                // Order renderers for improved local coherence
                Renderers = Renderers
                    .OrderBy(m => m.transform.position.x)
                    .OrderBy(m => m.transform.position.z)
                    .OrderBy(m => m.transform.position.y)
                    .ToArray();*/

                UnityEditor.EditorUtility.DisplayProgressBar($"Initializing", "Initializing", 0);

                if (!PreBake())
                {
                    yield return new OcculusionCullingBakeNotStartedYieldInstruction();
                }

                OcculusionCullingMonoGroup[] allMonoGroups = OcculusionCullingUtil.FindMonoGroupsForBakingBehaviour(this);

                HashSet<Renderer> copyAdditionalOccluders = new HashSet<Renderer>();

                if (additionalOccludersHashset != null)
                {
                    foreach (Renderer r in additionalOccludersHashset)
                    {
                        copyAdditionalOccluders.Add(r);
                    }
                }

                // Strip null references in additional occluders
                if (additionalOccluders.RemoveAll((x) => x == null) > 0)
                {
                    Debug.LogWarning($"Stripped some null references in {nameof(additionalOccluders)}");
                    
#if UNITY_EDITOR
                    UnityEditor.EditorUtility.SetDirty(this);
#endif
                }

                foreach (Renderer r in additionalOccluders)
                {
                    copyAdditionalOccluders.Add(r);
                }
                
                // Strip additional occluders that are already referenced by this behaviour
                HashSet<Renderer> allReferencedRenderers = new HashSet<Renderer>();

                foreach (OcculusionCullingBakeGroup group in bakeGroups)
                {
                    foreach (var r in group.renderers)
                    {
                        allReferencedRenderers.Add(r);
                    }
                }

                // Only keep occluders unreferenced
                copyAdditionalOccluders = new HashSet<Renderer>(copyAdditionalOccluders.Where((x) => !allReferencedRenderers.Contains(x)));
                
                CullAdditionalOccluders(ref copyAdditionalOccluders);
                
                // We cannot perform this in play mode due to static batching.
                // So lets do it here.
                foreach (OcculusionCullingBakeGroup group in bakeGroups)
                {
                    if (!group.CollectMeshStats())
                    {
                        UnityEditor.EditorUtility.DisplayDialog("Error: Invalid renderers detected!", "Error: Bake groups contain references to invalid renderers.\n\nExamples:\n- Renderer is null\n- MeshFilter is null\n- Mesh is null","OK");
                        
                        yield return new OcculusionCullingBakeNotStartedYieldInstruction();
                    }
                }
                
                foreach (var monoGroup in allMonoGroups)
                {
                    monoGroup.PreSceneSave(this);
                }

                UnityEditor.EditorUtility.SetDirty(this);
                UnityEditor.EditorUtility.SetDirty(BakeData);

                if (saveScene && UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().path != OcculusionCullingConstants.MultiSceneTempPath)
                {
                    if (!UnityEditor.SceneManagement.EditorSceneManager.SaveModifiedScenesIfUserWantsTo(new Scene[] { UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene() }))
                    {
                        yield return new OcculusionCullingBakeNotStartedYieldInstruction();
                    }
                }

                foreach (var monoGroup in allMonoGroups)
                {
                    monoGroup.PreBake(this);
                }
                
                BakeData.bakeCompleted = false;
                
                BakeData.PrepareForBake(this);
                InitializeAllSamplingProviders();

                needsSceneReload = true;

                List<Vector3> worldPositions = GetSamplingPositions(Space.World);

                List<OcculusionCullingBakeSettings.SamplingLocation> samplingLocations =
                    new List<OcculusionCullingBakeSettings.SamplingLocation>(worldPositions.Count);

                int activeSamplingPositionsCount = 0;

                for (int i = 0; i < worldPositions.Count; ++i)
                {
                    Vector3 pos = worldPositions[i];
                    bool active = SamplingProvidersIsPositionActive(worldPositions[i]);
                    
                    samplingLocations.Add(new OcculusionCullingBakeSettings.SamplingLocation(pos, active));

                    activeSamplingPositionsCount += active ? 1 : 0;
                }
                
                OcculusionCullingBakeSettings bakeSettings = new OcculusionCullingBakeSettings()
                {
                    Groups = bakeGroups,
                    
                    AdditionalOccluders = copyAdditionalOccluders,
                    
                    ActiveSamplingPositionCount = activeSamplingPositionsCount,
                    SamplingLocations = samplingLocations,

                    Width = OcculusionCullingSettings.Instance.bakeCameraResolutionWidth,
                    Height = OcculusionCullingSettings.Instance.bakeCameraResolutionHeight
                };
                
                using (OcculusionCullingBaker baker = OcculusionCullingBakerFactory.CreateBaker(bakeSettings))
                {
                    System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

                    List<Vector3> localPositions = GetSamplingPositions();

                    int totalBatchCounts = activeSamplingPositionsCount / baker.BatchCount;
                    int currentBatchCount = 0;

                    List<OcculusionCullingBakeHandle> pending = new List<OcculusionCullingBakeHandle>(localPositions.Count);

                    const float SMOOTHING_FACTOR = 0.005f;

                    float lastTime = Time.realtimeSinceStartup;
                    int lastElement = 0;

                    float lastSpeed = -1f;
                    float averageSpeed = OcculusionCullingSettings.Instance.bakeAverageSamplingSpeedMs / 1000f;

                    int bakedCellCount = 0;
                    
                    for (int i = 0; i < localPositions.Count; ++i)
                    { 
                        // We use bakedCellCount instead of i because we might not bake all cells
                        string strBakingTitle = $"[ETA {OcculusionCullingUtil.FormatSeconds((activeSamplingPositionsCount - bakedCellCount) * averageSpeed)}], Avg. speed: {System.Math.Round(averageSpeed * 1000f, 2)} ms | ";

                        if (!samplingLocations[i].Active)
                        {
                            // Don't validate. We don't want warnings for cells that are empty on purpose.
                            BakeData.SetRawData(i, System.Array.Empty<ushort>(), false);
                            
                            continue;
                        }

                        ++bakedCellCount;

                        OcculusionCullingBakerHandle handle = baker.SamplePosition(transform.rotation * localPositions[i] + transform.position);
                        
                        pending.Add(new OcculusionCullingBakeHandle()
                        {
                            Index = i,
                            Handle = handle
                        });

                        if (pending.Count >= baker.BatchCount)
                        {
                            // We call this here to grant some additional time to the GPU while we clean-up
                            System.GC.Collect();

                            if (UnityEditor.EditorUtility.DisplayCancelableProgressBar(strBakingTitle + "Performing readback ",
                                strBakingTitle + "Performing readback",
                                (currentBatchCount / (float) totalBatchCounts)))
                            {
                                yield return new OcculusionCullingBakeAbortedYieldInstruction();
                            }

                            CompletePending(pending);

                            // Give Unity some breathing room.
                            // This seems important because Unity internally might not de-allocate some resources otherwise.
                            yield return null;

                            ++currentBatchCount;

                            if (UnityEditor.EditorUtility.DisplayCancelableProgressBar(
                                strBakingTitle + $"Batch: {currentBatchCount}/{totalBatchCounts} ",
                                "Performing sampling batches...", (currentBatchCount / (float) totalBatchCounts)))
                            {
                                yield return new OcculusionCullingBakeAbortedYieldInstruction();
                            }

                            lastSpeed = (Time.realtimeSinceStartup - lastTime) / (currentBatchCount - lastElement) / (float) baker.BatchCount;

                            averageSpeed = SMOOTHING_FACTOR * lastSpeed + (1 - SMOOTHING_FACTOR) * averageSpeed;

                            lastTime = Time.realtimeSinceStartup;
                            lastElement = currentBatchCount;
                        }
                    }

                    if (UnityEditor.EditorUtility.DisplayCancelableProgressBar($"Finishing pending batches",
                        "Finishing pending batches",
                        (currentBatchCount / (float) totalBatchCounts)))
                    {
                        yield return new OcculusionCullingBakeAbortedYieldInstruction();
                    }

                    CompletePending(pending);

                    sw.Stop();
                    
                    Debug.Log($"Bake time: {OcculusionCullingUtil.FormatSeconds(sw.ElapsedMilliseconds * 0.001f)} | {(sw.ElapsedMilliseconds / (float)localPositions.Count)} ms per sample");

                    if (OcculusionCullingSettings.Instance.autoUpdateBakeAverageSamplingSpeedMs)
                    {
                        OcculusionCullingSettings.Instance.bakeAverageSamplingSpeedMs = (sw.ElapsedMilliseconds / (float)localPositions.Count);
                        
                        UnityEditor.EditorUtility.SetDirty(OcculusionCullingSettings.Instance);
                    }

                    if (UnityEditor.EditorUtility.DisplayCancelableProgressBar($"Performing post bake steps",
                            "Performing post bake steps",
                            (currentBatchCount / (float) totalBatchCounts)))
                    {
                        yield return new OcculusionCullingBakeAbortedYieldInstruction();
                    }
                    
                    PostBake();
                    
                    foreach (OcculusionCullingMonoGroup monoGroup in allMonoGroups)
                    {
                        monoGroup.PostBake(this);
                    }
                    
                    if (UnityEditor.EditorUtility.DisplayCancelableProgressBar($"Compressing data and finishing bake",
                        "Compressing data and finishing bake",
                        (currentBatchCount / (float) totalBatchCounts)))
                    {
                        yield return new OcculusionCullingBakeAbortedYieldInstruction();
                    }
                    
                    BakeData.CompleteBake();
                    
                    BakeData.strBakeDate = System.DateTime.UtcNow.ToString("o");
                    BakeData.bakeDurationMilliseconds = sw.ElapsedMilliseconds;
                }
                
                // Hash calculation needs to happen here to make sure we Disposed OcculusionCullingSceneColor because it might have temporarily modified the BakeGroup
                BakeData.bakeHash = GetBakeHash();
                BakeData.bakeCompleted = true;
                    
                UnityEditor.EditorUtility.SetDirty(BakeData);
                UnityEditor.AssetDatabase.SaveAssets();
            }
            finally
            {
                UnityEditor.EditorUtility.ClearProgressBar();

                // Reload scene
                if (needsSceneReload && sceneReload && OcculusionCullingConstants.AllowSceneReload)
                {
                    string scenePath = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().path;

                    if (!string.IsNullOrEmpty(scenePath))
                    {
                        UnityEditor.SceneManagement.EditorSceneManager.OpenScene(scenePath);
                    }
                    else
                    {
                        UnityEditor.SceneManagement.EditorSceneManager.NewScene(UnityEditor.SceneManagement.NewSceneSetup.DefaultGameObjects);
                    }
                }
            }
#endif
        }

        void CompletePending(List<OcculusionCullingBakeHandle> pending)
        {
            for (int k = 0; k < pending.Count; ++k)
            {
                pending[k].Handle.Complete();

                BakeData.SetRawData(pending[k].Index, pending[k].Handle.indices);
            }

            pending.Clear();
        }

        public void QueueToggleAllRenderers(bool state)
        {
            for (var index = 0; index < bakeGroups.Length; index++)
            {
                m_renderersState[index] = state;
            }
        }

        /// <summary>
        /// Queue up renderer state change. This will not take effect until ExecuteQueue was called.
        /// </summary>
        public void QueueToggleRenderer(int index, bool state, out OcculusionCullingBakeGroup modifiedBakeGroup)
        {
            m_renderersState[index] = state;
            modifiedBakeGroup = bakeGroups[index];
        }

        /// <summary>
        /// Applies renderers state changes.
        /// </summary>
        public void ExecuteQueue(bool forceNullCheck = false)
        {
            for (var index = 0; index < bakeGroups.Length; index++)
            {
                OcculusionCullingBakeGroup r = bakeGroups[index];
                r.Toggle(m_renderersState[index], forceNullCheck);
            }
        }

        public virtual void SetBakeData(OcculusionCullingBakeData bakeData) => throw new System.NotImplementedException();
        
        public virtual List<Vector3> GetSamplingPositions(Space space = Space.Self) => throw new System.NotImplementedException();

        public virtual void GetIndicesForWorldPos(Vector3 worldPos, List<ushort> indices) => throw new System.NotImplementedException();
        public virtual int GetIndexForWorldPos(Vector3 worldPos, out bool isOutOfBounds) => throw new System.NotImplementedException();

        public virtual void GetIndicesForIndex(int index, List<ushort> indices) =>
            BakeData.SampleAtIndex(index, indices);

        public virtual bool PreBake() => throw new System.NotImplementedException();
        public virtual void PostBake() => throw new System.NotImplementedException();
        
        public virtual int GetBakeHash() => throw new System.NotImplementedException();

        protected virtual void CullAdditionalOccluders(ref HashSet<Renderer> additionalOccluders) =>
            throw new System.NotImplementedException();
    }
}