using System;
using TransactionStore.API.Common;
using TransactionStore.Core.Enums;
using static TransactionStore.API.Common.ValidationMessage;

namespace TransactionStore.API.Models
{
    public class BaseTransactionInputModel
    {
        public TransactionType Type { get; set; }
    }
}
