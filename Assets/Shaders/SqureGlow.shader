Shader "Custom/EdgeGlow3D_Local"
{
    Properties
    {
        _GlowColor("Glow Color", Color) = (0, 0.7, 1, 1)
        _GlowPower("Glow Power", Float) = 4.0
        _InnerRadius("Inner Hollow Radius", Range(0,1)) = 0.3
        _EdgeWidth("Edge Width", Range(0,1)) = 0.3
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

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0; // Используем UV
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _GlowColor;
            float _GlowPower;
            float _InnerRadius;
            float _EdgeWidth;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 center = float2(0.5, 0.5); // Центр UV
                float dist = distance(i.uv, center);

                // Переход от пустоты к краю
                float edge = smoothstep(_InnerRadius, _InnerRadius + _EdgeWidth, dist);
                float glow = pow(1.0 - edge, _GlowPower);

                float alpha = edge;

                return fixed4(_GlowColor.rgb * glow, alpha * _GlowColor.a);
            }
            ENDCG
        }
    }
}
