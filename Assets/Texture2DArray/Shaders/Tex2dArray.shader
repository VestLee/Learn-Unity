Shader "Vesper/Texture2DArray"
{
    Properties
    {
        _MainTex ("Texture", 2DArray) = "" {}
        _Index("Index",Range(0, 4)) = 4
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

            #include "UnityCG.cginc"

            float _Index;

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

            UNITY_DECLARE_TEX2DARRAY(_MainTex);

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                _Index = 4 * frac(i.uv * 0.5);
                return UNITY_SAMPLE_TEX2DARRAY(_MainTex, float3(i.uv.x,i.uv.y,_Index));
            }
            ENDCG
        }
    }
}
