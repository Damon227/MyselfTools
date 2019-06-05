using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.Redis
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class RedisExtensions
    {
        public static void AddCQRedis(this IServiceCollection serviceCollection, Action<RedisCacheOptions> options)
        {
            serviceCollection.AddScoped<IRedisCache, RedisCache>();
            serviceCollection.Configure(options);
        }

        public static void AddCQRedis(this IServiceCollection serviceCollection, IConfiguration config)
        {
            serviceCollection.AddScoped<IRedisCache, RedisCache>();
            serviceCollection.Configure<RedisCacheOptions>(config);
        }
    }
}
