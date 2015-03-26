Shader "Custom/Health Bars" {
	Properties{
		_Width ("HP Bar Width", Float) = 1
		_Height ("HP Bar Height", Float) = 1
	}

	SubShader {
        Pass {

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            float _Width;
            float _Heigth;

            struct v2f {
                float4 pos : SV_POSITION;
                fixed3 color : COLOR0;
            };

            v2f vert (appdata_base v)
            {
                v2f o;
                float4 worldv = mul(_Object2World, float4(0,0,0,1));
                
                o.pos = worldv + v.vertex;
                
                //o.color = v.normal * 0.5 + 0.5;
                o.color = float4(0, 1, 0.4, 1);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4 (i.color, 1);
            }
            ENDCG

        }
    }
}
