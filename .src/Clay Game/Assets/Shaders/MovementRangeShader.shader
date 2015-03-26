Shader "Custom/MovementRangeShader" {
	Properties{
		_Range ("Movement Range", int) = 1
		
	}

	SubShader {
        Pass {

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            int _Range;
       
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
                if(i.texcoord0.x > 0.1*_Range && i.texcoord0.x < (1-0.1*_Range))
                {
                	color = fixed4(1.0,1.0,1.0,1.0);
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
