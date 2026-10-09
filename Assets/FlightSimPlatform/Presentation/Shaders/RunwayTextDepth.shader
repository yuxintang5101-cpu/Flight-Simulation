Shader "FlightSim/Runway Text Depth"
{
    Properties
    {
        _MainTex ("Font Texture", 2D) = "white" {}
        _Color ("Text Color", Color) = (1, 1, 1, 1)
        _Cutoff ("Alpha Cutoff", Range(0, 1)) = 0.25
    }

    SubShader
    {
        Tags
        {
            "Queue" = "AlphaTest"
            "IgnoreProjector" = "True"
            "RenderType" = "TransparentCutout"
        }

        Pass
        {
            Cull Off
            Lighting Off
            ZWrite On
            ZTest LEqual
            Blend Off

            CGPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            fixed4 _Color;
            fixed _Cutoff;

            struct AppData
            {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct VertexToFragment
            {
                float4 position : SV_POSITION;
                fixed4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            VertexToFragment Vert(AppData input)
            {
                VertexToFragment output;
                output.position = UnityObjectToClipPos(input.vertex);
                output.color = input.color * _Color;
                output.uv = input.uv;
                return output;
            }

            fixed4 Frag(VertexToFragment input) : SV_Target
            {
                fixed alpha = tex2D(_MainTex, input.uv).a * input.color.a;
                clip(alpha - _Cutoff);
                return fixed4(input.color.rgb, 1.0);
            }
            ENDCG
        }
    }
}
