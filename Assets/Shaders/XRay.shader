Shader "Custom/XRayColor"
{
	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)
		_LightingDirection("Lighting Direction", Vector) = (0, 0, 0, 0)
		_LightingStrength("Lighting Strength", Range(0, 1)) = 0.3
		_XRayColor("X-Ray Color", Color) = (0, 0, 0, 0.5)
		_Radius("X-Ray Radius", Float) = 0.5

		_Center("X-Ray Center", Vector) = (0, 0, 0, 0)
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
		ZWrite Off

		Pass{
			ZWrite On
			ColorMask 0
		}
		
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			CGPROGRAM
			#pragma fragment frag
			#pragma vertex vert

			#include "UnityCG.cginc"

			fixed4 _Color;
			vector _LightingDirection;
			float _LightingStrength;
			fixed4 _XRayColor;
			float _Radius;
			vector _Center;

			struct vertOut {
				float4 pos : SV_POSITION;
				float4 worldPos : TEXCOORD0;
				float4 color : TEXTCOORD1;
			};

			vertOut vert(appdata_base v)
			{
				vertOut o;

				vector norm = vector (v.normal, 0);
				float dotProd = dot(norm, _LightingDirection);
				float lengthProd = length(norm) * length(_LightingDirection);
				float piAngle = acos(dotProd / lengthProd) / 3.14;
				float lightColor = fixed4(piAngle * _Color.rgb, 1);

				
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.worldPos = mul(_Object2World, v.vertex);
				o.color = lerp(_Color, lightColor, _LightingStrength);

				return o;
			}

			fixed4 frag(vertOut fragIn) : COLOR
			{
				float dist = distance(_Center, fragIn.worldPos);
				float mainPart = clamp(log(dist / _Radius), 0, 1);
				float xRayPart = 1 - mainPart;

				fixed4 mainColor = mainPart * fixed4(fragIn.color.rgb, 1);
				fixed4 xRayColor = xRayPart * _XRayColor;

				return mainColor + xRayColor;
			}
			ENDCG
		}
	}

	// TODO: this is simply to inherit shadowcasting passes. Make it ourselves?
	FallBack "Transparent/Cutout/VertexLit"
}