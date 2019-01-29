// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/CompositeMap" {
	Properties {
		_MainTex ("Previous", 2D) = "black"
		
		cmOp ("cmMul", Float) = 0
		
		
		cmMask ("cmMask", 2D) = "white" {}
		cmMaskChannel ("cmMaskChannel", Float) = 0
		cmInverseMask ("cmInverseMask", Float) = 0
		
		
		
		cmMul ("cmMul", Color) = (1,1,1,1)
		cmMap ("cmMap", 2D) = "white" {}

		cmWriteMask ("cmMaskChannel", Color) = (1,1,1,1)

	}
	
	
	
	SubShader {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
 
        Blend off
        ZWrite off
 
        Pass {  
            CGPROGRAM
                #pragma vertex VS
                #pragma fragment PS
           
                #include "UnityCG.cginc"
 
 
				#define opNormal 0
				#define opMultiply 1
				#define opAdd 2
				#define opSubtract 3
				#define opInvSubtract 4

				float cmOp;

				sampler2D cmMask;
				float cmMaskChannel;
				float cmInverseMask;
				
				
				float4 cmMul;
				sampler2D cmMap;
				float4 cmWriteMask;
		
				sampler2D _MainTex;
 
 
 
                struct VSI {
                    float4 vertex : POSITION;
                    float2 texcoord : TEXCOORD0;
                };
 
                struct VSO {
                    float4 vertex : POSITION;
                    float2 texcoord : TEXCOORD0;
                };
 
           
                VSO VS (VSI In)
                {
                    VSO Out;
                    Out.vertex = UnityObjectToClipPos(In.vertex);
                    Out.texcoord = In.texcoord;
                    return Out;
                }
           
                float4 PS (VSO In) : COLOR
                {
                    float4 Source = tex2D (_MainTex, In.texcoord);
			
					float4 Mask = tex2D(cmMask, In.texcoord);
					if (cmInverseMask){
						Mask = 1-Mask;
					}
					if (cmMaskChannel<4){
						Mask = dot(Mask,cmMaskChannel==float4(0,1,2,3));
					}
					Mask*= cmWriteMask;
					
					float4 Map = tex2D (cmMap, In.texcoord);
					float4 LayerColor = Map*cmMul;
					
					half4 Out = 0;
					
					if (opNormal == cmOp){
						Out = lerp(Source, Map*cmMul ,Mask);
					}else if (opMultiply == cmOp){
						Out = lerp(Source, Source*Map*cmMul ,Mask);
					}else if (opAdd == cmOp){
						Out = lerp(Source, Source + Map*cmMul ,Mask);
					}else if (opSubtract == cmOp){
						Out = lerp(Source, Source - Map*cmMul ,Mask);
					}else if (opInvSubtract == cmOp){
						Out = lerp(Source, Map*cmMul - Source,Mask);
					}
					
					return Out;
                }
            ENDCG
        }
    }
}
