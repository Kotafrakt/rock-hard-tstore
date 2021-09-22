using System;

namespace TStore.Business.Exceptions
{
    public class CurrencyNotValidException : Exception
    {
        public CurrencyNotValidException(string message) : base(message) { }
    }
}