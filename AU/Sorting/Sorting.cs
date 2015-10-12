namespace AU.Sorting
{
    using AU.Lib;
    using System;

    public class Sorting
    {
        /// <summary>
        /// The selection sort procedure.
        /// Result:
        ///     elements of array are sorted into nondecreasing order.
        /// </summary>
        /// <typeparam name="T">The type of struct &quot; value type &quot; or class that implements IComparable.</typeparam>
        /// <param name="array">an array of type T.</param>
        /// <param name="size">the number of elements in array.</param>
        public void Selection<T>(T[] array, int size) where T : IComparable<T>
        {
            int smallest = 0;

            for (int i = 0; i < size - 1; ++i)
            {
                smallest = i;
                for (int j = i + 1; j < size; ++j)
                {
                    if (array[j].CompareTo(array[smallest]) < 0)
                        smallest = j;
                }
                Swap(ref array[i], ref array[smallest]);
            }
        }

        /// <summary>
        /// The insertion sort procedure.
        /// Inputs and result:
        ///     Same as SELECTION().
        /// </summary>
        public void Insertion<T>(T[] array, int size) where T : IComparable<T>
        {
            T key = default(T);
            int j = 0;

            for (int i = 1; i < size; i++)
            {
                key = array[i];
                j = i - 1;

                while (j >= 0 && array[j].CompareTo(key) > 0)
                {
                    array[j + 1] = array[j];
                    j--;
                }

                array[j + 1] = key;
            }
        }

        /// <summary>
        /// The bubble sort procedure.
        /// Inputs and result:
        ///     Same as SELECTION().
        /// </summary>
        /// <remarks>Not covered in the book. algolist.net provides a good explanation.</remarks>
        /// <seealso cref="http://www.algolist.net/Algorithms/Sorting/Bubble_sort"/>
        public void Bubble<T>(T[] array, int size) where T : IComparable<T>
        {
            bool swapepd = true;
            int j = 0;

            while (swapepd)
            {
                swapepd = false;
                j++;
                for (int i = 0; i < size - j; ++i)
                {
                    if (array[i].CompareTo(array[i + 1]) > 0)
                    {
                        Swap(ref array[i], ref array[i + 1]);
                        swapepd = true;
                    }
                }
            }
        }

        /// <summary>
        /// The merge sort procedure. A divide and conquer algorithm.
        /// Result:
        ///     The elements of the sub-array array[p .. r] are sorted into nondecreasing order.
        /// </summary>
        /// <typeparam name="T">The type of struct &quot; value type &quot; or class that implements IComparable.</typeparam>
        /// <param name="array">an array of type T.</param>
        /// <param name="p">The starting index of array. Set it always to 0.</param>
        /// <param name="r">The ending index of array, i.e. : the size.</param>
        public void MergSort<T>(T[] array, int p, int r) where T : IComparable<T>, new()
        {
            if (p >= r)
                return;

            //int q = (int)Math.Floor((double)(p + r) / 2);
            int q = (p + r) / 2;
            MergSort(array, p, q);
            MergSort(array, q + 1, r);

            Merg(array, p, q, r, Infinity<T>());
        }

        /// <summary>
        /// The internal sorting &quot;conquer&quot; part of the merge sort procedure.
        /// Result:
        ///     The sub-array array[p .. r] contains the elements originally in array[p .. q] and array[q + 1 .. r],
        ///     but now the entire sub-array array[p .. r] is sorted.
        /// </summary>
        /// <param name="p">indexes into array. Each of the sub-arrays array[p .. q] and array[q + 1.. r] is assumed to be already sorted.</param>
        /// <param name="infinity">The infinity or max value of the type T.</param>
        private void Merg<T>(T[] array, int p, int q, int r, T infinity) where T : IComparable<T>
        {
            int n1 = q - p + 1,
                n2 = r - q;

            T[] B = new T[n1 + 1],
                C = new T[n2 + 1];

            for (int i = 0; i < n1; ++i)
            {
                B[i] = array[p + i];
            }

            for (int j = 0; j < n2; ++j)
            {
                C[j] = array[q + j + 1];
            }

            B[n1] = infinity;
            C[n2] = infinity;

            int n = 0,
                m = 0;

            for (int k = p; k <= r; ++k)
            {
                if (B[n].CompareTo(C[m]) <= 0)
                {
                    array[k] = B[n];
                    n++;
                }
                else
                {
                    array[k] = C[m];
                    m++;
                }
            }
        }

        /// <summary>
        /// The quick sort procedure.
        /// Inputs and Result:
        ///     Same as MERGE-SORT.
        /// </summary>
        public void Quick<T>(T[] array, int p, int r) where T : IComparable<T>
        {
            if (p >= r)
                return;

            int q = Partition(array, p, r);
            Quick(array, p, q);
            Quick(array, q + 1, r);
        }

        /// <summary>
        /// Quick sort partitioning part procedure.
        /// Inputs:
        ///     Same as MERGE-SORT()
        /// Result:
        ///     Rearranges the elements of array[p .. r] so that every element in
        ///     array[p .. q ] is less than or equal to array[q] and every element in
        ///     array[q + 1 .. r] is greater than array[q]. Returns the index q to the caller.
        /// </summary>
        /// <returns>The index q to the caller</returns>
        private int Partition<T>(T[] array, int p, int r) where T : IComparable<T>
        {
            int q = p;
            for (int u = p; u < r - 1; ++u)
            {
                if (array[u].CompareTo(array[r - 1]) <= 0)
                {
                    Swap(ref array[u], ref array[q]);
                    q++;
                }
            }
            Swap(ref array[q], ref array[r - 1]);
            return q;
        }

        /// <summary>
        /// A trivial procedure to illustrate the idea behind count sort techniques,
        /// and how to beat the lower bond O(n log n) in sorting.
        /// </summary>
        /// <param name="array">An array in which each element is either 1 or 2.</param>
        /// <param name="size">The size of the array.</param>
        public void ReallySimpleSort(int[] array, int size)
        {
            int k = 0;
            for (int i = 0; i < size; ++i)
            {
                if (array[i] == 1)
                    k++;
            }

            for (int i = 0; i <= k; ++i)
            {
                array[i] = 1;
            }

            for (int i = k + 1; i < size; ++i)
            {
                array[i] = 2;
            }
        }

        /// <summary>
        /// The count sort procedure. 
        /// Strategy: Sorts an array without comparing pairs of its elements against each other.
        /// </summary>
        /// <param name="array">an array of integers in the range 0 to (range - 1).</param>
        /// <param name="size">The number of elements in array.</param>
        /// <param name="range">Defines the range of the values in array.</param>
        /// <returns>A sorted version the array.</returns>
        /// <remarks>
        /// you can see that COUNTING-SORT runs in time O(m + n), or O(n) when m is a constant.
        /// Counting sort beats the lower bound of O(n lg n) for comparison sorting because it never compares sort keys against each other. 
        /// Instead, it uses sort keys to index into arrays, which it can do because the sort keys are small integers. 
        /// If the sort keys were real numbers with fractional parts, or they were character strings, then we could not use counting sort.
        /// </remarks>
        public int[] Count(int[] array, int size, int range)
        {
            var equal = CountKeysEqual(array, size, range);

            var less = CountKeysLess(equal, range);

            return Rearrange(array, less, size, range);
        }

        /// <summary>
        /// Count-Key-Equal part procedure for the counting sort.
        /// Inputs and Result:
        ///     Same as COUNT().
        /// </summary>
        /// <returns>
        /// An array equal[0 .. range -1] such that equal[j] contains the number of elements of array that equals j,
        /// for j = 0, 1, 2 .., range - 1.
        /// </returns>
        private int[] CountKeysEqual(int[] array, int size, int range)
        {
            var equal = new int[range];
            int key;

            for (int i = 0; i < size; ++i)
            {
                key = array[i];
                equal[key]++;
            }
            return equal;
        }

        /// <summary>
        /// Count-Key-Less part procedure for the counting sort.
        /// </summary>
        /// <param name="equal">The array returned by COUNT-KEYS-EQUAL.</param>
        /// <param name="range">Defines the index range of equal: 0 to [range - 1]</param>
        /// <returns>
        /// An array less[0 .. range - 1] such that for j = 0, 1, 2 .. range -1,
        /// less[j] contains the sum equal[0] + equal[1] + .. + equal[j - 1].
        /// </returns>
        private int[] CountKeysLess(int[] equal, int range)
        {
            var less = new int[range];
            less[0] = 0;
            for (int j = 1; j < range; ++j)
            {
                less[j] = less[j - 1] + equal[j - 1];
            }
            return less;
        }

        /// <summary>
        /// Rearrange part procedure for count sorting.
        /// Inputs and Result:
        ///     Same as COUNT(). 
        ///     plus, less array returned by procedure Count-Key-Less().
        /// </summary>
        private int[] Rearrange(int[] array, int[] less, int size, int range)
        {
            int[] sorted = new int[size],
                next = new int[range];

            int key,
                index;

            for (int j = 0; j < range; ++j)
            {
                next[j] = less[j] + 1;
            }

            for (int i = 0; i < size; ++i)
            {
                key = array[i];
                index = next[key];
                sorted[index - 1] = array[i];
                next[key]++;
            }
            return sorted;
        }

        /// <summary>
        /// The heap sort procedure. Sorts in O(n log n).
        /// Result:
        ///     elements of array are sorted into nondecreasing order.
        /// </summary>
        /// <typeparam name="T">The type of struct &quot; value type &quot; or class that implements IComparable.</typeparam>
        /// <param name="array">an array of type T.</param>
        /// <param name="size">the number of elements in array.</param> 
        /// <param name="binaryHeap">A new instance of PriorityQueue of type parameter T.</param>
        /// <remarks>Its idea covered in chapter (6) : &quot; Shortest Path &quot; .</remarks>
        public T[] HeapSort<T>(T[] array, int size, PriorityQueue<T> binaryHeap) where T : IComparable<T>
        {
            foreach (var i in array)
                binaryHeap.Insert(i);

            var b = new T[size];

            for (int i = 0; i < size; ++i)
                b[i] = binaryHeap.ExtractMin();

            return b;
        }

        /// <summary>
        /// A utility function to exchange two elements of type T.
        /// </summary>
        protected void Swap<T>(ref T x, ref T y)
        {
            T temp = x;
            x = y;
            y = temp;
        }

        /// <summary>
        /// Helper to get the max value of a type parameter.
        /// </summary>
        /// <returns>The max value of the type if exists. Otherwise, an exception is thrown.</returns>
        /// <exception cref="ArgumentException">Thrown if the type does not have a max value.</exception>
        protected T Infinity<T>() where T : new()
        {
            // Try find infinity //
            var infinity = typeof(T).GetField("PositiveInfinity");
            if (infinity != null)
                return (T)infinity.GetValue(new T());

            // Try find MaxValue //
            var max = typeof(T).GetField("MaxValue");
            if (max != null)
                return (T)max.GetValue(new T());

            throw new ArgumentException("Type T does not have an infinity or max value.");
        }
    }
}