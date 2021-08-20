using System;

namespace TransactionStore.API.Models
{
    public class BaseTransactionOutputModel
    {
        public int Id { get; set; }
        public int TransactionType { get; set; }
        public DateTime Date { get; set; }
    }
}
