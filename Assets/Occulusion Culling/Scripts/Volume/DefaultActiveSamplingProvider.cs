//  
//

using System.Collections;
using System.Collections.Generic;
using JustStart.OcculusionCulling;
using UnityEngine;

namespace JustStart.OcculusionCulling
{
    public class DefaultActiveSamplingProvider : IActiveSamplingProvider
    {
        public static string DefaultActiveSamplingProviderName =>  nameof(DefaultActiveSamplingProvider);
        
        public string Name => DefaultActiveSamplingProviderName;

        private OcculusionCullingExcludeVolume[] m_excludeVolumes;
        public OcculusionCullingAlwaysIncludeVolume[] AlwaysIncludeVolumes;

        public void InitializeSamplingProvider()
        {
            m_excludeVolumes = Object.FindObjectsOfType<OcculusionCullingExcludeVolume>();
            AlwaysIncludeVolumes = Object.FindObjectsOfType<OcculusionCullingAlwaysIncludeVolume>();
        }
        
        public bool IsSamplingPositionActive(OcculusionCullingBakingBehaviour bakingBehaviour, Vector3 pos)
        {
            foreach (var bound in m_excludeVolumes)
            {
                if (bound.IsPositionActive(bakingBehaviour, pos))
                {
                    return false;
                }
            }

            return true;
        }
    }
}