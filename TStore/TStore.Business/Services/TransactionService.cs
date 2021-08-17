using System.Collections.Generic;
using TStore.DAL.Models;
using TStore.DAL.Repositories;

namespace TStore.Business.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public TransactionDto AddTransaction(TransactionDto dto)
        {
            int transactionId = _transactionRepository.AddTransaction(dto);
            dto.Id = transactionId;

            return dto;
        }

        public List<TransactionDto> GetAllTransaction()
        {
            var transactionListDtos = _transactionRepository.GetAllTransaction();
            return transactionListDtos;
        }
    }
}
