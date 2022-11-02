// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "DO/S_Zombie_b02"
{
	Properties
	{
		_MainTexture("Main Texture", 2D) = "white" {}
		_TintColor("Tint Color", Color) = (1,1,1,1)
		_Ins("Ins", Range( 1 , 50)) = 1
		_DissolveTexture("Dissolve Texture", 2D) = "white" {}
		_Dissolve("Dissolve", Range( -1 , 1)) = 1
		[Toggle(_USEPARTICLE_ON)] _UseParticle("Use Particle", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 2.0
		#pragma shader_feature_local _USEPARTICLE_ON
		#pragma exclude_renderers xboxseries playstation switch nomrt 
		#pragma surface surf Unlit alpha:fade keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 
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
				float staticSwitch18 = i.uv_texcoord.w;
			#else
				float staticSwitch18 = _Ins;
			#endif
			float4 uvs_MainTexture = i.uv_texcoord;
			uvs_MainTexture.xy = i.uv_texcoord.xy * _MainTexture_ST.xy + _MainTexture_ST.zw;
			half4 tex2DNode1 = tex2D( _MainTexture, uvs_MainTexture.xy );
			o.Emission = ( i.vertexColor * ( staticSwitch18 * ( _TintColor * tex2DNode1 ) ) ).rgb;
			float4 uvs_DissolveTexture = i.uv_texcoord;
			uvs_DissolveTexture.xy = i.uv_texcoord.xy * _DissolveTexture_ST.xy + _DissolveTexture_ST.zw;
			#ifdef _USEPARTICLE_ON
				float staticSwitch15 = i.uv_texcoord.z;
			#else
				float staticSwitch15 = _Dissolve;
			#endif
			o.Alpha = ( i.vertexColor.a * saturate( ( tex2DNode1.r * step( tex2D( _DissolveTexture, uvs_DissolveTexture.xy ).r , staticSwitch15 ) ) ) );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18935
1319;358;1137;878;1107.328;610.9724;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;11;-498,231.5;Float;False;Property;_Dissolve;Dissolve;4;0;Create;True;0;0;0;False;0;False;1;1;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;16;-427,340.5;Inherit;False;0;4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;14;-657,36.5;Inherit;False;0;9;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;9;-376,23.5;Inherit;True;Property;_DissolveTexture;Dissolve Texture;3;0;Create;True;0;0;0;False;0;False;-1;ed2cf0efcc6b5224e8fd3ac550dc00a5;ed2cf0efcc6b5224e8fd3ac550dc00a5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-1056,-410.5;Inherit;False;0;1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;15;-186,328.5;Float;False;Property;_UseParticle;Use Particle;5;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;10;44,56.5;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-657,-376.5;Inherit;True;Property;_MainTexture;Main Texture;0;0;Create;True;0;0;0;False;0;False;-1;fa8177d150ef9a44e9c65b1dff219708;f86b85d2a463df248a81cc06bc697ada;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;5;-146,-465.5;Float;False;Property;_TintColor;Tint Color;1;0;Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;6;-162.2,-591.9;Float;False;Property;_Ins;Ins;2;0;Create;True;0;0;0;False;0;False;1;1;1;50;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;18;183.1374,-531.0052;Float;False;Property;_UseParticle;Use Particle;5;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;383,20.5;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;118,-328.5;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;17;593.2498,19.34869;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;7;434,-481.5;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;384,-308.5;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;709,-336.5;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;785,-84.5;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1047,-285;Half;False;True;-1;0;ASEMaterialInspector;0;0;Unlit;DO/S_Zombie_b02;False;False;False;False;True;True;True;True;True;True;True;True;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;d3d9;d3d11_9x;d3d11;glcore;gles;gles3;metal;vulkan;xbox360;xboxone;ps4;psp2;n3ds;wiiu;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;9;1;14;0
WireConnection;15;1;11;0
WireConnection;15;0;16;3
WireConnection;10;0;9;1
WireConnection;10;1;15;0
WireConnection;1;1;2;0
WireConnection;18;1;6;0
WireConnection;18;0;16;4
WireConnection;12;0;1;1
WireConnection;12;1;10;0
WireConnection;3;0;5;0
WireConnection;3;1;1;0
WireConnection;17;0;12;0
WireConnection;4;0;18;0
WireConnection;4;1;3;0
WireConnection;8;0;7;0
WireConnection;8;1;4;0
WireConnection;13;0;7;4
WireConnection;13;1;17;0
WireConnection;0;2;8;0
WireConnection;0;9;13;0
ASEEND*/
//CHKSM=C6132224CE0E7F3887EC20AC9D0644A0A4C7CA04