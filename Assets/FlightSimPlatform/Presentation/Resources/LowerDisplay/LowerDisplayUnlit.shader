Shader "FlightSim/Lower Display Unlit"
{
    Properties { _MainTex("Live display",2D)="black" {} }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry+5" }
        Pass
        {
            Cull Off ZWrite On
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            sampler2D _MainTex;
            struct input { float4 vertex:POSITION;float2 uv:TEXCOORD0; };
            struct output { float4 pos:SV_POSITION;float2 uv:TEXCOORD0; };
            output vert(input v){output o;o.pos=UnityObjectToClipPos(v.vertex);o.uv=v.uv;return o;}
            fixed4 frag(output i):SV_Target{return fixed4(tex2D(_MainTex,i.uv).rgb,1);}
            ENDCG
        }
    }
}
