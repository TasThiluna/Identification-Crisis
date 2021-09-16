Shader "PostProcess/Vignette"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Vignette("Vignette", Float) = 0.0
		_Grayscale("Grayscale", Float) = 0.0
		_Timer("Timer", Float) = 0.0
	}

	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float _Vignette;
			uniform float _Grayscale;
			uniform float _Timer;

			float random (float2 st) {
                float OUT = sin(dot(floor(abs(float2(st.x*100.0,st.y*1000.0))),float2(12.9898,78.233)) + _Timer * 100.0)*43758.5453123;
                return abs(OUT) - floor(abs(OUT));
            }

			float4 frag(v2f_img i) : COLOR
			{
				float2 uvOffset = float2(i.uv.x - 0.5, i.uv.y - 0.5);				

				float4 color = lerp(tex2D(_MainTex, i.uv), float4(0, 0, 0, 1), _Vignette * length(uvOffset));
				float gray = dot(color.rgb, float3(0.2126, 0.7152, 0.0722));

				float4 baseColor = lerp(color, float4(1, 0, 0, 1), _Grayscale);
				float randomResult = random(i.uv);
				if (randomResult > 0.015)
					return baseColor;
				return clamp(randomResult, 0.0, 0.015);
			}
			ENDCG
		}
	}
}
