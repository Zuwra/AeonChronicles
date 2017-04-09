// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/UI Grayscale" {
Properties {
	_MainTex ("Texture", 2D) = "white" { }
	_StencilComp ("Stencil Comparison", Float) = 8
	_Stencil ("Stencil ID", Float) = 0
	_StencilOp ("Stencil Operation", Float) = 0
	_StencilWriteMask ("Stencil Write Mask", Float) = 255
	_StencilReadMask ("Stencil Read Mask", Float) = 255
	_ColorMask ("Color Mask", Float) = 15
	}
SubShader {

Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }
		
		ZTest [unity_GUIZTestMode]
        Cull Off
        Lighting Off
        ZWrite Off
        Fog { Mode Off }
        Blend SrcAlpha OneMinusSrcAlpha


Stencil {
		Ref [_Stencil]
		ReadMask [_StencilReadMask]
		WriteMask [_StencilWriteMask]
		Comp [_StencilComp]
		Pass [_StencilOp]
		}

		ColorMask [_ColorMask]

Pass {
	CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag
	#include "UnityCG.cginc"



	sampler2D _MainTex;
			struct v2f {
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
			};

			float4 _MainTex_ST;
			v2f vert (appdata_base v)
			{
			v2f o;
			o.pos = UnityObjectToClipPos (v.vertex);
			o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
			return o;
			}

			half4 frag (v2f i) : COLOR
			{
			half4 texcol = tex2D (_MainTex, i.uv);
			texcol.rgb = dot(texcol.rgb, float3(0.3, 0.59, 0.11));
			return texcol;
			}
		ENDCG
		}
	}

Fallback "VertexLit"
} 