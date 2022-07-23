//  
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JustStart.OcculusionCulling
{
    public class OcculusionCullingBakerFactory
    {
        public static OcculusionCullingBaker CreateBaker(OcculusionCullingBakeSettings bakeSettings)
        {
            bool isNativeRendererAvailable = OcculusionCullingBakerNativeWin64.IsAvailable() || OcculusionCullingBakerNativeVulkanWin64.IsAvailable();

#if !UNITY_EDITOR_WIN
            isNativeRendererAvailable = false;
#endif

            if (OcculusionCullingSettings.Instance.useUnityForRendering || !isNativeRendererAvailable)
            {
                return new OcculusionCullingBakerUnity(bakeSettings);
            }

            if (OcculusionCullingSettings.Instance.useNativeVulkanForRendering)
            {
                return new OcculusionCullingBakerNativeVulkanWin64(bakeSettings);
            }
            
            return new OcculusionCullingBakerNativeWin64(bakeSettings);
        }
    }
}