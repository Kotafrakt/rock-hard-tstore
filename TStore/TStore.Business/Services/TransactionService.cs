using Newtonsoft.Json;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionStore.Core.Enums;
using TransactionStore.DAL.Models;
using TransactionStore.DAL.Repositories;

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

        public async Task<long> AddDepositAsync(TransactionDto dto)
        {
            long transactionId = await _transactionRepository.AddDepositAsync(dto);
            Log.Information("Add {0} {1} {2} to account with Id {3}", dto.TransactionType, dto.Amount, dto.Currency, dto.AccountId);
            return transactionId;
        }

        public async Task<long> AddWithdrawAsync(TransactionDto dto)
        {
            long transactionId = await _transactionRepository.AddWithdrawAsync(dto);
            Log.Information("Add {0} {1} {2} to account with Id {3}", dto.TransactionType, dto.Amount, dto.Currency, dto.AccountId);
            return transactionId;
        }

        public async Task<List<long>> AddTransferAsync(TransferDto dto)
        {
            dto.RecipientAmount = _converterService.ConvertAmount(dto.Currency.ToString(), dto.RecipientCurrency.ToString(), dto.Amount);
            var transactionIds = await _transactionRepository.AddTransferAsync(dto);
            var result = new List<long>();
            result.Add(transactionIds.Item1);
            result.Add(transactionIds.Item2);
            Log.Information("Add {0} {1} {2} from account with Id {3} to account with Id{4} {5} {6}",
                dto.TransactionType, dto.Amount, dto.Currency, dto.AccountId, dto.RecipientAccountId, dto.RecipientAmount, dto.RecipientCurrency);
            return result;
        }

        public async Task<string> GetTransactionsByAccountIdAsync(int accountId)
        {
            var transactions = await _transactionRepository.GetTransactionsByAccountIdAsync(accountId);
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

            var output = result.OrderBy(t => t.Date).ToList();
            Log.Information("Get all transaction by Id = {0}", accountId);
            return JsonConvert.SerializeObject(output);
        }

        public async Task<string> GetTransactionsByPeriodAsync(GetByPeriodDto dto, string leadId)
        {
            List<TransactionDto> transactions;
            if (!Transactions.CheckDictionaryByUserName(leadId))
            {
                transactions = await GetTransactionsDtoAsync(dto);

                transactions.SetListToDictionary(leadId);

                Transactions.GetPart(leadId, out transactions);
            }
            else
            {
                Transactions.GetPart(leadId, out transactions);
            }

            if (dto.AccountId == null)
                Log.Information("Get all transaction by period from {0} to {1}", dto.From, dto.To);
            else
                Log.Information("Get all transaction by period from {0} to {1} by Id = {2}", dto.From, dto.To, dto.AccountId);

            return JsonConvert.SerializeObject(transactions);
        }

        private async Task<List<TransactionDto>> GetTransactionsDtoAsync(GetByPeriodDto dto)
        {
            var transactions = await _transactionRepository.GetTransactionsByPeriodAsync(dto.From, dto.To, dto.AccountId);
            var result = transactions.FindAll(t => t.TransactionType != TransactionType.Transfer).ToList();
            result.AddRange(transactions.FindAll(t => t.TransactionType == TransactionType.Transfer)
                .GroupBy(t => t.Date).Select(grp => grp.ToList()).Select(group =>
                    new TransferDto
                    {
                        Id = dto.AccountId == null ? group[0].Amount < 0 ? group[0].Id : group[1].Id : group[0].AccountId == dto.AccountId ? group[0].Id : group[1].Id,
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