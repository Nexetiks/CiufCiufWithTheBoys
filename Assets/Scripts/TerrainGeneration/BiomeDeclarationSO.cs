using System;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainGeneration
{
    [CreateAssetMenu(fileName = "Biome Declaration SO", menuName = "TerrainGeneration/Biome Declaration SO")]
    public class BiomeDeclarationSO : ScriptableObject
    {
        [field: SerializeField, ReorderableList]
        public List<GroundDeclaration> GroundDeclarations { get; private set; }
        [field: SerializeField]
        public uint Depth { get; private set; }
    }
}
