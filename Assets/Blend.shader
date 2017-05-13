﻿Shader "Unlit/Blend"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}

		[Header(Blend)]
        [Enum(UnityEngine.Rendering.BlendMode)] _BlendSrcFactor ("Blend SrcFactor", Int) = 0
        [Enum(UnityEngine.Rendering.BlendMode)] _BlendDstFactor ("Blend DstFactor", Int) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent"}

		Blend [_BlendSrcFactor] [_BlendDstFactor]

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
	}
}
