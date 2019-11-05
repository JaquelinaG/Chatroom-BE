using RabbitMQ.Client;

namespace Chatroom.Service
{
    public interface IRMQConfiguration
    {
        IModel CreateRMQConnection();

        void SendRMQMessage(byte[] message);
    }
}
