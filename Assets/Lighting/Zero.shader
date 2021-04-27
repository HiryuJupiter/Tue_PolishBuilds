Shader "Unlit/Lighting"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal: NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                //o.normal = v.normal;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {

                float3 N = normalize( i.normal);
                float3 L = _WorldSpaceLightPos0.xyz;
                float diffuseLight = saturate(dot(N, L)); //* _LightColor0.xyz;

                return float4(diffuseLight.xxx, 1);
                //return float4(L, 1);
                //return float4(i.uv, 0, 1); //this is something you can do to see your shader quickly
                //fixed4 col = tex2D(_MainTex, i.uv);
                //UNITY_APPLY_FOG(i.fogCoord, col);
                //return col;
            }
            ENDCG
        }
    }
}

/*
Shader "Unlit/Lighting"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal: NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = v.normal;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {

                return float4(i.normal, 1);
                //return float4(i.uv, 0, 1); //this is something you can do to see your shader quickly
                //fixed4 col = tex2D(_MainTex, i.uv);
                //UNITY_APPLY_FOG(i.fogCoord, col);
                //return col;
            }
            ENDCG
        }
    }
}

*/
