Shader "Custom/RimLightShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_RimColor ("Rim Color", Color) = (0.26, 0.19, 0.16, 0.0)
		_RimPower ("Rim Power", Range(0.5, 8.0)) = 3.0
		_ColorTint ("Tint", Color) = (0.0, 0.0, 0.0, 1.0)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		
		CGPROGRAM
		#pragma surface surf WrapLambert finalcolor:mycolor

		half4 LightingWrapLambert (SurfaceOutput s, half3 lightDir, half atten){
			half NdotL = dot (s.Normal, lightDir);
			half diff = NdotL * 0.5 + 0.2;
			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * (diff * atten * 2);
			c.a = s.Alpha;
			return c;
		}

		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
		};
		fixed4 _ColorTint;
		void mycolor (Input IN, SurfaceOutput o, inout fixed4 color)
		{
			color *= _ColorTint;
		}
		
		sampler2D _MainTex;
		float4 _RimColor;
		float _RimPower;

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
			half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
			o.Emission = _RimColor.rgb * pow (rim, _RimPower);
			
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
