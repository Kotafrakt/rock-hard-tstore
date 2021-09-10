using Microsoft.Extensions.Options;
using System;

namespace TransactionStore.Business.Services
{
    public class ConverterService : IConverterService
    {
        private readonly ICurrencyRatesService _currencyRatesService;
        private readonly string _baseCurrency;
        public ConverterService(ICurrencyRatesService currencyRatesService)
        {
            _currencyRatesService = currencyRatesService;
            _baseCurrency = _currencyRatesService.RatesModel.BaseCurrency;
        }

        public decimal ConvertAmount(string senderCurrency, string recipientCurrency, decimal amount)
        {
            if (!IsValid(senderCurrency) || !IsValid(recipientCurrency)) throw new Exception("Currency is not valid");
            _currencyRatesService.RatesModel.Rates.TryGetValue(senderCurrency + _baseCurrency, out var senderCurrencyValue);
            _currencyRatesService.RatesModel.Rates.TryGetValue(recipientCurrency + _baseCurrency, out var recipientCurrencyValue);
            if (senderCurrency == _baseCurrency)
                senderCurrencyValue = 1m;
            if (recipientCurrency == _baseCurrency)
                recipientCurrencyValue = 1m;
            return Decimal.Round((senderCurrencyValue / recipientCurrencyValue * amount), 3);
        }

        private bool IsValid(string currency)
        {
            if (currency == _baseCurrency)
                return true;
            return _currencyRatesService.RatesModel.Rates.ContainsKey(currency + _currencyRatesService.RatesModel.BaseCurrency);
        }
    }
}