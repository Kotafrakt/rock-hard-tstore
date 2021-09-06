using System.Collections.Generic;

namespace TransactionStore.Business.Services
{
    public class CurrencyRatesService
    {
        public string BaseCurrency { get; } = "USD";
        public Dictionary<string, decimal> CurrencyPair { get; set; }

        public CurrencyRatesService()
        {
            CurrencyPair = new() { { "RUBUSD", 0.014m }, { "EURUSD", 1.1862m }, { "JPYUSD", 0.0091m } };
        }
    }
}