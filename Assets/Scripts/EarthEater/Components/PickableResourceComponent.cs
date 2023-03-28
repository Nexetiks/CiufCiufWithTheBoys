using Common.InventorySystem;
using Entities;
using Entities.Components;
using Unity.VisualScripting;
using UnityEngine;

namespace EarthEater.Components
{
    public class PickableResourceComponent : BaseComponent, IAmPickable
    {
        [SerializeField]
        private InventoryItemData inventoryItem;
        
        public Rigidbody2D Rb { get; private set; }
        
        public override void Initialize(Entity myEntity)
        {
            base.Initialize(myEntity);
            Rb = myEntity.GameObject.GetComponent<Rigidbody2D>();
        }

        public void OnPickUp(object picker)
        {
            if (picker is ResourcesGathererComponent resourcesGathererComponent)
            {
                resourcesGathererComponent.Inventory.TryAddItem(inventoryItem.InventoryItem.Clone() as IAmInventoryItem);
            }
            GameObject.Destroy(MyEntity.GameObject);
        }
    }
}
