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

        public long AddDeposit(TransactionDto dto)
        {
            dto.Type = TransactionType.Deposit;
            long transactionId = _transactionRepository.AddDepositeOrWithdraw(dto);
            return transactionId;
        }

        public long AddWithdraw(TransactionDto dto)
        {
            dto.Type = TransactionType.Withdraw;
            long transactionId = _transactionRepository.AddDepositeOrWithdraw(dto);
            return transactionId;
        }

        public (long, long) AddTransfer(TransferDto dto)
        {
            // тут надо просчитать recipient amount
            dto.RecipientAmount = dto.SenderAmount;
            (long, long) transactionIds = _transactionRepository.AddTransfer(dto);
            return transactionIds;
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
