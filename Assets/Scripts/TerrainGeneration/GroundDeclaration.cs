using System;
using UnityEngine;

namespace TerrainGeneration
{
    [Serializable]
    public class GroundDeclaration
    {
        private const string OccurenceWeightTooltipInfo =
            "The higher the OccurenceWeight the higher the chance for Ground Material to appear";
        [field: SerializeField]
        public TerrainDeclaration TerrainDeclaration { get; private set; }
        [field: SerializeField, Tooltip(OccurenceWeightTooltipInfo)]
        public uint OccurenceWeight { get; private set; }
    }
}