// UNITY_SHADER_NO_UPGRADE

#ifndef _HDRPVLB_SHADER_PROPERTIES_INCLUDED_
#define _HDRPVLB_SHADER_PROPERTIES_INCLUDED_

#include "ShaderPropertySystem.cginc"

/// ****************************************
/// PROPERTIES DECLARATION
/// ****************************************
HDRPVLB_DEFINE_PROP_START

#if HDRPVLB_CUSTOM_INSTANCED_OBJECT_MATRICES
    HDRPVLB_DEFINE_PROP(float4x4, _LocalToWorldMatrix)
    HDRPVLB_DEFINE_PROP(float4x4, _WorldToLocalMatrix)
#endif

    // if HDRPVLB_COLOR_GRADIENT_MATRIX_HIGH || HDRPVLB_COLOR_GRADIENT_MATRIX_LOW
    HDRPVLB_DEFINE_PROP(float4x4, _ColorGradientMatrix)
    // else
    HDRPVLB_DEFINE_PROP(float4, _ColorFlat)
    // endif

    HDRPVLB_DEFINE_PROP(half, _AlphaInside)
    HDRPVLB_DEFINE_PROP(half, _AlphaOutside)
    HDRPVLB_DEFINE_PROP(float2, _ConeSlopeCosSin)   // between -1 and +1
    HDRPVLB_DEFINE_PROP(float2, _ConeRadius)        // x = start radius ; y = end radius
    HDRPVLB_DEFINE_PROP(float, _ConeApexOffsetZ)    // > 0
    HDRPVLB_DEFINE_PROP(float, _AttenuationLerpLinearQuad)
    HDRPVLB_DEFINE_PROP(float3, _DistanceFallOff)   // fallOffStart, fallOffEnd, maxGeometryDistance
    HDRPVLB_DEFINE_PROP(float, _DistanceCamClipping)
    HDRPVLB_DEFINE_PROP(float, _FadeOutFactor)
    HDRPVLB_DEFINE_PROP(float, _FresnelPow)             // must be != 0 to avoid infinite fresnel
    HDRPVLB_DEFINE_PROP(float, _GlareFrontal)
    HDRPVLB_DEFINE_PROP(float, _GlareBehind)
    HDRPVLB_DEFINE_PROP(float, _DrawCap)
    HDRPVLB_DEFINE_PROP(float4, _CameraParams)          // xyz: object space forward vector ; w: cameraIsInsideBeamFactor (-1 : +1)

    // if HDRPVLB_OCCLUSION_CLIPPING_PLANE
    HDRPVLB_DEFINE_PROP(float4, _DynamicOcclusionClippingPlaneWS)
    HDRPVLB_DEFINE_PROP(float,  _DynamicOcclusionClippingPlaneProps)
    // elif HDRPVLB_OCCLUSION_DEPTH_TEXTURE
    HDRPVLB_DEFINE_PROP(float,     _DynamicOcclusionDepthProps)
    // endif

    // if HDRPVLB_DEPTH_BLEND
    HDRPVLB_DEFINE_PROP(float, _DepthBlendDistance)
    // endif

    // if HDRPVLB_NOISE_3D
    HDRPVLB_DEFINE_PROP(float4, _NoiseVelocityAndScale)
    HDRPVLB_DEFINE_PROP(float2, _NoiseParam)
    // endif

    // if HDRPVLB_MESH_SKEWING
    HDRPVLB_DEFINE_PROP(float3, _LocalForwardDirection)
    // endif

    HDRPVLB_DEFINE_PROP(float2, _TiltVector)
    HDRPVLB_DEFINE_PROP(float4, _AdditionalClippingPlaneWS)

HDRPVLB_DEFINE_PROP_END

// UNITY_REVERSED_Z define is broken for WebGL and URP
uniform float _HDRPVLB_UsesReversedZBuffer; // not reversed in OpenGL on WebGL

#if HDRPVLB_OCCLUSION_DEPTH_TEXTURE
// Setting a Texture property to a GPU instanced material is not supported, so keep it as regular property
uniform sampler2D _DynamicOcclusionDepthTexture;
#endif

#if HDRPVLB_DITHERING
uniform float _HDRPVLB_DitheringFactor;
uniform sampler2D _HDRPVLB_DitheringNoiseTex;
uniform float4 _HDRPVLB_DitheringNoiseTex_TexelSize;
#endif

/// ****************************************

#endif
