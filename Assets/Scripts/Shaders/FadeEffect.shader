Shader "Custom/Image Effect/Fade"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_FadeTex ("Fade Texture", 2D) = "white" {}
		_FadeColor ("Fade Color", Color) = (0, 0, 0, 0)
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _FadeTex;
			fixed4 _FadeColor;
			float _globalImgEffectIterator;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 fade = tex2D(_FadeTex, i.uv);

				if(fade.x <= _globalImgEffectIterator)
					col = _FadeColor;

				return col;
			}
			ENDCG
		}
	}
}
