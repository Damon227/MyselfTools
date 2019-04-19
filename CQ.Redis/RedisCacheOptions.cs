using Microsoft.Extensions.Options;

namespace CQ.Redis
{
    public class RedisCacheOptions : IOptions<RedisCacheOptions>
    {
        /// <summary>
        ///     是否启用RedisCache，当配置为false时，数据不进行缓存。
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        ///     The configuration used to connect to Redis.
        /// </summary>
        public string ConfigurationString { get; set; }

        /// <summary>
        ///     The database index.
        /// </summary>
        public int Database { get; set; }

        /// <summary>
        ///     The Redis instance name.
        /// </summary>
        public string InstanceName { get; set; }

        RedisCacheOptions IOptions<RedisCacheOptions>.Value => this;
    }
}
