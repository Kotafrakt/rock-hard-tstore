using TransactionStore.Core.Enums;

namespace TransactionStore.DAL.Models
{
    public class TransferDto : BaseTransactionDto
    {
        public TransferDto()
        {
            Type = TransactionType.Transfer;
        }
        public int SenderAccountId { get; set; }
        public int RecipientAccountId { get; set; }
        public decimal SenderAmount { get; set; }
        public decimal RecipientAmount { get; set; }
        public Currency SenderCurrency { get; set; }
        public Currency RecipientCurrency { get; set; }
    }
}
