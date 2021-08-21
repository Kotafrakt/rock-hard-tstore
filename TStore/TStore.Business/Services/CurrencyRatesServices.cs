using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionStore.Business.Services
{
    public class CurrencyRatesService
    {
        public string BaseCurrency { get; } = "RUB";
        public Dictionary<string, decimal> CurrencyPair { get; set; }

        public CurrencyRatesService()
        {
            CurrencyPair = new();

            CurrencyPair.Add("USDRUB", 73.965m);
            CurrencyPair.Add("EURRUB", 86.594m);
            CurrencyPair.Add("JPYRUB", 0.68m);
        }
    }
}