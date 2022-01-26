namespace DataStructures
{
    public class QuickSort
    {
        public void Sort(int[] list, int l, int r)
        {
            if (l < r)
            {
                int p = Partition(list, l, r);
                Sort(list, l, p);
                Sort(list, p + 1, r);
            }
        }

        private int Partition(int[] array, int low, int high)
        {
            int pivot = array[(low + high) / 2];
            int i = low;
            int j = high;

            while (i <= j)
            {
                while (array[i] < pivot)
                    i++;
                while (array[j] > pivot)
                    j--;
                if (i >= j)
                    return j;

                Swap(ref array[i++], ref array[j--]);
            }
            return j;
        }

        private void Swap(ref int x, ref int y)
        {
            int temp = x;
            x = y;
            y = temp;
        }
    }
}
