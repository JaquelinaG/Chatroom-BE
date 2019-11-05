using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Chatroom.Domain;
using Chatroom.Service;

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
        public async Task<IActionResult> GetMessages()
        {
            var messages = await this.messageService.GetMessages();

            return Ok(messages);
        }

        [HttpPost("")]
        public async Task SendMessage([FromBody] Message message)
        {
            if (message != null)
            {
                if (!this.messageService.IsMessageForBot(message.Text))
                {
                    await this.messageService.SaveMessage(message);
                }

                await this.hub.Clients.All.SendAsync("shareMessage", message);
            }
        }
    }
}
