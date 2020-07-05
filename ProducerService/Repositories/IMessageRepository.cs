using ProducerService.Domain;
using System.Threading.Tasks;

namespace ProducerService.Repositories
{
    public interface IMessageRepository
    {
        Task<int> AddAsync(Message message);
    }
}
