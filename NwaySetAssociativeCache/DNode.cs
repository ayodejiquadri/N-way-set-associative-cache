namespace NwaySetAssociativeCache
{
    public class DNode<K, V>
    {
        public K Key;

        public V Val;

        public DNode<K, V> Previous;

        public DNode<K, V> Next;
   
        public DNode(K key, V val)
        {
            this.Key = key;
            this.Val = val;
        }
    }
}
