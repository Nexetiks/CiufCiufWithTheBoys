using UnityEngine;

namespace TerrainGeneration
{
    [CreateAssetMenu(fileName = "Ore Declaration", menuName = "TerrainGeneration/Ore Declaration")]
    public class OreDeclaration : ScriptableObject
    {
        private const string WEIGHT_FIELD_INFO =
            "Weight determines the rarity of the ore - the higher the weight the more common the ore is";

        [field: SerializeField]
        public Sprite Sprite { get; private set; }

        [field: SerializeField]
        [field: Tooltip(WEIGHT_FIELD_INFO)]
        public uint Weight { get; private set; }
    }
}