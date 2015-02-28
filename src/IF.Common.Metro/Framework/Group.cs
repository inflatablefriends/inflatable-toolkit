using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace IF.Common.Metro.Framework
{
    public class Group<T, U> : IGrouping<T, U>
    {
        private IEnumerable<U> _items;

        public Group(IEnumerable<U> items, T key)
        {
            _items = items;
            Key = key;
        }

        public T Key { get; private set; }

        public IEnumerator<U> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
