using System;
using System.ComponentModel.DataAnnotations;
using TransactionStore.API.Common;
using static TransactionStore.API.Common.ValidationMessage;

namespace TransactionStore.API.Models
{
    public class GetByPeriodInputModel
    {
        [Required(ErrorMessage = FromDateRequired)]
        [CustomDateFormatAttribute(ErrorMessage = WrongFormatDate)]
        public DateTime From { get; set; }
        [Required(ErrorMessage = ToDateRequired)]
        [CustomDateFormatAttribute(ErrorMessage = WrongFormatDate)]
        public DateTime To { get; set; }
        public int? AccountId { get; set; }
    }
}