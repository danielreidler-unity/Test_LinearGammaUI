using UnityEngine;
using UnityEditor;

public class SpritePostProcessor : AssetPostprocessor
{
    private void OnPostprocessSprites(Texture2D texture, Sprite[] sprites)
    {
        // Check first if this sprite should really be converted
        if(QualitySettings.activeColorSpace == ColorSpace.Linear && assetPath.StartsWith("Assets/Textures"))
        {
            ConvertAlphaColorSpace(texture);
        }
    }

    private void ConvertAlphaColorSpace(Texture2D texture, bool toLinear = true)
    {
        var conversionExponent = toLinear ? 0.4545f : 2.2f;
        
        var pixels = texture.GetPixels();
        var pixelCount = pixels.Length;
        
        for (int i = 0; i < pixelCount; i++)
        {
            var pixelValue = pixels[i];
            pixelValue.a = Mathf.Pow(pixelValue.a, conversionExponent);
            pixels[i] = pixelValue;
        }
        texture.SetPixels(pixels);
    }
}