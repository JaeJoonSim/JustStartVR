//  
//

using UnityEngine;

namespace JustStart.OcculusionCulling
{
    public abstract class SamplingProviderBase : MonoBehaviour, IActiveSamplingProvider
    { 
        private OcculusionCullingBakingBehaviour m_behaviour;

        protected virtual void OnEnable()
        {
            if (m_behaviour == null)
            {
                m_behaviour = GetComponent<OcculusionCullingBakingBehaviour>();
            }

            // Add the sampling provider to the BakingBehaviour
            m_behaviour.AddSamplingProvider(this);
        }

        protected virtual void OnDisable()
        { 
            if (m_behaviour == null)
            {
                m_behaviour = GetComponent<OcculusionCullingBakingBehaviour>();
            }
            
            // Remove the sampling provider to the BakingBehaviour
            m_behaviour.RemoveSamplingProvider(this);
        }

        public abstract string Name { get; }

        public abstract void InitializeSamplingProvider();

        public abstract bool IsSamplingPositionActive(OcculusionCullingBakingBehaviour bakingBehaviour, Vector3 pos);
    }
}