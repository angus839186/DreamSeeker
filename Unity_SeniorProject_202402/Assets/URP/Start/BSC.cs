using UnityEngine;

[ExecuteInEditMode]
//[RequireComponent(typeof(Camera))]
public class BSC : MonoBehaviour
{
    [Range(0.1f,1.0f)]
    public float brightness = 1.0f; //�G��
    [Range(0.1f, 1.0f)]
    public float saturation = 1.0f; //���M��
    [Range(0.1f, 1.0f)]
    public float contrast = 1.0f;   //����
    private Material material;

    public Shader shader;

    private void Start()
    {
        material = new Material(shader);
        material.hideFlags = HideFlags.DontSave;
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material != null)
        {
            material.SetFloat("_Brightness", brightness);
            material.SetFloat(" _Saturation", saturation);
            material.SetFloat("_Contrast",contrast);
            Graphics.Blit(src, dest, material);
        }
        else
        {
                Graphics.Blit(src, dest);
        }
    }
}
