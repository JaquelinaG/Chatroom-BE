using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Chatroom.Domain;
using Chatroom.Service;
using System.Threading.Tasks;

namespace Chatroom.WebApi.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IHubContext<ChatHub> hub;
        private readonly IMessagesServ messageService;

        public MessagesController(IHubContext<ChatHub> hub, IMessagesServ messagesServ)
        {
            this.hub = hub;
            this.messageService = messagesServ;
        }

        [HttpGet("")]
        public async Task<IEnumerable<Message>> GetMessages()
        {
            var messages = await this.messageService.GetMessages();

            return messages;
        }

        [HttpPost("")]
        public async Task SendMessage([FromBody] Message message)
        {
            if (message != null)
            {
                if (!message.Text.StartsWith('/'))
                {
                    await this.messageService.SaveMessage(message);
                }

                await this.hub.Clients.All.SendAsync("shareMessage", message);
            }            
        }
    }
}
