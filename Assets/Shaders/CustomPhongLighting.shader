Shader "MyShaders/CustomPhongLightning" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_MainTint ("Main Tint", Color) = (1,1,1,1)
		_SpecularColor ("Specular Color", Color) = (1,1,1,1)
		_SpecPower ("Specular Power", Range(0,30)) = 1
	}
	
	SubShader {
		Tags { "RenderType"="Opaque" }
		CGPROGRAM
		#pragma surface surf CustomPhong
		
		uniform sampler2D _MainTex;
		uniform half4 _MainTint;
		uniform half4 _SpecularColor;
		uniform float _SpecPower;
		
		struct Input{
				float2 uv_MainTex;	
		};
		
		inline float4 LightingCustomPhong(SurfaceOutput s, fixed3 lightDir, half3 viewDir, fixed atten){
			float diff = dot(s.Normal , lightDir);
			float3 reflectionVector = normalize(2.0 * s.Normal * diff - lightDir);
			
			float spec = pow(max(0,dot(reflectionVector, viewDir)),_SpecPower);
			half4 specVector = (_SpecularColor * spec);
			
			fixed4 c;
			
			
			c.rgb = (s.Albedo * _LightColor0.rgb * diff * atten * 2) + specVector.rgb * _LightColor0.rgb;
			c.a = 1.0;
			
			return c;
		}
		
		void surf(Input IN, inout SurfaceOutput o){
			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			
			o.Albedo = c.rgb * _MainTint.rgb;
			o.Alpha = c.a;
			o.Specular = _SpecPower;
			o.Gloss = 1.0;
			
		}
		
		ENDCG
	} 
	
	Fallback "Diffuse"
}
