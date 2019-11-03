using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chatroom.Data;
using Chatroom.Domain;

namespace Chatroom.Service
{
    public class MessagesServ : IMessagesServ
    {
        private readonly int maxMessages = 50;

        public MessagesServ()
        {
        }

        public async Task<IEnumerable<Message>> GetMessages()
        {
            using (var ctx = new ChatroomContext())
            {
                var query = ctx.Messages.OrderByDescending(x => x.Timestamp).Take(this.maxMessages);

                return query.ToArray();
            }
        }

        public async Task SaveMessage(Message message)
        {
            using (var ctx = new ChatroomContext())
            {
                ctx.Messages.Add(message);

                await ctx.SaveChangesAsync();
            }
        }
    }
}
