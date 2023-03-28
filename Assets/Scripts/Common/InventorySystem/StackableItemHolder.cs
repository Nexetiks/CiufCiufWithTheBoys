using System.Collections;
using System.Collections.Generic;
using Common.InventorySystem;
using UnityEngine;

public class StackableItemHolder : IAmItemHolder
{
    public int StackAmount { get; private set; }
    public bool CanHold(IAmInventoryItem item)
    {
        return item is IAmStackableItem;
    }

    public bool TryAdd(IAmInventoryItem item)
    {
        if (!CanHold(item)) return false;
        
        StackAmount += (item as IAmStackableItem).StackAmount;
        return true;
    }

    public bool TryRemove(IAmInventoryItem item)
    {
        return false;
    }
    
    
}
