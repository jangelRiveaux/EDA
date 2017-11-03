using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EDA
{
    public class AVL<T,S> : IAVL_Tree<T,S>
    {
        public S this[T index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

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
    }
}
