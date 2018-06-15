Shader "Custom/SurfaceShader_VC" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Normal("Normap Map", 2D) = "bump" {}

		_MainTex2("Texture", 2D) = "white" {}
		_MainTex3("Texture", 2D) = "white" {}
		_MainTex4("Texture", 2D) = "white" {}
		_SplatMap("Texture", 2D) = "white" {}

		_ScalarA("Wave Intensity", Range(0.0, 0.2)) = 0.01
		_ScalarB("Wave Density", Range(0.0, 100)) = 0.01
		_ScalarC("Texture Speed", Range(-1.0, 1)) = 0.01

	}
		SubShader{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 200
		Blend One OneMinusSrcAlpha
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

		float _ScalarA;
		float _ScalarB;
		float _ScalarC;
		sampler2D _MainTex;
		sampler2D _MainTex2;
		sampler2D _MainTex3;
		sampler2D _MainTex4;
		sampler2D _SplatMap;
		float _offset;

		v2f vert(appdata v)
		{
			v2f projection;
			v2f world;

			world.vertex = mul(UNITY_MATRIX_M, v.vertex);
			world.normal = UnityObjectToWorldNormal(v.normal);

			if (tex2D(_SplatMap, v.uv, 0, 0).g >= 0.5f)
			{
				float x = world.vertex.x;
				float y = world.vertex.y;
				float z = world.vertex.z;

				world.vertex.x = x + (sin((_Time[1]) + y * z * _ScalarB)*_ScalarA);
				world.vertex.y = y + (sin((_Time[1]) + x * z * _ScalarB)*_ScalarA);
				world.vertex.z = z + (sin((_Time[1]) + x * y * _ScalarB)*_ScalarA);
			}

			projection.vertex = mul(UNITY_MATRIX_VP, world.vertex);
			projection.normal = mul(UNITY_MATRIX_VP, world.normal);

			projection.uv = v.uv;// + fmod(_Time[1] * 0.2f, 1);

								 //To complete initialisation
			projection.local = float4(0, 0, 0, 1);

			projection.normal = UnityObjectToWorldNormal(v.normal);

			return projection;
		}

		fixed4 frag(v2f i) : SV_Target
		{
			float4 color = tex2D(_MainTex, i.uv, 0, 0);
			float2 uv2 = i.uv;
			uv2.y += _Time[1] * _ScalarC; //For water shader
			float4 color2 = tex2D(_MainTex2, uv2, 0, 0);
			float4 color3 = tex2D(_MainTex3, i.uv, 0, 0);
			float4 color4 = tex2D(_MainTex4, i.uv, 0, 0);
			float4 splatMap = tex2D(_SplatMap, i.uv, 0, 0);

			color = ((splatMap.r * color) + (splatMap.g * color2) + (splatMap.b * color3));// +(splatMap.a * color4)); // (splatMap.r + splatMap.g + splatMap.b + splatMap.a);
																						   //color.a = 0.5;
			return color;
		}
			ENDCG
		}
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard fullforwardshadows vertex:vert alpha:fade

		// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0

	sampler2D _MainTex;
	sampler2D _Normal;
	sampler2D _SplatMap;

	float _ScalarA;
	float _ScalarB;
	float _ScalarC;

	struct Input {
		float2 uv_MainTex;
		float4 vertex : SV_POSITION;
		float4 color : COLOR;
	};

	void vert(inout appdata_full v, out Input o)
	{
		UNITY_INITIALIZE_OUTPUT(Input, o);
		if (tex2D(_SplatMap, o.uv_MainTex, 0, 0).g >= 0.5f)
		{
			float x = v.vertex.x;
			float y = v.vertex.y;
			float z = v.vertex.z;

			v.vertex.x = x + (sin((_Time[1]) + y * z * _ScalarB)*_ScalarA);
			v.vertex.y = y + (sin((_Time[1]) + x * z * _ScalarB)*_ScalarA);
			v.vertex.z = z + (sin((_Time[1]) + x * y * _ScalarB)*_ScalarA);
		}
		o.color = v.color;
	}

	fixed4 _Color;

	void surf(Input IN, inout SurfaceOutputStandard o) {
		// Albedo comes from a texture tinted by color
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb*IN.color;
		o.Normal = UnpackNormal(tex2D(_Normal, IN.uv_MainTex));
		o.Alpha = c.a*IN.color.a;
	}
	ENDCG
	}
		FallBack "Diffuse"
}