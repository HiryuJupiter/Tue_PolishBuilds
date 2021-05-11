Shader "Unlit/Lighting"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Gloss("Gloss", Range(0, 1)) = 1
        //_Gloss("Gloss", float, Range(0,1)) = 1
            _Color("Color", Color) = (1,1,1,1)
            _GlowMag("Glow Magnitude", float) = 0.5
            _GlowFeq("Glow Frequency", float) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

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
                float3 wPos : TEXCORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _Gloss;
            float _GlowMag;
            float _GlowFeq;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.wPos = mul(unity_ObjectToWorld, v.vertex); //Gives the world position of each vertex
                //o.normal = v.normal;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                //Diffuse lighting
                float3 N = normalize(i.normal);
                float3 L = _WorldSpaceLightPos0.xyz;
                float3 lambert = saturate(dot(N, L));
                float3 diffuseLight = lambert * _LightColor0.xyz;


                //Specular lighting
                float3 V = normalize(_WorldSpaceCameraPos - i.wPos); //view vector
                float3 H = normalize(L + V);
                float3 specularLight = saturate(dot(H, N));// *(lambert > 0));
                float specularExponent = exp2(_Gloss * 11) + 2;
                specularLight = pow(specularLight, specularExponent); //Specular exponent
                specularLight *= _LightColor0.xyz;

                //Fresnel
                float fresnel = (1 - dot(V, N)) * (cos(_Time.y * 4 * _GlowFeq))  * _GlowMag;
                return fresnel;

                return float4((specularLight + diffuseLight * _Color), 1);
            }
            ENDCG
        }

        //Pass
        //{
        //    CGPROGRAM
        //    #pragma vertex vert
        //    #pragma fragment frag
        //    #pragma multi_compile_fog

        //    #include "UnityCG.cginc"
        //    #include "Lighting.cginc"
        //    #include "AutoLight.cginc"

        //    struct appdata
        //    {
        //        float4 vertex : POSITION;
        //        float3 normal: NORMAL;
        //        float2 uv : TEXCOORD0;
        //    };

        //    struct v2f
        //    {
        //        float4 vertex : SV_POSITION;
        //        float2 uv : TEXCOORD0;
        //        float3 normal : TEXCOORD1;
        //        float3 wPos : TEXCORD2;
        //    };

        //    sampler2D _MainTex;
        //    float4 _MainTex_ST;
        //    float4 _Color;
        //    float _Gloss;

        //    v2f vert(appdata v)
        //    {
        //        v2f o;
        //        o.vertex = UnityObjectToClipPos(v.vertex);
        //        o.uv = TRANSFORM_TEX(v.uv, _MainTex);
        //        o.normal = UnityObjectToWorldNormal(v.normal);
        //        o.wPos = mul(unity_ObjectToWorld, v.vertex); //Gives the world position of each vertex
        //        //o.normal = v.normal;
        //        return o;
        //    }

        //    fixed4 frag(v2f i) : SV_Target
        //    {
        //        //Diffuse lighting
        //        float3 N = normalize(i.normal);
        //        float3 L = _WorldSpaceLightPos0.xyz;
        //        float3 lambert = saturate(dot(N, L));
        //        float3 diffuseLight = lambert * _LightColor0.xyz;


        //        //Specular lighting
        //        float3 V = normalize(_WorldSpaceCameraPos - i.wPos);
        //        float3 H = normalize(L + V);

        //        float3 specularLight = saturate(dot(H, N));// *(lambert > 0));
        //        float specularExponent = exp2(_Gloss * 11) + 2;
        //        specularLight = pow(specularLight, specularExponent); //Specular exponent

        //        specularLight *= _LightColor0.xyz;

        //        return float4((specularLight + diffuseLight * _Color), 1);
        //    }
        //    ENDCG
        //}
    }
}
