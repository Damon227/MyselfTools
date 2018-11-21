using System;
using Microsoft.AspNetCore.Http.Connections.Client;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SignalRClientDemo
{
    class Program
    {
        private static void Main(string[] args)
        {
            //HubConnectionBuilder builder = new HubConnectionBuilder();
            //builder.Services.Configure((Action<HttpConnectionOptions>)(action => action.Url = new Uri("")));
            //builder.Services.AddSingleton<IConnectionFactory, HttpConnectionFactory>();

            //HubConnection connection = builder.Build();

            HubConnection connection = new HubConnectionBuilder().WithUrl("https://localhost:5001/messagehub").Build();
            connection.StartAsync().Wait();

            Message message = new Message
            {
                Title = "title",
                Time = DateTimeOffset.Now
            };
            connection.SendAsync("SendMessageAsync", "user1", "message1").Wait();
            connection.SendAsync("SendMessage2Async", message).Wait();

            Console.ReadLine();
        }
    }
}
