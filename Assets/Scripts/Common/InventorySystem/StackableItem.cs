using UnityEngine;

namespace Common.InventorySystem
{
    public class StackableItem : IAmStackableItem
    {
        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public Sprite Icon { get; private set; }

        [field: SerializeField]
        public int StackAmount { get; set; }
        
        public object Clone()
        {
            return new StackableItem
            {
                Name = Name,
                Icon = Icon,
                StackAmount = StackAmount
            };
        }
    }
}
