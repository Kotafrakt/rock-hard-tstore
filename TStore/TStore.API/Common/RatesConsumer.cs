using MassTransit;
using RatesApi.Models;
using System.Threading.Tasks;
using TransactionStore.Business.Services;

namespace TransactionStore.API.Common
{
    public class RatesConsumer :
        IConsumer<RatesOutputModel>
    {
        private readonly CurrencyRatesService _currencyRatesService;

        public RatesConsumer(CurrencyRatesService currencyRatesService)
        {
            _currencyRatesService = currencyRatesService;
        }

        public async Task Consume(ConsumeContext<RatesOutputModel> context)
        {
            _currencyRatesService.BaseCurrency = context.Message.BaseCurrency;
            _currencyRatesService.CurrencyPair = context.Message.Rates;
        }
    }
}
