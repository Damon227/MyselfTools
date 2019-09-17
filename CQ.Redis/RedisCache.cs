using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace CQ.Redis
{
    public class RedisCache : IRedisCache
    {
        private ConnectionMultiplexer _connection;
        private IDatabase _database;
        private readonly string _instance;

        private readonly RedisCacheOptions _options;

        public RedisCache(IOptions<RedisCacheOptions> optionsAccessor)
        {
            _options = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));

            if (!string.IsNullOrEmpty(_options.InstanceName))
            {
                _instance = _options.InstanceName;

                if (!_options.InstanceName.Substring(_options.InstanceName.Length - 1, 1).Equals(":"))
                {
                    _instance += ":";
                }
            }
            else
            {
                _instance = string.Empty;
            }
        }

        /// <summary>
        ///     查询Redis缓存。
        /// </summary>
        /// <param name="key">缓存键</param>
        public async Task<string> GetAsync(string key)
        {
            if (IsMock())
            {
                return null;
            }

            await ConnectAsync();

            string result = await _database.StringGetAsync(_instance + key);

            // 延长缓存时间
            //await _database.StringSetAsync(_instance + key, result, GetRandomExpiryTime());

            return result;
        }

        /// <summary>
        ///     查询Redis缓存。
        /// </summary>
        /// <param name="key">缓存键</param>
        public async Task<TResult> GetAsync<TResult>(string key)
        {
            if (IsMock())
            {
                return default(TResult);
            }

            await ConnectAsync();

            string result = await _database.StringGetAsync(_instance + key);

            if (string.IsNullOrEmpty(result))
            {
                return default(TResult);
            }

            // 延长缓存时间
            //await _database.StringSetAsync(_instance + key, result, GetRandomExpiryTime());

            return JsonConvert.DeserializeObject<TResult>(result);
        }

        /// <summary>
        ///     设置Redis缓存。
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="expiry">缓存过期时间，不传递过期时间时，会随机生成一个过期时间</param>
        public async Task SetAsync(string key, object value, TimeSpan? expiry = null)
        {
            if (IsMock())
            {
                return;
            }

            await ConnectAsync();

            await _database.StringSetAsync(_instance + key, JsonConvert.SerializeObject(value), expiry ?? GetRandomExpiryTime());
            
        }

        /// <summary>
        ///     设置Redis有序列表缓存。
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="score">缓存排序字段</param>
        /// <param name="expiry">缓存过期时间，不传递过期时间时，会随机生成一个过期时间</param>
        public async Task SortedSetAsync(string key, object value, long score, TimeSpan? expiry = null)
        {
            if (IsMock())
            {
                return;
            }

            await ConnectAsync();
            
            await _database.SortedSetAddAsync(_instance + key, JsonConvert.SerializeObject(value), score);
            await _database.KeyExpireAsync(_instance + key, expiry ?? GetRandomExpiryTime());
        }

        /// <summary>
        ///     设置Redis有序列表缓存。
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="valueTuples">缓存值</param>
        /// <param name="expiry">缓存过期时间，不传递过期时间时，会随机生成一个过期时间</param>
        public async Task SortedSetAsync(string key, IEnumerable<(object value, long score)> valueTuples, TimeSpan? expiry = null)
        {
            if (IsMock())
            {
                return;
            }

            await ConnectAsync();

            List<SortedSetEntry> entries = new List<SortedSetEntry>();

            foreach ((object value, long score) tuple in valueTuples)
            {
                entries.Add(new SortedSetEntry(JsonConvert.SerializeObject(tuple.value), tuple.score));
            }

            await _database.SortedSetAddAsync(_instance + key, entries.ToArray());
            await _database.KeyExpireAsync(_instance + key, expiry ?? GetRandomExpiryTime());
        }

        /// <summary>
        ///     分页查询Redis有序列表缓存中的数据。
        /// </summary>
        /// <typeparam name="TResult">返回的实体</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        public async Task<List<TResult>> SortedGetRangeAsync<TResult>(string key, int pageIndex, int pageSize)
        {
            if (IsMock())
            {
                return default(List<TResult>);
            }

            await ConnectAsync();

            IEnumerable<SortedSetEntry> result = _database.SortedSetRangeByScoreWithScores(_instance + key, -999999999999999999, 999999999999999999, Exclude.None, Order.Ascending, pageIndex * pageSize, pageSize);
            List<RedisValue> redisValues = result.Select(t => t.Element).ToList();

            if (redisValues.Count == 0)
            {
                return default(List<TResult>);
            }

            StringBuilder sb = new StringBuilder("[");
            foreach (RedisValue redisValue in redisValues)
            {
                sb.Append(redisValue.ToString().Replace("[", "").Replace("]", "")).Append(",");
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");

            List<TResult> list = JsonConvert.DeserializeObject<List<TResult>>(sb.ToString());

            return list;
        }

        /// <summary>
        ///     删除Redis有序列表缓存中的数据。
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="start">要删除数据的Rank起始值</param>
        /// <param name="stop">要删除数据的Rank结束值</param>
        public async Task SortedSetRemoveRangeByRankAsync(string key, long start, long stop)
        {
            if (IsMock())
            {
                return;
            }

            await ConnectAsync();

            await _database.SortedSetRemoveRangeByRankAsync(_instance + key, start, stop);
        }

        /// <summary>
        ///     删除Redis有序列表缓存中按Rank值排序的第一条数据。
        /// </summary>
        /// <param name="key">缓存键</param>
        public async Task SortedSetRemoveFirstAsync(string key)
        {
            await SortedSetRemoveRangeByRankAsync(key, 0, 0);
        }

        /// <summary>
        ///     删除Redis有序列表缓存中按Rank值排序的最后一条数据。
        /// </summary>
        /// <param name="key">缓存键</param>
        public async Task SortedSetRemoveLastAsync(string key)
        {
            await SortedSetRemoveRangeByRankAsync(key, -1, -1);
        }

        /// <summary>
        ///     删除Redis有序列表缓存中的数据。
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="start">要删除数据的Score起始值</param>
        /// <param name="stop">要删除数据的Score结束值</param>
        public async Task SortedSetRemoveRangeByScoreAsync(string key, long start, long stop)
        {
            if (IsMock())
            {
                return;
            }

            await ConnectAsync();

            await _database.SortedSetRemoveRangeByScoreAsync(_instance + key, start, stop);
        }

        /// <summary>
        ///     查询Redis哈希缓存。
        /// </summary>
        /// <typeparam name="TResult">返回的实体</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="secondKey">缓存哈希键</param>
        public async Task<TResult> HashGetAsync<TResult>(string key, string secondKey)
        {
            if (IsMock())
            {
                return default(TResult);
            }

            await ConnectAsync();

            RedisValue result = await _database.HashGetAsync(key, secondKey);

            if (!result.HasValue)
            {
                return default(TResult);
            }

            // 延长缓存时间
            //await _database.KeyExpireAsync(_instance + key, GetRandomExpiryTime());

            return JsonConvert.DeserializeObject<TResult>(result.ToString());
        }

        /// <summary>
        ///     查询Redis哈希缓存，返回一个列表。
        /// </summary>
        /// <typeparam name="TResult">返回的实体列表</typeparam>
        /// <param name="key">缓存键</param>
        public async Task<TResult> HashGetAllAsync<TResult>(string key)
        {
            if (IsMock())
            {
                return default(TResult);
            }

            await ConnectAsync();

            HashEntry[] entries = await _database.HashGetAllAsync(_instance + key);

            if (entries.Length == 0)
            {
                return default(TResult);
            }

            // 延长缓存时间
            //await _database.KeyExpireAsync(_instance + key, GetRandomExpiryTime());

            StringBuilder sb = new StringBuilder("[");
            foreach (HashEntry hashEntry in entries)
            {
                // Hash 每个key存储的结构为list
                sb.Append(hashEntry.Value.ToString().Replace("[","").Replace("]", "")).Append(",");
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");

            return JsonConvert.DeserializeObject<TResult>(sb.ToString());
        }

        /// <summary>
        ///     设置Redis哈希缓存。
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="secondKey">缓存哈希键</param>
        /// <param name="value">缓存值</param>
        /// <param name="expiry">缓存过期时间，不传递过期时间时，会随机生成一个过期时间</param>
        public async Task HashSetAsync(string key, string secondKey, object value, TimeSpan? expiry = null)
        {
            if (IsMock())
            {
                return;
            }

            await ConnectAsync();

            await _database.HashSetAsync(_instance + key, secondKey, JsonConvert.SerializeObject(value));
            await _database.KeyExpireAsync(_instance + key, expiry ?? GetRandomExpiryTime());
        }

        /// <summary>
        ///     删除Redis哈希缓存。
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="secondKey">缓存哈希键</param>
        public async Task HashDeleteAsync(string key, string secondKey)
        {
            if (IsMock())
            {
                return;
            }

            await ConnectAsync();

            await _database.HashDeleteAsync(_instance + key, secondKey);
        }

        /// <summary>
        ///     刷新Redis缓存过期时间。
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="expiry">缓存过期时间，不传递过期时间时，会随机生成一个过期时间</param>
        public async Task RefreshAsync(string key, TimeSpan? expiry = null)
        {
            if (IsMock())
            {
                return;
            }

            await ConnectAsync();

            string result = await _database.StringGetAsync(_instance + key);

            if (result != null)
            {
                await _database.StringSetAsync(_instance + key, result, expiry ?? GetRandomExpiryTime());
            }
        }

        /// <summary>
        ///     删除Redis缓存。
        /// </summary>
        /// <param name="key">缓存键</param>
        public async Task RemoveAsync(string key)
        {
            if (IsMock())
            {
                return;
            }

            await ConnectAsync();

            await _database.KeyDeleteAsync(_instance + key);
        }

        /// <summary>
        ///     把Redis缓存中的键值锁住。
        /// </summary>
        /// <param name="key">缓存键</param>
        public Task<RedisValue> LockQueryAsync(string key)
        {
            if (IsMock())
            {
                return Task.FromResult(RedisValue.Null);
            }

            return _database.LockQueryAsync(_instance + key);
        }

        public RedisValue LockQuery(string key)
        {
            if (IsMock())
            {
                return RedisValue.Null;
            }

            return _database.LockQuery(_instance + key);
        }

        public async Task<bool> LockTakeAsync(string key, string value, TimeSpan? expiry)
        {
            if (IsMock())
            {
                return await Task.FromResult(true);
            }

            if (expiry == null)
            {
                expiry = TimeSpan.FromSeconds(10);
            }

            await ConnectAsync();

            bool flag;

            try
            {
                flag = _database.StringSet(key, value, expiry, When.NotExists);
            }
            catch (Exception e)
            {
                flag = true;
            }

            return flag;
        }

        public bool LockTake(string key, RedisValue value, TimeSpan? expiry)
        {
            if (IsMock())
            {
                return true;
            }

            if (expiry == null)
            {
                expiry = TimeSpan.FromSeconds(10);
            }

            return _database.LockTake(_instance + key, value, expiry.Value);
        }

        /// <summary>
        ///     释放Redis缓存中相应键的锁。
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值，如果该值与Redis中的值不一致，则锁释放失败</param>
        public async Task<bool> LockReleaseAsync(string key, string value)
        {
            if (IsMock())
            {
                return await Task.FromResult(true);
            }

            string lua_script = @"

if (redis.call('GET', KEYS[1]) == ARGV[1]) then

redis.call('DEL', KEYS[1])

return true

else

return false

end

";
            try
            {
                await ConnectAsync();
                RedisResult result = await _database.ScriptEvaluateAsync(lua_script, new RedisKey[] { key }, new RedisValue[] { value });
                return (bool)result;
            }
            catch (Exception e)
            {
                return false;
            }
           
        }

        public bool LockRelease(string key, RedisValue value)
        {
            if (IsMock())
            {
                return true;
            }

            return _database.LockRelease(_instance + key, value);
        }

        private async Task ConnectAsync()
        {
            // Redis 连接字符串中，设置abortConnect=false可以自动重连
            if (_connection == null || !_connection.IsConnected)
            {
                _connection = await ConnectionMultiplexer.ConnectAsync(_options.ConfigurationString);
                _database = _connection.GetDatabase(_options.Database);
            }
        }

        private bool IsMock()
        {
            return !_options.Enable;
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
        }

        /// <summary>
        ///     获取一个随机过期时间，防止缓存雪崩
        /// </summary>
        private static TimeSpan GetRandomExpiryTime(int start = 20, int stop = 30)
        {
            Random random = new Random();
            int minutes = random.Next(start, stop);
            return TimeSpan.FromMinutes(minutes);
        }
    }
}
