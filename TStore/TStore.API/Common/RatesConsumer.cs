using Exchange;
using MassTransit;
using System.Threading.Tasks;
using TransactionStore.Business.Services;

namespace TransactionStore.API.Common
{
    public class RatesConsumer :
        IConsumer<RatesExchangeModel>
    {
        private readonly ICurrencyRatesService _currencyRatesService;

        public RatesConsumer(ICurrencyRatesService currencyRatesService)
        {
            _currencyRatesService = currencyRatesService;
        }

        public async Task Consume(ConsumeContext<RatesExchangeModel> context)
        {
            _currencyRatesService.SaveCurrencyRates(context.Message);
        }
    }
}
