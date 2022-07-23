//  
//

using System.Collections;
using System.Collections.Generic;
using JustStart.OcculusionCulling;
using UnityEngine;

namespace JustStart.OcculusionCulling
{
    public class OcculusionCullingBakerNativeWin64Handle : OcculusionCullingBakerHandle
    {
        public int[] m_hash;
        public int[] inputHashes;
        
        protected override void DoComplete()
        {
            indices = new ushort[inputHashes.Length];
            
            for (int i = 0; i < inputHashes.Length; ++i)
            {
                int q = inputHashes[i];

                int b = q / (256 * 256);
                q -= (b * 256 * 256);
                int g = q / 256;
                int r = q % 256;

                // The value returned might actually overflow so we cannot use q directly
                int index = (b * 256 * 256) + (g * 256) + r; //r + 256 * (g + 256 * b);

                indices[i] = (ushort) m_hash[index];
            }

            System.Array.Sort(indices);

            // TODO: Sanity test
        }
    }
}