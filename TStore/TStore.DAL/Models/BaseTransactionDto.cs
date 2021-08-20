using System;
using TransactionStore.Core.Enums;

namespace TransactionStore.DAL.Models
{
    public class BaseTransactionDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
    }
}