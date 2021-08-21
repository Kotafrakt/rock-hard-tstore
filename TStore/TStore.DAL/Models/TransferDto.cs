using TransactionStore.Core.Enums;

namespace TransactionStore.DAL.Models
{
    public class TransferDto : TransactionDto
    {
        public TransferDto()
        {
            TransactionType = TransactionType.Transfer;
        }
        public int RecipientAccountId { get; set; }
        public decimal RecipientAmount { get; set; }
        public Currency RecipientCurrency { get; set; }
    }
}
