namespace Common.InventorySystem
{
    /// <summary>
    /// The CurrencyHolder class represents a holder for a specific currency type.
    /// It stores the amount of currency and provides methods for adding currency, spending currency, and getting the current amount.
    /// This class makes it easy to manage currency values and perform operations related to currencies.
    /// </summary>
    public class CurrencyHolder
    {
        public CurrencyType CurrencyType { get; private set; }
        public int Amount { get; private set; }

        public CurrencyHolder(CurrencyType currencyType)
        {
            CurrencyType = currencyType;
            Amount = 0;
        }

        public void AddAmount(int amount)
        {
            if (amount > 0)
            {
                Amount += amount;
            }
        }

        public bool TrySpend(int amount)
        {
            if (Amount >= amount)
            {
                Amount -= amount;
                return true;
            }

            return false;
        }
    }
}