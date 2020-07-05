using ConsumerService.Domain;
using System.Threading.Tasks;

namespace ConsumerService.Repositories
{
    public interface IMessageRepository
    {
        Task AddAsync(Message message);
    }
}
