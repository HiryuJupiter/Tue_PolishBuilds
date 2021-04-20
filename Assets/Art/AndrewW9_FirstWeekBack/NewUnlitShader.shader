
//Slider property from 0, 1

Shader "Unlit/HealthBar"
{
    Properties
    {
        //Comment that removes the offset section
        [NoScaleOffset]
        _MainTex ("Texture", 2D) = "white" {}
        _Health ("Health", Range(0, 1)) = 1
        //_TexResolution ("TexResolution", float2) = (256, 32) 
    }

    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        //Tags { "RenderType"="Opaque" }

        Pass
        {
            ZWrite Off

            //scr * x + dst + y //This is the default
            //src * SrcAlpha + dst * (1 - SrcAlpha)
            Blend SrcAlpha OneMinusSrcAlpha 

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : POSITION;
                //float4 vertex : SV_POSITION;  ???
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _Health;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv; 
                //o.uv = TRANSFORM_TEX(v.uv, _MainTex); //This applies the tiling offset 

                return o;
            }


            //Mathf.lerp() is automatically clamped, while CG's lerp doesn't clamp
            float InverseLerp(float a, float b, float v)
            {
                //Gives a fraction (0.0~1.0)of these two numbers
                return saturate((v - a) / (b - a));
                //return (v - a) / (b - a);
            }


            fixed4 frag(v2f i) : SV_Target
            {
                //Mask
                 float healthBarMask = _Health > i.uv.x;

                //Sample the texture
                float3 healthBarColor = tex2D(_MainTex, float2(_Health, i.uv.y));
                
                float flash = (_Health < 0.2f) * cos(_Time.y * 15) * 0.4;
                //float flash = (1 - floor(_Health * 3)) * cos(_Time.y * 15) * 0.4;
                 //return float4(flash.xxx, 1);

                 return float4(healthBarColor + flash, healthBarMask);
                 //return float4(healthBarColor * flash, healthBarMask); //This darkens it
                //return float4(healthBarColor.rgb * healthBarMask, 1);
            }
            ENDCG
        }
    }
}
//To do: how do i select color on the image 

/*
        fixed4 frag(v2f i) : SV_Target
            {
                //Mask
                 float healthBarMask = _Health > i.uv.x;


                //Sample the texture
                float3 healthBarColor = tex2D(_MainTex, float2(_Health, i.uv.y));


                 //Flashing
                 //float freq = 1;
                 //float freq = 1;
float flash = 0;
if (_Health < 0.25)
    flash = cos(_Time.y * 15) * 0.4;
//return float4(flash.xxx, 1);

return float4(healthBarColor + flash, healthBarMask);
//return float4(healthBarColor * flash, healthBarMask); //This darkens it
//return float4(healthBarColor.rgb * healthBarMask, 1);
*/

/*

//Slider property from 0, 1

Shader "Unlit/HealthBar"
{
    Properties
    {
        //Comment that removes the offset section
        [NoScaleOffset]
        _MainTex ("Texture", 2D) = "white" {}
        _Health ("Health", Range(0, 1)) = 1
        //_TexResolution ("TexResolution", float2) = (256, 32)
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        //Tags { "RenderType"="Opaque" }

        Pass
        {
            ZWrite Off

            //scr * x + dst + y //This is the default
            //src * SrcAlpha + dst * (1 - SrcAlpha)
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : POSITION;
                //float4 vertex : SV_POSITION;  ???
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _Health;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                //o.uv = TRANSFORM_TEX(v.uv, _MainTex); //This applies the tiling offset

                return o;
            }


            //Mathf.lerp() is automatically clamped, while CG's lerp doesn't clamp
            float InverseLerp(float a, float b, float v)
            {
                //Gives a fraction (0.0~1.0)of these two numbers
                return saturate((v - a) / (b - a));
                //return (v - a) / (b - a);
            }


            fixed4 frag(v2f i) : SV_Target
            {
                //Sample the texture
                float h = InverseLerp(0.3, 0.31, _Health);
                fixed4 healthBarColor = tex2D(_MainTex, float2(floor(h * 8) /8, i.uv.y));

                //Mask
                 float healthBarMask = _Health > floor (i.uv.x * 8) / 8;
                return float4(healthBarColor.rgb * healthBarMask, 1);
            }
            ENDCG
        }
    }
}
*/

/*
//Sample the texture
                //fixed4 healthBarColor = tex2D(_MainTex, float2(1, i.uv.y));
                fixed4 healthBarColor = tex2D(_MainTex, float2(_Health, i.uv.y));
                //fixed4 healthBarColor = tex2D(_MainTex, i.uv.y);

                //Mask
                 float healthBarMask = _Health > i.uv.x;
                return float4(healthBarColor.rgb * healthBarMask, 1);
*/

/*
  fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);


                //Set up color
                //Saturate = clamp. Clamp is "Saturate"

                float tHealthColor = InverseLerp(0.3, 0.31, _Health);
            //float3 healthBarColor = (_MainTex, )
                float3 healthBarColor = lerp(float3(1, 0, 0), float3(0, 1, 0), tHealthColor);
                //float3 bgColor = float3(0, 0, 0);

                //Mask
                float healthBarMask = _Health > floor(i.uv.x * 8) / 8;
                //float healthBarMask = _Health > flooar((i.uv.x * 8) / 8);

                //Setting the alpha to 0 if the number is 0.
                //clip(healthBarMask - 0.001);

                //
                //float3 outColor = lerp(bgColor, healthBarColor, healthBarMask);

                //float healthBarColor = i.uv.x == _Health;

                return float4(healthBarColor, healthBarMask);

                //return healthBarMask * healthBarColor;
            }
*/

/*
                //fixed4 col = tex2D(_MainTex, i.uv);

                //Set up color
                //Saturate = clamp. Clamp is "Saturate"


                float tHealthColor = InverseLerp(0.3, 0.31, _Health);
            //float3 healthBarColor = (_MainTex, )
                float3 healthBarColor = lerp(float3(1, 0, 0), float3(0, 1, 0), tHealthColor);
                //float3 bgColor = float3(0, 0, 0);

                //Mask
                float healthBarMask = _Health > floor(i.uv.x * 8) / 8;
                //float healthBarMask = _Health > flooar((i.uv.x * 8) / 8);

                //Setting the alpha to 0 if the number is 0.
                //clip(healthBarMask - 0.001);

                //
                //float3 outColor = lerp(bgColor, healthBarColor, healthBarMask);

                //float healthBarColor = i.uv.x == _Health;

                return float4(healthBarColor, healthBarMask);

                //return healthBarMask * healthBarColor;
*/

/*
        fixed4 frag (v2f i) : SV_Target
            {
                //fixed4 col = tex2D(_MainTex, i.uv);

                //Set up color
                float3 healthBarColor = lerp(float3(1, 0, 0), float3(0, 1, 0), _Health);
                float3 bgColor = float3(0, 0, 0);

                //Mask
                float healthBarMask = _Health > i.uv.x;

                //
                float3 outColor = lerp(bgColor, healthBarColor, healthBarMask);

                //float healthBarColor = i.uv.x == _Health;

                return float4(outColor, 1);


                //return healthBarMask * healthBarColor;
            }
*/

/*



            fixed4 frag(v2f i) : SV_Target
            {
                //fixed4 col = tex2D(_MainTex, i.uv);

                //Set up color

                float tHealthColor = InverseLerp(0.5, 0.6, _Health);

                float3 healthBarColor = lerp(float3(1, 0, 0), float3(0, 1, 0), tHealthColor);

                //float2


                float3 bgColor = float3(0, 0, 0);

                //Mask
                float healthBarMask = _Health > floor(i.uv.x * 8) / 8;
                //float healthBarMask = _Health > flooar((i.uv.x * 8) / 8);

                //
                float3 outColor = lerp(bgColor, healthBarColor, healthBarMask);

                //float healthBarColor = i.uv.x == _Health;

                return float4(outColor, 1);


                //return healthBarMask * healthBarColor;
            }
            ENDCG
*/