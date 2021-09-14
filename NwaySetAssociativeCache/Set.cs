namespace NwaySetAssociativeCache
{
    public class Set<K, V>
    {
        public IReplacementAlgorithm<K, V> _algorithm;

        public Set(int sizeOfSet, ReplacementAlgorithms algorithm)
        {

            if (algorithm == ReplacementAlgorithms.Lru)
            {
                _algorithm = new LruReplacementAlgorithm<K, V>(sizeOfSet);
            }

            else if (algorithm == ReplacementAlgorithms.Mru)
            {
                _algorithm = new MruReplacementAlgorithm<K, V>(sizeOfSet);
            }
        }
        public Set(int sizeOfSet, IReplacementAlgorithm<K, V> algorithm)
        {
            _algorithm = algorithm;
        }

        public V Get(K key)
        {
            return _algorithm.Get(key);
        }

        public void Add(K newKey, V newValue)
        {
            _algorithm.Set(newKey, newValue);
        }

        public bool Contains(K key)
        {
            return _algorithm.Contains(key);
        }
    }
}
