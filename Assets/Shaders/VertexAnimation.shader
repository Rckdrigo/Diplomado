Shader "MyShaders/VertexAnimation" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Speed("Wave Speed" , Range(0.1,80)) = 5
		_Frequency("Frequency" , Range(0,5)) = 2
		_Amplitude("Amplitude" , Range(0,5)) = 2
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Pass{
			Cull Front
			
			CGPROGRAM
			#pragma vertex vert
	      	#pragma fragment frag
	        
	    	uniform sampler2D _MainTex;
	    	uniform float _Speed;
	    	uniform float _Frequency;
	    	uniform float _Amplitude;
	    	
			
	        struct vertexInput {
		        float4 vertex : POSITION;
		        float3 normalDir : NORMAL;
		        float2 uv_MainTex : TEXCOORD;
		    };
	        
			struct vertexOutput {
		        float4 pos : SV_POSITION;
		        float2 uv_MainTex : TEXCOORD;
		    };
		    
			vertexOutput vert (vertexInput v)
		    {
		    	vertexOutput o;
		       	float time = _Time * _Speed; 
		        float wave = sin(time + v.vertex.x * _Frequency) * _Amplitude;
		        
		       	float3 normal = normalize(float3(v.normalDir.x + wave, v.normalDir.y, v.normalDir.z));
		        v.uv_MainTex = o.uv_MainTex;
		        o.pos = mul(UNITY_MATRIX_MVP,float4(v.vertex.x,v.vertex.y + wave,v.vertex.z,1) + float4(normal,0));   
		     
		        return o;
		    }

		    half4 frag (vertexOutput i) : COLOR
		    {
	        	return tex2D(_MainTex,i.uv_MainTex);
		    }
			
			ENDCG
		}
		 
	}
}
