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
        public static List<decimal> GetValidListOfRubsAmounts { get; } = new List<decimal> {
            _currencyRatesService.CurrencyPair.GetValueOrDefault("USDRUB") * 10,
            _currencyRatesService.CurrencyPair.GetValueOrDefault("EURRUB") * 10,
            _currencyRatesService.CurrencyPair.GetValueOrDefault("JPYRUB") * 10
            };
    }
}
