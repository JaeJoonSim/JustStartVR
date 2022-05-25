// UNITY_SHADER_NO_UPGRADE

#ifndef _HDRPVLB_SHADER_PROPERTY_SYSTEM_INCLUDED_
#define _HDRPVLB_SHADER_PROPERTY_SYSTEM_INCLUDED_

/// ****************************************
/// PROPERTIES MACROS
/// ****************************************
#if HDRPVLB_INSTANCING_API_AVAILABLE && HDRPVLB_GPU_INSTANCING
    #if UNITY_VERSION < 201730 // https://unity3d.com/fr/unity/beta/unity2017.3.0b1
        // PRE UNITY 2017.3
        // for some reason, letting the default UNITY_MAX_INSTANCE_COUNT value generates the following error:
        // "Internal error communicating with the shader compiler process"
        #define UNITY_MAX_INSTANCE_COUNT 150
        #define HDRPVLB_DEFINE_PROP_START UNITY_INSTANCING_CBUFFER_START(Props)
        #define HDRPVLB_DEFINE_PROP_END UNITY_INSTANCING_CBUFFER_END
        #define HDRPVLB_GET_PROP(name) UNITY_ACCESS_INSTANCED_PROP(name)
    #else
        // POST UNITY 2017.3
        #define HDRPVLB_DEFINE_PROP_START UNITY_INSTANCING_BUFFER_START(Props)
        #define HDRPVLB_DEFINE_PROP_END UNITY_INSTANCING_BUFFER_END(Props)
        #define HDRPVLB_GET_PROP(name) UNITY_ACCESS_INSTANCED_PROP(Props, name)
    #endif

    #define HDRPVLB_DEFINE_PROP(type, name) UNITY_DEFINE_INSTANCED_PROP(type, name)
#elif HDRPVLB_SRP_API && HDRPVLB_SRP_BATCHER
    #define HDRPVLB_DEFINE_PROP_START CBUFFER_START(UnityPerMaterial)
    #define HDRPVLB_DEFINE_PROP_END CBUFFER_END
    #define HDRPVLB_DEFINE_PROP(type, name) type name;
    #define HDRPVLB_GET_PROP(name) name
#else
    #define HDRPVLB_DEFINE_PROP_START
    #define HDRPVLB_DEFINE_PROP_END
    #define HDRPVLB_DEFINE_PROP(type, name) uniform type name;
    #define HDRPVLB_GET_PROP(name) name
#endif
/// ****************************************

#endif
