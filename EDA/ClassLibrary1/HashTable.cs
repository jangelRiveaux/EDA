using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EDA
{
    public class HashTable<T,S> : IHashTable<T,S> where T : IComparable
    {
		private LinkedList<Tuple<T, S>>[] _items;
        private int _fillFactor = 3;
        private int _size;

        public HashTable()
        {
            _items = new LinkedList<Tuple<T, S>>[];
        }

		public void Add(T key, S value)
        {
            var pos = GetPosition(key, _items.Length);
            if (_items[pos] == null)
            {
                _items[pos] = new LinkedList<Tuple<T, S>>();
            }
            if (_items[pos].Any(x=>x.Item1.Equals(key)))
            {
                throw new Exception("Chave duplicada, não pode ser inserida.");
            }
            _size++;

            if (inserir())
            {
                InserindoHash();
            }
            pos = GetPosition(key, _items.Length);
            if (_items[pos] == null)
            {
                _items[pos] = new LinkedList<Tuple<T, TU>>();
            }
            _items[pos].AddFirst(new Tuple<T, TU>(key, value));
        }

        public void Remove(T key)
        {
            var pos = GetPosition(key, _items.Length);
            if (_items[pos] != null)
            {
                var objToRemove = _items[pos].FirstOrDefault(item => item.Item1.Equals(key));
                if (objToRemove == null) return;
                _items[pos].Remove(objToRemove);
                _size--;
            }
            else
            {
                throw new Exception("O valor não exite.");
            }
        }

        public S Get(T key)
        {
            var pos = GetPosition(key, _items.Length);
            foreach (var item in _items[pos].Where(item => item.Item1.Equals(key)))
            {
                return item.Item2;
            }
            throw new Exception("Chave não existe.");
        }

        private void InserindoHash()
        {
            _fillFactor *= 2;
            var newItems = new LinkedList<Tuple<T, S>>[_items.Length * 2];
            foreach (var item in _items.Where(x=>x != null))
            {
                foreach (var value in item)
                {
                    var pos = GetPosition(value.Item1, newItems.Length);
                    if (newItems[pos] == null)
                    {
                        newItems[pos] = new LinkedList<Tuple<T, S>>();
                    }
                    newItems[pos].AddFirst(new Tuple<T, S>(value.Item1, value.Item2));
                }
            }
            _items = newItems;
        }

        private int GetPosition(T key, int length)
        {
            var hash = key.GetHashCode();
            var pos = Math.Abs(hash % length);
            return pos;
        }

        private bool Inserir()
        {
            return _size >= _fillFactor;
        }
    }

	//-----------------------------------------------------------------Anne--------------------------------------------
		static hashtable GetHashtable()
		{
			
				Hashtable hashtable = new Hashtable();
				for(int i = 0; i < this.IList<S>.count; i++ ){
					hashtable.Add(IList<S>[i]);

				}
								
				return hashtable;
        }

		int count;
        Node<T,S> root;

        public IList<S> this[T index]
        {
		get
            {
                Node<T, S> current = GetNode(index);
                if (index < this.IList<S>.count)
				   return this.IList<S>[index];
				else
					throw new IndexOutOfRangeException("O índice está fora dos limites.");
        }

        set
        {
            this.IList<S>[index] = value;
        }

		public void Push(KeyValuePair<T, IList<S>> item)
		{
			//criar uma pilha
			Stack <hashtable> pilha = new Stack <hashtable>();
			for(int i = 0; i < this.IList<S>.count; i++ ){
					foreach (IList<S>[i] in hashtable)
					{
					pilha.push(IList<S>[i]);
					}
			}
            
        }

		// 
		public void Delete(IList<S>[index])
		{
			if(hashtable.ContainsKey(IList<S>[index]))
				hashtable.Remove(IList<S>[index]);
			else
				throw new IndexOutOfRangeException("Chave inexistente.");
		}
		 
}
   