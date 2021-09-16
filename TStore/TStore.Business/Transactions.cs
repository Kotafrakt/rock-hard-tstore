using System.Collections.Generic;
using TransactionStore.DAL.Models;

namespace TransactionStore.Business
{
    public class Transactions
    {
        public List<TransactionDto> List { get; set; }
        public bool Status { get; set; }

        public Transactions(List<TransactionDto> transaction)
        {
            List = transaction;
        }

        private Transactions(List<TransactionDto> transaction, bool status)
        {
            List = transaction;
            Status = status;
        }

        public static bool CheckDictionaryByUserName(string userName)
        {
            return TransactionsExtensions.Dictionary.ContainsKey(userName);
        }

        public static Transactions GetPart(string userName)
        {
            var count = TransactionsExtensions.Dictionary[userName].Count >= TransactionsExtensions.MaxSize
                ? TransactionsExtensions.MaxSize : TransactionsExtensions.Dictionary[userName].Count;

            var tmpTransactions = TransactionsExtensions.Dictionary[userName].GetRange(0, count);
            TransactionsExtensions.Dictionary[userName].RemoveRange(0, count);

            var status = true;
            if (TransactionsExtensions.Dictionary[userName].Count == 0)
            {
                TransactionsExtensions.Dictionary.Remove(userName);
                status = false;
            }
            return new Transactions(tmpTransactions, status);
        }
    }
}