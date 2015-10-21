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
			ZTest On	
			ColorMask RGB
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			fixed4 _Color;

			vector vert(vector v : POSITION) : SV_POSITION {
				return mul(UNITY_MATRIX_MVP, v);
			}

			fixed4 frag(vector f : SV_POSITION) : COLOR {
				return _Color;
			}
			ENDCG
		}

		Pass {
			Blend SrcAlpha OneMinusSrcAlpha
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
				float beat = (sin(_Beats * _Time.y) + 1) / 2;
				float dist = distance(_WorldSpaceCameraPos, fragIn.worldPos.xyz);
				float ratio = clamp((dist / _ViewDistance), 0.5, 1);

				fixed4 c = _Color;
				c.a = beat * (1 - ratio);
			
				return c;
			}
			ENDCG
		}
	}
}
