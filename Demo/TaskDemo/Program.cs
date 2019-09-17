using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CQ.Redis;
using KC.Fengniaowu.Eos.ResourceStorage;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace TaskDemo
{
    internal class Program
    {
        static IServiceProvider _provider = Startup.ConfigureServices();

        private static readonly Dictionary<string, string> s_cache = new Dictionary<string, string>();

        private static readonly List<string> s_accountIds = new List<string>
        {
            "902ACCE424774572A340BEDB3443D73F",
            "84DA6F12E59E49B186AD01653B957025",
            "6B3C9B4D7D6D49B3A385016566A311A4",
            "E87DE6B85DB5491B9C9A0165B328B501",
            "E2DF37D1715447D68A63016839577953",
            "1A666DF743D64B28BEE2016839577895",
            "2A27751C69B74C99896E01683957754C",
            "BF2CBF6CC792401E91BB0168395774BC",
            "0C6A4E9C73FA4E29966D01683957718C",
            "5D1CFD3B834B4B45B45B0168395770DC",
            "ED9FB9DB113E469FAAB40166A559633F"
        };

        private static async Task Main(string[] args)
        {
            //IServiceProvider provider = Startup.ConfigureServices();

            //IResourceStorageProvider storage = provider.GetService<IResourceStorageProvider>();

            //List<FileInfo> r1 = await storage.GetResourcesAsync("t1");
            //await storage.DeleteDirectory1Async("t1");

            //for (int i = 0; i < 10000; i++)
            //{
            //    SqlTest(i).Wait();
            //}

            CQRedisTest().Wait();

            Console.ReadLine();
        }

        private static async Task SqlTest(int i)
        {
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:8145/")
            };

            Random random = new Random();
            int index = random.Next(0, 4);

            var content = new
            {
                tenancyId = "3E0D49CE950A424FBF6D5F6A57231CF0",
                system = "Fengniaowu",
                category = 100,
                method = "StationMsg",
                title = "租约提醒",
                content = "您好，租客张一一签约了青年汇2-201B室",
                receiverInfos = new[]
                {
                    new
                    {
                        receiverId = s_accountIds[index],
                        receiverName = "青年汇"
                    }
                }

            };

            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/Message/SendStationMessage", content);
            Console.WriteLine($"{i},接口调用结果：{response.StatusCode}，时间：{DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss}");
        }

        private static async Task CQRedisTest()
        {
            string key = "key";
            string token = "token";
            IRedisCache redisCache = _provider.GetRequiredService<IRedisCache>();
            //await redisCache.SetAsync(key, token);
            bool take = await redisCache.LockTakeAsync(key, token, TimeSpan.FromSeconds(500));
            bool take2 = await redisCache.LockTakeAsync(key, token, TimeSpan.FromSeconds(500));
            await redisCache.LockReleaseAsync(key, token);
        }
    }
}
