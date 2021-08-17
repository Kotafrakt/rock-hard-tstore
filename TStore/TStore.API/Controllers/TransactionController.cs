using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using TStore.API.Models;
using TStore.Business.Services;
using TStore.DAL.Models;

namespace TStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        // api/transaction
        [HttpPost("transaction")]
        [Description("Add transaction")]
        public TransactionOutputModel AddTransaction([FromBody] TransactionInputModel inputModel)
        {
            var dto = _mapper.Map<TransactionDto>(inputModel);
            var returnedDto = _transactionService.AddTransaction(dto);
            var output = _mapper.Map<TransactionOutputModel>(returnedDto);

            return output;
        }

        // api/transaction/get-all-transactions
        [HttpPost("transaction/get-all-transactions")]
        [Description("Get all transactions")]
        public List<TransactionOutputModel> GetAllTransactions()
        {
            var resultDto = _transactionService.GetAllTransaction();
            var listOutputs = _mapper.Map<List<TransactionOutputModel>>(resultDto);

            return listOutputs;
        }

    }
}
