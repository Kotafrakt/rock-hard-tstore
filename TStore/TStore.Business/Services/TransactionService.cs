using System.Collections.Generic;
using TransactionStore.Core.Enums;
using TransactionStore.DAL.Models;
using TransactionStore.DAL.Repositories;

namespace TransactionStore.Business.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public TransactionDto AddDeposit(TransactionDto dto)
        {
            int transactionId = _transactionRepository.AddDepositeOrWithdraw(dto);
            dto.Type = (TransactionType)1;
            dto.Id = transactionId;
            return dto;
        }

        public TransactionDto AddWithdraw(TransactionDto dto)
        {
            int transactionId = _transactionRepository.AddDepositeOrWithdraw(dto);
            dto.Type = (TransactionType)2;
            dto.Id = transactionId;
            return dto;
        }

        public TransferDto AddTransfer(TransferDto dto)
        {
            // тут надо просчитать recipient amount
            dto.RecipientAmount = dto.SenderAmount;
            (int, int) transactionIds = _transactionRepository.AddTransfer(dto);
            dto.SenderAccountId = transactionIds.Item1;
            dto.RecipientAccountId = transactionIds.Item2;
            return dto;
        }

        public List<TransactionDto> GetAllTransactions()
        {
            var transactionListDtos = _transactionRepository.GetAllTransactions();
            return transactionListDtos;
        }
        
        public List<TransactionDto> GetTransactionsByAccountId(int accountId)
        {
            var transactionListDtos = _transactionRepository.GetTransactionsByAccountId(accountId);
            return transactionListDtos;
        }
    }
}
