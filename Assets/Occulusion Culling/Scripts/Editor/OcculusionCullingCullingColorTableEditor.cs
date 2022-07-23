//  
//

#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace JustStart.OcculusionCulling
{
    [CustomEditor(typeof(OcculusionCullingColorTable))]
    public class OcculusionCullingColorTableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("This asset stores pre-computed unique color information that is critical for the baking process.\nNothing useful to see here, I'm afraid.", MessageType.Info);
        }
    }
}
#endif