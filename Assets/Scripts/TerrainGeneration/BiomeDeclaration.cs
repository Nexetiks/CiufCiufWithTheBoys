using System;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainGeneration
{
    [CreateAssetMenu(fileName = "Biome Declaration", menuName = "TerrainGeneration/Biome Declaration")]
    public class BiomeDeclaration : ScriptableObject
    {
        [field: SerializeField]
        public List<OreDeclaration> OreDeclarations { get; private set; }
        [field: SerializeField]
        public uint Depth { get; private set; }
    }
}
