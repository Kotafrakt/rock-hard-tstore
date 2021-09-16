using System.Collections.Generic;
using TransactionStore.DAL.Models;

namespace TransactionStore.Business
{
    public static class TransactionsExtensions
    {
        public static int MaxSize = 50000;
        public static Dictionary<string, List<TransactionDto>> Dictionary = new();

        public static bool CheckAllowedSize(this List<TransactionDto> transactions)
        {
            return transactions.Count < MaxSize;
        }

        public static void SetListToDictionary(this List<TransactionDto> transactions, string userName)
        {
            Dictionary.Add(userName, transactions);
        }
    }
}