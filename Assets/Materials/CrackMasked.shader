Shader "Custom/CrackMasked"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap ("Normal Map",2D) = "bump" {}
        _Glossiness ("Smoothness", Range (0,1)) = 0.5
        _Metallic ("Metallic", Range (0,1)) = 0.0
    }

        SubShader{
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent+1" }
            ZTest Always


            Pass {
                // ステンシルバッファの設定
                Stencil{
                // ステンシルの番号
                Ref 2
                // Equal: ステンシルバッファの値がRefと同じであれば描画を行う
                Comp Equal
                }

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                };
                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                    half3 lightDir : TEXCOORD1;
                    half3 viewDir : TEXCOORD2;
                };

                float4 _LightColor0;
                sampler2D _MainTex;
                sampler2D _BumpMap;
                float4 _MainTex_ST;
                
                float4 _Color;

                v2f vert (appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos (v.vertex);
                    o.uv = TRANSFORM_TEX (v.uv, _MainTex);

                    TANGENT_SPACE_ROTATION;
                    o.lightDir = mul (rotation, ObjSpaceLightDir (v.vertex));
                    o.viewDir = mul (rotation, ObjSpaceViewDir (v.vertex));

                    return o;
                }
                half4 frag (v2f i) : SV_Target {
                    i.lightDir = normalize (i.lightDir);
                    i.viewDir = normalize (i.viewDir);
                    half3 halfDir = normalize (i.lightDir + i.viewDir);

                    //Moving uv for Crack animation
                    float4 st = (_SinTime + float4(1.0,1.0f,1.0f,1.0f)) / 2.0;
                    float2  uv = i.uv;
                    if (uv.y > st.w) {
                        uv = float2(0.0f, 0.0f);
                    }
                    //
                    half3 normal = UnpackNormal (tex2D (_BumpMap, i.uv));
                    normal = normalize (normal);

                    half3 diffuse = max (0, dot (normal, i.lightDir)) * _LightColor0.rgb;
                    half3 specular = pow (max (0, dot (normal, halfDir)),  128.0) * _LightColor0.rgb;

                    fixed4 color;
                    color.rgb = tex2D (_MainTex, i.uv) * diffuse + specular;
                    color.a = 0;
                    return color * _Color;
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}


