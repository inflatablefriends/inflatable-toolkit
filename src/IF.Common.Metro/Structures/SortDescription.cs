using System;
using System.Threading.Tasks;

namespace IF.Common.Metro.Structures
{
    public class SortDescriptionAsync<T>
    {
        public SortDescriptionAsync(string propertyName, Func<T, Task<object>> property, ListSortDirection direction)
        {
            PropertyName = propertyName;
            GetProperty = property;
            Direction = direction;
        }

        public string PropertyName { get; private set; }
        public Func<T, Task<object>> GetProperty { get; private set; }

        public ListSortDirection Direction { get; set; }
    }
}