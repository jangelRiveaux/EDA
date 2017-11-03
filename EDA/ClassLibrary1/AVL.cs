using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EDA
{
    public class AVL<T,S> : IAVL_Tree<T,S> where T : IComparable
    {
        int count;
        Node<T, S> root;

        public S this[T index] {
            get
            {
                Node<T,S> current = GetNode(index);
                if (current != null)
                {
                    return (S)current.Current;
                }
                throw new KeyNotFoundException("The key does wosnt found in the tree");
            } set { }
        }

        public int Count => count;

        public bool IsReadOnly => false;

        public void Add(KeyValuePair<T, S> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<T, S> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<T, S>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<T, S>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void InsertElement(T key, S value)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<T, S> item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private Node<T, S> GetNode(T key)
        {
            Node<T, S> current = root;
            int cmp = current.Key.CompareTo(key);
            while (current != null && cmp != 0)
            {                
                   current = cmp < 0? current.Left_Child: current.Right_Child;                
            }
            return current;
        }
    }

    internal class Node<T, S> : IEnumerable<S>, IEnumerator<S> 
    {
        int index;
        internal Node(KeyValuePair<T, S> key_value)
        {
            if (key_value.Value == null)
                throw new ArgumentNullException("the value of the key value can not be null");
            Key = key_value.Key;
            List = new EDAList<S>();
            List.Add(key_value.Value);
            index = 0;
        }

        public S Current => (index < List.Count) ? List[index] : throw new Exception();
        internal T Key { get; private set; }
        internal EDAList<S> List{ get; set; }
        internal Node<T, S> Left_Child { get; set; }
        internal Node<T, S> Right_Child { get; set; }
        internal Node<T, S> Parent { get; set; }

       object IEnumerator.Current => Current;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<S> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
