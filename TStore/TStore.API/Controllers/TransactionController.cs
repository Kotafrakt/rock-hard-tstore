using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using TransactionStore.API.Models;
using TransactionStore.Business.Services;
using TransactionStore.DAL.Models;

namespace TransactionStore.API.Controllers
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

        // api/transaction/deposit
        [HttpPost("deposit")]
        [Description("Add deposit")]
        [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
        public ActionResult<long> AddDeposit([FromBody] TransactionInputModel inputModel)
        {
            var dto = _mapper.Map<TransactionDto>(inputModel);
            var output = _transactionService.AddDeposit(dto);

            return StatusCode(201, output);
            //created
        }

        // api/transaction/withdraw
        [HttpPost("withdraw")]
        [Description("Add withdraw")]
        [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
        public ActionResult<long> AddWithdraw([FromBody] TransactionInputModel inputModel)
        {
            var dto = _mapper.Map<TransactionDto>(inputModel);
            var output = _transactionService.AddWithdraw(dto);

            return StatusCode(201, output);
        }

        // api/transaction/transfer
        [HttpPost("transfer")]
        [Description("Add transfer")]
        [ProducesResponseType(typeof((long, long)), StatusCodes.Status201Created)]
        public ActionResult<(long, long)> AddTransfer([FromBody] TransferInputModel inputModel)
        {
            var dto = _mapper.Map<TransferDto>(inputModel);
            var output = _transactionService.AddTransfer(dto);

            return StatusCode(201, output);
        }

        // api/transaction
        [HttpGet]
        [Description("Get all transactions")]
        [ProducesResponseType(typeof(List<TransactionOutputModel>), StatusCodes.Status200OK)]
        public List<TransactionOutputModel> GetAllTransactions()
        {
            var resultDto = _transactionService.GetAllTransactions();
            var listOutputs = _mapper.Map<List<TransactionOutputModel>>(resultDto);
            return listOutputs;
        }

        // api/transaction/by-account/{accountId}
        [HttpGet("by-account/{accountId}")]
        [Description("Get transactions by account")]
        [ProducesResponseType(typeof(List<TransactionOutputModel>), StatusCodes.Status200OK)]
        public List<TransactionOutputModel> GetTransactionsByAccountId(int accountId)
        {
            var resultDto = _transactionService.GetTransactionsByAccountId(accountId);
            var listOutputs = _mapper.Map<List<TransactionOutputModel>>(resultDto);
            return listOutputs;
        }

    }
}
