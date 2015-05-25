Shader "Custom/WavesWavesShader" {
	Properties{
		_Amplitude ("Wave Amplitude", Float) = 1
		_Frequency ("Wave Frequency", Float) = 1
		_Offset ("Wave Offset", Float) = 0
		_Speed("Wave Speed", Float) = 1
	}

	SubShader {
        Pass {

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            float _Amplitude;
            float _Frequency;
            float _Offset;
            float _Speed;

            struct v2f {
                float4 pos : SV_POSITION;
                fixed3 color : COLOR0;
            };

            v2f vert (appdata_base v)
            {
                v2f o;
                
                float4 worldv = mul(_Object2World, v.vertex);
                
                float height = cos((-v.vertex.x * _Frequency + _Offset + _Time.y)*_Speed)*_Amplitude +
                			sin((v.vertex.y * _Frequency + _Offset + _Time.y)*_Speed/4)*_Amplitude;
                //On Modifie la position des vertex
                //_Time.y est le temps qui passe en secondes , voir les fichiers unity
                //v.vertex = float4(v.vertex.x, height, v.vertex.z, v.vertex.w);
				v.vertex = float4(-v.vertex.x, v.vertex.z, height, v.vertex.w);               
                //Multiplication par les mtarices qui aligne le shader à l'ecran
                o.pos = mul (UNITY_MATRIX_MVP, v.vertex);  
				                  
                o.color = v.normal * 0.5 + 0.5;
                o.color = float4(height, height, 0.8, 0.5);
                return o;


            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4 (i.color, 0);
            }
            ENDCG

        }
    }
}
