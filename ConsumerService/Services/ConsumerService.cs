using ConsumerService.Configuration;
using ConsumerService.Domain;
using ConsumerService.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using STAN.Client;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsumerService.Services
{
    public class ConsumerService : IConsumerService
    {
        private readonly ILogger<ConsumerService> _logger;
        private readonly IMessageRepository _messageRepository;
        private readonly NatsOptions _natsOptions;
        private IStanConnection _stanConnection;

        public ConsumerService(
            ILogger<ConsumerService> logger,
            IOptions<NatsOptions> options,
            IMessageRepository messageRepository)
        {
            _logger = logger;
            _messageRepository = messageRepository;
            _natsOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task StartAsync(string consumerId)
        {
            StanOptions options = StanOptions.GetDefaultOptions();
            options.NatsURL = _natsOptions.Url;

            var stanSubOptions = StanSubscriptionOptions.GetDefaultOptions();
            stanSubOptions.DurableName = _natsOptions.DurableName;

            _stanConnection = new StanConnectionFactory()
                .CreateConnection(_natsOptions.ClusterId, consumerId, options);

            try
            {
                _stanConnection
                    .Subscribe(_natsOptions.Subject, stanSubOptions, (obj, args) =>
                    {
                        string messageData = Encoding.UTF8.GetString(args.Message.Data);
                        Console.WriteLine($"[#{args.Message.Sequence}] {messageData}");

                        var message = JsonSerializer.Deserialize<Message>(messageData);
                        message.Number = args.Message.Sequence;
                        _messageRepository.AddAsync(message).GetAwaiter().GetResult();
                    });
            }
            catch (Exception e)
            {
                _logger.LogError($"Ошибка подписки на сообщения: {e.ToString()}");
                CloseConnection();
            }

            await Task.CompletedTask;
        }

        public void CloseConnection()
        {
            _stanConnection.Close();
        }
    }
}
