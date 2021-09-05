using System.ComponentModel.DataAnnotations;
using TransactionStore.Core.Enums;
using static TransactionStore.API.Common.ValidationMessage;

namespace TransactionStore.API.Models
{
    public class TransferInputModel : TransactionInputModel
    {
        [Required(ErrorMessage = RecipientAccountIdRequired)]
        public int RecipientAccountId { get; set; }
        [Required(ErrorMessage = RecipientCurrencyRequired)]
        public Currency RecipientCurrency { get; set; }
    }
}