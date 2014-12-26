Shader "MyShaders/NormalMap" {
	Properties{
		_MainTex("Base",2D) = "white"{}
		_NormalTex("Normal",2D) = "bump"{}
	}
		
	SubShader{
		CGPROGRAM
		
		#pragma surface surf BlinnPhong
		
		uniform sampler2D _MainTex;
		uniform sampler2D _NormalTex; 
		
		struct Input{
			float2 uv_MainTex;
			float2 uv_NormalTex;
		};
		
		void surf(Input IN, inout SurfaceOutput o){
			half4 c = tex2D(_MainTex,IN.uv_MainTex);
			o.Albedo = c.rgb;	
			o.Alpha = c.a;
			
			float3 normalMap = UnpackNormal(tex2D(_NormalTex,IN.uv_NormalTex));
			o.Normal = normalMap.rgb;
		}
		
		
		
		ENDCG
	}
}
