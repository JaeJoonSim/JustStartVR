//  
//

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace JustStart.OcculusionCulling
{
    /// <summary>
    /// Allows to forcefully clamp the sampling position to the closest portal (instead of the closest cell)
    /// </summary>
    public class OcculusionCullingPortalCell : MonoBehaviour
    {
        [FormerlySerializedAs("CullingVolume")] public OcculusionCullingVolume cullingVolume;
        
        private void OnEnable()
        {
            cullingVolume.AddPortalCell(this);
        }

        private void OnDisable()
        {
            cullingVolume.RemovePortalCell(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, 0.25f * Vector3.one);
        }
    }
}