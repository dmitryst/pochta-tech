using Microsoft.EntityFrameworkCore;

namespace ProducerService.Entities
{
    public class MessageStoreDbContext : DbContext
    {
        public MessageStoreDbContext()
        {
        }

        public MessageStoreDbContext(DbContextOptions<MessageStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<MessageEntity> Messages { get; set; }
    }
}
