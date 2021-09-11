using Exchange;

namespace TransactionStore.Business.Services
{
    public interface ICurrencyRatesService
    {
        RatesExchangeModel RatesModel { get; set; }
        public RatesExchangeModel LoadCurrencyRates();
        void SaveCurrencyRates(RatesExchangeModel rates);
    }
}