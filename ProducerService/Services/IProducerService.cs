using System.Threading;
using System.Threading.Tasks;

namespace ProducerService.Services
{
    public interface IProducerService
    {
        Task StartAsync(CancellationToken cancellationToken);
    }
}
