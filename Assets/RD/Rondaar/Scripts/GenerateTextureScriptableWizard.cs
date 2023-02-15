using System.IO;
using Common.Util;
using TerrainGeneration;
using UnityEditor;
using UnityEngine;

//TODO: Add cave generation algorithm to make ore veins
public class GenerateTextureScriptableWizard : ScriptableWizard
{
    [SerializeField]
    private int resolution = 64;

    [SerializeField]
    private float perlinScale = .005f;

    [SerializeField]
    private int veinThickness = 10;
    
    [SerializeField]
    private int veinLength = 70;

    [SerializeField] 
    private float maxPerlinWormAngle = 180;

    [SerializeField]
    private int amountOfVeins = 3;

    [SerializeField]
    private BiomeDeclarationSO biomeDeclarationSo;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private static SpriteRenderer cachedRenderer;
    private static BiomeDeclarationSO cachedBiomeDeclarationSo;
    private static int cachedResolution = 1024;

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
        GenerateOreVeins(texture, amountOfVeins, veinLength, veinThickness);

        texture.Apply();

        // Create a new sprite from the texture
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, resolution, resolution), Vector2.one * .5f, resolution);
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/testPic2.png", bytes);

        spriteRenderer.sprite = sprite;
        SaveCache();
    }

    private void GenerateGround(Texture2D texture)
    {
        WeightedRandomObjectsPicker<TerrainDeclaration> materialsPicker =
            new WeightedRandomObjectsPicker<TerrainDeclaration>();

        foreach (GroundDeclaration groundDeclaration in biomeDeclarationSo.GroundDeclarations)
        {
            materialsPicker.AddObject(groundDeclaration.TerrainDeclaration, groundDeclaration.OccurenceWeight);
        }

        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                float perlinValue = Mathf.PerlinNoise((float)x * perlinScale, (float)y * perlinScale);
                Sprite groundSprite = materialsPicker.GetObjectByChanceValue(perlinValue).TerrainData.Sprite;
                Color col = GetPixelFromSprite(groundSprite, x, y);
                texture.SetPixel(x, y, col);
            }
        }
    }

    private void GenerateOreVeins(Texture2D texture, int amountOfVeins, int veinSize, int veinLength)
    {
        WeightedRandomObjectsPicker<TerrainDeclaration> oresPicker =
            new WeightedRandomObjectsPicker<TerrainDeclaration>();
        foreach (GroundDeclaration groundDeclaration in biomeDeclarationSo.OresDeclarations)
        {
            oresPicker.AddObject(groundDeclaration.TerrainDeclaration, groundDeclaration.OccurenceWeight);
        }
        
        for (int i = 0; i < amountOfVeins; i++)
        {
            Vector2 startPos = new Vector2(Random.Range(0, resolution), Random.Range(0, resolution));
            PerlinWorm perlinWorm = new PerlinWorm(startPos, maxPerlinWormAngle);
            TerrainDeclaration selectedOre = oresPicker.GetObjectByChanceValue(Random.value);

            for (int j = 0; j < veinSize; j++)
            {
                Vector2 pos = perlinWorm.Move();
                SetPixelWithThickness(texture, (int) pos.x, (int) pos.y, veinLength, selectedOre.TerrainData.Sprite);
            }
        }
    }

    private void SaveCache()
    {
        cachedRenderer = spriteRenderer;
        cachedResolution = resolution;
        cachedBiomeDeclarationSo = biomeDeclarationSo;
    }

    private static Color GetPixelFromSprite(Sprite sprite, int x, int y)
    {
        return sprite.texture.GetPixel(x % 64 + (int)sprite.rect.x,
            y % 64 + (int)sprite.rect.y);
    }

    public static void SetPixelWithThickness(Texture2D texture, int x, int y, int thickness, Sprite sprite)
    {
        int radius = (thickness - 1) / 2;
        for (int i = -radius; i <= radius; i++)
        {
            for (int j = -radius; j <= radius; j++)
            {
                int xCoord = x + i;
                int yCoord = y + j;
                if (xCoord < 0 || xCoord >= texture.width || yCoord < 0 || yCoord >= texture.height)
                {
                    continue;
                }

                if (xCoord >= 0 && xCoord < texture.width && yCoord >= 0 && yCoord < texture.height)
                {
                    float distance = Mathf.Sqrt(i * i + j * j);
                    if (distance <= radius)
                    {
                        texture.SetPixel(xCoord, yCoord,GetPixelFromSprite(sprite, xCoord, yCoord));
                    }
                }
            }
        }
    }
}