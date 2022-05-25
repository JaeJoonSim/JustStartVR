#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace HDRPVLB
{
    [CustomEditor(typeof(ConfigOverride))]
    public class ConfigOverrideEditor : ConfigEditor // useless override, only useful for backward compatibility
    {
    }
}
#endif
