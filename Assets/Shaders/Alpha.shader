Shader "MyShaders/Alpha" {
	Properties{
		_MainTex("Base Color (RGBA)",2D) = "white"{}
		_TransVal("Transparency",Range(0,1)) = 0.5
	}

	Subshader{			
		CGPROGRAM
		#pragma surface surf Lambert alpha
		
		sampler2D _MainTex;
		float _TransVal;
		
			struct Input {
          		float2 uv_MainTex;
  			};   
		
		void surf (Input IN, inout SurfaceOutput o){
	       		half4 c = tex2D(_MainTex,IN.uv_MainTex); 
	       		
	       		o.Albedo = c.rgb;
	       		o.Alpha = c.a;
	       	}
		ENDCG
				
	}	
}