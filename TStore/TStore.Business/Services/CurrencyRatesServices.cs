using System.IO;
using Exchange;
using TransactionStore.Business.Helpers;
using TStore.Business.Exceptions;

namespace TransactionStore.Business.Services
{
    public class CurrencyRatesService : ICurrencyRatesService
    {
        private const string _fileName = "CurrencyRates.json";
        private const string _apDir = "./CurrencyRates/";
        private string _fullPath;

        public RatesExchangeModel RatesModel { get; set; }
        public CurrencyRatesService()
        {
            _fullPath = Path.Combine(_apDir, _fileName);
        }

        public RatesExchangeModel LoadCurrencyRates()
        {
            if(RatesModel == default)
            RatesModel =  CurrencyRatesHelper.ReadCurrencyRates(_fullPath);
            return RatesModel;
        }

        public void SaveCurrencyRates(RatesExchangeModel rates)
        {
            RatesModel = rates;
            CurrencyRatesHelper.WriteCurrencyRates(rates, _apDir, _fullPath);
        }
    }
}