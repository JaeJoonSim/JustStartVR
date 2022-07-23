//  
//

using System.Collections.Generic;
using UnityEngine;

namespace JustStart.OcculusionCulling
{
    public class OcculusionCullingSceneGroup : OcculusionCullingMonoGroup
    {
        [SerializeField] private Renderer[] renderers;
        [SerializeField] private UnityEngine.Behaviour[] behaviours;
        
        public override List<Renderer> Renderers
        {
            get
            {
                List<Renderer> rs = new List<Renderer>(renderers);

                rs.RemoveAll((r) => r == null);

                return rs;
            }
        }

        public override List<UnityEngine.Behaviour> UnityBehaviours
        {
            get
            {
                List<UnityEngine.Behaviour> rs = new List<UnityEngine.Behaviour>(behaviours);

                rs.RemoveAll((r) => r == null);

                return rs;
            }
        }

        public void SetRenderers(Renderer[] newRenderers)
        {
            renderers = newRenderers;
        }

        public override void PreSceneSave(OcculusionCullingBakingBehaviour bakingBehaviour)
        {
        }

        public override void PreBake(OcculusionCullingBakingBehaviour bakingBehaviour)
        {
        }

        public override void PostBake(OcculusionCullingBakingBehaviour bakingBehaviour)
        {
        }
    }
}