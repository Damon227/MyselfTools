using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace CQ.Redis
{
    public interface IRedisCache : IDisposable
    {
        /// <summary>
        ///     查询Redis缓存。
        /// </summary>
        /// <param name="key">缓存键</param>
        Task<string> GetAsync(string key);

        /// <summary>
        ///     查询Redis缓存。
        /// </summary>
        /// <param name="key">缓存键</param>
        Task<TResult> GetAsync<TResult>(string key);

        /// <summary>
        ///     设置Redis缓存。
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="expiry">缓存过期时间，不传递过期时间时，会随机生成一个过期时间</param>
        Task SetAsync(string key, object value, TimeSpan? expiry = null);

        /// <summary>
        ///     设置Redis有序列表缓存。
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="score">缓存排序字段</param>
        /// <param name="expiry">缓存过期时间，不传递过期时间时，会随机生成一个过期时间</param>
        Task SortedSetAsync(string key, object value, long score, TimeSpan? expiry = null);

        /// <summary>
        ///     设置Redis有序列表缓存。
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="valueTuples">缓存值</param>
        /// <param name="expiry">缓存过期时间，不传递过期时间时，会随机生成一个过期时间</param>
        Task SortedSetAsync(string key, IEnumerable<(object value, long score)> valueTuples, TimeSpan? expiry = null);

        /// <summary>
        ///     分页查询Redis有序列表缓存中的数据。
        /// </summary>
        /// <typeparam name="TResult">返回的实体</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        Task<List<TResult>> SortedGetRangeAsync<TResult>(string key, int pageIndex, int pageSize);

        /// <summary>
        ///     删除Redis有序列表缓存中的数据。
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="start">要删除数据的Rank起始值</param>
        /// <param name="stop">要删除数据的Rank结束值</param>
        Task SortedSetRemoveRangeByRankAsync(string key, long start, long stop);

        /// <summary>
        ///     删除Redis有序列表缓存中按Rank值排序的第一条数据。
        /// </summary>
        /// <param name="key">缓存键</param>
        Task SortedSetRemoveFirstAsync(string key);

        /// <summary>
        ///     删除Redis有序列表缓存中按Rank值排序的最后一条数据。
        /// </summary>
        /// <param name="key">缓存键</param>
        Task SortedSetRemoveLastAsync(string key);

        /// <summary>
        ///     删除Redis有序列表缓存中的数据。
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="start">要删除数据的Score起始值</param>
        /// <param name="stop">要删除数据的Score结束值</param>
        Task SortedSetRemoveRangeByScoreAsync(string key, long start, long stop);

        /// <summary>
        ///     查询Redis哈希缓存。
        /// </summary>
        /// <typeparam name="TResult">返回的实体</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="secondKey">缓存哈希键</param>
        Task<TResult> HashGetAsync<TResult>(string key, string secondKey);

        /// <summary>
        ///     查询Redis哈希缓存，返回一个列表。
        /// </summary>
        /// <typeparam name="TResult">返回的实体列表</typeparam>
        /// <param name="key">缓存键</param>
        Task<TResult> HashGetAllAsync<TResult>(string key);

        /// <summary>
        ///     设置Redis哈希缓存。
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="secondKey">缓存哈希键</param>
        /// <param name="value">缓存值</param>
        /// <param name="expiry">缓存过期时间，不传递过期时间时，会随机生成一个过期时间</param>
        Task HashSetAsync(string key, string secondKey, object value, TimeSpan? expiry = null);

        /// <summary>
        ///     删除Redis哈希缓存。
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="secondKey">缓存哈希键</param>
        Task HashDeleteAsync(string key, string secondKey);

        /// <summary>
        ///     刷新Redis缓存过期时间。
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="expiry">缓存过期时间，不传递过期时间时，会随机生成一个过期时间</param>
        Task RefreshAsync(string key, TimeSpan? expiry = null);

        /// <summary>
        ///     删除Redis缓存。
        /// </summary>
        /// <param name="key">缓存键</param>
        Task RemoveAsync(string key);

        /// <summary>
        ///     把Redis缓存中的键值锁住。
        /// </summary>
        /// <param name="key">缓存键</param>
        Task<RedisValue> LockQueryAsync(string key);

        /// <summary>
        ///     把Redis缓存中的键值锁住。
        /// </summary>
        /// <param name="key">缓存键</param>
        RedisValue LockQuery(string key);

        /// <summary>
        ///     给指定键和值加锁。
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">锁过期时间</param>
        Task<bool> LockTakeAsync(string key, string value, TimeSpan? expiry);

        /// <summary>
        ///     给指定键和值加锁。
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">锁过期时间</param>
        bool LockTake(string key, string value, TimeSpan? expiry);

        /// <summary>
        ///     释放Redis缓存中相应键的锁。
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值，如果该值与Redis中的值不一致，则锁释放失败</param>
        Task<bool> LockReleaseAsync(string key, string value);

        /// <summary>
        ///     释放Redis缓存中相应键的锁。
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值，如果该值与Redis中的值不一致，则锁释放失败</param>
        bool LockRelease(string key, string value);
    }
}