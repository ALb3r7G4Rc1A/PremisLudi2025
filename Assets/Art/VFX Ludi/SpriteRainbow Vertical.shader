Shader "Custom/SpriteRainbow_WavyFill"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed ("Speed", Range(-10,10)) = 1
        _Saturation ("Saturation", Range(0,1)) = 1
        _Value ("Value", Range(0,1)) = 1
        _Scale ("Color Scale", Range(0.01,20)) = 1
        _Vertical ("Vertical Fill (0 horiz, 1 vert)", Range(0,1)) = 1
        _Invert ("Invert coord", Range(0,1)) = 0
        _Fill ("Fill Amount", Range(0,1)) = 0.5
        _WaveAmplitude ("Wave Amplitude", Range(0,0.2)) = 0.05
        _WaveFrequency ("Wave Frequency", Range(0,10)) = 5
        _WaveSpeed ("Wave Speed", Range(-5,5)) = 1
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
            "PreviewType"="Plane"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Speed;
            float _Saturation;
            float _Value;
            float _Scale;
            float _Vertical;
            float _Invert;
            float _Fill;
            float _WaveAmplitude;
            float _WaveFrequency;
            float _WaveSpeed;

            struct appdata_t { float4 vertex : POSITION; float2 texcoord : TEXCOORD0; };
            struct v2f { float4 vertex : SV_POSITION; float2 texcoord : TEXCOORD0; };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            float3 HSVtoRGB(float h, float s, float v)
            {
                float3 k = float3(1.0, 2.0/3.0, 1.0/3.0);
                float3 p = abs(frac(h + k) * 6.0 - 3.0) - 1.0;
                p = saturate(p);
                return v * lerp(float3(1,1,1), p, s);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.texcoord;
                float coord = lerp(uv.x, uv.y, _Vertical);
                if (_Invert > 0.5) coord = 1.0 - coord;

                // calcular onda según posición perpendicular
                float wave = _WaveAmplitude * sin(uv.x * _WaveFrequency * 6.28318 + _Time.y * _WaveSpeed);
                float fillPos = _Fill + wave;

                // descartar si fuera del fill ondulado
                if(coord > fillPos)
                    discard; // transparente fuera del fill

                float hue = frac(coord * _Scale + _Time.y * _Speed);
                float3 rainbow = HSVtoRGB(hue, _Saturation, _Value);

                float4 texColor = tex2D(_MainTex, uv);
                texColor.rgb *= rainbow;

                return texColor;
            }
            ENDCG
        }
    }
}
