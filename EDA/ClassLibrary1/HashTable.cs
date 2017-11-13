using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDA
{
    public class HashTable<T,S> : IHashTable<T,S> , IEnumerable<Tuple<T,S>>, IEnumerator<Tuple<T, S>>
    {
		private IList<Tuple<T, S>>[] _items;
        private int _fillFactor = 3;
        private int _size;
        int current__arr_pos, current_lst_pos;

        public Tuple<T, S> Current => _items[current__arr_pos][current_lst_pos];

        object IEnumerator.Current => throw new NotImplementedException();

        public S this[T index]
        {
            get { return Get(index).Item2; }
            set {
                var pos = GetPosition(index);
                for (int i = 0; i < _items[pos].Count; i++)
                {
                    if (_items[pos][i].Item1.Equals(index))
                    {
                        _items[pos][i] = new Tuple<T, S>(index, value);
                        return;
                    }
                }
                throw new KeyNotFoundException("chave nao encontrada");
            }
        }

        public HashTable()
        {
            _size = 0;
            _items = new EDAList<Tuple<T, S>>[_fillFactor];
            current_lst_pos =current__arr_pos = -1;
        }

		public void Add(T key, S value)
        {
            var pos = GetPosition(key);
            if (_items[pos] == null)
            {
                _items[pos] = new EDAList<Tuple<T, S>>();
            }
            if (_items[pos].Any(x=>x.Item1.Equals(key)))
            {
                throw new Exception("Chave duplicada, não pode ser inserida.");
            }
            _size++;
            if (Inserir())
            {
                InserindoHash();
            }

            pos = GetPosition(key);
            if (_items[pos] == null)
            {
                _items[pos] = new EDAList<Tuple<T, S>>();
            }
            _items[pos].Insert(0,new Tuple<T, S>(key, value));
        }

        public void Remove(T key)
        {
            var pos = GetPosition(key);
            if (_items[pos] != null)
            {
                var objToRemove = _items[pos].FirstOrDefault(item => item.Item1.Equals(key));
                if (objToRemove == null) throw new KeyNotFoundException("O valor não exite.");
                _items[pos].Remove(objToRemove);
                _size--;
            }
            else
            {
                throw new KeyNotFoundException("O valor não exite.");
            }
        }

        private Tuple<T, S> Get(T key)
        {
            var pos = GetPosition(key);
            if (_items[pos] == null) throw new KeyNotFoundException("Chave não existe.");
            ((EDAList<Tuple<T, S>>)_items[pos]).Reset();
            foreach (var item in _items[pos])
            {
                return item;
            }
            throw new KeyNotFoundException("Chave não existe.");
        }

        public bool Contain(T key)
        {
            var pos = GetPosition(key);
            if (_items[pos] == null) return false;
            ((EDAList<Tuple<T, S>>)_items[pos]).Reset();
            foreach (var item in _items[pos])
            {
                if(item.Item1.Equals(key))
                    return true;
            }
            return false;
        }

        private void InserindoHash()
        {
            _fillFactor *= 2;
            var newItems = new EDAList<Tuple<T, S>>[_fillFactor];
            foreach (var item in _items)
            {
                if (item != null)
                {
                    ((EDAList<Tuple<T, S>>)item).Reset();
                    foreach (var value in item)
                    {
                        var pos = GetPosition(value.Item1);
                        if (newItems[pos] == null)
                        {
                            newItems[pos] = new EDAList<Tuple<T, S>>();
                        }
                        newItems[pos].Insert(0, new Tuple<T, S>(value.Item1, value.Item2));
                    }
                }
            }
            _items = newItems;
        }

        private int GetPosition(T key)
        {
            var hash = Math.Abs(key.GetHashCode());
            var pos = hash % _fillFactor;
            return pos;
        }

        private bool Inserir()
        {
            return _size >= _fillFactor;
        }

        public void Push(T key, S value)
        {
            Add(key,value);
        }

        public void Delete(T key)
        {
            Remove(key);
        }

        public IEnumerator<Tuple<T, S>> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (current__arr_pos < _items.Length)
            {
                if (current__arr_pos == -1)
                {
                    current__arr_pos = 0;
                    current_lst_pos = -1;
                }
                if (_items[current__arr_pos] != null && _items[current__arr_pos].Count - 1 > current_lst_pos)
                {
                    current_lst_pos++;
                    return true;
                }
                else
                {
                    int i = current__arr_pos+1;
                    for (;i < _items.Length && (_items[i] == null || _items[i].Count == 0); i++) ;
                    if (i >= _items.Length) return false;
                    current__arr_pos = i;
                    current_lst_pos = 0;
                    return true;                    
                }
            }
            return false;
        }

        public void Reset()
        {
            current_lst_pos = current__arr_pos = -1;
        }

        public void Dispose()
        {            
        }
    } 
}
   