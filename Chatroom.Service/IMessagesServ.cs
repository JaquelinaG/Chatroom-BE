using System.Collections.Generic;
using System.Threading.Tasks;
using Chatroom.Domain;

namespace Chatroom.Service
{
    public interface IMessagesServ
    {
        Task SaveMessage(Message message);

        Task<IEnumerable<Message>> GetMessages();
    }
}
