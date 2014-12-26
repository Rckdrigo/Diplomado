Shader "MyShaders/EdgeDetection" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_MinValue("Min Value", float) = 0
		_MaxValue("Max Value", float) = 1
		_LineColor("LineColor", color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		Pass{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			float _MinValue;
			float _MaxValue;
			half4 _LineColor;

			float intensity(float3 renderTex){
				return (renderTex.r +renderTex.g + renderTex.b)/3;
			}

			half4 UV(float2 origin, float2 displacement) {
				return tex2D(_MainTex,origin - displacement);
			}
			
			float threshold(fixed minV, fixed maxV, fixed value){
				if(value < minV)
					return 0;
				if(value > maxV)
					return 1;
				return value;
			}

			half4 frag(v2f_img i):COLOR{
							
				int width = _ScreenParams.x;
				int height = _ScreenParams.y;
				
				float deltaX = 1/_ScreenParams.x;
				float deltaY = 1/_ScreenParams.y;
				
				half result =(abs(intensity(UV(i.uv,float2(-deltaX,0))) - intensity(UV(i.uv,float2(deltaX,0)))) 
							+ abs(intensity(UV(i.uv,float2(0,deltaY))) - intensity(UV(i.uv,float2(0,-deltaY))))
							+ abs(intensity(UV(i.uv,float2(-deltaX,-deltaY))) - intensity(UV(i.uv,float2(deltaX,deltaY))))
							+ abs(intensity(UV(i.uv,float2(-deltaX,deltaY))) - intensity(UV(i.uv,float2(deltaX,-deltaY)))))/4;
				
				float value = threshold(_MinValue,_MaxValue,result);
				if(value < 1)
					discard;
				return _LineColor * value;
				
					//return tex2D(_MainTex,i.uv);
			}
			ENDCG
		}
	} 
}
