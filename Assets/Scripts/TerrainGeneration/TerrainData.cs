using EarthEater.WorldGeneration;
using UnityEngine;

namespace TerrainGeneration
{
    [System.Serializable]
    public struct TerrainData
    {
        [field: SerializeField, InLineEditor]
        public Sprite Sprite { get; private set; }
        [field: SerializeField]
        public int Durability { get; private set; }
        [field: SerializeField]
        public DestroyedTerrainFragment DestroyedTerrainFragment { get; set; }
    }
}
