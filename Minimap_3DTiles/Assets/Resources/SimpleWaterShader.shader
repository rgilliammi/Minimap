Shader"Custom/SimpleWaterShader"
{
    Properties
    {
        _MainTex ("Water Texture", 2D) = "white" {}
        _Speed ("Scroll Speed", Range(0.1, 10.0)) = 1.0
        _WaveScale ("Wave Scale", Range(0.1, 10.0)) = 1.0
        _Color ("Water Color", Color) = (0, 0.4, 0.7, 1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
LOD100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"

struct appdata_t
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float4 pos : SV_POSITION;
    float2 uv : TEXCOORD0;
};

float _Speed;
float _WaveScale;
float4 _Color;

v2f vert(appdata_t v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv * _WaveScale + _Time.y * _Speed;
    return o;
}

half4 frag(v2f i) : SV_Target
{
    half4 c = tex2D(_MainTex, i.uv);
    c.rgb *= _Color.rgb;
    return c;
}
            ENDCG
        }
    }
}
