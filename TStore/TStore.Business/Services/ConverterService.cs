﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionStore.Core.Enums;

namespace TransactionStore.Business.Services
{
    public class ConverterService
    {
        private int _currencyLength = 3;
        private CurrencyRatesService _currencyRatesService;
        public ConverterService(CurrencyRatesService currencyRatesService)
        {
            _currencyRatesService = currencyRatesService;
        }

        public decimal ConvertAmount(string senderCurrency, string recipientCurrency, decimal amount)
        {
            if (senderCurrency == recipientCurrency) return Decimal.Round(amount, 3);
            if (!IsValid(senderCurrency) || !IsValid(recipientCurrency)) throw new Exception("Currency is not valid");
            _currencyRatesService.CurrencyPair.TryGetValue(senderCurrency + _currencyRatesService.BaseCurrency, out decimal SenderCurrencyValue);
            _currencyRatesService.CurrencyPair.TryGetValue(recipientCurrency + _currencyRatesService.BaseCurrency, out decimal RecipientCurrencyValue);
            return Decimal.Round((SenderCurrencyValue / RecipientCurrencyValue * amount), 3);
        }

        private bool IsValid(string currency)
        {
            return _currencyRatesService.CurrencyPair.ContainsKey(currency + _currencyRatesService.BaseCurrency);
        }
    }
}
