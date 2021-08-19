using System;

namespace TransactionStore.DAL.Models
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int TransactionType { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
