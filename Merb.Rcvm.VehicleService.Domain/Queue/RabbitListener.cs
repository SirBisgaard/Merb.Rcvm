using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Merb.Rcvm.VehicleService.Domain.Queue
{

    public class RabbitListener : IApplicationLifetime
    {
        private readonly RecyclingCenterQueueClient _queueClient;

        public RabbitListener()
        {
            _queueClient = new RecyclingCenterQueueClient();
        }

        public void StopApplication()
        {
            Deregister();
        }

        public CancellationToken ApplicationStarted { get; }
        public CancellationToken ApplicationStopping { get; }
        public CancellationToken ApplicationStopped { get; }


        public void Register()
        {
            _queueClient.SubscribeToRecyclingterDeletedQueue();
        }

        public void Deregister()
        {
            _queueClient.UnsubscribeToRecyclingterDeletedQueue();
        }
    }
}