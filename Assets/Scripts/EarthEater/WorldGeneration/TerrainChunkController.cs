using System;
using System.Collections;
using System.Collections.Generic;
using Destructible2D;
using EarthEater.WorldGeneration;
using TerrainGeneration;
using UnityEngine;
using TerrainData = TerrainGeneration.TerrainData;

public class TerrainChunkController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    
    private D2dDestructible destructible;
    [SerializeField, HideInInspector]
    private WorldData chunkTerrainData;
    private int chunkX;
    private int chunkY;

    private void Awake()
    {
        destructible = GetComponent<D2dDestructible>();
    }
    
    private void OnEnable()
    {
        destructible.OnModifiedPixels += Destructible_OnModifiedPixels;
    }

    private void OnDisable()
    {
        destructible.OnModifiedPixels -= Destructible_OnModifiedPixels;
    }

    public void InitializeChunk(WorldData worldData, int chunkX, int chunkY, int chunkSize = 128)
    {
        this.chunkX = chunkX;
        this.chunkY = chunkY;
        
        Texture2D texture = new Texture2D(chunkSize, chunkSize);
        chunkTerrainData = new WorldData(chunkSize, chunkSize);
        
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                Color color = Color.magenta;
                int worldCoordX = chunkX * chunkSize + x;
                int worldCoordY = chunkY * chunkSize + y;
                if (worldData.TryGetTerrain(out TerrainData terrainData, worldCoordX,worldCoordY))
                {
                    color = GetPixelFromSprite(terrainData.Sprite,worldCoordX,  worldCoordY);
                }

                chunkTerrainData.SetTerrain(x, y, terrainData);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, chunkSize, chunkSize), Vector2.one * .5f, 128);
        spriteRenderer.sprite = sprite;
    }

    private static Color GetPixelFromSprite(Sprite sprite, int x, int y)
    {
        return sprite.texture.GetPixel(x% 64 + (int)sprite.rect.x,
            y % 64 + (int)sprite.rect.y);
    }
    
    private void Destructible_OnModifiedPixels(List<int> pixelIndexes)
    {
        List<TerrainDestructionData> destroyedTerrainData = new List<TerrainDestructionData>();
        
        foreach (int pixelIndex in pixelIndexes)
        {
            int x = pixelIndex % chunkTerrainData.Rows;
            int y = pixelIndex / chunkTerrainData.Rows;
            chunkTerrainData.TryGetTerrain(out TerrainData terrainData, x, y);
            destroyedTerrainData.Add(new TerrainDestructionData( x+ chunkX * chunkTerrainData.Rows, y + chunkY * chunkTerrainData.Columns, terrainData));
            chunkTerrainData.SetTerrain(x, y, default);
        }
        
        new OnTerrainDestroyedAetherEvent(destroyedTerrainData).Invoke();
    }
}