using System;
using System.Collections.Generic;
using System.Text;

namespace EDA
{
    public interface IAVL_Tree<T,S>: ICollection<KeyValuePair<T,IList<S>>>
    {
        void InsertElement(T key, S value);
        IList<S> this[T index] { get; }
    }
}
