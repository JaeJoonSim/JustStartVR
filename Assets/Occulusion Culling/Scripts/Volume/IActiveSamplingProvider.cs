//  
//

using UnityEngine;

namespace JustStart.OcculusionCulling
{
    public interface IActiveSamplingProvider
    {
        string Name { get; }
        
        void InitializeSamplingProvider();
        bool IsSamplingPositionActive(OcculusionCullingBakingBehaviour bakingBehaviour, Vector3 pos);
    }
}