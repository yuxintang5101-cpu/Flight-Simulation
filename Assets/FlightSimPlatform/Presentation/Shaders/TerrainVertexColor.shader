Shader "FlightSim/Terrain Vertex Color"
{
    Properties { _Color("Tint",Color)=(1,1,1,1) }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert
        struct Input { float4 vertexColor : COLOR; };
        fixed4 _Color;
        void vert(inout appdata_full v,out Input o) { UNITY_INITIALIZE_OUTPUT(Input,o);o.vertexColor=v.color; }
        void surf(Input IN,inout SurfaceOutput o) { o.Albedo=IN.vertexColor.rgb*_Color.rgb;o.Alpha=1; }
        ENDCG
    }
    Fallback "Diffuse"
}
