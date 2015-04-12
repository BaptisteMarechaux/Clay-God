Shader "Custom/MovementRangeShaderVertex" {
	SubShader {
        Pass {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

           	struct vertOut {
                float4 pos:SV_POSITION;
                float4 scrPos;
            };

            vertOut vert(appdata_base v) {
                vertOut o;
                o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
                o.scrPos = ComputeScreenPos(o.pos);
                return o;
            }

            fixed4 frag(vertOut i) : SV_Target {
                return fixed4(0.18,0.37,0.73,1);
            }

            ENDCG
        }
    }
}
