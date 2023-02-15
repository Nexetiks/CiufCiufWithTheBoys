using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainGeneration
{
    [System.Serializable]
    public class WorldData
    {
        [field: SerializeField]
        public TerrainData[,] Terrain { get; private set; }

        [field: SerializeField]
        public int Rows { get;private set; }
        [field: SerializeField]
        public int Columns { get; private set; }

        public bool TryGetTerrain(out TerrainData terrainData, int row, int col)
        {
            terrainData = default;  
            if (row < 0 || row >= Rows || col < 0 || col >= Columns)
                return false;
            
            terrainData = Terrain[row, col];
            return true;
        }
        
        public void SetTerrain(int row, int col, TerrainData terrainData)
        {
            Terrain[row, col] = terrainData;
        }

        public WorldData(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            
            Terrain = new TerrainData[rows , columns];
        }
    }
}