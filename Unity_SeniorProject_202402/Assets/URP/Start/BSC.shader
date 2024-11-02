Shader "CustomRenderTexture/BSC"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex("Base (RGB)", 2D) = "white" {}
        _Brightness ("Brightness", Range(0,1)) = 1            // �G��
        _Saturation ("Saturation", Range(0,1)) = 1            // ���M��
        _Contrast ("Contrast", Range(0,1)) = 1                // ����
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

            sampler2D   _MainTex;   //�D���z
            half _Brightness;       //�G��
            half _Saturation;       //���M��
            half _Contrast;         //����

            float4 frag(v2f_img i) : SV_Target
            {
                float4 tex = tex2D(_MainTex, i.uv);
                
                fixed3 finalcolor = tex.rgb * _Brightness;                          //���ΫG��
                fixed luminance = 0.2125 * tex.r + 0.7154 * tex.g + 0.0721 * tex.b; //�p��G��
                fixed3 luminanceColor = fixed3(luminance,luminance,luminance);      //���M�׬�0 �G�׬�luminance
                finalcolor = lerp(luminanceColor,finalcolor,_Saturation);           //���ι��M��
                fixed3 avgColor = fixed3(0.5,0.5,0.5);                              //���M�׬�0 �G�׬�0.5���C��
                finalcolor = lerp(avgColor,finalcolor,_Contrast);                   //���ι���

                return fixed4(finalcolor,tex.a);
            }
            ENDCG
        }
    }
}
