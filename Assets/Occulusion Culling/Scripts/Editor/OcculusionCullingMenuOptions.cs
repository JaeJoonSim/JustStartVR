//  
//

#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using JustStart.OcculusionCulling;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace JustStart.OcculusionCulling
{
    public class OcculusionCullingMenuOptions
    {
        [UnityEditor.MenuItem("Occulusion Culling/Bake/Bake all", false, 0)]
        private static void BakeAll()
        {
            if (UnityEditor.SceneManagement.EditorSceneManager.sceneCount <= 1)
            {
                BakeSingle();
            }
            else
            {
                BakeMulti();
            }
        }

        private static void BakeSingle()
        {
            Debug.Log("Single scene bake.");
            
            OcculusionCullingBakingBehaviour[] bakingBehaviours = GameObject.FindObjectsOfType<OcculusionCullingBakingBehaviour>();

            OcculusionCullingBakingManager.BakeNow(bakingBehaviours);
        }

        private static void BakeMulti()
        {
            Debug.Log("Multi scene bake.");
            
            List<Scene> scenesToBake = new List<Scene>();
            
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = UnityEditor.SceneManagement.EditorSceneManager.GetSceneAt(i);

                scenesToBake.Add(scene);
            }
            
            OcculusionCullingBakingManager.BakeMultiScene(scenesToBake);
        }

        
        [UnityEditor.MenuItem("Occulusion Culling/Create/Baking volume", false, 0)]
        private static void CreateNewBakingVolume()
        {
            GameObject go = new GameObject("OcculusionCulling Baking Volume");

            OcculusionCullingVolume vol = go.AddComponent<OcculusionCullingVolume>();

            vol.bakeCellSize = new Vector3(10, 5, 10);
            vol.volumeBakeBounds = new Bounds(Vector3.zero, new Vector3(100, 5, 100));

            UnityEditor.Selection.activeObject = go;
            UnityEditor.SceneView.lastActiveSceneView.Frame(vol.volumeBakeBounds, false);
        }
        
        [UnityEditor.MenuItem("Occulusion Culling/Create/Exclude volume", false, 0)]
        private static void CreateNewExcludeVolume()
        {
            GameObject go = new GameObject("OcculusionCulling Exclude Volume");

            OcculusionCullingExcludeVolume vol = go.AddComponent<OcculusionCullingExcludeVolume>();

            vol.volumeExcludeBounds = new Bounds(Vector3.zero, new Vector3(100, 5, 100));

            UnityEditor.Selection.activeObject = go;
            UnityEditor.SceneView.lastActiveSceneView.Frame(vol.volumeExcludeBounds, false);
        }
        
        [UnityEditor.MenuItem("Occulusion Culling/Create/Always Include volume", false, 0)]
        private static void CreateNewAlwaysIncludeVolume()
        {
            GameObject go = new GameObject("OcculusionCulling Always Include Volume");

            OcculusionCullingAlwaysIncludeVolume vol = go.AddComponent<OcculusionCullingAlwaysIncludeVolume>();

            vol.volumeIncludeBounds = new Bounds(Vector3.zero, new Vector3(100, 5, 100));

            UnityEditor.Selection.activeObject = go;
            UnityEditor.SceneView.lastActiveSceneView.Frame(vol.volumeIncludeBounds, false);
        }

        [UnityEditor.MenuItem("Occulusion Culling/Create/Portal cell", false, 0)]
        private static void CreatePortalCell()
        {
            GameObject go = new GameObject("OcculusionCulling Portal Cell");

            go.AddComponent<OcculusionCullingPortalCell>();
            
            UnityEditor.Selection.activeObject = go;
        }
        
        [UnityEditor.MenuItem("Occulusion Culling/Settings")]
        private static void SelectSettings()
        {
            UnityEditor.Selection.activeObject = OcculusionCullingSettings.Instance;
        }
    }
}
#endif