using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TransactionStore.API.Extensions;
using TransactionStore.Core;
using TransactionStore.API.Configuration;

namespace TransactionStore.API
{
    public class Startup
    {
        private const string _pathToEnvironment = "ASPNETCORE_ENVIRONMENT";

        public Startup(IConfiguration configuration)
        {
            var currentEnvironment = configuration.GetValue<string>(_pathToEnvironment);
            var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.{currentEnvironment}.json");

            Configuration = builder.Build();
            Configuration.SetEnvironmentVariableForConfiguration();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddAppConfiguration(Configuration);
            services.AddCustomServices();
            services.AddRepositories();
            services.AddControllers();
            services.AddMassTransitService();
            services.AddOtherOptions();
            services.AddSwagger();
            services.AddOptions();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

            app.UseSwaggerUi3(settings => { settings.ValidateSpecification = true; });

            if (env.IsProduction())
            {
                app.UseMiddleware<CheckIPMiddleware>();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}