using System.Collections.Generic;
using System.Threading.Tasks;
using Chatroom.Domain;

namespace Chatroom.Service
{
    public interface IMessagesServ
    {
        Task<IEnumerable<Message>> GetMessages();

        bool IsMessageForBot(string text);

        Task SaveMessage(Message message);
    }
}
