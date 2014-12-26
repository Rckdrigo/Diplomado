Shader "MyShaders/CellShadeNormal" {
	Properties{
		_MainTex("Main Texture",2D) = "white"{}
		_RampTex("Ramp Texture",2D) = "white"{}
		
		_NormalTex("Normal",2D) = "bump"{}
		_LineWidth("Line Width",Range(0,0.005)) = 0
	}
	
	SubShader {
		Tags{"RenderType" = "Opaque"}
		
			CGPROGRAM
			#pragma surface surf BasicDiffuse
		
			uniform sampler2D _MainTex;
			uniform sampler2D _RampTex;
			uniform sampler2D _NormalTex; 
		
			struct Input{
				float2 uv_MainTex;
				float2 uv_NormalTex;
			};  
  			
	       	inline float4 LightingBasicDiffuse(SurfaceOutput s, fixed3 lightDir, fixed atten){
				float difLight = max(0,dot(s.Normal , lightDir));
				float hLambert = difLight / 2 + 0.5;
				float3 ramp = tex2D(_RampTex,float2(hLambert,hLambert)).rgb;
				float4 col;
				
				col.rgb = s.Albedo * _LightColor0.rgb * (ramp* atten * 2);
				col.a = s.Alpha;
				
				return col;
			}
			
	       	void surf (Input IN, inout SurfaceOutput o){
	       		half4 c = tex2D(_MainTex,IN.uv_MainTex); 
	       		
	       		o.Albedo = c.rgb;
	       		o.Alpha = c.a;
	       		
	       		float3 normalMap = UnpackNormal(tex2D(_NormalTex,IN.uv_NormalTex));
				o.Normal = normalMap.rgb;
	       	}
			ENDCG
			
			Pass{
				Cull Front
				
				CGPROGRAM
				#pragma vertex vert
		        #pragma fragment frag
		        
		    
				float _LineWidth;
		        struct vertexInput {
			        float4 vertex : POSITION;
			        float4 normal : NORMAL;   
			    };
		        
				struct vertexOutput {
			        float4 pos : SV_POSITION;
			        float4 objectPos : vertex;
			    };
			    
				vertexOutput vert (vertexInput v)
			    {
			        vertexOutput o;
			        o.pos = mul (UNITY_MATRIX_MVP, v.vertex + v.normal*_LineWidth);
			        
			        return o;
			    }

			    half4 frag (vertexOutput i) : COLOR
			    {
		        	return half4(0,0,0,1);
			    }
				
				ENDCG
			
			}
		
	} 
}
