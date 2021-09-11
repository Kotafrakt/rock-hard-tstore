using System.Collections.Generic;
using TransactionStore.DAL.Models;

namespace TransactionStore.Business
{
    public static class TransactionsExtensions
    {
        public static int MaxSize = 5000;
        public static Dictionary<string, List<TransactionDto>> Dictionary = new();
        
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