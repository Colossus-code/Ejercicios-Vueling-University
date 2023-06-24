using Net6Cache.Entity;

namespace Net6Cache.Contracts
{
    public interface ICacheTestService
    {

        (TestCacheSell, bool) IsCached();
    }
}
