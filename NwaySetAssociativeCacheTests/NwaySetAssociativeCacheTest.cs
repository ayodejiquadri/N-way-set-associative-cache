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
            Assert.Equal("Good", cache.Get("Something"));
            Assert.Equal("Bad", cache.Get("Always"));
        }
    }
}
