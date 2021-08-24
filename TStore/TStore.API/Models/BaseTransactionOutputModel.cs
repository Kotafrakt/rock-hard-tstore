using System;
using TransactionStore.Core.Enums;

namespace TransactionStore.API.Models
{
    public class BaseTransactionOutputModel
    {
        public int Id { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}