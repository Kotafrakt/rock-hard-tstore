using System;
using System.Collections.Generic;
using TransactionStore.Core.Enums;
using TransactionStore.DAL.Models;
using TransactionStore.DAL.Repositories;

namespace TransactionStore.Business.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private ConverterService _converterService;

        public TransactionService(ITransactionRepository transactionRepository, ConverterService converterService)
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

        public string AddTransfer(TransferDto dto)
        {
            // тут надо просчитать recipient amount
            dto.RecipientAmount = _converterService.ConvertAmount(dto.Currency.ToString(), dto.RecipientCurrency.ToString(), dto.Amount);
            var transactionIds = _transactionRepository.AddTransfer(dto);
            string result = $"{transactionIds.Item1}, {transactionIds.Item2}";
            return result;
        }

        public List<TransactionDto> GetTransactionsByAccountId(int accountId)
        {
            var depositesOrWithdraws = _transactionRepository.GetDepositOrWithdrawByAccountId(accountId);
            var transfers = _transactionRepository.GetTransfersByAccountId(accountId);
            var transactions = new List<TransactionDto>();
            transactions.AddRange(depositesOrWithdraws);
            transactions.AddRange(transfers);

            return transactions;
        }

        public List<TransactionDto> GetTransactionsByPeriod(DateTime from, DateTime to, int accountId)
        {
            var transactionListDtos = _transactionRepository.GetTransactionsByPeriod(from, to, accountId);
            return transactionListDtos;
        }

    }
}
