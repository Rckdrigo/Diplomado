Shader "MyShaders/CellShadeNormalSpecular" {
	Properties{
		_MainTex("Main Texture",2D) = "white"{}
		_NormalTex("Normal",2D) = "bump"{}
		
		_MainTint ("Main Tint", Color) = (1,1,1,1)
		_SpecularColor ("Specular Color", Color) = (1,1,1,1)
		_SpecPower ("Specular Power", Range(0,30)) = 1
		
		
		_LineColor("Line Color",Color) = (0,0,0,0)
		_LineWidth("Line Width",Range(0,0.02)) = 0
	}
	
	SubShader {
		Tags{"RenderType" = "Opaque"}
		
		CGPROGRAM
		#pragma surface surf CustomPhong
		
		uniform sampler2D _MainTex;
		uniform sampler2D _NormalTex; 
		
		uniform half4 _MainTint;
		uniform half4 _SpecularColor;
		uniform float _SpecPower;
		
		struct Input{
				float2 uv_MainTex;	
				float2 uv_NormalTex;
		};
		
		inline float4 LightingCustomPhong(SurfaceOutput s, fixed3 lightDir, half3 viewDir, fixed atten){
			float difLight = max(0,dot(s.Normal , lightDir));
			float hLambert = difLight / 2 + 0.5;
			
			float diff = dot(s.Normal , lightDir);
			float3 reflectionVector = normalize(2.0 * s.Normal * diff - lightDir);
			
			float spec = pow(max(0,dot(reflectionVector, viewDir)),_SpecPower);
			half4 specVector = (_SpecularColor * spec);
			
			fixed4 c;
			
			
			c.rgb = (s.Albedo * _LightColor0.rgb * difLight * atten * 2) + specVector.rgb * _LightColor0.rgb;
			c.a = 1.0;
			
			return c;
		}
		
		void surf(Input IN, inout SurfaceOutput o){
			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			
			o.Albedo = c.rgb * _MainTint.rgb;
			o.Alpha = c.a;
			o.Specular = _SpecPower;
			o.Gloss = 1.0;
			
			
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
				half4 _LineColor;
				
		        struct vertexInput {
			        float4 vertex : POSITION;
			        float4 normal : NORMAL;   
			    };
		        
				struct vertexOutput {
			        float4 pos : SV_POSITION;
			    };
			    
				vertexOutput vert (vertexInput v)
			    {
			        vertexOutput o;
			        o.pos = mul (UNITY_MATRIX_MVP, v.vertex + v.normal*_LineWidth);
			        
			        return o;
			    }

			    half4 frag (vertexOutput i) : COLOR
			    {
		        	return _LineColor;
			    }
				
				ENDCG
			
			}
		
	} 
}
