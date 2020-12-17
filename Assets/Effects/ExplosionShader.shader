Shader "Custom/ExplosionShader"
{
    Properties
    {

        _Interpolation("Interpolation", Range(0,1)) = 0.0
        _ColorStart("Start Color", Color) = (1,1,1,1)
        _TransitionColor("End Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        //_Glossiness ("Smoothness", Range(0,1)) = 0.5
        //_Metallic ("Metallic", Range(0,1)) = 0.0
        _Speed("Speed", Range(0,100)) = 15.0

    }
        SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:fade 
        #pragma target 3.0

        sampler2D _MainTex;
        float _Speed;

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

        struct Input
        {
            float2 uv_MainTex;
        };

        //half _Glossiness;
        //half _Metallic;
        fixed4 _TransitionColor;
        fixed4  _ColorStart;
        fixed _Interpolation;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = lerp(_ColorStart, _TransitionColor, _Interpolation) * tex2D(_MainTex, IN.uv_MainTex);
            
            o.Albedo = c.rgb;
            o.Alpha = tex2D(_MainTex, IN.uv_MainTex);
            // o.Metallic = _Metallic;
            // o.Smoothness = _Glossiness;                   
        }
        ENDCG
    }
        FallBack "Diffuse"
}
//faire la transparance plus animation et l'ajouter au material