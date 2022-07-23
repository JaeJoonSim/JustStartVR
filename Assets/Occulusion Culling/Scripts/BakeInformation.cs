//  
//

using System.Collections.Generic;
using UnityEngine;

namespace JustStart.OcculusionCulling
{
    public class BakeInformation
    {
        public OcculusionCullingBakingBehaviour BakingBehaviour;
        public HashSet<Renderer> AdditionalOccluders;
    }
}