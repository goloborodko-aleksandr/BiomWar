Shader "Custom/Glow_ColorShift"
{
    Properties
    {
        _GlowColorA("Color A", Color) = (0, 0.8, 1, 1)
        _GlowColorB("Color B", Color) = (1, 0, 1, 1)
        _GlowPower("Glow Power", Float) = 4.0
        _InnerRadius("Inner Hollow Radius", Range(0,1)) = 0.3
        _EdgeWidth("Edge Width", Range(0,1)) = 0.3
        _ShiftSpeed("Color Shift Speed", Float) = 1.0
        _GlowIntensity("Glow Intensity", Float) = 1.5
        _WaveAmp("Wave Amplitude", Float) = 0.15
        _WaveFreq("Wave Frequency", Float) = 4.0
        _WaveSpeed("Wave Speed", Float) = 2.0
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float4 _GlowColorA;
            float4 _GlowColorB;
            float _GlowPower;
            float _InnerRadius;
            float _EdgeWidth;
            float _ShiftSpeed;
            float _GlowIntensity;
            float _WaveAmp;
            float _WaveFreq;
            float _WaveSpeed;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 localPos : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.localPos = v.vertex.xyz;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 posXZ = i.localPos.xz;
                float dist = distance(posXZ, float2(0, 0));
                float edge = smoothstep(_InnerRadius, _InnerRadius + _EdgeWidth, dist);
                float glowShape = pow(1.0 - edge, _GlowPower);

                // Вычисляем угол по XZ — по окружности
                float angle = atan2(i.localPos.z, i.localPos.x);
                float wave = sin(angle * _WaveFreq + _Time.y * _WaveSpeed);

                // Нормализуем Y и добавим волновое смещение
                float yNorm = saturate(i.localPos.y + 0.5);
                float waveOffset = wave * _WaveAmp;

                // Добавляем волны в переход прозрачности
                float alphaHeight = smoothstep(1.0, 0.0, yNorm + waveOffset);

                float t = sin(_Time.y * _ShiftSpeed) * 0.5 + 0.5;
                float3 color = lerp(_GlowColorA.rgb, _GlowColorB.rgb, t);

                float alpha = edge * _GlowColorA.a * alphaHeight;

                return fixed4(color * glowShape * _GlowIntensity, alpha);
            }
            ENDCG
        }
    }
}
