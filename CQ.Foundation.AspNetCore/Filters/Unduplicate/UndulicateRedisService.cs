using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CQ.Redis;
using Microsoft.Extensions.Options;

namespace CQ.Foundation.AspNetCore.Filters
{
    public class UndulicateRedisService : IUnduplicateService
    {
        private readonly IRedisCache _redisCache;
        private readonly UnduplicateOptions _options;

        public UndulicateRedisService(IRedisCache redisCache, IOptions<UnduplicateOptions> optionsAccessor)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
            _options = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));
        }

        public async Task<bool> IsResubmitAsync(int code)
        {
            string time = await _redisCache.GetAsync<string>(code.ToString());
            if (string.IsNullOrEmpty(time))
            {
                await _redisCache.SetAsync(code.ToString(), "Undulicate", TimeSpan.FromSeconds(_options.DuplicateInterval));
                return false;
            }

            return true;
        }

        public async Task ClearAsync(int code)
        {
            await _redisCache.RemoveAsync(code.ToString());
        }
    }
}
