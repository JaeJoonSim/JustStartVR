//  
//

#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JustStart.OcculusionCulling
{
    [CustomEditor(typeof(OcculusionCullingSettings))]
    public class OcculusionCullingSettingsEditor : Editor
    {
#pragma warning disable 414
#pragma warning restore 414

        SerializedObject so; // OcculusionCullingSettings SO
        
        SerializedProperty useUnityForRendering;
        SerializedProperty useUnityForRenderingCpuCompute;
        
        SerializedProperty useNativeVulkanForRendering;
        
        SerializedProperty renderTransparency;
        SerializedProperty bakeCameraResolution;
        
        SerializedProperty autoUpdateBakeAverageSamplingSpeedMs;
        SerializedProperty bakeAverageSamplingSpeedMs;
        
        private void OnEnable()
        {
            OcculusionCullingSettings targetSettings = (OcculusionCullingSettings)target;
            
            so = new SerializedObject(targetSettings);

            useUnityForRendering = so.FindProperty("useUnityForRendering");
            useUnityForRenderingCpuCompute = so.FindProperty("useUnityForRenderingCpuCompute");
            
            useNativeVulkanForRendering = so.FindProperty("useNativeVulkanForRendering");
            
            renderTransparency = so.FindProperty("renderTransparency");
            bakeCameraResolution = so.FindProperty("bakeCameraResolution");
            
            autoUpdateBakeAverageSamplingSpeedMs = so.FindProperty("autoUpdateBakeAverageSamplingSpeedMs");
            bakeAverageSamplingSpeedMs = so.FindProperty("bakeAverageSamplingSpeedMs");
        }

        public override void OnInspectorGUI()
        {
                       
            so.Update();
            {
                OcculusionCullingSettings targetSettings = (OcculusionCullingSettings)target;
                
                EditorGUILayout.PropertyField(useUnityForRendering, new GUIContent( "Use Unity For Rendering" ) );
                
                if (useUnityForRendering.boolValue)
                {
                    ++EditorGUI.indentLevel;
                    
                    using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                    {
                        EditorGUILayout.LabelField(new GUIContent("Using Unity"), EditorStyles.boldLabel);
                        EditorGUILayout.PropertyField(useUnityForRenderingCpuCompute,
                            new GUIContent("No Compute Shader support (computes on CPU)"));

                        if (useUnityForRenderingCpuCompute.boolValue)
                        {
                            EditorGUILayout.HelpBox(
                                "\nMake sure to drop your Bake Camera Resolution or your bakes will take very very long.\nGood starting value: 32\nYou might ramp it up further but start small because it is getting slower and slower as you scale this up!\n",
                                MessageType.Warning);
                        }
                    }
                    
                    --EditorGUI.indentLevel;
                }
                else
                {
                    ++EditorGUI.indentLevel;
                    
                    using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                    {
                        EditorGUILayout.LabelField(new GUIContent("Using Native Renderer"), EditorStyles.boldLabel);
                        EditorGUILayout.PropertyField(useNativeVulkanForRendering, new GUIContent( "Native Vulkan Renderer (Experimental)" ) );
                    }
                    
                    --EditorGUI.indentLevel;
                    //EditorGUILayout.HelpBox("Native renderer is being deprecated and will eventually be removed.\n\nReason: Unity Editor performance was and is still seeing major improvements and the need for a faster native renderer fades.", MessageType.Warning);    
                }
                
                GUILayout.Space(10);

                bakeCameraResolution.intValue = Mathf.Clamp(Mathf.ClosestPowerOfTwo(bakeCameraResolution.intValue), 16, 2048);
                
                EditorGUILayout.PropertyField(renderTransparency, new GUIContent( "Render Transparency" ) );

                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.PropertyField(bakeCameraResolution, new GUIContent("Bake Camera Resolution"));

                    if (GUILayout.Button("<", GUILayout.Width(25)))
                    {
                        int prevPowerOfTwo = 16;
                        int currentPowerOfTWo = Mathf.ClosestPowerOfTwo(bakeCameraResolution.intValue);
                        
                        while (true)
                        {
                            int newPowerOfTwo = Mathf.NextPowerOfTwo(prevPowerOfTwo + 1);
                            
                            if (newPowerOfTwo >= currentPowerOfTWo)
                            {
                                break;
                            }

                            prevPowerOfTwo = newPowerOfTwo;
                        }

                        bakeCameraResolution.intValue  = prevPowerOfTwo;
                    }
                    
                    
                    if (GUILayout.Button(">", GUILayout.Width(25)))
                    {
                        bakeCameraResolution.intValue = Mathf.NextPowerOfTwo(bakeCameraResolution.intValue + 1);
                    }
                }
                
                GUILayout.Space(10);
                
                EditorGUILayout.PropertyField(autoUpdateBakeAverageSamplingSpeedMs, new GUIContent( "Auto Update Bake Average Sampling Speed" ) );
                EditorGUILayout.PropertyField(bakeAverageSamplingSpeedMs, new GUIContent( "Bake Average Sampling Speed" ) );
            }
            so.ApplyModifiedProperties();
        }
    }
}
#endif