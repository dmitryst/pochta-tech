using System.Threading;
using System.Threading.Tasks;

namespace ConsumerService.Services
{
    public interface IConsumerService
    {
        Task StartAsync(string consumerId);

        void CloseConnection();
    }
}
