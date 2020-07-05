using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProducerService.Configuration;
using ProducerService.Entities;
using ProducerService.Repositories;
using ProducerService.Services;
using System.IO;
using System.Threading.Tasks;

namespace ProducerService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            await serviceProvider.GetService<App>().Run();
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

            var connection = configuration.GetValue<string>("ConnectionString");
            services.AddDbContext<MessageStoreDbContext>(options => options.UseSqlServer(connection));

            services.Configure<NatsOptions>(configuration.GetSection(nameof(NatsOptions)));

            services.AddScoped<IProducerService, Services.ProducerService>();
            services.AddScoped<IMessageRepository, MessageRepository>();           

            services.AddScoped<App>();
        }
    }
}
