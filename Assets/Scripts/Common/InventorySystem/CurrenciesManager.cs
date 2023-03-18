using System;
using System.Collections.Generic;

namespace Common.InventorySystem
{
    /// <summary>
    /// The CurrenciesManager class is responsible for managing different types of currencies and their corresponding currency holders.
    /// It provides methods for adding, spending, and getting the amount of a specific currency.
    /// It initializes currency holders for all available currency types and handles operations related to currencies.
    /// </summary>
    public class CurrenciesManager
    {
        private readonly Dictionary<CurrencyType, CurrencyHolder> currencyHolders;

        public CurrenciesManager()
        {
            currencyHolders = new Dictionary<CurrencyType, CurrencyHolder>();
            InitializeCurrencyHolders();
        }

        private void InitializeCurrencyHolders()
        {
            foreach (CurrencyType currencyType in Enum.GetValues(typeof(CurrencyType)))
            {
                currencyHolders.Add(currencyType, new CurrencyHolder(currencyType));
            }
        }

        public void AddCurrency(CurrencyType currencyType, int amount)
        {
            if (currencyHolders.TryGetValue(currencyType, out CurrencyHolder currencyHolder))
            {
                currencyHolder.AddAmount(amount);
            }
        }

        public bool TrySpendCurrency(CurrencyType currencyType, int amount)
        {
            if (currencyHolders.TryGetValue(currencyType, out CurrencyHolder currencyHolder))
            {
                return currencyHolder.TrySpend(amount);
            }

            return false;
        }

        public int GetCurrencyAmount(CurrencyType currencyType)
        {
            return currencyHolders.TryGetValue(currencyType, out CurrencyHolder currencyHolder) ? currencyHolder.Amount : 0;
        }
    }
}