using Entities.Abilities;
using Entities.Components;
using UnityEngine;

namespace Entities
{
    [System.Serializable]
    public class EntityDefaultData
    {
        [field: SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName), ReorderableList]
        public IAmAbility[] StartingAbilities { get; private set; }

        [field: SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName), ReorderableList]
        public BaseComponent[] Components { get; private set; }

        public EntityDefaultData()
        {
        }
    }
}