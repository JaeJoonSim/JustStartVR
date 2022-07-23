//  
//

using System.Collections;
using System.Collections.Generic;
using JustStart.OcculusionCulling;
using UnityEngine;

namespace JustStart.OcculusionCulling.SamplingProviders
{
    [RequireComponent(typeof(OcculusionCullingBakingBehaviour))]
    [ExecuteAlways]
    public class ExcludeInsideCollidersSamplingProvider : SamplingProviderBase
    {
        private static readonly Collider[] OverlapCollidersNonAllocBuffer = new Collider[128];

        public override string Name => nameof(ExcludeInsideCollidersSamplingProvider);

        [Header("Notice: Concave MeshColliders are unsupported by PhysX and thus ignored")]
        public LayerMask layerMask = ~0;
        
        public override void InitializeSamplingProvider()
        {
        }
        
        public override bool IsSamplingPositionActive(OcculusionCullingBakingBehaviour bakingBehaviour, Vector3 pos)
        {
            int overlapCount = Physics.OverlapSphereNonAlloc(pos, 0.01f, OverlapCollidersNonAllocBuffer, layerMask.value);

            return overlapCount <= 0;
        }
    }
}