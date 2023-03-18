using System.Collections.Generic;

namespace Common.InventorySystem.Items
{
    public class ValuableItem : IAmInventoryItem, IAmValuable
    {
        public string Name { get; }
        private Dictionary<CurrencyType, int> values;

        public ValuableItem(string name, Dictionary<CurrencyType, int> values)
        {
            Name = name;
            this.values = values;
        }

        public int GetValue(CurrencyType currency)
        {
            return values.ContainsKey(currency) ? values[currency] : 0;
        }
    }
}