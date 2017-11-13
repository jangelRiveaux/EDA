using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EDA
{
    public class EDAList<T> : IList<T>, IEnumerator<T>
    {
        int count;
        internal ListNode<T> first;
        internal  ListNode<T> last;
        ListNode<T> current;

        public EDAList()
        {
            count = 0;
            current = null;
            first = null;
            last = null;
            this.Reset();
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

            this.Reset();
        }

        public void Clear()
        {
            first = last = null;
            count = 0;
        }

        public virtual bool Contains(T item)
        {
            ListNode<T> current_n = first;
            while (current_n != null)
            {
                if (current_n.Value.Equals(item)) return true;
                current_n = current_n.Next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("the array can not be null");
            if (arrayIndex < 0 || array.Length - arrayIndex < count)
                throw new ArgumentOutOfRangeException("The index can not be less than 0");
            ListNode<T> current_n = first;
            while(current_n != null)
            {
                array[arrayIndex] = current_n.Value;
                arrayIndex++;
                current_n = current_n.Next;
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
            ListNode<T> current_n = first;
            while (current_n != null)
            {
                if (current_n.Value.Equals(item)) return index;
                index++;
                current_n = current_n.Next;
            }
            return -1;
        }

        public virtual void Insert(int index, T item)
        {
            if (index > count || index < 0)
            {
                string format_string = string.Format("%d and %d", 0, count - 1);
                throw new ArgumentOutOfRangeException("the index most be between " + format_string);
            }

            ListNode<T> newNode = new ListNode<T> { Value = item };
            if (index == 0)
            {
                newNode.Next = first;
                first = newNode;
                if (last == null)
                {
                    last = first;
                }
            }
            else {
                ListNode<T> previus = GetNode(index - 1);
                newNode.Next = previus.Next;
                previus.Next = newNode;
            }
            count++;
            this.Reset();
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
            ListNode<T> current_n = first;
            ListNode<T> before = null;
            while (current_n != null)
            {
                if (current_n.Value.Equals(item))
                {
                    if (before != null)
                        before.Next = current_n.Next;
                    else
                    {
                        first = current_n.Next;
                    }
                    return true;
                }
                before = current_n;
                current_n = current_n.Next;
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
            if (first == last)
            {
                first = last = null;
            }
            else if (index == 0)
            {
                first = first.Next;
            }
            else
            {
                ListNode<T> previus = GetNode(index - 1);
                previus.Next = previus.Next.Next;
            }
            count--;
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
            ListNode<T> current_n = first;
            for (int i = 0; i < index; i++)
            {
                current_n = current_n.Next;
            }
            return current_n;
        }
    }

    public class ListNode<T>
    {
        public T Value { get; set; }
        public ListNode<T> Next { get; internal set; }
    }
}
