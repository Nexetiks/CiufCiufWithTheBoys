using UnityEngine;

namespace TerrainGeneration
{
    [CreateAssetMenu(fileName = "Material Declaration", menuName = "TerrainGeneration/Material Declaration")]
    public class MaterialDeclaration : ScriptableObject
    {
        [field: SerializeField]
        public Sprite Sprite { get; private set; }
    }
}