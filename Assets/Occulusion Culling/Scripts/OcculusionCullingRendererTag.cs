//  
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace JustStart.OcculusionCulling
{
    [RequireComponent(typeof(Renderer))]
    public class OcculusionCullingRendererTag : MonoBehaviour
    {
        public bool ExcludeRendererFromBake => excludeRendererFromBake;
        public bool RenderDoubleSided => renderDoubleSided;
        public EBakeRenderMode ForcedBakeRenderMode => forcedBakeRenderMode;
        
        [SerializeField] private bool excludeRendererFromBake = false;
        [SerializeField] private bool renderDoubleSided = false;

        [SerializeField] private EBakeRenderMode forcedBakeRenderMode = EBakeRenderMode.None;
    }
}