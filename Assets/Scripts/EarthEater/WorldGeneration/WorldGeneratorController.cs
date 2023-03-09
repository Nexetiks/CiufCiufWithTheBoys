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
    [EditorButton(nameof(SpawnEmptyChunks))]
    [EditorButton(nameof(DestroyEmptyChunks))]
    [SerializeField]
    private BiomeDeclarationSO biomeDeclarationSo;
    
    [SerializeField]
    private List<TerrainChunkController> terrainChunkControllers;

    [SerializeField]
    private TerrainChunkController chunkPrefab;

    [SerializeField]
    private GameObject emptyChunkPrefab;

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

    [SerializeField]
    private float noiseFrequency = 0.005f;

    [SerializeField]
    private float wormNoiseFrequency = 0.005f;
    [SerializeField]
    private int veinSize = 5;
    [SerializeField]
    private int veinLength = 70;
    [SerializeField]
    private float maxPerlinWormAngle = 180;
    [SerializeField]
    private int terrainNoiseOctaves = 1;

    [SerializeField]
    private int emptyChunkColumns = 8;
    [SerializeField]
    private List<GameObject> emptyChunks = new List<GameObject>();

    [SerializeField, HideInInspector]
    private WorldData worldData;

    private void Awake()
    {
        GenerateWorld();
        UpdateChunks();
    }

    public void GenerateWorld()
    {
        worldData = WorldFactory.GenerateWorld(worldRows, worldColumns, biomeDeclarationSo, veinsAmount, noiseFrequency, wormNoiseFrequency, veinSize, veinLength, maxPerlinWormAngle, terrainNoiseOctaves);
    }

    public void SpawnChunks()
    {
        for(int x = 0; x < chunkColumns; x++)
        {
            for(int y = 0; y < chunkRows; y++)
            {
                TerrainChunkController chunk = Instantiate(chunkPrefab, transform);
                chunk.InitializeChunk(worldData, x, y, chunkResolution);
                terrainChunkControllers.Add(chunk);
                chunk.transform.localPosition = new Vector3(x * distanceBetweenChunks, y * distanceBetweenChunks, 0);
            }
        }
    }

    public void SpawnEmptyChunks()
    {
        DestroyEmptyChunks();
        SpawnEmptyChunksOnLeft();
        SpawnEmptyChunksOnRight();
        SpawnEmptyChunksBottom();
    }

    private void SpawnEmptyChunksOnLeft()
    {
        for(int x = emptyChunkColumns-1; x >= 0; x--)
        {
            for(int y = 0; y < chunkRows; y++)
            {
                GameObject chunk = Instantiate(emptyChunkPrefab, transform);
                emptyChunks.Add(chunk);
                chunk.transform.localPosition = new Vector3(-x * distanceBetweenChunks- distanceBetweenChunks, y * distanceBetweenChunks, 0);
            }
        }
    }
    

    private void SpawnEmptyChunksOnRight()
    {
        for(int x = 0; x < emptyChunkColumns; x++)
        {
            for(int y = 0; y < chunkRows; y++)
            {
                GameObject chunk = Instantiate(emptyChunkPrefab, transform);
                emptyChunks.Add(chunk);
                chunk.transform.localPosition = new Vector3((x + chunkRows) * distanceBetweenChunks, y * distanceBetweenChunks, 0);
            }
        }
    }

    private void SpawnEmptyChunksBottom()
    {
        for(int x = -emptyChunkColumns; x < chunkRows + emptyChunkColumns; x++)
        {
            for(int y = 0; y < chunkRows; y++)
            {
                GameObject chunk = Instantiate(emptyChunkPrefab, transform);
                emptyChunks.Add(chunk);
                chunk.transform.localPosition = new Vector3(x * distanceBetweenChunks, -(y+1) * distanceBetweenChunks, 0);
            }
        }
    }

    public void UpdateChunks()
    {
        for (int x = 0; x < chunkColumns; x++)
        {
            for(int y = 0; y < chunkRows; y++)
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
    
    private void DestroyEmptyChunks()
    {
        if (emptyChunks != null)
        {
            foreach (GameObject emptyChunk in emptyChunks)
            {
                DestroyImmediate(emptyChunk);
            }

            emptyChunks.Clear();
        }
    }
}