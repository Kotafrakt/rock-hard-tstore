using AutoMapper;
using Exchange;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TransactionStore.API.Models;
using TransactionStore.Business;
using TransactionStore.Business.Services;
using TransactionStore.DAL.Models;

namespace TransactionStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ICurrencyRatesService _currencyRatesService;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionService transactionService, ICurrencyRatesService currencyRatesService, IMapper mapper)
        {
            _transactionService = transactionService;
            _currencyRatesService = currencyRatesService;
            _mapper = mapper;
        }

        // api/transaction/deposit
        [HttpPost("deposit")]
        [Description("Add deposit")]
        [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
        public async Task<ActionResult<long>> AddDepositAsync([FromBody] TransactionInputModel inputModel)
        {
            var dto = _mapper.Map<TransactionDto>(inputModel);
            var output = await _transactionService.AddDepositAsync(dto);

            return StatusCode(201, output);
        }

        // api/transaction/withdraw
        [HttpPost("withdraw")]
        [Description("Add withdraw")]
        [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
        public async Task<ActionResult<long>> AddWithdrawAsync([FromBody] TransactionInputModel inputModel)
        {
            var dto = _mapper.Map<TransactionDto>(inputModel);
            var output = await _transactionService.AddWithdrawAsync(dto);

            return StatusCode(201, output);
        }

        // api/transaction/transfer
        [HttpPost("transfer")]
        [Description("Add transfer")]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        public async Task<ActionResult<List<long>>> AddTransferAsync([FromBody] TransferInputModel inputModel)
        {
            var dto = _mapper.Map<TransferDto>(inputModel);
            var output = await _transactionService.AddTransferAsync(dto);

            return StatusCode(201, output);
        }

        // api/transaction/by-account/{accountId}
        [HttpGet("by-account/{accountId}")]
        [Description("Get transactions by account")]
        [ProducesResponseType(typeof(List<TransactionOutputModel>), StatusCodes.Status200OK)]
        public async Task<string> GetTransactionsByAccountIdAsync(int accountId)
        {
            return await _transactionService.GetTransactionsByAccountIdAsync(accountId);
        }

        // api/transaction/by-period
        [HttpPost("by-period")]
        [Description("Get transactions by period")]
        [ProducesResponseType(typeof(List<TransactionOutputModel>), StatusCodes.Status200OK)]
        public async Task<string> GetTransactionsByPeriodAsync([FromBody] GetByPeriodInputModel inputModel)
        {
            var leadId = Request.Headers["LeadId"];
            var dto = _mapper.Map<GetByPeriodDto>(inputModel);
            return await _transactionService.GetTransactionsByPeriodAsync(dto, leadId);
        }

        // api/transaction/by-period
        [HttpPost("by-accountIds")]
        [Description("Get transactions by accountIds for two months")]
        [ProducesResponseType(typeof(List<TransactionOutputModel>), StatusCodes.Status200OK)]
        public string GetTransactionsByAccountIdsForTwoMonths([FromBody] List<int> accountIds)
        {
            return _transactionService.GetTransactionsByAccountIdsForTwoMonths(accountIds);
        }

        // api/transaction/currency-rates
        [HttpGet("currency-rates")]
        [Description("Get current currency rates")]
        [ProducesResponseType(typeof(RatesExchangeModel), StatusCodes.Status200OK)]
        public RatesExchangeModel GetCurrencyRates()
        {
            return _currencyRatesService.RatesModel;
        }
    }
}