using System;

namespace TransactionStore.API.Models
{
    public class TransferOutputModel : BaseTransactionOutputModel
    {
        public int SenderAccountId { get; set; }
        public string SenderCurrency { get; set; }
        public int RecipientAccountId { get; set; }
        public string RecipientCurrency { get; set; }
    }
}
