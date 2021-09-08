using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using RatesApi.Models;

namespace TransactionStore.Business.Services
{
    public class CurrencyRatesService : ICurrencyRatesService
    {
        public string BaseCurrency { get; set; } = "USD";
        public Dictionary<string, decimal> CurrencyPair { get; set; }

        public CurrencyRatesService()
        {
        }
    }
}