// The following comment prevents Unity from auto upgrading the shader. Please keep it to keep backward compatibility.
// UNITY_SHADER_NO_UPGRADE

#ifndef _HDRPVLB_SHADER_SPECIFIC_INCLUDED_
#define _HDRPVLB_SHADER_SPECIFIC_INCLUDED_

// POSITION TRANSFORM
#if HDRPVLB_CUSTOM_INSTANCED_OBJECT_MATRICES
    #define __HDRPVLBMatrixWorldToObject  UNITY_ACCESS_INSTANCED_PROP(Props, _WorldToLocalMatrix)
    #define __HDRPVLBMatrixObjectToWorld  UNITY_ACCESS_INSTANCED_PROP(Props, _LocalToWorldMatrix)
    #define __HDRPVLBMatrixV              unity_MatrixV
    inline float4 HDRPVLBObjectToClipPos(in float3 pos) { return mul(mul(unity_MatrixVP, __HDRPVLBMatrixObjectToWorld), float4(pos, 1.0)); }
#else
    #define __HDRPVLBMatrixWorldToObject    unity_WorldToObject
    #define __HDRPVLBMatrixObjectToWorld    unity_ObjectToWorld
    #define __HDRPVLBMatrixV                UNITY_MATRIX_V
    inline float4 HDRPVLBObjectToClipPos(in float3 pos) { return mul(UNITY_MATRIX_VP, mul(UNITY_MATRIX_M, float4(pos.xyz, 1.0))); }
#endif

inline float4 HDRPVLBObjectToWorldPos(in float4 pos)    { return mul(__HDRPVLBMatrixObjectToWorld, pos); }
#define HDRPVLBWorldToViewPos(pos)                      (mul(__HDRPVLBMatrixV, float4(pos.xyz, 1.0)).xyz)

// FRUSTUM PLANES
#define HDRPVLBFrustumPlanes unity_CameraWorldClipPlanes

// CAMERA
inline float3 __HDRPVLBWorldToObjectPos(in float3 pos) { return mul(__HDRPVLBMatrixWorldToObject, float4(pos, 1.0)).xyz; }
inline float3 HDRPVLBGetCameraPositionObjectSpace(float3 scaleObjectSpace)
{
    return __HDRPVLBWorldToObjectPos(_WorldSpaceCameraPos).xyz * scaleObjectSpace;
}

// DEPTH
#define HDRPVLBSampleDepthTexture(/*float4*/uv) (SampleSceneDepth((uv.xy) / (uv.w)))
#define HDRPVLBLinearEyeDepth(depth) LinearEyeDepth((depth), _ZBufferParams)

// FOG
#if defined(FOG_LINEAR) || defined(FOG_EXP) || defined(FOG_EXP2)
    #define HDRPVLB_FOG_MIX(color, fogColor, posClipSpace)  color.rgb = MixFogColor(color.rgb, fogColor.rgb, ComputeFogFactor(posClipSpace.z * posClipSpace.w))

    #if HDRPVLB_ALPHA_AS_BLACK
        #define HDRPVLB_FOG_APPLY(color) \
                float4 fogColor = unity_FogColor; \
                fogColor.rgb *= color.a;  \
                HDRPVLB_FOG_MIX(color, fogColor, i.posClipSpace);
    #else
        #define HDRPVLB_FOG_APPLY(color) HDRPVLB_FOG_MIX(color, unity_FogColor, i.posClipSpace);
    #endif
#endif

#endif // _HDRPVLB_SHADER_SPECIFIC_INCLUDED_
