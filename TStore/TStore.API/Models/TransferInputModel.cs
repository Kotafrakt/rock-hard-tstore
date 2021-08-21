using System;
using TransactionStore.Core.Enums;

namespace TransactionStore.API.Models
{
    public class TransferInputModel : TransactionInputModel
    {
        public int RecipientAccountId { get; set; }
        public string RecipientCurrency { get; set; }
    }
}
