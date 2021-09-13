using System.IO;
using Exchange;
using TransactionStore.Business.Helpers;
using TStore.Business.Exceptions;

namespace TransactionStore.Business.Services
{
    public class CurrencyRatesService : ICurrencyRatesService
    {
        private const string _fileName = "CurrencyRates.json";
        private string _apDir = "./";
        private string _fullPath;

        public RatesExchangeModel RatesModel { get; set; }
        public CurrencyRatesService()
        {
            _fullPath = Path.Combine(_apDir, _fileName);
            RatesModel = LoadCurrencyRates();
        }

        public RatesExchangeModel LoadCurrencyRates()
        {
            return CurrencyRatesHelper.ReadCurrencyRates(_fullPath);
        }

        public void SaveCurrencyRates(RatesExchangeModel rates)
        {
            RatesModel = rates;
            CurrencyRatesHelper.WriteCurrencyRates(rates, _apDir, _fullPath);
        }
    }
}