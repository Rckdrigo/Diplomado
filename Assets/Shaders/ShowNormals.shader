Shader "MyShaders/ShowNormals"{
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
			    fixed4 color : COLOR;
			};

			vertexOutput vert (vertexInput v)
			{
			    vertexOutput o;
			    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			    o.color.xyz = mul (_Object2World, v.vertex) * 0.5 + 0.5;
			    o.color.w = 1;
			    return o;
			} 

			half4 frag (vertexOutput i) : COLOR
			{
				return i.color;
			}

			ENDCG
				
		}	
	}	
}

