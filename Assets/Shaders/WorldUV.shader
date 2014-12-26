Shader "MyShaders/WorldUV"{

	Properties{
		_Gradient("Gradient force",Range(1,10)) = 1
	}

	Subshader{
		Pass{				
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			struct vertexInput {
			    float4 vertex : POSITION;
			    float3 normal : Normal;
			};

			struct vertexOutput {
			    float4 pos : SV_POSITION;
			    fixed4 vertex : vertex;
			};


			float _Gradient;
	
			vertexOutput vert (vertexInput v)
			{
			    vertexOutput o;
			    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			    o.vertex = mul (_Object2World, v.vertex);
			    return o;
			} 

			half4 frag (vertexOutput i) : COLOR
			{
				return lerp( half4(1,0,0,1),half4(0,0,1,1),i.vertex.y*_Gradient+0.5);
			}

			ENDCG
				
		}	
	}	
}
