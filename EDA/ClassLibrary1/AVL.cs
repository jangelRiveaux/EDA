using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EDA
{    
    public class AVL<T, S> : IAVL_Tree<T, S> where T : IComparable
    {
        int count;
        Node<T, S> root;
        Node<T, S> last_inserted_node;

        public IList<S> this[T index]
        {
            get
            {
                Node<T, S> current = GetNode(index);
                if (current != null)
                {
                    return current.Elements;
                }
                throw new KeyNotFoundException("The key does wosnt found in the tree");
            }
        }

        public int Count => count;

        public bool IsReadOnly => false;

        public void Add(KeyValuePair<T, IList<S>> item)
        {
            if (item.Value == null)
            {
                throw new ArgumentException("the value of the pair can not be null");
            }
            Node<T, S> node = GetNode(item.Key);
            EDAList<S> lst = item.Value as EDAList<S>;

            if (lst == null)
            {
                lst = new EDAList<S>();
                foreach (var obj in item.Value)
                {
                    lst.Add(obj);
                }
            }

            if (node != null)
            {
                node.Concat(lst);
                last_inserted_node = node;
                return;
            }

            Node<T, S> newNode = new Node<T, S>(new KeyValuePair<T, EDAList<S>>(item.Key, lst));
            if (root == null)
            {
                root = newNode;
            }
            last_inserted_node = newNode;
            AddRecursive(ref root, newNode);
            count++;
        }

        private Node<T, S> AddRecursive(ref Node<T, S> current, Node<T, S> newNode)
        {
            if (current == null)
            {
                return newNode;
            }
            else if (newNode.Key.CompareTo(current.Key) < 0)
            {
                var lft = current.Left_Child;
                current.Left_Child = AddRecursive(ref lft, newNode);
                current.Left_Child.Parent = current;
                current = balance_tree(ref current);
            }
            else if (newNode.Key.CompareTo(current.Key) > 0)
            {
                var rgh = current.Right_Child;
                current.Right_Child = AddRecursive(ref rgh, newNode);
                current.Right_Child.Parent = current;
                current = balance_tree(ref current);
            }
            return current;
        }

        public void Clear()
        {
            root = null;
            count = 0;
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
            IList<S> list = new EDAList<S>();
            list.Add(value);
            Add(new KeyValuePair<T, IList<S>>(key, list));
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
            while (current != null && current.Key.CompareTo(key) != 0)
            {
                current = current.Key.CompareTo(key) < 0 ? current.Left_Child : current.Right_Child;
            }
            return current;
        }

        void RotateRight(ref Node<T, S> current)
        {
            var leftChild = current.Left_Child;
            leftChild.Parent = current.Parent;
            if (current.Parent != null)
            {
                if (current == current.Parent.Left_Child)
                {
                    current.Parent.Left_Child = leftChild;
                }
                else
                {
                    current.Parent.Right_Child = leftChild;
                }
            }
            current.Left_Child = leftChild.Right_Child;
            if (leftChild.Right_Child != null)
                leftChild.Right_Child.Parent = current;
            leftChild.Right_Child = current;
            current.Parent = leftChild;
            current = leftChild;
        }

        void RotateLeft(ref Node<T, S> current)
        {
            var rightChild = current.Right_Child;
            rightChild.Parent = current.Parent;
            if (current.Parent != null)
            {
                if (current == current.Parent.Left_Child)
                {
                    current.Parent.Left_Child = rightChild;
                }
                else
                {
                    current.Parent.Right_Child = rightChild;
                }
            }
            current.Right_Child = rightChild.Left_Child;
            if (rightChild.Left_Child != null)
                rightChild.Left_Child.Parent = current;
            rightChild.Left_Child = current;
            current.Parent = rightChild;
            current = rightChild;
        }

        void RotateLeftRight(ref Node<T, S> current)
        {
            var left = current.Left_Child;
            RotateLeft(ref left);
            RotateRight(ref current);
        }

        void RotateRightLeft(ref Node<T, S> current)
        {
            var right = current.Right_Child;
            RotateRight(ref right);
            RotateLeft(ref current);
        }

        private Node<T, S> balance_tree(ref Node<T, S> current)
        {
            int b_factor = balance_factor(current);
            if (b_factor > 1)
            {
                if (balance_factor(current.Left_Child) > 0)
                {
                    RotateRight(ref current);
                }
                else
                {
                    RotateLeftRight(ref current);
                }
            }
            else if (b_factor < -1)
            {
                if (balance_factor(current.Right_Child) > 0)
                {
                    RotateRightLeft(ref current);
                }
                else
                {
                    RotateLeft(ref current);
                }
            }
            return current;
        }

        private int balance_factor(Node<T, S> current)
        {
            int lheight = (current.Left_Child == null) ? -1 : current.Left_Child.Height;
            int rheight = (current.Right_Child == null) ? -1 : current.Right_Child.Height;
            return lheight - rheight;
        }

        public IEnumerable<Tuple<T, S>> GreaterEgualsThan(T key)
        {
            Node<T, S> current = root;
            Node<T, S> last = root;
            int cmp = current.Key.CompareTo(key);
            while (current != null && cmp != 0)
            {
                last = current;
                current = cmp < 0 ? current.Left_Child : current.Right_Child;
            }
            while(last != null)
            {
                foreach (var item in last.Elements)
                {
                    yield return new Tuple<T, S>(last.Key,item);
                }
                if (last.Right_Child != null)
                {
                    foreach (var item in EntreOrdem(last.Right_Child))
                    {
                        yield return item;
                    }
                }
                last = last.Parent;
            }
        }

        private IEnumerable<Tuple<T, S>> EntreOrdem(Node<T, S> node)
        {
            if (node.Left_Child != null)
            {
                foreach (var item in EntreOrdem(node.Left_Child))
                {
                    yield return item;
                }
            }
            foreach (var item in node.Elements)
            {
                yield return new Tuple<T, S>(node.Key, item);
            }
            if (node.Right_Child != null)
            {
                foreach (var item in EntreOrdem(node.Right_Child))
                {
                    yield return item;
                }
            }
        }

        internal Node<T, S> Last_Inserted_Node { get => last_inserted_node; }

        internal T MinKey{ get {
                if (root == null) { throw new InvalidOperationException("this is an empty tree");
                }
                var current = root;
                while (current.Left_Child != null)
                {
                    current = current.Left_Child;
                }
                return current.Key;
            }
        }

        internal T MaxKey
        {
            get
            {
                if (root == null)
                {
                    throw new InvalidOperationException("this is an empty tree");
                }
                var current = root;
                while (current.Right_Child != null)
                {
                    current = current.Right_Child;
                }
                return current.Key;
            }
        }
    }

    internal class Node<T, S>  
    {
		EDAList<S> [] set; int size; int cnt;
        internal Node(KeyValuePair<T, EDAList<S>> key_value) : base()
        {
            if (key_value.Value == null)
                throw new ArgumentNullException("the value of the key value can not be null");
            Key = key_value.Key;
            size = 300;
            set = new EDAList<S>[size];
            cnt = 0;
            Concat(key_value.Value);
        }

		public void Concat(EDAList<S> lst)
		{
            lst.Reset();
            foreach (var item in lst)
            {
                if (IndexOf(item) == -1)
                { 
                    int code = Math.Abs(item.GetHashCode());
                    if (set[code] == null)
                        set[code] = new EDAList<S>();
                    set[code % size].Add(item);
                    cnt++;
                }
			}
		}

        public int Height {
			get {
				if (Right_Child == null && Left_Child == null) return 0;
				if (Right_Child == null) return Left_Child.Height + 1;
				if (Left_Child == null) return Right_Child.Height + 1;
				return Math.Max(Right_Child.Height, Left_Child.Height)+1;
			}
		}
		internal EDAList<S> Elements
		{
			get
			{
				EDAList<S> lst = new EDAList<S>();
				for (int i = 1; i < 100; i++)
				{
					if (set[i] != null)
					{
                        for(int j = 0; j < set[i].Count; j++)
                        {
                            lst.Add(set[i][j]);
                        }
					}
				}
				return lst;
			}
		}
        internal T Key { get; private set; }
        internal Node<T, S> Left_Child { get; set; }
        internal Node<T, S> Right_Child { get; set; }
        internal Node<T, S> Parent { get; set; }
        private int IndexOf(S value)
        {
            int code = Math.Abs(value.GetHashCode())%size;
            if (set[code] == null) return -1;
            for (int i = 0; i < set[code].Count; i++)
            {
                if (value.Equals(set[code][i]))
                {
                    return i;
                }
            }
            return -1;
        }

        internal bool IsEmpty { get => cnt == 0; }

        internal void Delete(S value)
        {
            int pos = IndexOf(value);
            if (pos == -1)
                throw new Exception("object not found");
            int cod = value.GetHashCode();
            set[cod % size].RemoveAt(pos);
            cnt--;

        }

    }
}
