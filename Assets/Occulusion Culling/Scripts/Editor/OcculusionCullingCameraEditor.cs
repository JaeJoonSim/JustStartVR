//  
//

#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using JustStart.OcculusionCulling;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace JustStart.OcculusionCulling
{
    [CustomEditor(typeof(OcculusionCullingCamera))]
    public class OcculusionCullingCameraEditor : Editor
    {
        private SerializedObject so; // OcculusionCullingCamera SO
        private SerializedProperty includeNeighborCells;
        private SerializedProperty visibilityLayer;

        private void OnEnable()
        {
            OcculusionCullingCamera camera = target as OcculusionCullingCamera;

            so = new SerializedObject(camera);

            includeNeighborCells = so.FindProperty("NeighborCellIncludeRadius");
            visibilityLayer = so.FindProperty("visibilityLayer");
        }

        public override void OnInspectorGUI()
        {
            so.Update();
            {
                OcculusionCullingCamera camera = target as OcculusionCullingCamera;

                DrawUI(camera);
            }
            so.ApplyModifiedProperties();
        }

        void DrawUI(OcculusionCullingCamera camera)
        {
            Setup(camera);

            Stats(camera);

            Utility(camera);
            
            if (camera.GetComponent<Camera>().useOcclusionCulling)
            {
                EditorGUILayout.HelpBox("You are using Umbra (Unity's Occlusion Culling system) and Occulusion Culling simultaneously. Consider picking one.", MessageType.Info);
            }
            
            if (StaticOcclusionCulling.umbraDataSize > 0)
            {
                EditorGUILayout.HelpBox("You baked Occlusion Data for Umbra (Unity's Occlusion Culling system). This might impact the Frustum Culling preview.", MessageType.Info);
            }
        }

        void Setup(OcculusionCullingCamera camera)
        {
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("Setup", EditorStyles.boldLabel);
                
                EditorGUILayout.PropertyField( includeNeighborCells, new GUIContent( "Include Neighbor Cell Radius" ) );

                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.PrefixLabel("Visibility Layer");
                    visibilityLayer.intValue = (int)(OcculusionCullingVisibilityLayer) EditorGUILayout.EnumFlagsField((OcculusionCullingVisibilityLayer)visibilityLayer.intValue);
                }
            }
        }

        private bool m_camFoldout;
        private bool m_volumeFoldout;
        
        void Stats(OcculusionCullingCamera camera)
        {
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("Stats", EditorStyles.boldLabel);
                
                using (new EditorGUILayout.HorizontalScope())
                {
                    // Indent this correctly so it doesn't overlap weirdly.
                    GUILayout.Space(10);

                    m_camFoldout = EditorGUILayout.Foldout(m_camFoldout,
                        $"Active culling cameras ({OcculusionCullingCamera.AllCameras.Count})");

                }
                
                if (m_camFoldout)
                {
                    foreach (var cam in OcculusionCullingCamera.AllCameras)
                    { 
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            GUILayout.Space(10);
                            GUILayout.Label(cam.name);
                            
                            if (GUILayout.Button("Select", GUILayout.Width(150)))
                            {
                                UnityEditor.Selection.activeObject = cam;
                            }
                        }
                    }
                }

                using (new EditorGUILayout.HorizontalScope())
                {
                    // Indent this correctly so it doesn't overlap weirdly.
                    GUILayout.Space(10);

                    m_volumeFoldout = EditorGUILayout.Foldout(m_volumeFoldout,
                        $"Active culling volumes ({OcculusionCullingVolume.AllVolumes.Count})");

                }
                
                if (m_volumeFoldout)
                {
                    foreach (var vol in OcculusionCullingVolume.AllVolumes)
                    { 
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            GUILayout.Space(10);
                            GUILayout.Label(vol.name);

                            if (GUILayout.Button("Select", GUILayout.Width(150)))
                            {
                                UnityEditor.Selection.activeObject = vol;
                            }
                        }
                    }
                }

                GUILayout.Space(15);

                if (camera.LastTotal != 0)
                {
                    GUILayout.Label($"Total renderers: {camera.LastTotal}");
                    GUILayout.Label(
                        $" - Culled: {camera.LastCulled} ({Mathf.Round((camera.LastCulled / (float) camera.LastTotal) * 100f)}%)");

                    GUILayout.Label($" - Visible: {camera.LastVisible}");
                    GUILayout.Label(
                        $" - Culled verts: {OcculusionCullingUtil.FormatNumber(camera.LastCulledVertices)}/{OcculusionCullingUtil.FormatNumber(camera.LastTotalVertices)} ({Mathf.Round((camera.LastCulledVertices / (float) camera.LastTotalVertices) * 100f)}%)\n");
                    GUILayout.Label($"Last Frame Hash (only changes on culling updates): {camera.LastFrameHash}");
                }
                
                EditorGUI.BeginChangeCheck();

                bool showInGameStats = GUILayout.Toggle(camera.ShowInGameStats , " Show in-game stats window");
                    
                if (EditorGUI.EndChangeCheck())
                {
                    camera.ShowInGameStats = showInGameStats;
                    
                    UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
                }
            }
        }
        
        void Utility(OcculusionCullingCamera camera)
        {
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("Utility", EditorStyles.boldLabel);
                GUI.enabled = Application.isPlaying;
                {
                    if (GUILayout.Button("Align with closest grid cell"))
                    {
                        float closestDist = float.MaxValue;

                        // Gonna change during iteration, need to cache it
                        Vector3 cameraPos = camera.transform.position;
                        
                        foreach (OcculusionCullingVolume vol in OcculusionCullingVolume.AllVolumes)
                        {
                            Vector3 newPos = vol.AlignToGrid(cameraPos);

                            float dist = (cameraPos - newPos).sqrMagnitude;

                            if (closestDist > dist)
                            {
                                camera.transform.position = newPos;

                                closestDist = dist;
                            }
                        }
                    }

                    // Invert Culling (renders culled objects)
                    EditorGUI.BeginChangeCheck();

                    bool invertCulling = GUILayout.Toggle(camera.InvertCulling, " Invert Culling (renders culled objects)");
                    
                    if (EditorGUI.EndChangeCheck())
                    {
                        camera.InvertCulling = invertCulling;
                        
                        UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
                    }

                    // Visualize Frustum Culling
                    EditorGUI.BeginChangeCheck();
                    
                    bool visualizeFrustumCulling = GUILayout.Toggle(camera.VisualizeFrustumCulling, " Visualize Frustum Culling");
                    
                    if (EditorGUI.EndChangeCheck())
                    {
                        camera.VisualizeFrustumCulling = visualizeFrustumCulling;
                        
                        UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
                    }
                }
                GUI.enabled = true;
                
                if (!Application.isPlaying)
                {
                    GUILayout.Space(5);
                    
                    EditorGUILayout.HelpBox($"Some functionality is only available in Play Mode!", MessageType.Info);
                }
            }
        }
    }
}

#endif