// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyShaders/BrickShader"
{
	Properties
	{
			_MainTex("Texture", 2D) = "white" {}
			_brickColor("BrickColor", color) = (1, 0.5, 0, 1)
			_cementColor("CementColor", color) = (0.5, 0.5, 0.5, 1)
			_verticalBricks("Vertical bricks", int) = 6
			_horizontalBricks("Horizontal bricks", int) = 4
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
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
					float4 local : TEXCOORD1;
				};

				sampler2D _MainTex;
				float4 _brickColor;
				float4 _cementColor;
				int _verticalBricks;
				int _horizontalBricks;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					o.local = v.vertex; //original position
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					float u = i.uv.x;
					float v = i.uv.y;

					float x = (u * (_horizontalBricks * 5));
					float y = ((v * (_verticalBricks * 5)));
					//float z = (u * 20) % 5;

					//Set an offset for x based on whether the row is even (from the top)
					if (floor(y / 5) % 2 == 0)
					{
						x += 2.5;
					}


					x = x % 5;
					y = y % 5;
					//float c = (y > x) ? y : x; // square boxes
					float c = 0;
					if (y > x)
						c = y;
					else
						c = x;

					if (c < 3)
					{
						return _brickColor;
					}
					else if (c < 4)
					{
						//shaded version
						return (_brickColor * 0.8);
					}
					else
					{
						return _cementColor;
					}
				}
				ENDCG
			}
		}
}
