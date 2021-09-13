using Exchange;
using System;
using TStore.Business.Exceptions;

namespace TransactionStore.Business.Services
{
    public class ConverterService : IConverterService
    {
        private readonly string _baseCurrency;
        private readonly RatesExchangeModel _ratesModel;
        public ConverterService(ICurrencyRatesService currencyRatesService)
        {
            _ratesModel = currencyRatesService.LoadCurrencyRates();
            _baseCurrency = _ratesModel.BaseCurrency;
        }

        public decimal ConvertAmount(string senderCurrency, string recipientCurrency, decimal amount)
        {
            if (_ratesModel.Rates?.Count == 0) throw new CurrencyRatesNotFoundException("There are no current Currency Rates");
            if (!IsValid(senderCurrency))  throw new CurrencyNotValidException($"Sender currency is not valid");
            if (!IsValid(recipientCurrency)) throw new CurrencyNotValidException($"Recipient currency is not valid");
            _ratesModel.Rates.TryGetValue($"{_baseCurrency}{senderCurrency}", out var senderCurrencyValue);
            _ratesModel.Rates.TryGetValue($"{_baseCurrency}{recipientCurrency}", out var recipientCurrencyValue);
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
            return _ratesModel.Rates.ContainsKey($"{_baseCurrency}{currency}");
        }
    }
}