using System.Collections.Generic;

namespace NwaySetAssociativeCache
{
    public class Set<K, V>
    {
        public IReplacementAlgorithm<K> _algorithm;
        private Dictionary<K, V> _setData = new Dictionary<K, V>();
        private readonly int _sizeOfSet;

        public Set(int sizeOfSet, ReplacementAlgorithms algorithm)
        {
            _sizeOfSet = sizeOfSet;
            if (algorithm == ReplacementAlgorithms.Lru)
            {
                _algorithm = new LruReplacementAlgorithm<K>();
            }

            else if (algorithm == ReplacementAlgorithms.Mru)
            {
                _algorithm = new MruReplacementAlgorithm<K>();
            }
        }
        public Set(int sizeOfSet, IReplacementAlgorithm<K> algorithm)
        {
            _sizeOfSet = sizeOfSet;
            _algorithm = algorithm;
        }

        public V Get(K key)
        {
            if (_setData.ContainsKey(key))
            {
                _algorithm.OnGet(key);
                return _setData[key];
            }
            return default;
        }

        public void Add(K newKey, V newValue)
        {
            if (_setData.ContainsKey(newKey))
            {
                _setData[newKey] = newValue;
                _algorithm.OnSetUpdate(newKey);
                return;
            }
            if (_setData.Count >= _sizeOfSet)
            {
                var key = _algorithm.GetKeyToRemove();
                _setData.Remove(key);
                _algorithm.OnRemove();
            }
            _setData.Add(newKey, newValue);
            _algorithm.OnSetNew(newKey);
        }
    }
}
