using System.Collections.Generic;

namespace NwaySetAssociativeCache
{
    public class LruReplacementAlgorithm<K, V> : IReplacementAlgorithm<K, V>
    {
        private Dictionary<K, DNode<K, V>> _setData = new Dictionary<K, DNode<K, V>>();
        private int _sizeOfSet;
        DNode<K,V> _head, _tail;
        public LruReplacementAlgorithm(int sizeOfSet)
        {
            _sizeOfSet = sizeOfSet;
        }
        
        public V Get(K key)
        {
            if (_setData.TryGetValue(key, out DNode<K, V> d)) 
            {
                if(_head ==  null) 
                {
                    _head = d;
                    _tail = d;
                }
                else
                {
                    _head.Previous = d;
                    d.Next = _head;
                    _head = d;
                }
            }
            if (d == null)
                return default;

            return d.Val;
        }

        public void Set(K key, V data)
        {
            DNode<K, V> dNew = new DNode<K, V>(key, data);
            Remove(dNew);

            if (_setData.Count >= _sizeOfSet && _tail != null)
            {
                Remove(_tail);
            }
            _setData.Add(key, dNew);
            if (_head == null)
            {
                _head = dNew;
                _tail = dNew;
            }
            else
            {
                _head.Previous = dNew;
                dNew.Next = _head;
                _head = dNew;
            }
        }
        public bool Contains(K key)
        {
            return _setData.ContainsKey(key);
        }
        private void Remove(DNode<K, V> d)
        {
            DNode<K, V> actualValue = d;
            _setData.TryGetValue(d.Key, out actualValue);

            _setData.Remove(d.Key);
            RemoveDNode(actualValue);
        }

        private void RemoveDNode(DNode<K, V> d)
        {
            if (d == null)
                return;
            if (d.Previous != null)
                d.Previous.Next = d.Next;
            if (d.Next != null)
                d.Next.Previous = d.Previous;
            if (d == _tail)
                _tail = d.Previous;
            if (d == _head)
                _head = d.Next;
        }
    }
}
