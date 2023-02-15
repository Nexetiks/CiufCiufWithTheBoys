using Common.Util;
using UnityEngine;

namespace TerrainGeneration
{
    public class WorldFactory
    {
        public static WorldData GenerateWorld(int rows, int columns, BiomeDeclarationSO biomeDeclarationSo, int veinsAmount, float perlinScale = 0.005f)
        {
            WorldData worldData = new WorldData(rows, columns);
            GenerateGround(ref worldData, biomeDeclarationSo);
            GenerateOreVeins(ref worldData, biomeDeclarationSo, veinsAmount, 5, 70, 180);
            return worldData;
        }
        
        private static void GenerateGround(ref WorldData worldData, BiomeDeclarationSO biomeDeclarationSo, float perlinScale = 0.005f)
        {
            WeightedRandomObjectsPicker<TerrainDeclaration> materialsPicker =
                new WeightedRandomObjectsPicker<TerrainDeclaration>();

            foreach (GroundDeclaration groundDeclaration in biomeDeclarationSo.GroundDeclarations)
            {
                materialsPicker.AddObject(groundDeclaration.TerrainDeclaration, groundDeclaration.OccurenceWeight);
            }

            for (int x = 0; x < worldData.Rows; x++)
            {
                for (int y = 0; y < worldData.Columns; y++)
                {
                    float perlinValue = Mathf.PerlinNoise((float)x * perlinScale, (float)y * perlinScale);
                    perlinValue = Mathf.Clamp01(perlinValue);
                    TerrainData selectedTerrainData = materialsPicker.GetObjectByChanceValue(perlinValue).TerrainData;
                    worldData.SetTerrain(x,y , selectedTerrainData);
                }
            }
        }
        
        private static void GenerateOreVeins(ref WorldData worldData, BiomeDeclarationSO biomeDeclarationSo, int amountOfVeins, int veinSize, int veinLength, float maxPerlinWormAngle)
        {
            WeightedRandomObjectsPicker<TerrainDeclaration> oresPicker =
                new WeightedRandomObjectsPicker<TerrainDeclaration>();
            foreach (GroundDeclaration groundDeclaration in biomeDeclarationSo.OresDeclarations)
            {
                oresPicker.AddObject(groundDeclaration.TerrainDeclaration, groundDeclaration.OccurenceWeight);
            }
        
            for (int i = 0; i < amountOfVeins; i++)
            {
                Vector2 startPos = new Vector2(Random.Range(0, worldData.Rows), Random.Range(0, worldData.Columns));
                PerlinWorm perlinWorm = new PerlinWorm(startPos, maxPerlinWormAngle);
                TerrainDeclaration selectedOre = oresPicker.GetObjectByChanceValue(Random.value);

                for (int j = 0; j < veinLength; j++)
                {
                    Vector2 pos = perlinWorm.Move();
                    SetTerrainPixelWithThickness(ref worldData, (int) pos.x, (int) pos.y, veinSize, selectedOre);
                }
            }
        }
        
        private static void SetTerrainPixelWithThickness(ref WorldData worldData, int x, int y, int thickness, TerrainDeclaration selectedOre)
        {
            int radius = (thickness - 1) / 2;
            for (int i = -radius; i <= radius; i++)
            {
                for (int j = -radius; j <= radius; j++)
                {
                    int xCoord = x + i;
                    int yCoord = y + j;
                    if (xCoord < 0 || xCoord >= worldData.Rows || yCoord < 0 || yCoord >= worldData.Columns)
                    {
                        continue;
                    }

                    float distance = Mathf.Sqrt(i * i + j * j);
                    if (distance <= radius)
                    {
                        worldData.SetTerrain(xCoord, yCoord, selectedOre.TerrainData);
                    }
                }
            }
        }
    }
}
