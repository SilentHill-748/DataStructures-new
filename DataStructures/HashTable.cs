using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class HashTable<TKey, TValue>
    {
        private readonly List<TValue>[] _values;


        public HashTable(int size)
        {
            _values = new List<TValue>[size];
        }


        public void Add(TKey key, TValue value)
        {
            var itemKey = GetHash(key);

            if (_values[itemKey] is null)
                _values[itemKey] = new List<TValue> { value };
            else
                _values[itemKey].Add(value);
        }

        public bool Search(TKey key, TValue value)
        {
            var itemKey = GetHash(key);

            return _values[itemKey]?.Contains(value) ?? false;
        }

        private int GetHash(TKey item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));
            
            string stringItem = item.ToString() ?? string.Empty; // item never be null.

            return (int)(stringItem.Sum(x => char.GetNumericValue(x)));
        }
    }
}
