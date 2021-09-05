using System.ComponentModel.DataAnnotations;
using TransactionStore.API.Common;
using TransactionStore.Core.Enums;
using static TransactionStore.API.Common.ValidationMessage;

namespace TransactionStore.API.Models
{
    public class TransferInputModel : TransactionInputModel
    {
        [CustomRequired(ErrorMessage = RecipientAccountIdRequired)]
        public int RecipientAccountId { get; set; }
        [CustomRequired(ErrorMessage = RecipientCurrencyRequired)]
        public Currency RecipientCurrency { get; set; }
    }
}