using Microsoft.Extensions.Caching.Memory;
using Net6Cache.Contracts;
using Net6Cache.Entity;

namespace Net6Cache.Service
{
    public class CacheTestService : ICacheTestService
    {

        private readonly IMemoryCache _memoryCacheService; 
        
        public CacheTestService(IMemoryCache memoryCache)
        {
            _memoryCacheService = memoryCache;
        }
        public (TestCacheSell, bool) IsCached()
        {

            var output = _memoryCacheService.Get<TestCacheSell>("FirstSold");

            if(output != null)
            {

                return (output, true); 
            }
            
            else
            {
                TestCacheSell firstCacheSell = new TestCacheSell
                {
                    SoldTime = DateTime.UtcNow,
                    TestCacheObj = new TestCacheObj
                    {
                        Id = 1,
                        ProductDescription = "A bike of colour red",
                        ProductName = "Bike"
                    }
                };

                _memoryCacheService.Set("FirstSold",firstCacheSell,TimeSpan.FromSeconds(120));

                return (firstCacheSell,false);
            }


        }
    }
}
