using System;
using System.Collections.Generic;

namespace NwaySetAssociativeCache
{
    public class NwaySetAssociativeCache<K, V> : ICache<K, V>
    {
        private readonly int _numberOfSets;
        private readonly int _setSize;
        private readonly IReplacementAlgorithm<K, V> _replacementAlgorithm;
        private List<CacheElement<K, V>>[] _cacheContents;


        private static IReplacementAlgorithm<K, V> GetReplacementAlgorithm(ReplacementAlgorithms replacementAlgorithm) 
        {
            IReplacementAlgorithm<K, V> algorithm = null;
            if (replacementAlgorithm == ReplacementAlgorithms.Lru)
                algorithm = new LruReplacementAlgorithm<K, V>();
            else if (replacementAlgorithm == ReplacementAlgorithms.Mru)
                algorithm = new MruReplacementAlgorithm<K, V>();

            return algorithm;
        }
        public NwaySetAssociativeCache(int numberOfSets, int setSize, ReplacementAlgorithms replacementAlgorithm):
            this(numberOfSets,setSize, GetReplacementAlgorithm(replacementAlgorithm))
        {
        }
        public NwaySetAssociativeCache(int numberOfSets, int setSize, IReplacementAlgorithm<K, V> replacementAlgorithm)
        {
            _numberOfSets = numberOfSets;
            _setSize = setSize;
            _replacementAlgorithm = replacementAlgorithm;
            _cacheContents = new List<CacheElement<K, V>>[numberOfSets];
        }
        public V Get(K key)
        {
            var searchSet = GetSet(key);
            V data = default;
            foreach (var setElement in searchSet)
            {
                if (setElement.Key.Equals(key)) 
                {
                    _replacementAlgorithm.OnGet(setElement);
                    data = setElement.Data;
                    //break;
                }
            }
            return data;
        }

        public void Put(K key, V data)
        {
            var searchSet = GetSet(key);

            CacheElement<K, V> evictionCandidate = null;

            foreach (var setElement in searchSet)
            {
                if (searchSet.Count == _setSize) 
                {
                    evictionCandidate = ChooseBetterEvictionCandidate(evictionCandidate, setElement);
                }
                if (setElement.Key.Equals(key))
                {
                    setElement.SetKeyValue(key, data);
                    _replacementAlgorithm.OnPut(setElement);
                    return;
                }
            }

            if(searchSet.Count < _setSize) 
            {
                CacheElement<K, V> element = new CacheElement<K, V> (key, data) ;
                _replacementAlgorithm.OnPut(element);
                searchSet.Add(element);
                return;
            }
            if (evictionCandidate != null) //means set is full
            {
                evictionCandidate.SetKeyValue(key, data);
                _replacementAlgorithm.OnPut(evictionCandidate);
            }
        }

        private CacheElement<K, V> ChooseBetterEvictionCandidate(CacheElement<K, V> current, CacheElement<K, V> candidate) 
        {
            if (current == null)
                return candidate;
            if (_replacementAlgorithm.Compare(current, candidate) > 0)
                return candidate;
            return current;
        }

        private List<CacheElement<K, V>> GetSet(K key) 
        {
            int index = Math.Abs(key.GetHashCode() % _numberOfSets);

            if (_cacheContents[index] == null)
                _cacheContents[index] = new List<CacheElement<K, V>>(_setSize);

            return _cacheContents[index];
        }
    }
}
