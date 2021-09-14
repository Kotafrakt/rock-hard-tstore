using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using TransactionStore.API.Common;
using TransactionStore.Business.Services;
using TransactionStore.Core;
using TransactionStore.DAL.Repositories;

namespace TransactionStore.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAppConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<DatabaseSettings>()
                .Bind(configuration.GetSection(nameof(DatabaseSettings)))
                .ValidateDataAnnotations();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITransactionRepository, TransactionRepository>();
        }

        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IConverterService, ConverterService>();
            services.AddSingleton<ICurrencyRatesService, CurrencyRatesService>();
        }

        public static void AddMassTransitService(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<RatesConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint("rates-queue", e =>
                    {
                        e.ConfigureConsumer<RatesConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();
        }
    }
}