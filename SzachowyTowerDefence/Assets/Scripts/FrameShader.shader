Shader "Custom/FrameShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
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
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float tlx;
                float tly;
                float brx;
                float bry;


                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    float x = 1 - i.uv.x;
                    float y = 1 - i.uv.y;

                   if((((x < tlx + brx && x > tlx + brx - 0.01) || (x > tlx && x < tlx + 0.01)) 
                       || ((y > tly && y < tly + 0.01) || (y < tly + bry && y > tly + bry - 0.01))) 
                       && (x > tlx && x < tlx + brx) && (y > tly && y < tly + bry))
                   {    
                      col = fixed4(1, 0, 0, 1);
                   }

                   return col;
                }
            ENDCG
                }
        }
                Fallback "Diffuse"
}
