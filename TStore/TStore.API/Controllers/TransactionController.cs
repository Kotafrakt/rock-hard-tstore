using AutoMapper;
using Exchange;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public ActionResult<long> AddDeposit([FromBody] TransactionInputModel inputModel)
        {
            var dto = _mapper.Map<TransactionDto>(inputModel);
            var output = _transactionService.AddDeposit(dto);

            return StatusCode(201, output);
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
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        public ActionResult<List<long>> AddTransfer([FromBody] TransferInputModel inputModel)
        {
            var dto = _mapper.Map<TransferDto>(inputModel);
            var output = _transactionService.AddTransfer(dto);

            return StatusCode(201, output);
        }

        // api/transaction/by-account/{accountId}
        [HttpGet("by-account/{accountId}")]
        [Description("Get transactions by account")]
        [ProducesResponseType(typeof(List<TransactionOutputModel>), StatusCodes.Status200OK)]
        public string GetTransactionsByAccountId(int accountId)
        {
            var resultDto = _transactionService.GetTransactionsByAccountId(accountId);
            return JsonConvert.SerializeObject(resultDto);
        }

        // api/transaction/by-period
        [HttpPost("by-period")]
        [Description("Get transactions by period")]
        [ProducesResponseType(typeof(List<TransactionOutputModel>), StatusCodes.Status200OK)]
        public string GetTransactionsByPeriod([FromBody] GetByPeriodInputModel inputModel)
        {
            Transactions transactions;
            string userName = HttpContext.User.Identity?.Name;
            if (!Transactions.CheckDictionaryByUserName(userName))
            {
                var dto = _mapper.Map<GetByPeriodDto>(inputModel);
                transactions = new Transactions(_transactionService.GetTransactionsByPeriod(dto));
                if (!transactions.CheckAllowedSize())
                {
                    transactions.SetListToDictionary(userName);
                    transactions = Transactions.GetPart(userName);
                }
            }
            else
            {
                transactions = Transactions.GetPart(userName);
            }

            return JsonConvert.SerializeObject(transactions);
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