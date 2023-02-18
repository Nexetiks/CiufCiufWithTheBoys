using System.Collections.Generic;
using Aether;

namespace EarthEater.WorldGeneration
{
    public class OnTerrainDestroyedAetherEvent : Event<OnTerrainDestroyedAetherEvent>
    {
        public List<TerrainDestructionData> TerrainDestructionData { get; private set; }
        
        public OnTerrainDestroyedAetherEvent(List<TerrainDestructionData> terrainDestructionData)
        {
            TerrainDestructionData = terrainDestructionData;
        }
    }
}