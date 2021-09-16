using Exchange;
using System;
using TStore.Business.Exceptions;

namespace TransactionStore.Business.Services
{
    public class ConverterService : IConverterService
    {
        public string BaseCurrency { get; private set; }
        public RatesExchangeModel RatesModel { get; private set; }
        private ICurrencyRatesService _currencyRatesService;
        public ConverterService(ICurrencyRatesService currencyRatesService)
        {
            _currencyRatesService = currencyRatesService;
        }

        public decimal ConvertAmount(string senderCurrency, string recipientCurrency, decimal amount)
        {
            RatesModel = _currencyRatesService.LoadCurrencyRates();
            if(RatesModel != default)
            BaseCurrency = RatesModel.BaseCurrency;
            if (RatesModel == default) throw new CurrencyRatesNotFoundException("There are no current Currency Rates");
            if (!IsValid(senderCurrency)) throw new CurrencyNotValidException($"Sender currency is not valid");
            if (!IsValid(recipientCurrency)) throw new CurrencyNotValidException($"Recipient currency is not valid");
            RatesModel.Rates.TryGetValue($"{BaseCurrency}{senderCurrency}", out var senderCurrencyValue);
            RatesModel.Rates.TryGetValue($"{BaseCurrency}{recipientCurrency}", out var recipientCurrencyValue);
            if (senderCurrency == BaseCurrency)
                senderCurrencyValue = 1m;
            if (recipientCurrency == BaseCurrency)
                recipientCurrencyValue = 1m;
            return Decimal.Round((recipientCurrencyValue / senderCurrencyValue * amount), 3);
        }

        private bool IsValid(string currency)
        {
            if (currency == BaseCurrency)
                return true;
            return RatesModel.Rates.ContainsKey($"{BaseCurrency}{currency}");
        }
    }
}