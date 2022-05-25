// The following comment prevents Unity from auto upgrading the shader. Please keep it to keep backward compatibility.
// UNITY_SHADER_NO_UPGRADE

#ifndef _HDRPVLB_SHADER_SPECIFIC_INCLUDED_
#define _HDRPVLB_SHADER_SPECIFIC_INCLUDED_

// POSITION TRANSFORM
#if UNITY_VERSION < 540
    #define __HDRPVLBMatrixWorldToObject    _World2Object
    #define __HDRPVLBMatrixObjectToWorld    _Object2World
    #define __HDRPVLBMatrixV                UNITY_MATRIX_V
    inline float4 HDRPVLBObjectToClipPos(in float3 pos) { return mul(UNITY_MATRIX_MVP, float4(pos, 1.0)); }
#else
    #if HDRPVLB_CUSTOM_INSTANCED_OBJECT_MATRICES
        #define __HDRPVLBMatrixWorldToObject  UNITY_ACCESS_INSTANCED_PROP(Props, _WorldToLocalMatrix)
        #define __HDRPVLBMatrixObjectToWorld  UNITY_ACCESS_INSTANCED_PROP(Props, _LocalToWorldMatrix)
        #define __HDRPVLBMatrixV              unity_MatrixV
        inline float4 HDRPVLBObjectToClipPos(in float3 pos) { return mul(mul(unity_MatrixVP, __HDRPVLBMatrixObjectToWorld), float4(pos, 1.0)); }
    #else
        #define __HDRPVLBMatrixWorldToObject    unity_WorldToObject
        #define __HDRPVLBMatrixObjectToWorld    unity_ObjectToWorld
        #define __HDRPVLBMatrixV                UNITY_MATRIX_V
        #define HDRPVLBObjectToClipPos          UnityObjectToClipPos
    #endif
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
#ifndef UNITY_DECLARE_DEPTH_TEXTURE // handle Unity pre 5.6.0
#define UNITY_DECLARE_DEPTH_TEXTURE(tex) sampler2D_float tex
#endif
UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);

#define HDRPVLBSampleDepthTexture(/*float4*/uv) (SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, (uv)/(uv.w)))
#define HDRPVLBLinearEyeDepth(depth) (LinearEyeDepth(depth))

// FOG
#define HDRPVLB_FOG_UNITY_BUILTIN_COORDS

#if HDRPVLB_ALPHA_AS_BLACK
#define HDRPVLB_FOG_APPLY(color) \
        float4 fogColor = unity_FogColor; \
        fogColor.rgb *= color.a;  \
        UNITY_APPLY_FOG_COLOR(i.fogCoord, color, fogColor);
        // since we use this shader with Additive blending, fog color should be modulated by general alpha
#else
#define HDRPVLB_FOG_APPLY(color) UNITY_APPLY_FOG(i.fogCoord, color);
#endif

#endif // _HDRPVLB_SHADER_SPECIFIC_INCLUDED_
