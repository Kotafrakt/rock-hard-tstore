using System;
using TransactionStore.Core.Enums;

namespace TransactionStore.DAL.Models
{
    public class BaseTransactionDto
    {
        public long Id { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
    }
}