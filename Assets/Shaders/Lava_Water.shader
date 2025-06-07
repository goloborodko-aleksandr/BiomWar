Shader "Custom/Lava_Water_URP_WithOutline"
{
    Properties
    {
        // Основные слоты для лавы
        _MainTex          ("Lava Texture",    2D)     = "white" {}
        _NoiseTex         ("Noise Texture",   2D)     = "white" {}
        _Cubes            ("Overlay Texture", 2D)     = "white" {}
        _Contrast         ("Contrast",        Range(0.1, 2)) = 1.0
        _Intensity        ("Emission Intensity", Range(0, 5)) = 1.0
        _Speed            ("Scroll Speed",    Float)  = 0.1
        _Tiling           ("Texture Tiling",  Float)  = 2.0

        [Space]
        // Rim-light (как в ToonLightBase)
        _RimStep          ("RimStep",         Range(0, 1)) = 0.65
        _RimStepSmooth    ("RimStepSmooth",   Range(0, 1)) = 0.4
        _RimColor         ("RimColor",        Color)        = (1,1,1,1)

        [Space]
        // Outline (обводка из ToonLightBase)
        _OutlineWidth     ("OutlineWidth",    Range(0.0, 1.0)) = 0.15
        _OutlineColor     ("OutlineColor",    Color)            = (0,0,0,1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        LOD 200

        // ----------------------------------------
        // Основной Pass (Unlit + Rim + Lava Emission)
        // ----------------------------------------
        Pass
        {
            Name "Unlit"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS   : NORMAL;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 worldPos    : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float3 viewDirWS   : TEXCOORD2;
                float2 uv          : TEXCOORD3;
            };

            // Сэмплеры и параметры
            sampler2D _MainTex;
            sampler2D _NoiseTex;
            sampler2D _Cubes;
            float4   _MainTex_ST;
            float4   _NoiseTex_ST;
            float4   _Cubes_ST;

            float _Contrast;
            float _Intensity;
            float _Speed;
            float _Tiling;

            float _RimStep;
            float _RimStepSmooth;
            float4 _RimColor;

            float _OutlineWidth;   // используется только в Outline Pass
            float4 _OutlineColor;  // используется только в Outline Pass

            // Вершинный шейдер
            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                // Мировые координаты и нормаль
                float3 worldP = TransformObjectToWorld(IN.positionOS.xyz);
                float3 worldN = normalize(TransformObjectToWorldNormal(IN.normalOS));
                float3 viewDirection = normalize(GetCameraPositionWS() - worldP);

                // Передаём UV
                OUT.uv = IN.uv;

                // Передаём необходимые данные
                OUT.worldPos    = worldP;
                OUT.worldNormal = worldN;
                OUT.viewDirWS   = viewDirection;

                // Гомогенные координаты
                float4 posH = float4(worldP, 1.0);
                OUT.positionHCS = TransformWorldToHClip(posH);

                return OUT;
            }

            // Triplanar-сэмплирование базового цвета
            float3 TriplanarUV(float3 worldPos, float3 worldNormal, float tiling)
            {
                float3 blending = abs(worldNormal);
                blending = pow(blending, 4.0);
                blending /= (blending.x + blending.y + blending.z + 1e-5);

                float2 uvX = worldPos.zy * tiling;
                float2 uvY = worldPos.xz * tiling;
                float2 uvZ = worldPos.xy * tiling;

                float3 x = tex2D(_MainTex, uvX).rgb * blending.x;
                float3 y = tex2D(_MainTex, uvY).rgb * blending.y;
                float3 z = tex2D(_MainTex, uvZ).rgb * blending.z;

                return x + y + z;
            }

            // Применение контраста к цвету
            float3 ApplyContrast(float3 color, float contrast)
            {
                return (color - 0.5) * contrast + 0.5;
            }

            float4 frag(Varyings IN) : SV_Target
            {
                // Время для паннинга
                float time = _Time.y * _Speed;

                // TRIPLANAR базового цвета лавы
                float3 baseColor = TriplanarUV(IN.worldPos, IN.worldNormal, _Tiling);

                // Шум с анимацией по UV
                float2 scrollUV = IN.uv + float2(time, time);
                float3 noise = tex2D(_NoiseTex, scrollUV).rgb;

                // Применяем контраст к лаве
                float3 lava = ApplyContrast(baseColor * noise, _Contrast);

                // Эмиссия лавы
                float3 emissionColor = lava * _Intensity;

                // Дополнительный “Overlay” (Cubes)
                float2 cubesUV = IN.uv * _Cubes_ST.xy + _Cubes_ST.zw;
                float3 cubesLayer = tex2D(_Cubes, cubesUV).rgb;

                // -----------------------
                // Добавляем Rim Lighting
                // -----------------------
                // NV = dot(N, V)
                float NV = saturate(dot(IN.worldNormal, IN.viewDirWS));
                // Rim-фактор
                float rimFactor = smoothstep((1.0 - _RimStep) - _RimStepSmooth * 0.5,
                                             (1.0 - _RimStep) + _RimStepSmooth * 0.5,
                                             0.5 - NV);
                float3 rimColor = _RimColor.rgb * rimFactor;

                // Финальный цвет = Эмиссия + Overlay + Rim
                float3 finalColor = emissionColor + cubesLayer + rimColor;

                return float4(finalColor, 1.0);
            }
            ENDHLSL
        }

        // -----------------------
        // Outline Pass (Обводка)
        // -----------------------
        Pass
        {
            Name "Outline"
            Cull Front
            Tags { "LightMode"="SRPDefaultUnlit" }

            HLSLPROGRAM
            #pragma vertex vertOutline
            #pragma fragment fragOutline
            #pragma multi_compile_fog
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct AttributesO
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct VaryingsO
            {
                float4 positionHCS : SV_POSITION;
                float fogCoord     : TEXCOORD0;
            };

            float _OutlineWidth;
            float4 _OutlineColor;

            VaryingsO vertOutline(AttributesO v)
            {
                VaryingsO o;

                // Смещаем вершину вдоль нормали
                float3 offsetPos = v.vertex.xyz + v.normal * (_OutlineWidth * 0.1);

                // Трансформируем в гомогенные координаты
                float4 posH = float4(offsetPos, 1.0);
                o.positionHCS = TransformObjectToHClip(posH);

                // Вычисляем fogCoord
                o.fogCoord = ComputeFogFactor(o.positionHCS.z);
                return o;
            }

            float4 fragOutline(VaryingsO i) : SV_Target
            {
                // Применяем фог (если нужен)
                float3 colorFog = MixFog(_OutlineColor.rgb, i.fogCoord);
                return float4(colorFog, 1.0);
            }
            ENDHLSL
        }

        // Используем стандартный ShadowCaster из URP
        UsePass "Universal Render Pipeline/Lit/ShadowCaster"
    }

    FallBack "Hidden/Universal Render Pipeline/FallbackError"
}
