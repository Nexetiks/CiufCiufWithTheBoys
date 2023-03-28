using System.Collections;
using System.Collections.Generic;
using Common.InventorySystem;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemData", menuName = "Inventory/InventoryItemData")]
public class InventoryItemData : ScriptableObject
{
    [field: SerializeReference]
    [field: ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName)]
    public IAmInventoryItem InventoryItem;
}
