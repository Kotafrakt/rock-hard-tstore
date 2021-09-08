using System.Collections.Generic;

namespace TransactionStore.Business.Services
{
    public interface ICurrencyRatesService
    {
        string BaseCurrency { get; set; }
        Dictionary<string, decimal> CurrencyPair { get; set; }
    }
}