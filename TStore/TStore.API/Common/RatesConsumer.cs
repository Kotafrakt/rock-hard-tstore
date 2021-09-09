using MassTransit;
using Exchange;
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
            _currencyRatesService.RatesModel = context.Message;
            _currencyRatesService.SaveCurrencyRates(context.Message);
        }
    }
}
