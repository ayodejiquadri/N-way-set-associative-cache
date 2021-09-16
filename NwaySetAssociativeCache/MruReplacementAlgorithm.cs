using System.Collections.Generic;

namespace NwaySetAssociativeCache
{
    public class MruReplacementAlgorithm<K> : IReplacementAlgorithm<K>
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
            return _head.Key;
        }

        public void OnRemove()
        {
            RemoveHead();
        }

        private void RemoveHead()
        {
            _data.Remove(_head.Key);
            if (_head.Next != null)
            {
                _head.Next.Previous = null;
                _head = _head.Next;
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

