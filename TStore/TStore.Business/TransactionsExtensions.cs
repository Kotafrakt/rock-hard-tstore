using System.Collections.Generic;
using TransactionStore.DAL.Models;

namespace TransactionStore.Business
{
    public static class TransactionsExtensions
    {
        public static int MaxSize { get; set; } = 5000;
        public static Dictionary<string, List<TransactionDto>> Dictionary { get; set; } = new();
        
        public static bool CheckAllowedSize(this Transactions transactions)
        {
            return transactions.List.Count < MaxSize;
        }
        
        public static void SetListToDictionary(this Transactions transactions, string userName)
        {
            Dictionary.Add(userName, transactions.List);
        }
    }
}