using System;
using TransactionStore.Core.Enums;

namespace TransactionStore.DAL.Models
{
    public class TransactionDto
    {
        public long Id { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime Date { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
    }
}