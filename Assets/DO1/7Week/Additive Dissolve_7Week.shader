// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "DO/S_Zombie_b08"
{
	Properties
	{
		_MainTexture("Main Texture", 2D) = "white" {}
		_TintColor("Tint Color", Color) = (1,1,1,1)
		_Ins("Ins", Range( 1 , 20)) = 1
		_DissolveTexture("DissolveTexture", 2D) = "white" {}
		_Dissolve("Dissolve", Range( -1 , 1)) = 0.5664522
		_UPanner("UPanner", Float) = 0
		_VPanner("VPanner", Float) = 0
		_NoiseVal("Noise Val", Range( 0 , 5)) = 0
		[Toggle(_USEPARTICLE_ON)] _UseParticle("Use Particle", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Custom"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Off
		ZWrite Off
		Blend SrcAlpha One
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 2.0
		#pragma shader_feature_local _USEPARTICLE_ON
		#pragma exclude_renderers xboxseries playstation switch nomrt 
		#pragma surface surf Unlit keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 
		#undef TRANSFORM_TEX
		#define TRANSFORM_TEX(tex,name) float4(tex.xy * name##_ST.xy + name##_ST.zw, tex.z, tex.w)
		struct Input
		{
			float4 vertexColor : COLOR;
			float4 uv_texcoord;
		};

		uniform float _Ins;
		uniform float4 _TintColor;
		uniform sampler2D _MainTexture;
		uniform float4 _MainTexture_ST;
		uniform float _NoiseVal;
		uniform sampler2D _DissolveTexture;
		uniform float _UPanner;
		uniform float _VPanner;
		uniform float4 _DissolveTexture_ST;
		uniform float _Dissolve;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 uvs_MainTexture = i.uv_texcoord;
			uvs_MainTexture.xy = i.uv_texcoord.xy * _MainTexture_ST.xy + _MainTexture_ST.zw;
			half2 appendResult25 = (half2(_UPanner , _VPanner));
			float4 uvs_DissolveTexture = i.uv_texcoord;
			uvs_DissolveTexture.xy = i.uv_texcoord.xy * _DissolveTexture_ST.xy + _DissolveTexture_ST.zw;
			half2 panner21 = ( 1.0 * _Time.y * appendResult25 + uvs_DissolveTexture.xy);
			half4 tex2DNode15 = tex2D( _DissolveTexture, panner21 );
			#ifdef _USEPARTICLE_ON
				float staticSwitch33 = i.uv_texcoord.z;
			#else
				float staticSwitch33 = _Dissolve;
			#endif
			o.Emission = ( i.vertexColor * ( _Ins * ( _TintColor * ( tex2D( _MainTexture, ( uvs_MainTexture.xy + ( _NoiseVal * tex2DNode15.r ) ) ) * saturate( ( tex2DNode15.r + staticSwitch33 ) ) ) ) ) ).rgb;
			o.Alpha = i.vertexColor.a;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18935
1319;358;1137;878;1703.8;53.4312;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;22;-1987.73,143.3116;Float;False;Property;_UPanner;UPanner;6;0;Create;True;0;0;0;False;0;False;0;-0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-1993.33,230.4115;Float;False;Property;_VPanner;VPanner;7;0;Create;True;0;0;0;False;0;False;0;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;16;-1991.729,-137.5391;Inherit;True;0;15;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;25;-1814.094,145.9836;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;21;-1705.67,-121.9718;Inherit;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-1439.302,-332.0688;Float;False;Property;_NoiseVal;Noise Val;8;0;Create;True;0;0;0;False;0;False;0;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;34;-1476.3,336.0688;Inherit;True;0;4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;15;-1478.019,-133.1568;Inherit;True;Property;_DissolveTexture;DissolveTexture;4;0;Create;True;0;0;0;False;0;False;-1;ed2cf0efcc6b5224e8fd3ac550dc00a5;aedaa6367edc97047a7b8418e6e21d7f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;18;-1503.737,112.637;Float;True;Property;_Dissolve;Dissolve;5;0;Create;True;0;0;0;False;0;False;0.5664522;-0.25;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-1132.653,-325.1145;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;33;-1143.769,108.2458;Float;True;Property;_UseParticle;Use Particle;9;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-1389.715,-691.0305;Inherit;True;0;3;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;32;-976.8079,-613.1756;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;17;-828.5267,-33.65907;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;20;-588.705,-42.97604;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;-731.4423,-549.8473;Inherit;True;Property;_MainTexture;Main Texture;1;0;Create;True;0;0;0;False;0;False;-1;d8d17acdbbca6414f8410208f108735c;568d8d8ba605a8544a15401e9f830a7d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;7;-287.0968,-477.0072;Float;False;Property;_TintColor;Tint Color;2;0;Create;True;0;0;0;False;0;False;1,1,1,1;0.490566,0.3189224,0.2661089,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-368.2341,-260.0603;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-19,-371;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-256.6341,-559.5854;Float;False;Property;_Ins;Ins;3;0;Create;True;0;0;0;False;0;False;1;8;1;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;206.3804,-383.6633;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;13;76.68786,-683.3732;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;299,-619;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;553,-500;Half;False;True;-1;0;ASEMaterialInspector;0;0;Unlit;DO/S_Zombie_b08;False;False;False;False;True;True;True;True;True;True;True;True;False;False;False;False;False;False;False;False;False;Off;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Custom;;Transparent;All;14;d3d9;d3d11_9x;d3d11;glcore;gles;gles3;metal;vulkan;xbox360;xboxone;ps4;psp2;n3ds;wiiu;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;8;5;False;-1;1;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;25;0;22;0
WireConnection;25;1;23;0
WireConnection;21;0;16;0
WireConnection;21;2;25;0
WireConnection;15;1;21;0
WireConnection;29;0;31;0
WireConnection;29;1;15;1
WireConnection;33;1;18;0
WireConnection;33;0;34;3
WireConnection;32;0;4;0
WireConnection;32;1;29;0
WireConnection;17;0;15;1
WireConnection;17;1;33;0
WireConnection;20;0;17;0
WireConnection;3;1;32;0
WireConnection;19;0;3;0
WireConnection;19;1;20;0
WireConnection;6;0;7;0
WireConnection;6;1;19;0
WireConnection;10;0;11;0
WireConnection;10;1;6;0
WireConnection;12;0;13;0
WireConnection;12;1;10;0
WireConnection;0;2;12;0
WireConnection;0;9;13;4
ASEEND*/
//CHKSM=390AD2F655A70D499C1B0B30E229EF5216978859