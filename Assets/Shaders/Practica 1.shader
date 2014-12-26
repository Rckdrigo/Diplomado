Shader "MyShaders/Practica 1" {
	Properties{
		_MainTex("Base Texture (RGB)",2D) = "white"{}
		_MainTint("Color",Color) = (1,1,1,1)
		
		_NormalTex("Normal Map",2D) = "white"{}
		
		_SpecularTex ("Specular Mask (A)",2D) = "white"{}
		_SpecularColor ("Specular Color", Color) = (1,1,1,1)
		_SpecPower ("Specular Power", Range(0,30)) = 1
		
		//_EmissionTex("Emission Mask (A)",2D) = "white"{}
		_EmissionColor("Emission Color",Color) = (1,1,1,1)
	}
	
	
	SubShader {
		Tags{"RenderType" = "Opaque"}
		
		CGPROGRAM
		#pragma surface surf CustomPhong
		
		uniform sampler2D _MainTex;
		uniform half4 _MainTint;
		uniform sampler2D _NormalTex; 
		
		uniform sampler2D _SpecularTex;
		uniform half4 _SpecularColor;
		uniform float _SpecPower;
		
		uniform sampler2D _EmissionTex;
		uniform half4 _EmissionColor;
		
		struct Input{
				float2 uv_MainTex;	
				float2 uv_NormalTex;
				float2 uv_SpecularTex;
				float2 uv_EmissionTex;
		};
		
		struct SurfaceCustomOutput{
			fixed3 Albedo;
			fixed3 Normal;
			fixed3 Emission;
			
			fixed SpecularColor;
			half Specular;
			fixed Alpha;
			fixed Gloss;
		};
		
		inline float4 LightingCustomPhong(SurfaceCustomOutput s, fixed3 lightDir, half3 viewDir, fixed atten){

			float difLight = max(0,dot(s.Normal , lightDir));
			float hLamber = difLight / 2 + 0.5;
			
			float diff = dot(s.Normal , lightDir);
			float3 reflectionVector = normalize(2.0 * s.Normal * diff - lightDir);
			
			float spec = pow(max(0.5,dot(reflectionVector, viewDir)),_SpecPower);
			half4 specVector = _SpecularColor * spec * s.SpecularColor;
			
			fixed4 c;
			
			
			c.rgb = (s.Albedo * _LightColor0.rgb * difLight * atten * 2) + specVector.rgb * _LightColor0.rgb * 2;
			c.a = 1.0;
			
			return c;
		}
		
		void surf(Input IN, inout SurfaceCustomOutput o){
			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			half4 s = tex2D(_SpecularTex, IN.uv_SpecularTex);
			//half4 e = tex2D(_EmissionTex, IN.uv_EmissionTex);
			
			o.Albedo = c.rgb * _MainTint.rgb;
			o.Alpha = c.a;
			
			o.SpecularColor = s.a;
			o.Specular = _SpecPower;
			o.Gloss = 1.0;
			
			
       		float3 normalMap = UnpackNormal(tex2D(_NormalTex,IN.uv_NormalTex));
			o.Normal = normalMap.rgb;
			
			o.Emission = _EmissionColor.rgb * c.a * c.rgb;
		}
		
		ENDCG
	}
	FallBack "Diffuse"
}
