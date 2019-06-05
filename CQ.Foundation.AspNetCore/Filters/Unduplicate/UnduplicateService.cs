using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace CQ.Foundation.AspNetCore.Filters
{
    public class UnduplicateService : IUnduplicateService
    {
        private static ConcurrentDictionary<int, DateTimeOffset> _store = new ConcurrentDictionary<int, DateTimeOffset>();

        private readonly UnduplicateOptions _options;

        public UnduplicateService(IOptions<UnduplicateOptions> optionsAccessor)
        {
            _options = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));
        }

        public Task<bool> IsResubmitAsync(int code)
        {
            if (!_store.TryGetValue(code, out DateTimeOffset time))
            {
                _store.TryAdd(code, DateTimeOffset.UtcNow.AddSeconds(_options.DuplicateInterval));
                return Task.FromResult(false);
            }

            return Task.FromResult(DateTimeOffset.UtcNow < time);
        }

        public Task ClearAsync(int code)
        {
            _store.TryRemove(code, out DateTimeOffset _);

            return Task.FromResult(true);
        }
    }
}
