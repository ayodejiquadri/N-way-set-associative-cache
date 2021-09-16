using System;
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
                MoveToHead(d);
                return d.Val;
            }
            return default;
        }

        public void Set(K key, V data)
        {
            if (_setData.ContainsKey(key)) 
            {
                _setData[key].Val = data;
                MoveToHead(_setData[key]);
                return;
            }
            if (_setData.Count >= _sizeOfSet && _tail != null)
            {
                RemoveTail();
            }
            DNode<K, V> dNew = new DNode<K, V>(key, data);
            _setData.Add(key, dNew);
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

        private void RemoveTail()
        {
            _setData.Remove(_tail.Key);
            if (_tail.Previous != null) 
            {
                _tail.Previous.Next = null;
                _tail = _tail.Previous;
            }
        }

        private void MoveToHead(DNode<K, V> dNode)
        {
            if (_head == dNode)
                return;
            if(dNode.Previous != null)
                dNode.Previous.Next = dNode.Next;
            if (dNode.Next != null)
                dNode.Next.Previous = dNode.Previous;
            if (dNode == _tail)
                _tail = dNode.Previous;

            _head.Previous = dNode;
            dNode.Next = _head;
            _head = dNode;
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
