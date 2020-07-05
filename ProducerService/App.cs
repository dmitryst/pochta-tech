using Microsoft.Extensions.Logging;
using ProducerService.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerService
{
    public class App
    {
        private readonly ILogger<App> _logger;
        private readonly IProducerService _producer;

        public App(ILogger<App> logger, IProducerService producer)
        {
            _logger = logger;
            _producer = producer;
        }

        public async Task Run()
        {
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            _ = Task.Run(() => _producer.StartAsync(cancellationToken));

            _logger.LogInformation("Началась отправка сообщений. Нажмите любую кнопку для отмены.");
            Console.ReadKey();
            cts.Cancel();

            await Task.CompletedTask;
        }
    }
}
