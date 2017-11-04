using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EDA
{
    public class AVL<T,S> : IAVL_Tree<T,S> where T : IComparable
    {
        int count;
        Node<T,S> root;

        public IList<S> this[T index]
        {
            get
            {
                Node<T, S> current = GetNode(index);
                if (current != null)
                {
                    return current;
                }
                throw new KeyNotFoundException("The key does wosnt found in the tree");
            }
        }

        public int Count => count;

        public bool IsReadOnly => false;

        public void Add(KeyValuePair<T, IList<S>> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<T, IList<S>> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<T, IList<S>>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<T, IList<S>>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void InsertElement(T key, S value)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<T, IList<S>> item)
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

    internal class Node<T, S> : EDAList<S> 
    {
        internal Node(KeyValuePair<T, S> key_value)
        {
            if (key_value.Value == null)
                throw new ArgumentNullException("the value of the key value can not be null");
            Key = key_value.Key;
            this.Add(key_value.Value);
        }
        
        internal T Key { get; private set; }
        internal Node<T, S> Left_Child { get; set; }
        internal Node<T, S> Right_Child { get; set; }
        internal Node<T, S> Parent { get; set; }

    }
}
