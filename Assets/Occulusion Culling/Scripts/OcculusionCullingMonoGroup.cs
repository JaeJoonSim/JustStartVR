//  
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JustStart.OcculusionCulling
{
    public abstract class OcculusionCullingMonoGroup : MonoBehaviour
    {
        [Header("Allows to exclude this from other OcculusionCullingBakingBehaviours")]
        [SerializeField] public OcculusionCullingBakingBehaviour[] restrictToBehaviours = System.Array.Empty<OcculusionCullingBakingBehaviour>();
        
        public virtual List<Renderer> Renderers => throw new System.NotImplementedException();
        public virtual List<UnityEngine.Behaviour> UnityBehaviours => throw new System.NotImplementedException();

        public abstract void PreSceneSave(OcculusionCullingBakingBehaviour bakingBehaviour);
        public abstract void PreBake(OcculusionCullingBakingBehaviour bakingBehaviour);
        public abstract void PostBake(OcculusionCullingBakingBehaviour bakingBehaviour);
    }
}