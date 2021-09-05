using TransactionStore.API.Common;
using static TransactionStore.API.Common.ValidationMessage;

namespace TransactionStore.API.Models
{
    public class GetByPeriodInputModel
    {
        [CustomRequired(ErrorMessage = FromDateRequired)]
        [CustomDateFormat(ErrorMessage = WrongFormatDate)]
        public string From { get; set; }
        [CustomRequired(ErrorMessage = DateRequired)]
        [CustomDateFormat(ErrorMessage = WrongFormatDate)]
        public string To { get; set; }
        public int? AccountId { get; set; }
    }
}