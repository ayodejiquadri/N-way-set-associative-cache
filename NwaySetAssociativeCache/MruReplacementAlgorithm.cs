using System;
using System.Collections.Generic;

namespace NwaySetAssociativeCache
{
    public class MruReplacementAlgorithm<K, V> : IReplacementAlgorithm<K, V>
    {
        public int Compare(CacheElement<K, V> current, CacheElement<K, V> candidate)
        {
            return -Comparer<long>.Default.Compare((long)current.Metadata, (long)candidate.Metadata);
        }

        public void OnGet(CacheElement<K, V> element)
        {
            element.Metadata = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        public void OnPut(CacheElement<K, V> element)
        {
            OnGet(element);
        }
    }
}
