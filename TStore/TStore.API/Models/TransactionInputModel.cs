using System;
using System.ComponentModel.DataAnnotations;
using TransactionStore.API.Common;
using TransactionStore.Core.Enums;
using static TransactionStore.API.Common.ValidationMessage;

namespace TransactionStore.API.Models
{
    public class TransactionInputModel
    {
        [CustomRequired(ErrorMessage = AmountRequired)]
        public decimal Amount { get; set; }
        [CustomRequired(ErrorMessage = AccountIdRequired)]
        public int AccountId { get; set; }
        [Required(ErrorMessage = CurrencyRequired)]
        public Currency Currency { get; set; }
        public DateTime Date { get; set; }
    }
}