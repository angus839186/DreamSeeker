Shader "CustomRenderTexture/BSC"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex("Base (RGB)", 2D) = "white" {}
        _Brightness ("Brightness", Range(0,1)) = 1            // 亮度
        _Saturation ("Saturation", Range(0,1)) = 1            // 飽和度
        _Contrast ("Contrast", Range(0,1)) = 1                // 對比度
     }

     SubShader
     {
        Pass
        {
            Name "BSC"

            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex vert_img
            #pragma fragment frag

            sampler2D   _MainTex;   //主紋理
            half _Brightness;       //亮度
            half _Saturation;       //飽和度
            half _Contrast;         //對比度

            float4 frag(v2f_img i) : SV_Target
            {
                float4 tex = tex2D(_MainTex, i.uv);
                
                fixed3 finalcolor = tex.rgb * _Brightness;                          //應用亮度
                fixed luminance = 0.2125 * tex.r + 0.7154 * tex.g + 0.0721 * tex.b; //計算亮度
                fixed3 luminanceColor = fixed3(luminance,luminance,luminance);      //飽和度為0 亮度為luminance
                finalcolor = lerp(luminanceColor,finalcolor,_Saturation);           //應用飽和度
                fixed3 avgColor = fixed3(0.5,0.5,0.5);                              //飽和度為0 亮度為0.5的顏色
                finalcolor = lerp(avgColor,finalcolor,_Contrast);                   //應用對比度

                return fixed4(finalcolor,tex.a);
            }
            ENDCG
        }
    }
}
