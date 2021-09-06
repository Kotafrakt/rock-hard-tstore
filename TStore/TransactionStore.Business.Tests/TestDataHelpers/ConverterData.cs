using System;
using System.Collections.Generic;
using TransactionStore.Business.Services;
using TransactionStore.Core.Enums;
using TransactionStore.DAL.Models;

namespace TransactionStore.Business.Tests
{
    public static class ConverterData
    {
        private static CurrencyRatesService _currencyRatesService = new CurrencyRatesService();

        private static decimal usd = 1m;
        private static decimal rub = _currencyRatesService.CurrencyPair.GetValueOrDefault("RUBUSD");
        private static decimal eur = _currencyRatesService.CurrencyPair.GetValueOrDefault("EURUSD");
        private static decimal jpy = _currencyRatesService.CurrencyPair.GetValueOrDefault("JPYUSD");

        public static List<decimal> GetValidListOfRecipientAmount(string currency, decimal amount)
        {
            if (currency == "USD")
            {
                //return new List<decimal> { Decimal.Round((currency / GetValidListOfCurrencies[0] * amount), 3) }
            }
            return default;
        }
    }
}
