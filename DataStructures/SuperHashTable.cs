using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class SuperHashTable<T>
    {
        private readonly Item<T>[] _items;


        public SuperHashTable(int size)
        {
            _items = new Item<T>[size];
            for (int i = 0; i < size; i++)
                _items[i] = new Item<T>(i);
        }


        public void Add(T item)
        {
            var key = GetHash(item);
            _items[key].Nodes.Add(item);
        }

        public bool Search(T item)
        {
            var key = GetHash(item);
            return _items[key].Nodes.Contains(item);
        }

        private int GetHash(T item)
        {
            return item.GetHashCode() % _items.Length;
        }
    }

    public class Item<T>
    {
        public int Key { get; set; }

        public List<T> Nodes { get; set; }


        public Item(int key)
        {
            Key = key;
            Nodes = new List<T>();
        }
    }
}
