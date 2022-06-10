Shader "Unlit/Ирэ"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _a ("Float display name", Float) = 0.5


        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                //float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                //float4 normal : TEXCOORD1;
            };

            sampler2D _MainTex;
            float _a;
            float4 _MainTex_ST;
            
            v2f vert (appdata v)
            {
                v2f o;

                float4 localCenter = v.vertex;
                float4 worldCenter = UnityObjectToClipPos(localCenter);

                //o.normal = v.normal;

                float lx =  localCenter.x;
                float ly =  localCenter.y;
                float lz =  localCenter.z;
                float lw =  localCenter.w;

                float wx =  worldCenter.x;
                float wy =  worldCenter.y;
                float wz =  worldCenter.z;
                float ww =  worldCenter.w;

                float vx =  lerp(lx, wx,.5);
                float vy =  lerp(ly, wy,.5);
                float vz =  lerp(lz, wz,.5);
                float vw =  lerp(lw, ww,.5);

                //o.vertex = float4(vx,vy,vz,vw);
                  o.vertex = float4(worldCenter);

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                
                _a =    v.vertex;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
				
                // apply fog
                
				
				col = float4(0.99999999999999,0.0,0.9,0.5 + abs(_SinTime.w)*0.01010);
				col = _Time;
				
				UNITY_APPLY_FOG(i.fogCoord, col);
				
                return col;
            }
            ENDCG
        }
    }
}
