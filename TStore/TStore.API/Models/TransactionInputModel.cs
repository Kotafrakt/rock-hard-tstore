using System;
using TransactionStore.Core.Enums;

namespace TransactionStore.API.Models
{
    public class TransactionInputModel : BaseTransactionInputModel
    {
        public int AccountId { get; set; }
        public string Currency { get; set; }
    }
}
