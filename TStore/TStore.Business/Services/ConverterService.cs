using Microsoft.Extensions.Options;
using System;

namespace TransactionStore.Business.Services
{
    public class ConverterService : IConverterService
    {
        private readonly ICurrencyRatesService _currencyRatesService;
        public ConverterService(ICurrencyRatesService currencyRatesService)
        {
            _currencyRatesService = currencyRatesService;
            if(_currencyRatesService.RatesModel == null)
                _currencyRatesService.LoadCurrencyRates();
        }

        public decimal ConvertAmount(string senderCurrency, string recipientCurrency, decimal amount)
        {
            decimal SenderCurrencyValue, RecipientCurrencyValue;
            if (senderCurrency == recipientCurrency) return Decimal.Round(amount, 3);
            if (!IsValid(senderCurrency) || !IsValid(recipientCurrency)) throw new Exception("Currency is not valid");
            _currencyRatesService.RatesModel.Rates.TryGetValue(senderCurrency + _currencyRatesService.RatesModel.BaseCurrency, out SenderCurrencyValue);
            _currencyRatesService.RatesModel.Rates.TryGetValue(recipientCurrency + _currencyRatesService.RatesModel.BaseCurrency, out RecipientCurrencyValue);
            if (senderCurrency == "USD")
                SenderCurrencyValue = 1m;
            if (recipientCurrency == "USD")
                RecipientCurrencyValue = 1m;
            return Decimal.Round((SenderCurrencyValue / RecipientCurrencyValue * amount), 3);
        }

        private bool IsValid(string currency)
        {
            if (currency == "USD")
                return true;
            return _currencyRatesService.RatesModel.Rates.ContainsKey(currency + _currencyRatesService.RatesModel.BaseCurrency);
        }
    }
}