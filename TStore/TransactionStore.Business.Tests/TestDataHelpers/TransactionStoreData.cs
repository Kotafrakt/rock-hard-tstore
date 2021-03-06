using System;
using System.Collections.Generic;
using TransactionStore.Core.Enums;
using TransactionStore.DAL.Models;

namespace TransactionStore.Business.Tests
{
    public static class TransactionStoreData
    {
        private static DateTime date1 = DateTime.Parse("01.08.2021 12:15");
        private static DateTime date2 = DateTime.Parse("02.09.2021 13:15");
        private static DateTime date3 = DateTime.Parse("03.10.2021 14:15");
        private static DateTime date4 = DateTime.Parse("04.10.2021 15:15");
        private static DateTime date5 = DateTime.Parse("05.10.2021 16:15");

        private static decimal usd = 1m;
        private static decimal rub = 73.1519m;
        private static decimal eur = 0.84645m;
        private static decimal jpy = 109.885m;
        private static decimal recipientAmount = Decimal.Round(rub / usd * 100m, 3);

        public static TransactionDto GetDeposit()
        {
            return new TransactionDto { Id = 1, AccountId = 1, Amount = 100m, Currency = Currency.RUB, Date = date1, TransactionType = TransactionType.Deposit };
        }

        public static TransactionDto GetWithdraw()
        {
            return new TransactionDto { Id = 2, AccountId = 2, Amount = -100m, Currency = Currency.USD, Date = date2, TransactionType = TransactionType.Withdraw };
        }

        public static TransferDto GetTransfer()
        {
            return new TransferDto
            {
                Id = 3,
                AccountId = 3,
                Amount = -100m,
                Currency = Currency.USD,
                Date = date3,
                TransactionType = TransactionType.Transfer,
                RecipientAccountId = 4,
                RecipientAmount = recipientAmount,
                RecipientCurrency = Currency.RUB
            };
        }

        public static List<TransactionDto> GetListOfTransactions()
        {
            return new List<TransactionDto> {
                new TransactionDto { Id = 4, AccountId = 1, Amount = 76m, Currency = Currency.RUB, Date = date1, TransactionType = TransactionType.Deposit },
                new TransactionDto { Id = 5, AccountId = 1, Amount = 54m, Currency = Currency.USD, Date = date2, TransactionType = TransactionType.Withdraw },
                new TransactionDto { Id = 6, AccountId = 1, Amount = -100m, Currency = Currency.USD, Date = date3, TransactionType = TransactionType.Transfer},
                new TransactionDto { Id = 7, AccountId = 2, Amount = recipientAmount, Currency = Currency.RUB, Date = date3, TransactionType = TransactionType.Transfer},
                new TransactionDto { Id = 8, AccountId = 1, Amount = 72m, Currency = Currency.EUR, Date = date4, TransactionType = TransactionType.Withdraw },
                new TransactionDto { Id = 9, AccountId = 1, Amount = 88m, Currency = Currency.JPY, Date = date5, TransactionType = TransactionType.Deposit }
            };
        }

        public static List<TransactionDto> GetSameListOfTransactionsWithTransfersByAccountIdEqualOne()
        {
            return new List<TransactionDto> {
                new TransactionDto { Id = 4, AccountId = 1, Amount = 76m, Currency = Currency.RUB, Date = date1, TransactionType = TransactionType.Deposit },
                new TransactionDto { Id = 5, AccountId = 1, Amount = 54m, Currency = Currency.USD, Date = date2, TransactionType = TransactionType.Withdraw },
                new TransferDto { Id = 6, AccountId = 1, Amount = -100m, Currency = Currency.USD, Date = date3, TransactionType = TransactionType.Transfer,
                    RecipientAccountId = 2, RecipientAmount = recipientAmount, RecipientCurrency = Currency.RUB },
                new TransactionDto { Id = 8, AccountId = 1, Amount = 72m, Currency = Currency.EUR, Date = date4, TransactionType = TransactionType.Withdraw },
                new TransactionDto { Id = 9, AccountId = 1, Amount = 88m, Currency = Currency.JPY, Date = date5, TransactionType = TransactionType.Deposit }
            };
        }

        public static List<TransactionDto> GetSameListOfTransactionsWithTransfersByAccountIdIsNull()
        {
            return new List<TransactionDto> {
                new TransactionDto { Id = 4, AccountId = 1, Amount = 76m, Currency = Currency.RUB, Date = date1, TransactionType = TransactionType.Deposit },
                new TransactionDto { Id = 5, AccountId = 1, Amount = 54m, Currency = Currency.USD, Date = date2, TransactionType = TransactionType.Withdraw },
                new TransferDto { Id = 6, AccountId = 1, Amount = -100m, Currency = Currency.USD, Date = date3, TransactionType = TransactionType.Transfer,
                    RecipientAccountId = 2, RecipientAmount = recipientAmount, RecipientCurrency = Currency.RUB },
                new TransactionDto { Id = 8, AccountId = 1, Amount = 72m, Currency = Currency.EUR, Date = date4, TransactionType = TransactionType.Withdraw },
                new TransactionDto { Id = 9, AccountId = 1, Amount = 88m, Currency = Currency.JPY, Date = date5, TransactionType = TransactionType.Deposit }
            };
        }

        public static GetByPeriodDto GetByPeriodDtoWithAccountIdEqualNull()
        {
            return new GetByPeriodDto { AccountId = null, From = date1, To = date5 };
        }

        public static GetByPeriodDto GetByPeriodDtoWithAccountIdEqualOne()
        {
            return new GetByPeriodDto { AccountId = 1, From = date1, To = date5 };
        }
    }
}
