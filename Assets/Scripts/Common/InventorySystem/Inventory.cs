using System;
using Common.InventorySystem.Items;

namespace Common.InventorySystem
{
    /// <summary>
    /// The Inventory class acts as a facade for managing inventory items and currencies.
    /// It exposes methods and events from both the InventoryItemsManager and CurrenciesManager classes.
    /// The Inventory class allows users to add, remove, and exchange items, as well as manage currencies.
    /// It provides access to item holders and currency holders, making it easy to interact with inventory items
    /// and currencies without directly dealing with the underlying managers.
    /// </summary>
    public class Inventory
    {
        private readonly InventoryItemsManager itemsManager;
        private readonly CurrenciesManager currenciesManager;

        public event Action<IAmInventoryItem> ItemAdded
        {
            add => itemsManager.ItemAdded += value;
            remove => itemsManager.ItemAdded -= value;
        }

        public event Action<IAmInventoryItem> ItemRemoved
        {
            add => itemsManager.ItemRemoved += value;
            remove => itemsManager.ItemRemoved -= value;
        }

        public event Action InventoryChanged
        {
            add => itemsManager.InventoryChanged += value;
            remove => itemsManager.InventoryChanged -= value;
        }

        public Inventory()
        {
            itemsManager = new InventoryItemsManager();
            currenciesManager = new CurrenciesManager();
        }

        public bool TryAddItem(IAmInventoryItem item)
        {
            return itemsManager.TryAddItem(item);
        }

        public bool TryRemoveItem(IAmInventoryItem item)
        {
            return itemsManager.TryRemoveItem(item);
        }

        public bool TryAddHolder(IAmItemHolder holder)
        {
            return itemsManager.TryAddHolder(holder);
        }

        public bool TryGetItemHolder<T>(out T holder) where T : IAmItemHolder
        {
            return itemsManager.TryGetItemHolder(out holder);
        }

        public void AddCurrency(CurrencyType currencyType, int amount)
        {
            currenciesManager.AddCurrency(currencyType, amount);
        }

        public bool TrySpendCurrency(CurrencyType currencyType, int amount)
        {
            return currenciesManager.TrySpendCurrency(currencyType, amount);
        }

        public int GetCurrencyAmount(CurrencyType currencyType)
        {
            return currenciesManager.GetCurrencyAmount(currencyType);
        }

        public bool TryExchangeValuableForCurrencies(ValuableItem valuableItem)
        {
            if (!itemsManager.TryRemoveItem(valuableItem)) return false;

            foreach (CurrencyType currencyType in Enum.GetValues(typeof(CurrencyType)))
            {
                int currencyValue = valuableItem.GetValue(currencyType);
                if (currencyValue > 0)
                {
                    currenciesManager.AddCurrency(currencyType, currencyValue);
                }
            }

            return true;
        }
    }
}