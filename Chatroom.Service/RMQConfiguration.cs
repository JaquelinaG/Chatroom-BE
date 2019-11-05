using RabbitMQ.Client;

namespace Chatroom.Service
{
    public class RMQConfiguration : IRMQConfiguration
    {
        private const string RMQHostName = "localhost";
        private const string RMQUserName = "guest";
        private const string RMQPasword = "guest";
        private IModel model;

        public IModel CreateRMQConnection()
        {
            var connectionfactory = new ConnectionFactory
            {
                HostName = RMQHostName,
                UserName = RMQUserName,
                Password = RMQPasword
            };

            var connection = connectionfactory.CreateConnection();
            this.model = connection.CreateModel();

            this.model.QueueDeclare("StockQueue", true, false, false, null);
            this.model.ExchangeDeclare("ChatroomExchange", "direct", false, false, null);
            this.model.QueueBind("StockQueue", "ChatroomExchange", "rkey", null);

            return this.model;
        }

        public void SendRMQMessage(byte[] message)
        {
            this.model.BasicPublish("ChatroomExchange", "rkey", null, message);
        }
    }
}
