using ConsumerService.Domain;
using ConsumerService.Entities;
using System;
using System.Threading.Tasks;

namespace ConsumerService.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MessageReceiptDbContext _dbContext;

        public MessageRepository(MessageReceiptDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Message message)
        {
            var messageEntity = new MessageEntity
            {
                Number = message.Number,
                PublishTimestamp = message.PublishTimestamp,
                Text = message.Text,
                Hash = message.Hash,
                DeliveryTimestamp = message.DeliveryTimestamp
            };

            _dbContext.Messages.Add(messageEntity);
            await _dbContext.SaveChangesAsync();

        }
    }
}
