//  
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JustStart.OcculusionCulling
{
    public class OcculusionCullingResourcesLocator : ScriptableObject
    {
        private static OcculusionCullingResourcesLocator m_instance;

        public static OcculusionCullingResourcesLocator Instance
        {
            get
            {
                if (m_instance == null)
                {
                    OcculusionCullingResourcesLocator[] tmp = Resources.LoadAll<OcculusionCullingResourcesLocator>(OcculusionCullingConstants.ResourcesFolder);

                    if (tmp.Length == 0)
                    {
                        return null;
                    }
                    
                    m_instance = tmp[0];
                }

                return m_instance;
            }
        }
        
        [Header("Internally used references. Please do not modify!")]
        public ComputeShader PointExtractorComputeShader;
        public Material UnlitTagMaterial;
        public UnityEngine.Object NativeLib;
        public UnityEngine.Object NativeVulkanLib;

        public OcculusionCullingSettings Settings;
        public OcculusionCullingColorTable ColorTable;
    }
}