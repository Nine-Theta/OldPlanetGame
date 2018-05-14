// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyShaders/WoodShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_woodColor("Wood Color", Color) = (0.5, 0.5, 0.1, 1)
		_ringColor("Ring Color", Color) = (0.3, 0.3, 0.1, 1)
		_ringMultiplier("RingFactor", int) = 7
		_stopDarkening("StopDarkening", range(0, 2)) = 1.0
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 100

			Pass
			{

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				// make fog work
				#pragma multi_compile_fog

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
					float3 normal : NORMAL;
					float4 localPos : TEXCOORD1;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
					float3 normal : NORMAL;
					float4 localPos : TEXCOORD1;
				};

				sampler2D _MainTex;
				float4 _woodColor;
				float4 _ringColor;
				float _ringMultiplier;
				float _stopDarkening;

				v2f vert(appdata local)
				{
					v2f output;
					output.vertex = UnityObjectToClipPos(local.vertex);
					output.uv = local.uv;
					output.localPos = local.vertex; //original position
					output.normal = local.normal;
					return output;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					float u = i.localPos.x;
					float v = i.localPos.y;
					float w = i.localPos.z;

					float x = (u) * 2;
					float y = (v) * 3;
					float z = (w) * 3;

					//This +10 is to fix a floating point error resulting from the sqrt function
					float c = sqrt(abs(x * x + y * y + z * x));

					float s = ((c * _ringMultiplier) % 1);


					if(_stopDarkening > 1)
					{
						if (s > 0.6)
							return (s * _woodColor);
						else if (s > 0.4)
							return ((s * _ringColor) + (s * _woodColor)) / 2;
						else
							return (s * _ringColor);
					}
					else if (_stopDarkening > 0)
					{
						return (_ringColor + (s * _woodColor)) / 2;
					}
					else
					{
						if (s > 0.6)
							return _woodColor;
						else if (s > 0.4)
							return (_ringColor + _woodColor) / 2;
						else
							return _ringColor;
					}
				}
				ENDCG
			}
		}
}
