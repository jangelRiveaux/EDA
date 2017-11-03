using System;
using System.Collections.Generic;
using System.Text;

namespace EDA
{
    public interface IHashTable<T,S> 
    {
        S this[T index] { get; set; }
        void Push(T key, S value);
        void Delete(T key);
    }
}
