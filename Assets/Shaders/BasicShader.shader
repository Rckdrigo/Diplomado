Shader "MyShaders/BasicShader" {
	Properties{
		_Color("Bottom Color (RGB)",Color) = (1,0,0,1)
		_Value("Value",Range(0,1)) = 0
		_MainTex("Main Texture",2D) = "white"{}
	}
	
	SubShader {
		Pass{
		
			CGPROGRAM
			#pragma vertex vert
	        #pragma fragment frag
	        
	        uniform float4 _Color;
	        uniform float _Value;
	        uniform sampler2D _MainTex;
	        
	        struct vertexInput {
		        float4 vertex : POSITION;
		        float4 normal : NORMAL;
		        float2 uv : TEXCOORD;     
		    };
	        
			struct vertexOutput {
		        float4 pos : SV_POSITION;
		        float4 objectPos : vertex;
		        float2 uv : TEXCOORD;
		    };
		    
			vertexOutput vert (vertexInput v)
		    {
		        vertexOutput o;
		        o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
		        o.uv = v.uv;
		        o.objectPos = v.vertex;
		        
		        return o;
		    }

		    half4 frag (vertexOutput i) : COLOR
		    {
	        	if(tex2D(_MainTex ,i.uv).g < _Value)
	        		return tex2D(_MainTex ,i.uv) * _Color;
        		return tex2D(_MainTex ,i.uv);
		    }
			
			ENDCG
		}
	} 
}
