using System;

namespace TransactionStore.DAL.Models
{
    public class GetByPeriodDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int? AccountId { get; set; }
    }
}
