using System;

namespace TransactionStore.DAL.Models
{
    public class TransactionDto : BaseTransactionDto
    {
        public int AccountId { get; set; }
    }
}
