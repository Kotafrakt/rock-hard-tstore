using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using TransactionStore.Core.Enums;

namespace TransactionStore.API.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomRequiredAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is int)
                return (int)value > 0;
            if (value is decimal)
                return (decimal)value > 0m;
            if (value is string)
                return value.ToString().Length > 0;
            if (value is DateTime)
                return (DateTime)value > DateTime.MinValue;
            if (value is Currency)
                return (Currency)value >= Currency.RUB && (Currency)value <= Currency.JPY;
            return false;
        }
    }
}