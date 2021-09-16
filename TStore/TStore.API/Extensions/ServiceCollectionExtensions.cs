using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using TransactionStore.API.Common;
using TransactionStore.API.Configuration;
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
            services.AddOptions<AppSettings>()
              .Bind(configuration.GetSection(nameof(AppSettings)))
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
            services.AddScoped<ICurrencyRatesService, CurrencyRatesService>();
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

        public static void AddOtherOptions(this IServiceCollection services)
        {
            services
            .AddMvc()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var exc = new ValidationExceptionResponse(context.ModelState);
                    return new UnprocessableEntityObjectResult(exc);
                };
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerDocument(document =>
            {
                document.DocumentName = "EndpointsForTransactionStore";
                document.Title = "TransactionStore API";
                document.Version = "v8";
                document.Description = "An interface for TransactionStore.";
            });
        }
    }
}