// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "DO/S_Zombie_b07"
{
	Properties
	{
		_MainTexture("Main Texture", 2D) = "white" {}
		_TinColor("Tin Color", Color) = (1,1,1,1)
		_Ins("Ins", Range( 0 , 10)) = 1
		_DissolveTexture("Dissolve Texture", 2D) = "white" {}
		_Dissolve("Dissolve", Range( -1 , 1)) = 1
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
		#pragma target 2.0
		#pragma shader_feature_local _USEPARTICLE_ON
		#pragma exclude_renderers xboxseries playstation switch nomrt 
		#pragma surface surf Unlit keepalpha noshadow 
		#undef TRANSFORM_TEX
		#define TRANSFORM_TEX(tex,name) float4(tex.xy * name##_ST.xy + name##_ST.zw, tex.z, tex.w)
		struct Input
		{
			float4 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform float _Ins;
		uniform float4 _TinColor;
		uniform sampler2D _MainTexture;
		uniform float4 _MainTexture_ST;
		uniform sampler2D _DissolveTexture;
		uniform float4 _DissolveTexture_ST;
		uniform float _Dissolve;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			#ifdef _USEPARTICLE_ON
				float staticSwitch16 = i.uv_texcoord.z;
			#else
				float staticSwitch16 = _Ins;
			#endif
			float4 uvs_MainTexture = i.uv_texcoord;
			uvs_MainTexture.xy = i.uv_texcoord.xy * _MainTexture_ST.xy + _MainTexture_ST.zw;
			float4 uvs_DissolveTexture = i.uv_texcoord;
			uvs_DissolveTexture.xy = i.uv_texcoord.xy * _DissolveTexture_ST.xy + _DissolveTexture_ST.zw;
			#ifdef _USEPARTICLE_ON
				float staticSwitch17 = i.uv_texcoord.w;
			#else
				float staticSwitch17 = _Dissolve;
			#endif
			o.Emission = ( ( staticSwitch16 * ( _TinColor * ( tex2D( _MainTexture, uvs_MainTexture.xy ) * saturate( ( tex2D( _DissolveTexture, uvs_DissolveTexture.xy ).r + staticSwitch17 ) ) ) ) ) * i.vertexColor ).rgb;
			o.Alpha = i.vertexColor.a;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18935
1319;358;1137;878;2764.821;1238.595;3.119051;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;15;-1262.897,-213.8718;Inherit;False;0;6;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;9;-1266.807,23.75016;Float;False;Property;_Dissolve;Dissolve;5;0;Create;True;0;0;0;False;0;False;1;1;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;18;-1574.033,-492.1263;Inherit;False;0;4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;17;-916.1509,28.6652;Float;False;Property;_UseParticle;Use Particle;6;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;6;-993.9121,-254.758;Inherit;True;Property;_DissolveTexture;Dissolve Texture;4;0;Create;True;0;0;0;False;0;False;-1;ed2cf0efcc6b5224e8fd3ac550dc00a5;ed2cf0efcc6b5224e8fd3ac550dc00a5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;7;-681.9841,-141.0987;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;14;-997.897,-406.8718;Inherit;False;0;1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;11;-485.7449,-152.7616;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-729.326,-380.174;Inherit;True;Property;_MainTexture;Main Texture;1;0;Create;True;0;0;0;False;0;False;-1;2aa6dfab576b33c4abeb8f8c6e51f740;0cbf980b9452ab04ebce3ac44232ebef;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;5;-492.1428,-676.1052;Float;False;Property;_Ins;Ins;3;0;Create;True;0;0;0;False;0;False;1;2.39;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;3;-363.6733,-503.1778;Float;False;Property;_TinColor;Tin Color;2;0;Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-341.4727,-338.5308;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-129.0831,-413.7927;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;16;-191.797,-626.7717;Float;False;Property;_UseParticle;Use Particle;6;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;86.72071,-493.8483;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;12;100.2689,-289.5258;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;309.2689,-503.5258;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;543.8524,-492.1287;Float;False;True;-1;0;ASEMaterialInspector;0;0;Unlit;DO/S_Zombie_b07;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Custom;;Transparent;All;14;d3d9;d3d11_9x;d3d11;glcore;gles;gles3;metal;vulkan;xbox360;xboxone;ps4;psp2;n3ds;wiiu;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;8;5;False;-1;1;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;17;1;9;0
WireConnection;17;0;18;4
WireConnection;6;1;15;0
WireConnection;7;0;6;1
WireConnection;7;1;17;0
WireConnection;11;0;7;0
WireConnection;1;1;14;0
WireConnection;10;0;1;0
WireConnection;10;1;11;0
WireConnection;2;0;3;0
WireConnection;2;1;10;0
WireConnection;16;1;5;0
WireConnection;16;0;18;3
WireConnection;4;0;16;0
WireConnection;4;1;2;0
WireConnection;13;0;4;0
WireConnection;13;1;12;0
WireConnection;0;2;13;0
WireConnection;0;9;12;4
ASEEND*/
//CHKSM=C4EE70140BFED422C6E9A9785CD59075B4615284