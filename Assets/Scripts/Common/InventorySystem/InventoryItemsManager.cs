using System;
using System.Collections.Generic;

namespace Common.InventorySystem
{
    /// <summary>
    /// The InventoryItemsManager class is responsible for managing inventory items and their associated item holders.
    /// It provides methods for adding and removing items, as well as adding and retrieving item holders.
    /// One item can be held in multiple item holders, enabling the system to group items based on their types or functionalities.
    /// </summary>
    public class InventoryItemsManager
    {
        private readonly Dictionary<Type, IAmItemHolder> holders;
        private readonly List<IAmInventoryItem> items;
        private readonly Dictionary<IAmInventoryItem, List<IAmItemHolder>> itemToHoldersMap;

        public event Action<IAmInventoryItem> ItemAdded;
        public event Action<IAmInventoryItem> ItemRemoved;
        public event Action InventoryChanged;

        public InventoryItemsManager()
        {
            holders = new Dictionary<Type, IAmItemHolder>();
            items = new List<IAmInventoryItem>();
            itemToHoldersMap = new Dictionary<IAmInventoryItem, List<IAmItemHolder>>();
        }

        public bool TryAddItem(IAmInventoryItem item)
        {
            items.Add(item);
            ItemAdded?.Invoke(item);
            InventoryChanged?.Invoke();

            List<IAmItemHolder> itemHolders = new List<IAmItemHolder>();
            foreach (IAmItemHolder holder in holders.Values)
            {
                if (!holder.CanHold(item)) continue;

                holder.TryAdd(item);
                itemHolders.Add(holder);
            }

            itemToHoldersMap.Add(item, itemHolders);
            return true;
        }

        public bool TryRemoveItem(IAmInventoryItem item)
        {
            if (!items.Contains(item)) return false;

            items.Remove(item);
            ItemRemoved?.Invoke(item);
            InventoryChanged?.Invoke();

            if (!itemToHoldersMap.TryGetValue(item, out List<IAmItemHolder> itemHolders)) return true;

            foreach (IAmItemHolder holder in itemHolders)
            {
                holder.TryRemove(item);
            }

            itemToHoldersMap.Remove(item);
            return true;
        }

        public bool TryAddHolder(IAmItemHolder holder)
        {
            Type holderType = holder.GetType();

            if (!holders.ContainsKey(holderType))
            {
                holders.Add(holderType, holder);

                foreach (var item in items)
                {
                    if (holder.CanHold(item))
                    {
                        holder.TryAdd(item);
                        if (itemToHoldersMap.TryGetValue(item, out List<IAmItemHolder> itemHolders))
                        {
                            itemHolders.Add(holder);
                        }
                    }
                }

                return true;
            }

            return false;
        }

        public bool TryGetItemHolder<T>(out T holder) where T : IAmItemHolder
        {
            if (holders.TryGetValue(typeof(T), out IAmItemHolder untypedHolder))
            {
                holder = (T) untypedHolder;
                return true;
            }

            holder = default(T);
            return false;
        }
    }
}