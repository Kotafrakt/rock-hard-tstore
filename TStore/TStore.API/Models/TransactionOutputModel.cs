using System;

namespace TStore.API.Models
{
    public class TransactionOutputModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int TransactionType { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
