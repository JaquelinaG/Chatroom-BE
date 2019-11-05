using System;

namespace Chatroom.Domain
{
    public class Message
    {
        public int ID { get; set; }

        public string Text { get; set; }

        public DateTime? Timestamp { get; set; }

        public string Name { get; set; }
    }
}
