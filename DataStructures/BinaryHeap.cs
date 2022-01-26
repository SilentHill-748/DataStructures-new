using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class BinaryHeap<T> : IEnumerable<T>
        where T : IComparable
    {
        private readonly List<T> _items;

        public int Count => _items.Count;


        public BinaryHeap()
        {
            _items = new List<T>();
        }

        public BinaryHeap(IEnumerable<T> items)
            : this()
        {
            _items.AddRange(items);
            for (int i = Count; i >= 0; i--)
                Sort(i);
        }


        public T? Peek()
        {
            if (Count < 0)
                return default;

            return _items[0];
        }

        public void Add(T item)
        {
            _items.Add(item);
            RebalanceHeap();
        }

        public T GetMax()
        {
            T result = _items[0];

            _items[0] = _items[Count - 1];
            _items.RemoveAt(Count - 1);

            Sort(0);

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            while (Count > 0)
                yield return GetMax();
        }

        public void Sort(int currentIndex)
        {
            int leftIndex, 
                rightIndex, 
                maxIndex = currentIndex;

            while (currentIndex < Count)
            {
                leftIndex = (2 * currentIndex) + 1;
                rightIndex = (2 * currentIndex) + 2;

                if (leftIndex < Count && _items[leftIndex].CompareTo(_items[maxIndex]) > 0)
                {
                    maxIndex = leftIndex;
                }

                if (rightIndex < Count && _items[rightIndex].CompareTo(_items[maxIndex]) > 0)
                {
                    maxIndex = rightIndex;
                }

                if (maxIndex == currentIndex)
                    break;

                Swap(currentIndex, maxIndex);
                currentIndex = maxIndex;
            }
        }

        private void RebalanceHeap()
        {
            int current = Count - 1;
            int parent = GetParenIndex(current);
             
            while ((_items[parent].CompareTo(_items[current]) < 0) &&
                   (current > 0))
            {
                Swap(current, parent);

                current = parent;
                parent = GetParenIndex(current);
            }
        }

        private void Swap(int indexA, int indexB)
        {
            T temp = _items[indexA];
            _items[indexA] = _items[indexB];
            _items[indexB] = temp;
            
        }

        private static int GetParenIndex(int currentIndex) => 
            (currentIndex - 1) / 2;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
