Shader "Custom/MirrorShader"
{
	Properties
	{
		_SmudgeTex ("Smudge", 2D) = "white" {}
		_OffsetTex ("Refraction Map", 2D) = "white" {}
		_RefractionMag ("Refraction Magnitude", Range(0, 0.1)) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }

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
				half4 color : COLOR;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float2 screenuv : TEXCOORD1;
				half4 color : COLOR;
			};

			sampler2D _SmudgeTex;
			uniform sampler2D _GlobalMirrorTex;
			sampler2D _OffsetTex;
			float _RefractionMag;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.screenuv = ((o.vertex.xy/o.vertex.w) + 1) * 0.5;
				o.color = v.color;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float4 offset = tex2D(_OffsetTex, i.screenuv + i.uv);
				float4 refl = tex2D(_GlobalMirrorTex, i.screenuv + offset.xy * _RefractionMag * 5);
				float4 smudge = tex2D(_SmudgeTex, i.uv);
				return refl * smudge;
			}
			ENDCG
		}
	}
}
