Shader "Custom/ObjectiveBlink" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_Beats("Beats Per Second", Float) = 1
		_ViewDistance("Maximum View Distance", Float) = 25
	}
	SubShader{
		Tags { "Queue" = "Overlay" }
		ZWrite Off
		ZTest Off
		Lighting Off
		Cull Off

		Pass{
			ZWrite On
			ColorMask 0
		}

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			fixed4 _Color;
			float _Beats;
			float _ViewDistance;

			struct vertOut {
				float4 pos : SV_POSITION;
				float4 worldPos : TEXCOORD0;
			};

			vertOut vert(vector v : POSITION)
			{
				vertOut o;

				o.pos = mul(UNITY_MATRIX_MVP, v);
				o.worldPos = mul(_Object2World, v);

				return o;
			}

			fixed4 frag(vertOut fragIn) : COLOR
			{
				float dist = distance(_WorldSpaceCameraPos, fragIn.worldPos.xyz);
				float ratio = clamp(dist / _ViewDistance, 0, 1);

				fixed4 c = _Color;
				c.a = 1 - ratio;
			
				return c;
			}
			ENDCG
		}
	}
}
