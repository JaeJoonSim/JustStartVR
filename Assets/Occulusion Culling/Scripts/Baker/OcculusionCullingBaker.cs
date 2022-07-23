//  
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JustStart.OcculusionCulling
{
    public abstract class OcculusionCullingBaker : System.IDisposable
    {
        public virtual int BatchCount => OcculusionCullingConstants.SampleBatchCount;
        
        protected readonly OcculusionCullingBakeSettings m_bakeSettings;
        
        public OcculusionCullingBaker(OcculusionCullingBakeSettings OcculusionCullingBakeSettings)
        {
            m_bakeSettings = OcculusionCullingBakeSettings;
        }

        public abstract OcculusionCullingBakerHandle SamplePosition(Vector3 pos);

        public abstract void Dispose();
    }
}