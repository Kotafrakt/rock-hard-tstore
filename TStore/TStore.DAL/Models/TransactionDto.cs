using TransactionStore.Core.Enums;

namespace TransactionStore.DAL.Models
{
    public class TransactionDto : BaseTransactionDto
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
    }
}