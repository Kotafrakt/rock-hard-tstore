using System.Collections.Generic;
using TransactionStore.DAL.Models;

namespace TransactionStore.Business
{
    public class Transactions
    {
        public static bool CheckDictionaryByUserName(string userName)
        {
            return TransactionsExtensions.Dictionary.ContainsKey(userName);
        }

        public static void GetPart(string userName, out List<TransactionDto> transactions)
        {
            transactions = new List<TransactionDto>();
            if (TransactionsExtensions.Dictionary[userName].Count == 0)
            {
                TransactionsExtensions.Dictionary.Remove(userName);
                return;
            }

            var count = TransactionsExtensions.Dictionary[userName].Count >= TransactionsExtensions.MaxSize
                ? TransactionsExtensions.MaxSize : TransactionsExtensions.Dictionary[userName].Count;

            transactions = TransactionsExtensions.Dictionary[userName].GetRange(0, count);
            TransactionsExtensions.Dictionary[userName].RemoveRange(0, count);
        }
    }
}