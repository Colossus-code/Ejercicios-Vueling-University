namespace Net6Cache.Entity
{
    public class TestCacheSell
    {
        public TestCacheObj TestCacheObj { get; set; }

        public DateTime SoldTime { get; set; }

        public override string ToString()
        {
            SoldTime = DateTime.UtcNow;
            
            return $"Sold at {SoldTime}, product ID: {TestCacheObj.Id}, product name {TestCacheObj.ProductName} , product description {TestCacheObj.ProductDescription}"; 
        }
    }
}
