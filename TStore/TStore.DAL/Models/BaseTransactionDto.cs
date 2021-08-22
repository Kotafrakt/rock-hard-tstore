using System;
using TransactionStore.Core.Enums;

namespace TransactionStore.DAL.Models
{
    public class BaseTransactionDto
    {
        public long Id { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime Date { get; set; }
    }
}