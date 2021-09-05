using DevEdu.Business.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using TransactionStore.API.Common;
using TStore.Business.Exceptions;

namespace DevEdu.API.Configuration
{
    public class ValidationExceptionResponse
    {

        public int Code { get; set; }
        public string Message { get; set; }
        public List<ValidationError> Errors { get; set; }

        private const int ValidationCode = 1000;
        private const string MessageValidation = "Validation exception";

        public ValidationExceptionResponse(ValidationException exception)
        {
            Errors = new List<ValidationError>
            {
                new() {Code = ValidationCode, Field = exception.Field, Message = exception.Message}
            };
        }

        public ValidationExceptionResponse(ModelStateDictionary modelState)
        {
            Code = ValidationCode;
            Message = MessageValidation;
            Errors = new List<ValidationError>();
            foreach (var state in modelState)
            {
                if (state.Value.Errors.Count == 0)
                    continue;
                Errors.Add(new ValidationError
                {
                    Code = GetValidationCode(state.Value.Errors[0].ErrorMessage),
                    Field = state.Key,
                    Message = state.Value.Errors[0].ErrorMessage
                });
            }
        }

        private static int GetValidationCode(string exception)
        {
            return exception switch
            {
                ValidationMessage.WrongFormatDate => 1001,
                ValidationMessage.FromDateRequired => 1002,
                ValidationMessage.ToDateRequired => 1003,
                ValidationMessage.AmountRequired => 1004,
                ValidationMessage.AccountIdRequired => 1005,
                ValidationMessage.CurrencyRequired => 1006,
                ValidationMessage.RecipientAccountIdRequired => 1007,
                ValidationMessage.RecipientCurrencyRequired => 1008,
                _ => 1500
            };
        }
    }
}
