using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraBlurEffect : MonoBehaviour
{
    public Material blurMaterial; // ぼかしマテリアルをインスペクタから設定

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (blurMaterial != null)
        {
            // ぼかしマテリアルを使用して画像を処理
            Graphics.Blit(source, destination, blurMaterial);
        }
        else
        {
            // マテリアルが設定されていない場合はそのまま出力
            Graphics.Blit(source, destination);
        }
    }
}
