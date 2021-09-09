using Exchange;
using System.Collections.Generic;

namespace TransactionStore.Business.Services
{
    public interface ICurrencyRatesService
    {
        //string BaseCurrency { get; set; }
        //Dictionary<string, decimal> CurrencyPair { get; set; }
        //string Updated { get; set; }
        RatesExchangeModel RatesModel { get; set; }
        void LoadCurrencyRates();
        void SaveCurrencyRates(RatesExchangeModel rates);
    }
}