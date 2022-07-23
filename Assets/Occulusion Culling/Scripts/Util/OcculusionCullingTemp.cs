//  
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JustStart.OcculusionCulling
{
    /// <summary>
    /// Collection of objects we only need temporarily and thus can be re-used.
    /// </summary>
    public static class OcculusionCullingTemp
    {
        public static readonly List<ushort> ListUshort = new List<ushort>(OcculusionCullingConstants.MaxRenderers);
        
        public static readonly List<int> ListInt = new List<int>(OcculusionCullingConstants.MaxRenderers);
    }
}