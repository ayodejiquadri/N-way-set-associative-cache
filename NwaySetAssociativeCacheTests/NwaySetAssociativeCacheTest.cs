using NwaySetAssociativeCache;
using Xunit;

namespace NwaySetAssociativeCacheTests
{
    public class NwaySetAssociativeCacheTest
    {
        [Fact]
        public void Put_and_Get_Should_Return_Success()
        {
            ICache<string, string> cache = new NwaySetAssociativeCache<string, string>(5, 5, ReplacementAlgorithms.Lru);

            cache.Put("Something", "Good");
            cache.Put("Always", "Bad");
            cache.Put("Always", "Bad2");
            cache.Put("test1", "test1v");
            cache.Put("test2", "test2v");
            Assert.Equal("Good", cache.Get("Something"));
            Assert.Equal("Bad2", cache.Get("Always"));
           // Assert.Equal("test1v", cache.Get("test1"));
        }
    }
}
