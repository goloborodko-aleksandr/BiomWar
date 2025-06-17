Shader "Lpk/LightModel/ToonOutline"
{
    Properties
    {
        _OutlineWidth ("Outline Width", Range(0.0, 1.0)) = 0.15
        _OutlineColor ("Outline Color", Color) = (0.0, 0.0, 0.0, 1)
    }

    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Opaque" }

        Pass
        {
            Name "Outline"
            Tags { "LightMode"="SRPDefaultUnlit" }
            Cull Front

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #pragma multi_compile_instancing

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // GPU Instancing Properties
            UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(float, _OutlineWidth)
                UNITY_DEFINE_INSTANCED_PROP(float4, _OutlineColor)
            UNITY_INSTANCING_BUFFER_END(Props)

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float fogCoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            v2f vert(appdata v)
            {
                UNITY_SETUP_INSTANCE_ID(v);
                v2f o;
                UNITY_TRANSFER_INSTANCE_ID(v, o);

                float outlineWidth = UNITY_ACCESS_INSTANCED_PROP(Props, _OutlineWidth);
                float3 offset = v.normal * outlineWidth * 0.1;
                float3 newPos = v.vertex.xyz + offset;

                o.pos = TransformObjectToHClip(float4(newPos, 1.0));
                o.fogCoord = ComputeFogFactor(o.pos.z);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);
                float4 outlineColor = UNITY_ACCESS_INSTANCED_PROP(Props, _OutlineColor);
                float3 foggedColor = MixFog(outlineColor.rgb, i.fogCoord);
                return float4(foggedColor, 1.0);
            }
            ENDHLSL
        }
    }
}
