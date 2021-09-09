namespace TransactionStore.API.Common
{
    public static class ValidationMessage
    {
        public const string WrongFormatDate = "The date must be in the format dd.mm.yyyy HH:mm";
        public const string FromDateRequired = "From Date must be provided";
        public const string DateRequired = "Date must be provided";
        public const string AmountRequired = "Amount must be provided and be greater 0";
        public const string AccountIdRequired = "AccountId must be provided and be greater 0";
        public const string CurrencyRequired = "Currency must be provided";
        public const string RecipientAccountIdRequired = "RecipientAccountId must be provided";
        public const string RecipientCurrencyRequired = "RecipientCurrency must be provided";

    }
}