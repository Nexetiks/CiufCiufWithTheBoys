using Common.InventorySystem;
using Entities.Components;
using UnityEngine;

namespace EarthEater.Components
{
    public class InventoryComponent : BaseComponent
    {
        private InventoryItemsManager inventory;

        public InventoryItemsManager Inventory => inventory;

        public override object Clone()
        {
            InventoryComponent inventoryComponent = (InventoryComponent)base.Clone();
            inventoryComponent.inventory = new InventoryItemsManager();
            foreach (IAmInventoryItem item in inventoryComponent.Inventory.Items)
            {
                inventoryComponent.inventory.Items.Add(item);
            }

            return inventoryComponent;
        }
    }
}
