namespace NwaySetAssociativeCache
{
    public class CacheElement<K,V>
    {
        public CacheElement(K key, V data)
        {
            Key = key;
            Data = data;
        }
        public V Data { get; set; }
        public K Key { get; set; }
        public object Metadata { get; set; }

        public void SetKeyValue(K key, V data) 
        {
            Key = key;
            Data = data;
        }


    }
}
