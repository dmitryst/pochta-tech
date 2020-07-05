using Microsoft.EntityFrameworkCore;

namespace ConsumerService.Entities
{
    public class MessageReceiptDbContext : DbContext
    {
        public MessageReceiptDbContext()
        {
        }

        public MessageReceiptDbContext(DbContextOptions<MessageReceiptDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-J01R736;Database=MessageReceiptDb;Trusted_Connection=True;");
        }

        public DbSet<MessageEntity> Messages { get; set; }
    }
}
