using TransactionStore.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionStore.DAL.Repositories;
using TransactionStore.Business.Services;

namespace TransactionStore.API.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAppConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<DatabaseSettings>()
                .Bind(configuration.GetSection(nameof(DatabaseSettings)))
                .ValidateDataAnnotations();
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ITransactionService, TransactionService>();

            return services;
        }
    }
}
