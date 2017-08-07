Shader "Custom/Vanishing Platform"
{
	Properties
	{
		_Color ("Color", Color) = (0, 0, 0, 0)
		_BackgroundColor ("Background Color", Color) = (0, 0, 0, 0)
		_MainTex ("Texture", 2D) = "white" {}
		_Noise ("Noise", 2D) = "white" {}
		_Iterator("Iterator", Range(0, 1)) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" }
		LOD 100

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
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _Noise;
			float4 _Color;
			float4 _BackgroundColor;
			float _Offset;
			float _Amplitude;
			float _Iterator;
			
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
				col *= _Color;

				float alpha = tex2D(_Noise, i.uv).r;
				if(alpha >= _Iterator) {
					col = _BackgroundColor;
				}

				return col;
			}
			ENDCG
		}
	}
}
