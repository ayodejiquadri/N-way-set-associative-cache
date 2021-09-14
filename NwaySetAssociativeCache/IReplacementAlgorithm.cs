using System.Collections.Generic;

namespace NwaySetAssociativeCache
{
    public interface IReplacementAlgorithm<K,V>
    {
       // void Set(K newkey, V data);
        bool Contains(K key);
       // void SetCache(List<Dictionary<K, V>> cache);
        V Get(K key);
        void Set(K newkey, V data);
    }
}
