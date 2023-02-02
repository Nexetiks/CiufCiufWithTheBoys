using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GenerateTextureScriptableWizard : ScriptableWizard
{
    [SerializeField]
    private int resolution = 512;

    [SerializeField]
    private float perlinScale = .125f;

    [SerializeField]
    private Sprite groundSprite;

    [SerializeField]
    private Sprite goldSprite;

    [SerializeField]
    private Sprite rockSprite;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private static SpriteRenderer cachedRenderer;
    private static Sprite cachedGroundSprite;
    private static Sprite cachedGoldSprite;
    private static int cachedResolution;

    [MenuItem("Tools/Generate Perlin Noise Sprite")]
    static void CreateWizard()
    {
        DisplayWizard<GenerateTextureScriptableWizard>("Generate Perlin Noise Sprite", "Generate")
            .Initialize(cachedRenderer, cachedResolution, cachedGroundSprite, cachedGoldSprite);
    }

    private void Initialize(SpriteRenderer spriteRenderer, int resolution, Sprite groundSprite, Sprite goldSprite)
    {
        this.spriteRenderer = spriteRenderer;
        this.resolution = resolution;
        this.groundSprite = groundSprite;
        this.goldSprite = goldSprite;
    }

    void OnWizardCreate()
    {
        // Create the texture
        Texture2D texture = new Texture2D(resolution, resolution);
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                float perlinValue = Mathf.PerlinNoise((float)x * perlinScale, (float)y * perlinScale);
                Color col = new Color(1, 1, 1);
                if (perlinValue > .85f)
                {
                    col = GetPixelFromSprite(goldSprite, x, y);
                }
                else if(perlinValue > .6f)
                {
                    col = GetPixelFromSprite(rockSprite, x, y);
                }
                else
                {
                    col = GetPixelFromSprite(groundSprite, x, y);
                }

                texture.SetPixel(x, y, col);
            }
        }

        texture.Apply();

        // Create a new sprite from the texture
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, resolution, resolution), Vector2.one * .5f, resolution);
        spriteRenderer.sprite = sprite;
        cachedRenderer = spriteRenderer;
        cachedResolution = resolution;
        cachedGoldSprite = goldSprite;
        cachedGroundSprite = groundSprite;
    }
    
    private Color GetPixelFromSprite(Sprite sprite, int x, int y)
    {
        return sprite.texture.GetPixel(x % 64 + (int)sprite.rect.x,
            y % 64 + (int)sprite.rect.y);
    }
}