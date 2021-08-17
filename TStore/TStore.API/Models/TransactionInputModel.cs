﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TStore.API.Models
{
    public class TransactionInputModel
    {
        public int AccountId { get; set; }
        public int TransactionType { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}