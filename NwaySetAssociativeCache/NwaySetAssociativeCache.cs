using System;
using System.Collections.Generic;

namespace NwaySetAssociativeCache
{
    public class NwaySetAssociativeCache<K, V> : ICache<K, V>
    {
        private readonly int _numberOfSets;
        private readonly int _setSize;
        public List<Set<K,V>> _cache = new List<Set<K,V>>();

        public NwaySetAssociativeCache(int numberOfSets, int setSize, ReplacementAlgorithms algorithm)
        {
            _numberOfSets = numberOfSets;
            _setSize = setSize;
            for (int i = 0; i < numberOfSets; i++)
            {
                _cache.Add(new Set<K, V>(setSize, algorithm));
            }
        }
        public NwaySetAssociativeCache(int numberOfSets, int setSize, IReplacementAlgorithm<K> algorithm)
        {
            _numberOfSets = numberOfSets;
            _setSize = setSize;
            for (int i = 0; i < numberOfSets; i++)
            {
                _cache.Add(new Set<K, V>(setSize, algorithm));
            }
        }
        public V Get(K key)
        {
            return _cache[GetSetIndex(key)].Get(key);
        }

        public void Put(K key, V data)
        {
            _cache[GetSetIndex(key)].Add(key, data);
        }

        private int GetSetIndex(K key) 
        {
            int index = Math.Abs(key.GetHashCode() % _numberOfSets);
            return index;
        }
    }
}
