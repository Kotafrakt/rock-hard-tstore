using System.Collections.Generic;
using TransactionStore.DAL.Models;

namespace TransactionStore.Business
{
    public class Transactions
    {
        private const string Finish = "Finish";
        private const string Part = "Part";
        public List<TransactionDto> List { get; set; }
        private string Status { get; set; }

        public Transactions(List<TransactionDto> transaction)
        {
            List = transaction;
        }

        private Transactions(List<TransactionDto> transaction, string status)
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
            int count = TransactionsExtensions.Dictionary[userName].Count >= TransactionsExtensions.MaxSize ? TransactionsExtensions.MaxSize : TransactionsExtensions.Dictionary[userName].Count;
            List<TransactionDto> tmpTransactions = TransactionsExtensions.Dictionary[userName].GetRange(0, count);
            for (var i = 0; i < count; i++)
            {
                TransactionsExtensions.Dictionary[userName].RemoveAt(i);
            }
            string status = count == TransactionsExtensions.MaxSize ? Finish : Part;
            return new Transactions(tmpTransactions, status);
        }
    }
}