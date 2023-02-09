using System;
using Entities.Components;
using UnityEngine;

namespace Entities
{
    [Serializable]
    public class EntityDefaultData
    {
        [field: SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName), ReorderableList]
        public BaseComponent[] Components { get; private set; }

        [field: SerializeField]
        public Sprite Sprite { get; private set; }

        public EntityDefaultData()
        {
        }
    }
}