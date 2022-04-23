using System;

namespace OneDay.Core.Modules
{
    public interface IValletModule : IModule
    {
        Action<(string currencyType, int change, int total)> CurrencyChangedDelegate { get; set; }
        int GetCurrency(string currencyName);
    }

    public class ValletModule : IValletModule
    {
        public Action<(string currencyType, int change, int total)> CurrencyChangedDelegate { get; set; }

        private readonly ValletData valletData;
        public ValletModule(ValletData data) => valletData = data;
        public int GetCurrency(string currencyName)
        {
            return valletData.Currencies[currencyName];
        }

        public void AddCurrency(string currencyName, int amount)
        {
            valletData.Currencies[currencyName] += amount;
            CurrencyChangedDelegate?.Invoke((currencyName, amount, valletData.Currencies[currencyName]));
        }
    }
}
