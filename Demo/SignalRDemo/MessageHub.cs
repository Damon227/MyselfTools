using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRDemo
{
    public class MessageHub : Hub
    {
        public async Task SendMessageAsync(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendMessage2Async(Message message)
        {
            await Clients.All.SendAsync("ReceiveMessage2", message);
        }
    }

    public class Message
    {
        public string Title { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
