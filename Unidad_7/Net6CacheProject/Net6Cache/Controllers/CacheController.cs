using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Net6Cache.Contracts;
using Net6Cache.Entity;

namespace Net6Cache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {

        private readonly IMemoryCache _memoryCache;
        private readonly ICacheTestService _cacheService; 

        public CacheController(IMemoryCache memoryCache, ICacheTestService cacheService)
        {
            _memoryCache = memoryCache;
            _cacheService = cacheService;
        }

        [HttpGet(Name = "CacheControll")]
        public IActionResult Get()
        {

            var result = _cacheService.IsCached();

            if(result.Item2 == false)
            {

                return BadRequest($"Not found in cache. Created {result.Item1}");

            }
            else
            {
                return Ok($"Found in cache: {result.Item1}");
            }
            
        }
    }
}

