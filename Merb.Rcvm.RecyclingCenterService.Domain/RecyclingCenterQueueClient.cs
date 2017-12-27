using RabbitMQ.Client;

namespace Merb.Rcvm.RecyclingCenterService.Domain
{
    public class RecyclingCenterQueueClient
    {
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _model;

        private const string DeletedQueueName = "ReyclingCenter_ReyclingCenterDeleted";

        public RecyclingCenterQueueClient()
        {
           CreateConnection();
        }

        private void CreateConnection()
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();
            _model.ExchangeDeclare(DeletedQueueName, ExchangeType.Fanout, true);
        }


        public void RecyclingCenterDeleted(RecyclingCenter center)
        {
            _model.BasicPublish(DeletedQueueName, "", null, center.Serialize());
        }

    }
}