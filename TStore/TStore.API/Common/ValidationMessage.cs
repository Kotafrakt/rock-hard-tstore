namespace TransactionStore.API.Common
{
    public static class ValidationMessage
    {
        public const string WrongFormatDate = "the date must be in the format dd.mm.yyyy";
        public const string FromDateRequired = "From Date must be provided";
        public const string ToDateRequired = "To Date must be provided";
    }
}