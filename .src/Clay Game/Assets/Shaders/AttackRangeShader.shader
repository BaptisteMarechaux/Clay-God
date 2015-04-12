Shader "Custom/AttackRangeShader" {
	Properties{
		_Range ("Range", int) = 1
		_GridThickness ("Grid Thickness", Float) = 0.01

      _GridSpacingX ("Grid Spacing X", Float) = 1.0

      _GridSpacingY ("Grid Spacing Y", Float) = 1.0

      _GridOffsetX ("Grid Offset X", Float) = 0

      _GridOffsetY ("Grid Offset Y", Float) = 0
	}

	SubShader {
		Tags {"RenderType" = "Transparent" "Queue" = "Transparent"} 
			Blend SrcAlpha OneMinusSrcAlpha 
			cull Off
        Pass {
        	
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            
            int _Range;
            uniform float _GridThickness;
	        uniform float _GridSpacingX;
	        uniform float _GridSpacingY;
	        uniform float _GridOffsetX;
	        uniform float _GridOffsetY;
	        uniform float4 _GridColour;
	        uniform float4 _BaseColour;
       
            struct vertexInput {
                float4 vertex : POSITION;
                float4 texcoord0 : TEXCOORD0;
            };

            struct fragmentInput{
                float4 position : SV_POSITION;
                float4 texcoord0 : TEXCOORD0;
            };
            
            fragmentInput vert(vertexInput i){
                fragmentInput o;
                o.position = mul (UNITY_MATRIX_MVP, i.vertex);
                o.texcoord0 = i.texcoord0;
                return o;
            }

            fixed4 frag(fragmentInput i) : SV_Target {
                fixed4 color;
                float center = 0.5;
                float pSize = 0.01; //Panel size - Taille d'une case
                int truc = abs(50 - (i.texcoord0.y+0.005)/pSize); //Variable qui va servir à régler un probleme de position de pixels
                if(i.texcoord0.y < 0.5)
                {
                	truc += 1;
                }
                
                if(i.texcoord0.x > (center - pSize *_Range - 0.005) && i.texcoord0.x < (center + pSize *_Range + 0.005))
                {
					if(i.texcoord0.y > (center - pSize *_Range- 0.005) && i.texcoord0.y < (center + pSize*_Range+ 0.005))
					{
						if(i.texcoord0.x > center + pSize*truc - 0.005 - _Range*pSize && i.texcoord0.x < center - pSize*truc + 0.005 + _Range*pSize || i.texcoord0.y > center-pSize+0.005 && i.texcoord0.y < center+pSize-0.005)
						{
							if (frac((i.texcoord0.x + _GridOffsetX)/_GridSpacingX) < (_GridThickness / _GridSpacingX) || frac((i.texcoord0.y + _GridOffsetY)/_GridSpacingY) < (_GridThickness / _GridSpacingY))
							{
				            	color = fixed4(0.65,0.22,0.22,1);
					        }
					        else
					        {
								color = fixed4(0.9,0.3,0.3,1);
							}
						}
						else
						{
							discard;
						}
						
					}
					else
					{
						discard;
					}
                	
                }
                else
                {
                	discard;
                }
               
                return color;
            }
            
            
            
            ENDCG
        }
    }
}
