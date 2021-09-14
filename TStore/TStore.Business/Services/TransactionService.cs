using System.Collections.Generic;
using TransactionStore.Core.Enums;
using TransactionStore.DAL.Models;
using TransactionStore.DAL.Repositories;
using System.Linq;
using Newtonsoft.Json;

namespace TransactionStore.Business.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IConverterService _converterService;

        public TransactionService(ITransactionRepository transactionRepository, IConverterService converterService)
        {
            _transactionRepository = transactionRepository;
            _converterService = converterService;
        }

        public long AddDeposit(TransactionDto dto)
        {
            dto.TransactionType = TransactionType.Deposit;
            long transactionId = _transactionRepository.AddDepositeOrWithdraw(dto);
            return transactionId;
        }

        public long AddWithdraw(TransactionDto dto)
        {
            dto.TransactionType = TransactionType.Withdraw;
            long transactionId = _transactionRepository.AddDepositeOrWithdraw(dto);
            return transactionId;
        }

        public List<long> AddTransfer(TransferDto dto)
        {
            dto.RecipientAmount = _converterService.ConvertAmount(dto.Currency.ToString(), dto.RecipientCurrency.ToString(), dto.Amount);
            var transactionIds = _transactionRepository.AddTransfer(dto);
            var result = new List<long>();
            result.Add(transactionIds.Item1);
            result.Add(transactionIds.Item2);
            return result;
        }

        public string GetTransactionsByAccountId(int accountId)
        {
            var transactions = _transactionRepository.GetTransactionsByAccountId(accountId);
            var result = transactions.FindAll(t => t.TransactionType != TransactionType.Transfer).ToList();
            result.AddRange(transactions.FindAll(t => t.TransactionType == TransactionType.Transfer)
                .GroupBy(t => t.Date).Select(grp => grp.ToList()).Select(group =>
                    new TransferDto
                    {
                        Id = group[0].AccountId == accountId ? group[0].Id : group[1].Id,
                        AccountId = group[0].Amount < 0 ? group[0].AccountId : group[1].AccountId,
                        RecipientAccountId = group[0].Amount > 0 ? group[0].AccountId : group[1].AccountId,
                        Amount = group[0].Amount < 0 ? group[0].Amount : group[1].Amount,
                        RecipientAmount = group[0].Amount > 0 ? group[0].Amount : group[1].Amount,
                        TransactionType = group[0].TransactionType,
                        Date = group[0].Date,
                        Currency = group[0].Amount < 0 ? group[0].Currency : group[1].Currency,
                        RecipientCurrency = group[0].Amount > 0 ? group[0].Currency : group[1].Currency
                    }));

            var output= result.OrderBy(t => t.Date).ToList();
            return JsonConvert.SerializeObject(output);
        }

        public string GetTransactionsByPeriod(GetByPeriodDto dto, string leadId)
        {
            Transactions transactions;
            if (!Transactions.CheckDictionaryByUserName(leadId))
            {
                transactions = new Transactions(GetTransactionsDto(dto));
                if (transactions.List.Count == 0)
                {
                    return string.Empty;
                }

                if (!transactions.CheckAllowedSize())
                {
                    transactions.SetListToDictionary(leadId);
                    transactions = Transactions.GetPart(leadId);
                }
            }
            else
            {
                transactions = Transactions.GetPart(leadId);
            }


            return JsonConvert.SerializeObject(transactions);
        }

        private List<TransactionDto> GetTransactionsDto(GetByPeriodDto dto)
        {
            var transactions = _transactionRepository.GetTransactionsByPeriod(dto.From, dto.To, dto.AccountId);
            var result = transactions.FindAll(t => t.TransactionType != TransactionType.Transfer).ToList();
            result.AddRange(transactions.FindAll(t => t.TransactionType == TransactionType.Transfer)
                .GroupBy(t => t.Date).Select(grp => grp.ToList()).Select(group =>
                    new TransferDto
                    {
                        Id = dto.AccountId == null ? group[0].Amount < 0 ? group[0].AccountId : group[1].AccountId : group[0].AccountId == dto.AccountId ? group[0].Id : group[1].Id,
                        AccountId = group[0].Amount < 0 ? group[0].AccountId : group[1].AccountId,
                        RecipientAccountId = group[0].Amount > 0 ? group[0].AccountId : group[1].AccountId,
                        Amount = group[0].Amount < 0 ? group[0].Amount : group[1].Amount,
                        RecipientAmount = group[0].Amount > 0 ? group[0].Amount : group[1].Amount,
                        TransactionType = group[0].TransactionType,
                        Date = group[0].Date,
                        Currency = group[0].Amount < 0 ? group[0].Currency : group[1].Currency,
                        RecipientCurrency = group[0].Amount > 0 ? group[0].Currency : group[1].Currency
                    }));
            return result.OrderBy(t => t.Date).ToList();
        }
    }
}