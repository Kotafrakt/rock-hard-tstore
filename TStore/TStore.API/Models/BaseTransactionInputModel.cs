﻿using System;
using TransactionStore.Core.Enums;

namespace TransactionStore.API.Models
{
    public class BaseTransactionInputModel
    {
        public decimal Amount { get; set; }
    }
}