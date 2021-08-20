using System;
using TransactionStore.Core.Enums;

namespace TransactionStore.API.Models
{
    public class BaseTransactionInputModel
    {
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
    }
}