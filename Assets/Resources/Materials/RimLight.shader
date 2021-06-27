Shader "Custom/RimLight" 
{
	Properties
	{
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Alpha ("Alpha", Range (0.1, 10)) = 0.4
		_RimLightColor ("Rim Light Color", Color) = (1, 1, 1, 1)
		_RimLightPower ("Rim Light Power", Range (0.1, 40.0)) = 5.0
	}

    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        Pass{
            ZWrite ON
            ColorMask 0
        }
        
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite OFF
        Pass 
        {
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
                 float3 normal: NORMAL;
                 float2 uv : TEXCOORD0;
            };
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 viewDir : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };

            float4 _MainTex_ST;
            sampler2D _MainTex;
            fixed4 _RimLightColor;
            half _RimLightPower;
            fixed4 _Color;
            half _Alpha;

            v2f vert (appdata v) {
                 v2f o;
                 o.vertex = UnityObjectToClipPos (v.vertex);
                 o.uv = TRANSFORM_TEX (v.uv, _MainTex);
                 float4x4 modelMatrix = unity_ObjectToWorld;
                 o.normalDir = normalize (UnityObjectToWorldNormal (v.normal));
                 o.viewDir = normalize (_WorldSpaceCameraPos - mul (modelMatrix, v.vertex).xyz);
                 return o;
            }

            fixed4 frag (v2f i) : SV_Target{
                fixed4 col = tex2D (_MainTex, i.uv) * _Color;

                half rim = 1.0 - abs (dot (i.viewDir, i.normalDir));
                fixed3 rimColor = pow (rim, _RimLightPower) * _RimLightColor;
                col.rgb += rimColor;

                half alpha = rim;
                alpha = clamp (alpha * _Alpha, 0.1, 1.0);
                col = fixed4 (col.rgb, alpha);

                return col;
            }


            ENDCG
        }
        
    }
}
