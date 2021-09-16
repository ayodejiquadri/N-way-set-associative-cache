namespace NwaySetAssociativeCache
{
    public class DNode<K>
    {
        public K Key;


        public DNode<K> Previous;

        public DNode<K> Next;
   
        public DNode(K key)
        {
            this.Key = key;
        }
    }
}
