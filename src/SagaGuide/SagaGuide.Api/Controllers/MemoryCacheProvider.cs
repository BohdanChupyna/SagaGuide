using Microsoft.Extensions.Caching.Memory;
using SagaGuide.Api.Constants;

namespace SagaGuide.Api.Controllers;

public class MemoryCacheProvider
{
   public IMemoryCache Cache { get; set; } = new MemoryCache(new MemoryCacheOptions
    {
        SizeLimit = 8
    });

   public void UpdateCacheKey<T>(string key, T data)
   {
       var cacheEntryOptions = new MemoryCacheEntryOptions()
           .SetSlidingExpiration(CacheKeys.SlidingExpiration)
           .SetAbsoluteExpiration(CacheKeys.AbsoluteExpiration)
           .SetPriority(CacheItemPriority.Normal)
           .SetSize(1);
       
       Cache.Set(key, data, cacheEntryOptions);
   }
}