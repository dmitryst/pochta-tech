using ConsumerService.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsumerService
{
    public class App
    {
        private readonly ILogger<App> _logger;
        private readonly IConsumerService _consumer;

        public App(ILogger<App> logger, IConsumerService consumer)
        {
            _logger = logger;
            _consumer = consumer;
        }

        public async Task Run(string[] args)
        {
            if (args.Length != 1)
            {
                _logger.LogInformation("Укажите параметр для ConsumerId, например: dotnet run client01");
                return;
            }

            string clientId = args[0];

            await _consumer.StartAsync(clientId);
            _logger.LogInformation("Начался прием сообщений. Нажмите любую кнопку для отмены.");

            Console.ReadKey();
            _consumer.CloseConnection();

            await Task.CompletedTask;
        }
    }
}
