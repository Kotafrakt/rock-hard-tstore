using System;

namespace TStore.Business.Exceptions
{
    public class CurrencyRatesNotFoundException : Exception
    {
        public CurrencyRatesNotFoundException(string message) : base(message) { }
    }
}
