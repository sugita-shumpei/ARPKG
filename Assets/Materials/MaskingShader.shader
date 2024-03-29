Shader "Custom/MaskingShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range (0,1)) = 0.5
        _Metallic ("Metallic", Range (0,1)) = 0.0
    }

        SubShader{
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            Pass {
                // ステンシルバッファの設定
                Stencil{
                    // ステンシルの番号
                    Ref 2
                    // Always: このシェーダでレンダリングされたピクセルのステンシルバッファを「対象」とするという意味
                    Comp Always
                    // Replace: 「対象」としたステンシルバッファにRefの値を書き込む、という意味
                    Pass Replace
                }


                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };
                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float4 _Color;

                v2f vert (appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos (v.vertex);
                    o.uv = TRANSFORM_TEX (v.uv, _MainTex);
                    return o;
                }
                half4 frag (v2f i) : SV_Target{
                    return  tex2D (_MainTex, i.uv) * _Color;
                }
                ENDCG
            }
        }
        FallBack "Diffuse"
}

