using ProducerService.Domain;
using ProducerService.Entities;
using System.Threading.Tasks;

namespace ProducerService.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MessageStoreDbContext _dbContext;

        public MessageRepository(MessageStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(Message message)
        {
            var entity = new MessageEntity
            {
                PublishTimestamp = message.PublishTimestamp,
                Text = message.Text,
                Hash = message.Hash
            };

            _dbContext.Messages.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }
    }
}
