using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Chatroom.Domain;

namespace Chatroom.WebApi
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message)
        {
            await this.Clients.All.SendAsync("shareMessage", message);
        }
    }
}
