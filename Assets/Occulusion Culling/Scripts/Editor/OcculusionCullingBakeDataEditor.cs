//  
//

#if UNITY_EDITOR
using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace JustStart.OcculusionCulling
{
    [CustomEditor(typeof(OcculusionCullingBakeData), true)]
    public class OcculusionCullingBakeDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            OcculusionCullingBakeData data = target as OcculusionCullingBakeData;
            
            if (!data.bakeCompleted)
            {
                EditorGUILayout.HelpBox("This bake was not completed and might not function correctly.", MessageType.Error);
            }
            
            GUILayout.Label("Bake information", EditorStyles.boldLabel);
            
            data.DrawInspectorGUI();

            if (!string.IsNullOrEmpty(data.strBakeDate))
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.PrefixLabel("Bake date");
                    EditorGUILayout.LabelField(System.DateTime.ParseExact(data.strBakeDate, "o", CultureInfo.InvariantCulture).ToLocalTime().ToString());
                }
            }

            if (data.bakeDurationMilliseconds > 0)
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.PrefixLabel("Bake duration");
                    EditorGUILayout.LabelField(OcculusionCullingUtil.FormatSeconds(data.bakeDurationMilliseconds * 0.001f));
                }
            }

            GUILayout.Space(10);
            
            if (OcculusionCullingEditorUtil.TryGetAssetBakeSize(target as OcculusionCullingBakeData, out float bakeSize))
            {
                GUILayout.Label($"Bake size: {bakeSize} mb(s)");
            }
        }
    }
}
#endif