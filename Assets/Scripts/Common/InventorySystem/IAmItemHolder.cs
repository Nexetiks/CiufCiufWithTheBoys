namespace Common.InventorySystem
{
    /// <summary>
    /// The IAmItemHolder interface defines the common functionalities and properties for item holders.
    /// Any class that implements this interface must provide methods for adding, removing, and checking if an item can be held.
    /// This interface allows for a flexible and extensible system for managing different types of item holders.
    /// </summary>
    public interface IAmItemHolder
    {
        bool CanHold(IAmInventoryItem item);
        bool TryAdd(IAmInventoryItem item);
        bool TryRemove(IAmInventoryItem item);
    }
}