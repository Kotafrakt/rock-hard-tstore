using System.ComponentModel.DataAnnotations;
using TransactionStore.Core.Enums;
using static TransactionStore.API.Common.ValidationMessage;

namespace TransactionStore.API.Models
{
    public class TransactionInputModel
    {
        [Required(ErrorMessage = AmountRequired)]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = AccountIdRequired)]
        public int AccountId { get; set; }
        [Required(ErrorMessage = CurrencyRequired)]
        public Currency Currency { get; set; }
    }
}