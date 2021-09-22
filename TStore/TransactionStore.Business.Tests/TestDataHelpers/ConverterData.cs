using System;
using System.Collections.Generic;

namespace TransactionStore.Business.Tests
{
    public static class ConverterData
    {
        private static decimal usd = 1m;
        private static decimal rub = 73.1519m;
        private static decimal eur = 0.84645m;
        private static decimal jpy = 109.885m;

        public static List<decimal> GetValidListOfRecipientAmount(string currency, decimal amount)
        {
            if (currency == "USD")
            {
                return new List<decimal>
                {
                    Decimal.Round((rub / usd * amount), 3) ,
                    Decimal.Round((eur / usd * amount), 3) ,
                    Decimal.Round((jpy / usd * amount), 3)
                };
            }
            if (currency == "RUB")
            {
                return new List<decimal>
                {
                    Decimal.Round((usd / rub * amount), 3) ,
                    Decimal.Round((eur / rub * amount), 3) ,
                    Decimal.Round((jpy / rub * amount), 3)
                };
            }
            if (currency == "EUR")
            {
                return new List<decimal>
                {
                    Decimal.Round((usd / eur * amount), 3) ,
                    Decimal.Round((rub / eur * amount), 3) ,
                    Decimal.Round((jpy / eur * amount), 3)
                };
            }
            if (currency == "JPY")
            {
                return new List<decimal>
                {
                    Decimal.Round((usd / jpy * amount), 3) ,
                    Decimal.Round((rub / jpy * amount), 3) ,
                    Decimal.Round((eur / jpy * amount), 3)
                };
            }
            return default;
        }
    }
}
