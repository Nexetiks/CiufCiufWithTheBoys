using UnityEngine;

namespace TerrainGeneration
{
    [CreateAssetMenu(fileName = "Terrain Declaration", menuName = "TerrainGeneration/Terrain Declaration")]
    public class TerrainDeclaration : ScriptableObject
    {
        [field: SerializeField]
        public TerrainData TerrainData { get; set; }
    }
}