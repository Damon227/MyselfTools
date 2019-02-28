using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exceptionless;
using Microsoft.AspNetCore.Http.Connections.Client;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SignalRClientDemo
{
    class Program
    {
        private static bool flag;
        private static Timer _timer;

        private static void Main(string[] args)
        {
            List<Message> messages = new List<Message>
            {
                new Message
                {
                    MessageId = "1",
                    Title = "title1",
                    Content = "content1"
                },
                new Message
                {
                    MessageId = "1",
                    Title = "title1",
                    Content = "content1"
                },
                new Message
                {
                    MessageId = "2",
                    Title = "title2",
                    Content = "content2"
                },
                new Message
                {
                    MessageId = "3",
                    Title = "title3",
                    Content = "content3"
                },
                new Message
                {
                    MessageId = "3",
                    Title = "title3",
                    Content = "content1"
                },
                new Message
                {
                    MessageId = "3",
                    Title = "title2",
                    Content = "content1"
                },
                new Message
                {
                    MessageId = "4",
                    Title = "title4",
                    Content = "content1"
                }
            };

            _timer = new Timer(Test, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(500));


            var list = messages.GroupBy(t => new { t.MessageId, t.Title }, t => t).ToList();
            foreach (var grouping in list)
            {
                List<Message> temp = grouping.ToList();
            }

            //ExceptionlessClient.Default.Startup("zUot66NiE00DaDsXIa9QzpEikTcBayJO13ZRUQyH");
            //ExceptionlessClient.Default.Startup("MgPAWFFyzPfWFV91qIHJu4sXEuRIJWnHtx6FhPNq");
            //ExceptionlessClient.Default.SubmitLog("测试Exceptionless 1");


            //ExceptionlessClient client = new ExceptionlessClient(c =>
            //{
            //    c.ApiKey = "MgPAWFFyzPfWFV91qIHJu4sXEuRIJWnHtx6FhPNq";
            //    c.ServerUrl = "http://localhost:8008/";
            //});

            //client.SubmitLog("测试Exceptionless");

            //HubConnectionBuilder builder = new HubConnectionBuilder();
            //builder.Services.Configure((Action<HttpConnectionOptions>)(action => action.Url = new Uri("")));
            //builder.Services.AddSingleton<IConnectionFactory, HttpConnectionFactory>();

            //HubConnection connection = builder.Build();

            //HubConnection connection = new HubConnectionBuilder().WithUrl("https://kc-fengniaowu-pandora-staging.fengniaowu.com:4431/messagehub").Build();
            //connection.StartAsync().Wait();

            //Message message = new Message
            //{
            //    Title = "title",
            //    CreateTime = DateTimeOffset.Now
            //};
            ////connection.SendAsync("SendMessageAsync", "user1", "message1").Wait();
            ////connection.SendAsync("SendMessage2Async", message).Wait();

            //// 监听 ReceiveMessage 方法发出的消息
            //connection.On<string, string>("ReceiveMessage", (user, msg) =>
            //{
            //    Console.WriteLine($"user : {user}");
            //    Console.WriteLine($"msg : {msg}");
            //});
            //connection.On<string>("StationMsgA0D1CBC3E78A40FE99C80160EE5520EC", msg =>
            //{
            //    Console.WriteLine($"msg : {msg}");
            //});
            //connection.On<string>("NotifyMsg",  async msg =>
            //{
            //    await Task.Delay(TimeSpan.FromSeconds(5));
            //    Console.WriteLine($"msg : {msg}");
            //});

            Console.ReadLine();
        }

        private static void Test(object state)
        {
            //if (!flag)
            //{
            //    flag = true;
                //_timer.Change(TimeSpan.FromDays(1), TimeSpan.FromDays(1));
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}, Time {DateTimeOffset.Now}");
                Thread.Sleep(TimeSpan.FromSeconds(5));
                //_timer.Change(TimeSpan.Zero, TimeSpan.FromMilliseconds(500));
            //    flag = false;
            //}
        }
    }
}
