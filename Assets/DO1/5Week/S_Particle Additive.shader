// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "DO/S_Zombie_b05"
{
	Properties
	{
		_MainTexture("Main Texture", 2D) = "white" {}
		_Ins("Ins", Range( 0 , 10)) = 5.319912
		[Toggle(_USEPARTICLE_ON)] _Useparticle("Use particle", Float) = 0
		_TintColor("Tint Color", Color) = (1,1,1,1)
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
		#pragma target 2.0
		#pragma shader_feature_local _USEPARTICLE_ON
		#pragma exclude_renderers xboxseries playstation switch nomrt 
		#pragma surface surf Unlit keepalpha noshadow 
		#undef TRANSFORM_TEX
		#define TRANSFORM_TEX(tex,name) float4(tex.xy * name##_ST.xy + name##_ST.zw, tex.z, tex.w)
		struct Input
		{
			float4 vertexColor : COLOR;
			float4 uv_texcoord;
		};

		uniform float4 _TintColor;
		uniform sampler2D _MainTexture;
		uniform float4 _MainTexture_ST;
		uniform float _Ins;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 uvs_MainTexture = i.uv_texcoord;
			uvs_MainTexture.xy = i.uv_texcoord.xy * _MainTexture_ST.xy + _MainTexture_ST.zw;
			#ifdef _USEPARTICLE_ON
				float staticSwitch4 = i.uv_texcoord.z;
			#else
				float staticSwitch4 = _Ins;
			#endif
			o.Emission = ( i.vertexColor * ( ( _TintColor * tex2D( _MainTexture, uvs_MainTexture.xy ) ) * staticSwitch4 ) ).rgb;
			o.Alpha = i.vertexColor.a;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18935
1319;358;1137;878;1025.417;803.7378;1;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;10;-885.917,-418.7378;Inherit;False;0;1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;7;-570.7471,-578.8102;Float;False;Property;_TintColor;Tint Color;4;0;Create;True;0;0;0;False;0;False;1,1,1,1;0.9563229,1,0.2877358,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-648.9,-395.4;Inherit;True;Property;_MainTexture;Main Texture;1;0;Create;True;0;0;0;False;0;False;-1;None;63ba024c6d40ca84fa7454a2c9f0d39b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexCoordVertexDataNode;5;-622.4474,-93.3102;Inherit;False;0;4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;3;-708.1342,-177.8382;Float;False;Property;_Ins;Ins;2;0;Create;True;0;0;0;False;0;False;5.319912;10;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-325.147,-481.2103;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;4;-357.4471,-178.0103;Float;False;Property;_Useparticle;Use particle;3;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-82.13429,-312.1381;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;8;-52.73608,-493.4839;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;168.0399,-520.0848;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;398.9001,-395.7;Half;False;True;-1;0;ASEMaterialInspector;0;0;Unlit;DO/S_Zombie_b05;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Custom;;Transparent;All;14;d3d9;d3d11_9x;d3d11;glcore;gles;gles3;metal;vulkan;xbox360;xboxone;ps4;psp2;n3ds;wiiu;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;8;5;False;-1;1;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;1;1;10;0
WireConnection;6;0;7;0
WireConnection;6;1;1;0
WireConnection;4;1;3;0
WireConnection;4;0;5;3
WireConnection;2;0;6;0
WireConnection;2;1;4;0
WireConnection;9;0;8;0
WireConnection;9;1;2;0
WireConnection;0;2;9;0
WireConnection;0;9;8;4
ASEEND*/
//CHKSM=AD0D8AEE410DF13BCEB44648798E724D72AA3361