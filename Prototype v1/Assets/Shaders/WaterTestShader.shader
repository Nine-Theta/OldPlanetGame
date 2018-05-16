Shader "MyShaders/WaterTestShader" {
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Scalar("Scalar", Float) = 1.0 

	}
		SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
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
			float3 normal : NORMAL;
			float2 uv : TEXCOORD0;
		};

		struct v2f
		{
			float2 uv : TEXCOORD0;
			float4 vertex : SV_POSITION;
			float3 normal : NORMAL;
			float4 local : TEXCOORD1;
		};

		sampler2D _MainTex;
		float _Scalar;

		v2f vert(appdata v)
		{
			//The following line is Etienne van der Kaap's pattented CG random value
			float random = frac(sin(dot(v.uv, float3(12.9898, 78.233, 45.5432))) * 43758.5453);

			v2f projection;
			v2f world;
					
			world.vertex = mul(UNITY_MATRIX_M, v.vertex);
			world.normal = UnityObjectToWorldNormal(v.normal);

			//world.vertex.y = world.vertex.y * (sin((_Time[1]) + world.vertex.x * world.vertex.z));
			//float3 sinF = (sin(_Time[1]), sin(_Time[1]), sin(_Time[1]));
			//world.vertex = (world.vertex.x + scale.x, world.vertex.y + scale.y, world.vertex.z + scale.z, world.vertex.w);
			//world.vertex = (float4)(world.vertex.x, world.vertex.y, world.vertex.z, world.vertex.a);
			float x = world.vertex.x;
			float y = world.vertex.y;
			float z = world.vertex.z;

			world.vertex.x = x + (sin((_Time[1]) + y * z)*_Scalar);
			world.vertex.y = y + (sin((_Time[1]) + x * z)*_Scalar);
			world.vertex.z = z + (sin((_Time[1]) + x * y)*_Scalar);

			projection.vertex = mul(UNITY_MATRIX_VP, world.vertex);

			projection.uv = v.uv;

			//To complete initialisation
			projection.local = float4(0, 0, 0, 1);

			return projection;
		}

		fixed4 frag(v2f i) : SV_Target
		{
			float4 color = tex2D(_MainTex, i.uv, 0, 0);
			color.a = 1;
			return color;
		}
			ENDCG
		}
	}
}

