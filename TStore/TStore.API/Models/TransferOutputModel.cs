namespace TransactionStore.API.Models
{
    public class TransferOutputModel : BaseTransactionOutputModel
    {
        public int RecipientAccountId { get; set; }
        public string RecipientCurrency { get; set; }
    }
}