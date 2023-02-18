using TerrainGeneration;

namespace EarthEater.WorldGeneration
{
    public struct TerrainDestructionData
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public TerrainData TerrainData { get; private set; }
        
        public TerrainDestructionData(int x, int y, TerrainData terrainData)
        {
            X = x;
            Y = y;
            TerrainData = terrainData;
        }
    }
}