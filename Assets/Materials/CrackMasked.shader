Shader "Custom/CrackGlass"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range (0,1)) = 0.5
        _Metallic ("Metallic", Range (0,1)) = 0.0
        _BumpMap ("Normal Map",2D) = "bump" {}
        _BumpScale ("Normal Scale",Range (0,1)) = 1.0
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent+1" }
            ZTest Always
            

            Stencil{
                // ステンシルの番号
                Ref 2
                // Equal: ステンシルバッファの値がRefと同じであれば描画を行う
                Comp Equal
            }
            CGPROGRAM


            // Physically based Standard lighting model, and enable shadows on all light types
            #pragma surface surf Standard fullforwardshadows alpha

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

            sampler2D _MainTex;

            struct Input
            {
                float2 uv_MainTex;
            };

            half      _Glossiness;
            half      _Metallic;
            fixed4    _Color;
            sampler2D _BumpMap;
            half      _BumpScale;
            // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
            // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
            // #pragma instancing_options assumeuniformscaling
            UNITY_INSTANCING_BUFFER_START (Props)
                // put more per-instance properties here
            UNITY_INSTANCING_BUFFER_END (Props)

            void surf (Input IN, inout SurfaceOutputStandard o)
            {
                float4 st = (_SinTime + float4(1.0,1.0f,1.0f,1.0f)) / 2.0;
                float2  uv = IN.uv_MainTex;
                if (uv.y > st.w) {
                    uv = float2(0.0f,0.0f);
                }
                // Albedo comes from a texture tinted by color
                fixed4 c = tex2D (_MainTex, uv) * _Color;
                fixed4 n = tex2D (_BumpMap, uv);
                o.Albedo = c.rgb;
                // Metallic and smoothness come from slider variables
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
                o.Alpha = 0.1;
                o.Normal = UnpackScaleNormal (n,_BumpScale);
            }
            ENDCG
        }
            FallBack "Diffuse"
}



