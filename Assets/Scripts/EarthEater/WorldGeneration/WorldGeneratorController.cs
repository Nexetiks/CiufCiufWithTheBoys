using System;
using System.Collections;
using System.Collections.Generic;
using TerrainGeneration;
using UnityEditor;
using UnityEngine;

public class WorldGeneratorController : MonoBehaviour
{
    [EditorButton(nameof(GenerateWorld))]
    [EditorButton(nameof(SpawnChunks))]
    [EditorButton(nameof(ClearChunks))]
    [EditorButton(nameof(UpdateChunks))]
    [SerializeField]
    private BiomeDeclarationSO biomeDeclarationSo;
    
    [SerializeField]
    private List<TerrainChunkController> terrainChunkControllers;

    [SerializeField]
    private TerrainChunkController chunkPrefab;

    [SerializeField]
    private int worldRows = 3840;

    [SerializeField]
    private int worldColumns = 3840;

    [SerializeField]
    private int chunkRows = 8;

    [SerializeField]
    private int chunkColumns = 8;
    
    [SerializeField]
    private int chunkResolution = 128;

    [SerializeField]
    private float distanceBetweenChunks = 8;

    [SerializeField]
    private int veinsAmount = 1;

    [SerializeField, HideInInspector]
    private WorldData worldData;

    private void Awake()
    {
        GenerateWorld();
        UpdateChunks();
    }

    public void GenerateWorld()
    {
        worldData = WorldFactory.GenerateWorld(worldRows, worldColumns, biomeDeclarationSo, veinsAmount);
    }

    public void SpawnChunks()
    {
        for(int x = 0; x < chunkRows; x++)
        {
            for(int y = 0; y < chunkColumns; y++)
            {
                TerrainChunkController chunk = Instantiate(chunkPrefab, transform);
                chunk.InitializeChunk(worldData, x, y, chunkResolution);
                terrainChunkControllers.Add(chunk);
                chunk.transform.localPosition = new Vector3(x * distanceBetweenChunks, y * distanceBetweenChunks, 0);
            }
        }
    }

    public void UpdateChunks()
    {
        for (int x = 0; x < chunkRows; x++)
        {
            for(int y = 0; y < chunkColumns; y++)
            {
                TerrainChunkController chunk = terrainChunkControllers[x * chunkColumns + y];
                chunk.InitializeChunk(worldData, x, y, chunkResolution);
            }
        }
    }
    
    // WARNING: this is dependent on the Alpha resolution of the Destructible2D sprite. Alpha res can be smaller than the actual sprite!!!
    // then we'll have to refactor this
    public Vector2 TerrainDataPixelToWorldPosition(int x, int y)
    {
        return new Vector2((float) x / 16 + transform.position.x - 4,
            (float) y / 16 + transform.position.y - 4);
    }
    
    private void ClearChunks()
    {
        foreach (var chunk in terrainChunkControllers)
        {
            DestroyImmediate(chunk.gameObject);
        }
        terrainChunkControllers.Clear();
    }
}
