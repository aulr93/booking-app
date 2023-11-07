using Booking.Application.Commons.Resources;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System.Xml.Linq;

namespace Booking.Application.Commons.Helpers
{
    public class MessageLanguage
    {
        private readonly IStringLocalizer<MessageResource> _localizer;
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions _memoryCacheEntry;

        public MessageLanguage(IStringLocalizer<MessageResource> localizer,
            IConfiguration configuration,
            IMemoryCache memoryCache)
        {
            _localizer = localizer;
            _memoryCache = memoryCache;
            _memoryCacheEntry = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(double.Parse(configuration["MemoryCacheEntryOptions:SlidingExpiration"] ?? "60")))
                                                             .SetAbsoluteExpiration(TimeSpan.FromSeconds(double.Parse(configuration["MemoryCacheEntryOptions:AbsoluteExpiration"] ?? "3600")))
                                                             .SetPriority(CacheItemPriority.Normal);
        }

        public string this[string key]
        {
            get { return GetLabels(key); }
        }

        public string GetLabels(string key)
        {
            try
            {
                _memoryCache.TryGetValue($"msg|{key}", out string? cacheValue);
                if (!string.IsNullOrEmpty(cacheValue))
                    return cacheValue;

                _memoryCache.Set($"msg|{key}", _localizer[key], _memoryCacheEntry);

                return _localizer[key];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
