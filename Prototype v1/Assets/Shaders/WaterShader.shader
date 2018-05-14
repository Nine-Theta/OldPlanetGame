Shader "MyShaders/WaterShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		LOD 100

		Blend SrcAlpha OneMinusSrcAlpha

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
				//The following line is Etienne van der Kaap's pattented CG random value
				float random = frac(sin(dot(v.uv, float3(12.9898, 78.233, 45.5432))) * 43758.5453);
				
				v2f projection;
				v2f world;

				world.vertex = mul(UNITY_MATRIX_M, v.vertex);

				world.vertex.y = world.vertex.y * (sin((_Time[1]) + world.vertex.x * world.vertex.z));

				projection.vertex = mul(UNITY_MATRIX_VP, world.vertex);

				projection.uv = v.uv;

				//To complete initialisation
				projection.local = float4(0, 0, 0, 1);

				return projection;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float4 color = tex2D(_MainTex, i.uv, 0, 0);
				color.a = 0.5;
				return color;
			}
			ENDCG
		}
	}
}
