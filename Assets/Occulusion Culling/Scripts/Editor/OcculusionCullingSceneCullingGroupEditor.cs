//  
//

#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace JustStart.OcculusionCulling
{
    [CustomEditor((typeof(OcculusionCullingSceneGroup)))]
    public class OcculusionCullingSceneCullingGroupEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            OcculusionCullingSceneGroup monoCullingGroup = target as OcculusionCullingSceneGroup;
            
            if (GUILayout.Button("Collect children"))
            {
                UnityEditor.Undo.RecordObject(monoCullingGroup, "Collected children in SceneCullingGroup");

                monoCullingGroup.SetRenderers(monoCullingGroup.GetComponentsInChildren<Renderer>());
                
                EditorUtility.SetDirty(monoCullingGroup);
            }
            
            if (GUILayout.Button("Clear"))
            {
                if (UnityEditor.EditorUtility.DisplayDialog("Clear all renderers in this group?",
                    "This will clear all renderers in this group.", "OK", "Cancel"))
                {
                    UnityEditor.Undo.RecordObject(monoCullingGroup, "Cleared all renderers in SceneCullingGroup");
                    
                    monoCullingGroup.SetRenderers(System.Array.Empty<Renderer>());

                    EditorUtility.SetDirty(monoCullingGroup);
                }
            }
        }
    }
}
#endif