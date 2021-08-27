using System.Collections.Generic;

namespace TransactionStore.Business.Services
{
    public class CurrencyRatesService
    {
        public string BaseCurrency { get; } = "RUB";
        public Dictionary<string, decimal> CurrencyPair { get; set; }

        public CurrencyRatesService()
        {
            CurrencyPair = new() { { "USDRUB", 73.965m }, { "EURRUB", 86.594m }, { "JPYRUB", 0.68m } };
        }
    }
}