using System.Collections.Generic;

namespace NwaySetAssociativeCache
{
    public interface IReplacementAlgorithm<K,V> : IComparer<CacheElement<K,V>>
    {
        void OnGet(CacheElement<K, V> element); 
        void OnPut(CacheElement<K, V> element);
    }
}
