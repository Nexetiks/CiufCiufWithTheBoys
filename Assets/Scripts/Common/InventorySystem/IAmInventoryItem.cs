using UnityEngine;

namespace Common.InventorySystem
{
    public interface IAmInventoryItem
    {
        string Name { get; }
        Sprite Icon { get; }
        object Clone();
    }
}