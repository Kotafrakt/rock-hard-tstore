using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(typeof(TransactionOutputModel), StatusCodes.Status201Created)]
        public ActionResult<TransactionOutputModel> AddTransaction([FromBody] TransactionInputModel inputModel)
        {
            var dto = _mapper.Map<TransactionDto>(inputModel);
            var returnedDto = _transactionService.AddTransaction(dto);
            var output = _mapper.Map<TransactionOutputModel>(returnedDto);

            return StatusCode(201, output);
        }

        // api/transaction/get-all-transactions
        [HttpGet("transaction/get-all-transactions")]
        [Description("Get all transactions")]
        [ProducesResponseType(typeof(List<TransactionOutputModel>), StatusCodes.Status200OK)]
        public List<TransactionOutputModel> GetAllTransactions()
        {
            var resultDto = _transactionService.GetAllTransactions();
            var listOutputs = _mapper.Map<List<TransactionOutputModel>>(resultDto);

            return listOutputs;
        }

        // api/transaction/by-accountId/{accountId}
        [HttpPost("transaction/by-accountId/{accountId}")]
        [Description("Get transactions by account")]
        public List<TransactionOutputModel> GetTransactionsByAccountId(int accountId)
        {
            var resultDto = _transactionService.GetTransactionsByAccountId(accountId);
            var listOutputs = _mapper.Map<List<TransactionOutputModel>>(resultDto);

            return listOutputs;
        }

    }
}
