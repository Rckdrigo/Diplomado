Shader "MyShaders/PhongLightning" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_MainTint ("Main Tint", Color) = (1,1,1,1)
		_SpecularColor ("Specular Color", Color) = (1,1,1,1)
		_SpecPower ("Specular Power", Range(0,1)) = 0.5
	}
	
	SubShader {
		Tags { "RenderType"="Opaque" }
		CGPROGRAM
		#pragma surface surf BlinnPhong
		
		uniform sampler2D _MainTex;
		uniform half4 _MainTint;
		uniform half4 _SpecularColor;
		uniform float _SpecPower;
		
		struct Input{
				float2 uv_MainTex;	
		};
		
		void surf(Input IN, inout SurfaceOutput o){
			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			
			o.Albedo = c.rgb * _MainTint;
			o.Alpha = c.a;
			o.Specular = _SpecPower;
			o.Gloss = 1.0;
			
		}
		
		ENDCG
	} 
	
	Fallback "Diffuse"
}
