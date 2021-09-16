using System.Collections.Generic;

namespace NwaySetAssociativeCache
{
    public class LruReplacementAlgorithm<K> : IReplacementAlgorithm<K>
    {
        private Dictionary<K, DNode<K>> _data = new Dictionary<K, DNode<K>>();
        DNode<K> _head, _tail;

        public void OnGet(K key)
        {
            MoveToHead(_data[key]);
        }

        public void OnSetNew(K key)
        {
            DNode<K> dNew = new DNode<K>(key);
            _data.Add(key, dNew);
            if (_head == null)
            {
                _head = dNew;
                _tail = dNew;
            }
            else
            {
                dNew.Next = _head;
                _head.Previous = dNew;
                _head = dNew;
            }
        }

        public void OnSetUpdate(K key)
        {
            MoveToHead(_data[key]);
        }

        public K GetKeyToRemove()
        {
            return _tail.Key;
        }

        public void OnRemove()
        {
            RemoveTail();
        }

        private void RemoveTail()
        {
            _data.Remove(_tail.Key);
            if (_tail.Previous != null)
            {
                _tail.Previous.Next = null;
                _tail = _tail.Previous;
            }
        }

        private void MoveToHead(DNode<K> dNode)
        {
            if (_head == dNode)
                return;
            if (dNode.Previous != null)
                dNode.Previous.Next = dNode.Next;
            if (dNode.Next != null)
                dNode.Next.Previous = dNode.Previous;
            if (dNode == _tail)
                _tail = dNode.Previous;

            _head.Previous = dNode;
            dNode.Next = _head;
            _head = dNode;
        }
    }
}
