using System.Text;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using Chatroom.Domain;
using Chatroom.Service;

namespace Chatroom.WebApi
{
    public class RabbitMQClient : IRabbitMQClient
    {
        private readonly IRMQConfiguration RMQconfiguration;
        private readonly IHubContext<ChatHub> hub;

        public RabbitMQClient(IRMQConfiguration RMQconfiguration, IHubContext<ChatHub> hubContext)
        {
            this.RMQconfiguration = RMQconfiguration;
            this.hub = hubContext;
        }

        public void Configure()
        {
            var model = this.RMQconfiguration.CreateRMQConnection();

            var consumer = new EventingBasicConsumer(model);
            consumer.Received += this.Consumer_Received;

            var result = model.BasicConsume("StockQueue", autoAck: true, consumer: consumer);
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            var message = Encoding.UTF8.GetString(body);

            var chatMessage =  JsonConvert.DeserializeObject<Message>(message);

            this.hub.Clients.All.SendAsync("shareMessage", chatMessage);
        }
    }
}
