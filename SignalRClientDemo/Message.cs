using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRClientDemo
{
    public class Message
    {
        public string MessageId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ReceiverId { get; set; }

        public bool HasRead { get; set; }

        public DateTimeOffset CreateTime { get; set; }
    }
}
