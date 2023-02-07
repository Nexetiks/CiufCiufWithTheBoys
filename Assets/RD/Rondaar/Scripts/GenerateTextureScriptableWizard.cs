using Common.Util;
using TerrainGeneration;
using UnityEditor;
using UnityEngine;

//TODO: Add cave generation algorithm to make ore veins
public class GenerateTextureScriptableWizard : ScriptableWizard
{
    [SerializeField]
    private int resolution = 512;

    [SerializeField]
    private float perlinScale = .005f;

    [SerializeField]
    private BiomeDeclarationSO biomeDeclarationSo;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private static SpriteRenderer cachedRenderer;
    private static BiomeDeclarationSO cachedBiomeDeclarationSo;
    private static int cachedResolution;

    [MenuItem("Tools/Generate Perlin Noise Sprite")]
    private static void CreateWizard()
    {
        DisplayWizard<GenerateTextureScriptableWizard>("Generate Perlin Noise Sprite", "Generate")
            .Initialize(cachedRenderer, cachedResolution, cachedBiomeDeclarationSo);
    }

    private void Initialize(SpriteRenderer spriteRenderer, int resolution, BiomeDeclarationSO biomeDeclarationSo)
    {
        this.spriteRenderer = spriteRenderer;
        this.resolution = resolution;
        this.biomeDeclarationSo = biomeDeclarationSo;
    }

    private void OnWizardCreate()
    {
        // Create the texture
        Texture2D texture = new Texture2D(resolution, resolution);
        GenerateGround(texture);

        texture.Apply();

        // Create a new sprite from the texture
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, resolution, resolution), Vector2.one * .5f, resolution);
        spriteRenderer.sprite = sprite;
        SaveCache();
    }

    private void GenerateGround(Texture2D texture)
    {
        WeightedRandomObjectsPicker<MaterialDeclaration> materialsPicker =
            new WeightedRandomObjectsPicker<MaterialDeclaration>();

        foreach (GroundDeclaration groundDeclaration in biomeDeclarationSo.GroundDeclarations)
        {
            materialsPicker.AddObject(groundDeclaration.MaterialDeclaration, groundDeclaration.OccurenceWeight);
        }

        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                float perlinValue = Mathf.PerlinNoise((float)x * perlinScale, (float)y * perlinScale);
                Sprite groundSprite = materialsPicker.GetObjectByChanceValue(perlinValue).Sprite;
                Color col = GetPixelFromSprite(groundSprite, x, y);
                texture.SetPixel(x, y, col);
            }
        }
    }

    private void SaveCache()
    {
        cachedRenderer = spriteRenderer;
        cachedResolution = resolution;
        cachedBiomeDeclarationSo = biomeDeclarationSo;
    }

    private Color GetPixelFromSprite(Sprite sprite, int x, int y)
    {
        return sprite.texture.GetPixel(x % 64 + (int)sprite.rect.x,
            y % 64 + (int)sprite.rect.y);
    }
}