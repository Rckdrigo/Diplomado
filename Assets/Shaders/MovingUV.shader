Shader "MyShaders/MovingUV" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_NormalTex ("Bump Map", 2D) = "white" {}
		
		_FilterColor("Filter Color", color) = (1,1,1,1)
	}
	SubShader {
		CGPROGRAM
		#pragma surface surf Lambert
			
		uniform sampler2D _MainTex;
		uniform sampler2D _NormalTex;
		uniform half4 _FilterColor;
			
		struct Input{
			float2 uv_MainTex;	
			float2 uv_NormalTex;
		};

		void surf(Input IN, inout SurfaceOutput o){ 
			float2 uvAnim = float2(_Time.x * 10,-_Time.x*20) ;
		
			half4 c = tex2D(_MainTex, IN.uv_MainTex + uvAnim);
		
			
			
			//if((IN.uv_MainTex + uvAnim).x < 0.2)
			//	discard;
			//else{
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			//}
			float3 normalMap = UnpackNormal(tex2D(_NormalTex,IN.uv_NormalTex + uvAnim));
			o.Normal = normalMap.rgb;
		}

		ENDCG		
			
	} 
	FallBack "Diffuse"
}
