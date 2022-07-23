﻿//  
//

using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.Rendering;

namespace JustStart.OcculusionCulling
{
    public class OcculusionCullingExcludeVolume : MonoBehaviour, CustomHandle.IResizableByHandle
    {
        static readonly Bounds UniformBounds = new Bounds(Vector3.zero, Vector3.one);

        [SerializeField] public Vector3 volumeSize = Vector3.one;
        [SerializeField] public OcculusionCullingBakingBehaviour[] restrictToBehaviours = System.Array.Empty<OcculusionCullingBakingBehaviour>();

        public Bounds volumeExcludeBounds
        {
            get => new Bounds(transform.position, volumeSize);

            set
            {
                // TODO: Causes annoying offset, gonna need to solve this in a different way.
                transform.position = value.center;
                
                volumeSize = new Vector3(
                    Mathf.Max(1, (value.size.x)), 
                    Mathf.Max(1, (value.size.y)), 
                    Mathf.Max(1, (value.size.z)));
            }
        }

        public bool IsPositionActive(OcculusionCullingBakingBehaviour bakingBehaviour, Vector3 pos)
        {
            if (restrictToBehaviours.Length > 0)
            {
                if (System.Array.IndexOf(restrictToBehaviours, bakingBehaviour) < 0)
                {
                    return false;
                }
            }
            
#if true
            // This should be much faster
            Transform tf = transform;

            Vector3 localPos = Quaternion.Inverse(tf.rotation) * (tf.position - pos);

            Vector3 scale = volumeExcludeBounds.extents;
            
            localPos.x *= 1F / scale.x;
            localPos.y *= 1F / scale.y;
            localPos.z *= 1F / scale.z;

            return localPos.x <= 1.0f && localPos.x >= -1.0f
                && localPos.y <= 1.0f && localPos.y >= -1.0f
                && localPos.z <= 1.0f && localPos.z >= -1.0f;
#else
            Matrix4x4 matrix4X4 = Matrix4x4.TRS(transform.position, transform.rotation, volumeExcludeBounds.size).inverse;
            
            return UniformBounds.Contains(matrix4X4.MultiplyPoint3x4(pos));
#endif
        }

        public Vector3 HandleSized
        {
            get => volumeExcludeBounds.size;
            set => volumeExcludeBounds = new Bounds(transform.position, value);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Matrix4x4 trs = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
            
            Handles.color = new Color(1, 0, 0, 1.0f);
            Handles.zTest = CompareFunction.LessEqual;
            Handles.lighting = false;
            
            Handles.matrix = trs;
            Handles.DrawWireCube(Vector3.zero, volumeSize);
            
            // CubeHandleCap doesn't allow to specify non-uniform scale so we need to mess with the matrix
            Handles.matrix = trs * Matrix4x4.Scale(volumeSize);

            Handles.color = new Color(1, 0, 0, OcculusionCullingConstants.VolumeInsideAlpha);
            Handles.CubeHandleCap(-1, Vector3.zero, Quaternion.identity, 1.0f, EventType.Repaint);
        }
#endif
    }
}