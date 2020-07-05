using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProducerService.Configuration;
using ProducerService.Domain;
using ProducerService.Repositories;
using STAN.Client;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerService.Services
{
    public class ProducerService : IProducerService
    {
        private readonly ILogger<ProducerService> _logger;
        private readonly IMessageRepository _messageRepository;
        private readonly NatsOptions _natsOptions;

        public ProducerService(
            ILogger<ProducerService> logger,
            IOptions<NatsOptions> options,
            IMessageRepository messageRepository)
        {
            _logger = logger;
            _messageRepository = messageRepository;
            _natsOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            string clientId = $"producer-{Guid.NewGuid().ToString()}";

            StanOptions stanOptions = StanOptions.GetDefaultOptions();
            stanOptions.NatsURL = _natsOptions.Url;

            using (var c = new StanConnectionFactory()
                .CreateConnection(_natsOptions.ClusterId, clientId, stanOptions))
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var message = new Message();
                    await _messageRepository.AddAsync(message);

                    var json = JsonSerializer.Serialize(message);
                    Console.WriteLine($"Отправка {json}");
                    c.Publish(_natsOptions.Subject, Encoding.UTF8.GetBytes(json));

                    await Task.Delay(1000);
                }

                _logger.LogInformation("Отправка сообщений отменена.");
            }
        }
    }
}
