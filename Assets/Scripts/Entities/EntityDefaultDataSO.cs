using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(fileName = "New EntityData", menuName = "New EntityData")]
    public class EntityDefaultDataSO : ScriptableObject
    {
        [field: SerializeField]
        public EntityDefaultData EntityDefaultData { get; private set; }
    }
}