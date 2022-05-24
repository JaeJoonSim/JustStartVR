// UNITY_SHADER_NO_UPGRADE

#ifndef _HDRPVLB_SHADER_DEFINES_INCLUDED_
#define _HDRPVLB_SHADER_DEFINES_INCLUDED_

/// ****************************************
/// GLOBAL DEFINES
/// ****************************************
#if UNITY_VERSION < 201810 // SRP support introduced in Unity 2018.1.0
#undef HDRPVLB_SRP_API
#endif

#if UNITY_VERSION >= 560 // Instancing API introduced in Unity 5.6
#define HDRPVLB_INSTANCING_API_AVAILABLE 1

#if defined(INSTANCING_ON)
#define HDRPVLB_GPU_INSTANCING 1
#endif

#endif

#if UNITY_VERSION >= 550 // Single Pass Instanced rendering introduced in Unity 5.5
#define HDRPVLB_STEREO_INSTANCING 1
#endif

#if HDRPVLB_SRP_API && HDRPVLB_INSTANCING_API_AVAILABLE && HDRPVLB_GPU_INSTANCING
// When using SRP API and GPU Instancing, the unity_WorldToObject and unity_ObjectToWorld matrices are not sent, so we have to manually send them
#define HDRPVLB_CUSTOM_INSTANCED_OBJECT_MATRICES 1
#endif
/// ****************************************

/// ****************************************
/// DEBUG
/// ****************************************
#define DEBUG_VALUE_DEPTHBUFFER 1
#define DEBUG_VALUE_DEPTHBLEND 2
#define DEBUG_VALUE_DEPTHSTEREOEYE 3
//#define DEBUG_DEPTH_MODE DEBUG_VALUE_DEPTHBUFFER
//#define DEBUG_SHOW_NOISE3D 1
//#define DEBUG_BLEND_INSIDE_OUTSIDE 1

#if DEBUG_DEPTH_MODE && !HDRPVLB_DEPTH_BLEND
#define HDRPVLB_DEPTH_BLEND 1
#endif

#if DEBUG_SHOW_NOISE3D && !HDRPVLB_NOISE_3D
#define HDRPVLB_NOISE_3D 1
#endif
/// ****************************************

/// ****************************************
/// OPTIM
/// ****************************************
/// compute most of the intensity in VS => huge perf improvements
#if !HDRPVLB_SHADER_ACCURACY_HIGH
#define OPTIM_VS 1
#endif

/// when OPTIM_VS is enabled, also compute fresnel in VS => better perf,
/// but require too much tessellation for the same quality
//#define OPTIM_VS_FRESNEL_VS 1
/// ****************************************

/// ****************************************
/// FIXES
/// ****************************************
#define FIX_DISABLE_DEPTH_BLEND_WITH_OBLIQUE_PROJ 1
/// ****************************************


#endif
