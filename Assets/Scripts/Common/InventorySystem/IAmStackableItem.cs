namespace Common.InventorySystem
{
    public interface IAmStackableItem : IAmInventoryItem
    {
        int StackAmount { get; set;  }
    }
}
