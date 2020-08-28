using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver.Core.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Merb.Rcvm.VehicleService.Domain.Queue
{
    public class RecyclingCenterQueueClient
    {
        private const string DeletedQueueName = "ReyclingCenter_ReyclingCenterDeleted";
        private readonly ConnectionFactory _factory;

        private IConnection _connection;
        private IModel _channel;

        public RecyclingCenterQueueClient()
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
        }

        public void SubscribeToRecyclingterDeletedQueue()
        {
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(DeletedQueueName, true, false, false, null);
            _channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(_channel);
            _channel.BasicConsume(DeletedQueueName, false, consumer);
            consumer.Received += async (ch, ea) =>
            {
                var message = (RecyclingCenter)ea.Body.DeSerialize(typeof(RecyclingCenter));

                var repository = new VehicleRepository();
                var vehicles = await repository.GetAllVehicles(message.Id);
                foreach (var vehicle in vehicles)
                {
                    await repository.DeleteVehicle(vehicle.Id);
                }

                _channel.BasicAck(ea.DeliveryTag, false);
            };
        }

        public void UnsubscribeToRecyclingterDeletedQueue()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}