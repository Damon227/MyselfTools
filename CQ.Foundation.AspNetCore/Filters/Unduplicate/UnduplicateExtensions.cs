using System;
using System.Collections.Generic;
using System.Text;
using CQ.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.Foundation.AspNetCore.Filters
{
    public static class UnduplicateExtensions
    {
        /// <summary>
        ///     注入防重服务，普通级别，适用于单节点应用。
        /// </summary>
        public static void AddUndulicate(this IServiceCollection service)
        {
            service.Configure<UnduplicateOptions>(options => options.DuplicateInterval = 10);
            service.AddScoped<IUnduplicateService, UnduplicateService>();
        }

        /// <summary>
        ///     注入防重服务，普通级别，适用于单节点应用。
        /// </summary>
        /// <param name="service"></param>
        /// <param name="duplicateInterval">重复提交时间间隔，单位：秒。</param>
        public static void AddUndulicate(this IServiceCollection service, int duplicateInterval)
        {
            service.Configure<UnduplicateOptions>(options => options.DuplicateInterval = duplicateInterval);
            service.AddScoped<IUnduplicateService, UnduplicateService>();
        }

        /// <summary>
        ///     注入防重服务，Redis级别，适用于多点应用。
        /// </summary>
        public static void AddUndulicateWithRedis(this IServiceCollection service, IConfiguration redisConfig)
        {
            service.AddScoped<IUnduplicateService, UndulicateRedisService>();
            service.Configure<UnduplicateOptions>(options => options.DuplicateInterval = 10);
            service.AddCQRedis(redisConfig);
        }

        /// <summary>
        ///     注入防重服务，Redis级别，适用于多点应用。
        /// </summary>
        /// <param name="service"></param>
        /// <param name="redisConfig">Redis配置</param>
        /// <param name="duplicateInterval">重复提交时间间隔，单位：秒。</param>
        public static void AddUndulicateWithRedis(this IServiceCollection service, IConfiguration redisConfig, int duplicateInterval)
        {
            service.AddScoped<IUnduplicateService, UndulicateRedisService>();
            service.Configure<UnduplicateOptions>(options => options.DuplicateInterval = duplicateInterval);
            service.AddCQRedis(redisConfig);
        }
    }
}
