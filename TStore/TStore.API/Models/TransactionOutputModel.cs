using System;

namespace TransactionStore.API.Models
{
    public class TransactionOutputModel : BaseTransactionOutputModel
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
