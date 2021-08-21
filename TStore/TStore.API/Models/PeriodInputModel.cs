using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionStore.API.Models
{
    public class PeriodInputModel
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int AccountId { get; set; }
    }
}
