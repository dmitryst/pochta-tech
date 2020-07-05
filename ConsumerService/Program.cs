using ConsumerService.Configuration;
using ConsumerService.Entities;
using ConsumerService.Repositories;
using ConsumerService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace ConsumerService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            await serviceProvider.GetService<App>().Run(args);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
                builder.AddFilter("Microsoft.EntityFrameworkCore.Infrastructure", LogLevel.Warning);
                builder.AddConsole();
                builder.AddDebug();
            });

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            services.AddDbContext<MessageReceiptDbContext>( 
                options => options.UseSqlServer(configuration.GetValue<string>("ConnectionString")),
                ServiceLifetime.Transient);

            services.Configure<NatsOptions>(configuration.GetSection(nameof(NatsOptions)));

            services.AddScoped<IConsumerService, Services.ConsumerService>();
            services.AddScoped<IMessageRepository, MessageRepository>();

            services.AddScoped<App>();
        }
    }
}
