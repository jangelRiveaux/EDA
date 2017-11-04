using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EDA
{
    public class EDAList<T> : IList<T>, IEnumerator<T>
    {
        int count;
        ListNode<T> first;
        ListNode<T> last;
        ListNode<T> current;

        public EDAList()
        {
            count = 0;
        }
        
        public T this[int index] {
            get {
                if (index >= count || index < 0)
                {
                    string format_string = string.Format("%d and %d", 0, count - 1);
                    throw new ArgumentOutOfRangeException("the index most be between " + format_string);
                }
                return GetNode(index).Value;
            } set
            {
                if (index >= count || index < 0)
                {
                    string format_string = string.Format("%d and %d", 0, count - 1);
                    throw new ArgumentOutOfRangeException("the index most be between " + format_string);
                }
                GetNode(index).Value = value;
            }
         }

        public int Count => count;

        public bool IsReadOnly => false;

        public T Current => current.Value;

        object IEnumerator.Current => Current;

        public void Add(T item)
        {
            ListNode<T> temp = new ListNode<T> { Value = item };
            if (first == null)
            {
                first = last = temp;
            }
            else {
                last.Next = temp;
                last = temp;
            }
            count++;
        }

        public void Clear()
        {
            first = last = null;
            count = 0;
        }

        public virtual bool Contains(T item)
        {
            ListNode<T> current = first;
            while (current != null)
            {
                if (current.Value.Equals(item)) return true;
                current = current.Next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("the array can not be null");
            if (arrayIndex < 0 || array.Length - arrayIndex < count)
                throw new ArgumentOutOfRangeException("The index can not be less than 0");
            ListNode<T> current = first;
            while(current != null)
            {
                array[arrayIndex] = current.Value;
                arrayIndex++;
                current = current.Next;
            }

        }

        public void Dispose()
        {            
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        public virtual int IndexOf(T item)
        {
            int index = 0;
            ListNode<T> current = first;
            while (current != null)
            {
                if (current.Value.Equals(item)) return index;
                index++;
                current = current.Next;
            }
            return -1;
        }

        public virtual void Insert(int index, T item)
        {
            if (index >= count || index < 0)
            {
                string format_string = string.Format("%d and %d", 0, count - 1);
                throw new ArgumentOutOfRangeException("the index most be between " + format_string);
            }

            ListNode<T> newNode = new ListNode<T> { Value = item };
            if (index == 0)
            {
                newNode.Next = first;
                first = newNode;
            }
            else {
                ListNode<T> previus = GetNode(index - 1);
                newNode.Next = previus.Next;
                previus.Next = newNode;
            }
        }

        public bool MoveNext()
        {
            if (first == null) return false;
            if (current == null)
            {
                current = first;
                return true;
            }
            if (current.Next != null)
            {
                current = current.Next;
                return true;
            }
            return false;
        }

        public virtual bool Remove(T item)
        {
            ListNode<T> current = first;
            ListNode<T> before = null;
            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    if (before != null)
                        before.Next = current.Next;
                    else
                    {
                        first = current.Next;
                    }
                    return true;
                }
                before = current;
                current = current.Next;
            }
            return false;
        }

        public virtual void RemoveAt(int index)
        {
            if (index >= count || index < 0)
            {
                string format_string = string.Format("%d and %d", 0, count - 1);
                throw new ArgumentOutOfRangeException("the index most be between " + format_string);
            }
            
            if (index == 0)
            {
                first = first.Next;
            }
            else
            {
                ListNode<T> previus = GetNode(index - 1);
                previus.Next = previus.Next.Next;
            }
        }

        public void Reset()
        {
            current = null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        private ListNode<T> GetNode(int index)
        {
            ListNode<T> current = first;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current;
        }
    }

    public class ListNode<T>
    {
        public T Value { get; set; }
        public ListNode<T> Next { get; internal set; }
    }
}
