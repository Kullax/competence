Shader "Custom/XRayColor"
{
	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)
		_XRayColor("X-Ray Color", Color) = (0, 0, 0, 0.5)
		_Radius("X-Ray Radius", Float) = 0.5

		_Center("X-Ray Center", Vector) = (0, 0, 0, 0)
	}
	SubShader
	{
		Cull Off
		Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True"}
		
		Pass
		{
			ZWrite On
			ColorMask 0
		}

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma fragment frag
			#pragma vertex vert

			fixed4 _Color;
			fixed4 _XRayColor;
			float _Radius;
			vector _Center;

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

			fixed4 frag (vertOut fragIn) : COLOR
			{
				float dist = distance(_Center, fragIn.worldPos);
				float mainPart = clamp(log(dist / _Radius), 0, 1);
				float xRayPart = 1 - mainPart;

				return mainPart * _Color + xRayPart * _XRayColor;
			}
			ENDCG
		}
	}

	// TODO: this is simply to inherit shadowcasting/receiving passes. Make it ourselves?
	FallBack "Diffuse"
}