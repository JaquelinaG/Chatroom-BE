using System.IO;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Chatroom.Domain;
using System;
using Chatroom.Service;

namespace Chatroom.Bot.RabbitMQ
{
    public class RabbitMQClient : IRabbitMQClient
    {
        private readonly IRMQConfiguration RMQConfiguration;
        private readonly IBotService botService;

        public RabbitMQClient(IRMQConfiguration RMQconfiguration, IBotService botService)
        {
            this.RMQConfiguration = RMQconfiguration;
            this.botService = botService;
        }

        public void Configure()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json")
                  .Build();

            var chatHubUrl = configuration.GetSection("ChatroomApi").GetValue<string>("chatHubUrl");
            var hub = new HubConnectionBuilder().WithUrl(chatHubUrl).Build();

            hub.On<Message>("shareMessage", (message) => {
                if (message.Name != "bot")
                {
                    string result;

                    if (this.botService.IsValidCommand(message.Text))
                    {
                        result = this.botService.ProcessCommand(message.Text).Result;

                        this.SendMessage(result);
                    }
                    else
                    {
                        result = $"Invalid command. You must use {this.botService.GetCommandPattern()}";
                    }                    
                }
            });

            hub.StartAsync();
        }

        public void SendMessage(string stockMessage)
        {
            this.RMQConfiguration.CreateRMQConnection();

            var message = new Message()
            {
                Name = "bot",
                Text = stockMessage,
                Timestamp = DateTime.Now
            };

            this.RMQConfiguration.SendRMQMessage(message.Serialize());
        }       
    }
}
