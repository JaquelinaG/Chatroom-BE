using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chatroom.Domain;

namespace Chatroom.Service
{
    public class MessagesServ : IMessagesServ
    {
        public Task<IEnumerable<Message>> GetMessages()
        {
            var list = new List<Message>() { new Message() { ID = 1, Name = "Jaqui", Text = "hola", Timestamp = new DateTime() } };
            return Task.FromResult(list as IEnumerable<Message>);
        }

        public Task SaveMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
