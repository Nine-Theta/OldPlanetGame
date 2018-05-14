// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyShaders/MyTestShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.local = v.vertex; //original position
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float u = i.uv.x;
				float v = i.uv.y;

				float x = (u - 0.5) * 2;
				float y = (v - 0.5) * 2;

				float c = sqrt(x * x + y * y);

				return fixed4(c, c, c, 1);
			}
			ENDCG
		}
	}
}
