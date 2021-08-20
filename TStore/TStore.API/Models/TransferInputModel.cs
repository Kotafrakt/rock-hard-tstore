using System;
using TransactionStore.Core.Enums;

namespace TransactionStore.API.Models
{
    public class TransferInputModel : BaseTransactionInputModel
    {
        public int SenderAccountId { get; set; }
        public int RecipientAccountId { get; set; }
        public string SenderCurrency { get; set; }
        public string RecipientCurrency { get; set; }
    }
}
