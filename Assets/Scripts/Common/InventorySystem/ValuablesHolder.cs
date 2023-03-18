using System.Collections.Generic;

namespace Common.InventorySystem.Items
{
    public class ValuablesHolder : IAmItemHolder
    {
        private List<IAmInventoryItem> valuables;

        public ValuablesHolder()
        {
            valuables = new List<IAmInventoryItem>();
        }

        public bool CanHold(IAmInventoryItem item)
        {
            return item is IAmValuable;
        }

        public bool TryAdd(IAmInventoryItem item)
        {
            if (CanHold(item))
            {
                valuables.Add(item);
                return true;
            }
            return false;
        }

        public bool TryRemove(IAmInventoryItem item)
        {
            return valuables.Remove(item);
        }
    }

}
