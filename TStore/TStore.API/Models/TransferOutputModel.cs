namespace TransactionStore.API.Models
{
    public class TransferOutputModel : TransactionOutputModel
    {
        public decimal RecipientAmount { get; set; }
        public int RecipientAccountId { get; set; }
        public string RecipientCurrency { get; set; }
    }
}