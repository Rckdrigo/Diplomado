Shader "MyShaders/NightVision" {
	Properties{
		_MainTex("Base",2D) = "white"{}
		_VignetteTex("Vignette Texture",2D) = "white"{}
		_ScanLineTex("ScanLine Texture",2D) = "white"{}
		_NoiseTex("Noise Texture",2D) = "white"{}
		
		_NightVisionColor("Night Vision Color",color) = (1,1,1,1)
		
		_NoiseXSpeed("Noise X Speed",float) = 100
		_NoiseYSpeed("Noise Y Speed",float) = 100
		_ScanLineTileAmount("ScanLine Tile Amount",float) = 4
		_Contrast("Contrast",Range(0,4)) = 2
		_Brightness("Brightness",Range(0,2)) = 1
		_RandomValue("Random Value",float) = 0
		_Distortion("Distortion",float) = 0.2
		_Scale("Scale (Zoom)",float) = 0.8
	}

	SubShader{
		Pass{
			CGPROGRAM
			
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"
			
			uniform sampler2D _MainTex;
			uniform sampler2D _VignetteTex;
			uniform sampler2D _ScanLineTex;
			uniform sampler2D _NoiseTex;
			
			uniform half4 _NightVisionColor;
			
			uniform fixed _Contrast;
			uniform fixed _ScanLineTileAmount;
			uniform fixed _Brightness;
			uniform fixed _RandomValue;
			uniform fixed _NoiseXSpeed;
			uniform fixed _NoiseYSpeed;
			uniform fixed _Distortion;
			uniform fixed _Scale;
			
			float2 barrelDistortion(float2 coord){
				float2 h = coord.xy - float2(0.5,0.5);
				float r2 = pow(h.x,2)+ pow(h.y,2);
				float f = 1 + r2 * (_Distortion * sqrt(r2));
				
				return f * _Scale * h + 0.5;
			}
			
			half4 frag(v2f_img i):COLOR{
				half2 distortedUV = barrelDistortion(i.uv);
				half4 renderTex =  tex2D(_MainTex,barrelDistortion(i.uv));
				half4 vignetteTex = tex2D(_VignetteTex,i.uv);
				
				half2 scanLinesUV = half2(i.uv.x * _ScanLineTileAmount,i.uv.y * _ScanLineTileAmount);
				half4 scanLineTex = tex2D(_ScanLineTex,scanLinesUV);
				
				half2 noiseUV = half2(i.uv.x + (_SinTime.z * _RandomValue * _NoiseXSpeed),i.uv.y + (_Time.x * _RandomValue * _NoiseYSpeed));
				half4 noiseTex =  tex2D(_NoiseTex,noiseUV);
				
				fixed lum = dot(fixed3(0.299,0.587,0.114), renderTex.rgb);
				lum += _Brightness;
				
				half4 finalColor = (lum*2) + _NightVisionColor;
				finalColor = pow(finalColor,_Contrast);
				finalColor *= vignetteTex;
				finalColor *= scanLineTex;
				finalColor *= noiseTex;
				
				return finalColor;
			}
			
			ENDCG
		}
	}
	
	Fallback "Diffuse"
}