namespace TransactionStore.API.Common
{
    public static class ValidationMessage
    {
        public const string WrongFormatDate = "The date must be in the format dd.mm.yyyy";
        public const string FromDateRequired = "From Date must be provided";
        public const string ToDateRequired = "To Date must be provided";
        public const string AmountRequired = "To Amount must be provided";
        public const string AccountIdRequired = "To AccountId must be provided";
        public const string CurrencyRequired = "To Currency must be provided";
        public const string RecipientAccountIdRequired = "To RecipientAccountId must be provided";
        public const string RecipientCurrencyRequired = "To RecipientCurrency must be provided";

    }
}