using TransactionStore.Core.Enums;

namespace TransactionStore.API.Models
{
    public class TransactionInputModel
    {
        public decimal Amount { get; set; }
        public int AccountId { get; set; }
        public Currency Currency { get; set; }
    }
}