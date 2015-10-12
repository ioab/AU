namespace AU.Searching
{
    using System;

    public class Searching
    {
        public Searching()
        { }

        /// <summary>
        /// The traditional linear search procedure.
        /// </summary>
        /// <typeparam name="T">The type of struct &quot; value type &quot; or class that implements IComparable.</typeparam>
        /// <param name="array">an array of type T.</param>
        /// <param name="size">the number of elements in array to search through.</param>
        /// <param name="key">the value being searched for.</param>
        /// <returns>
        /// Either an index i for which array[i] = key, or the special value NOT-FOUND,
        /// which could be any invalid index into the array, such as any negative integer.
        /// </returns>
        public int LinearSearch<T>(T[] array, int size, T key) where T : IComparable<T>
        {
            int answer = NotFound();

            for (int i = 0; i < size; ++i)
            {
                if (array[i].CompareTo(key) == 0)
                    answer = i;
            }
            return answer;
        }

        /// <summary>
        /// An enhanced version of linear search algorithm. Stops when key is found instead of continue searching.
        /// Inputs and output: Same as LINEAR-SEARCH.
        /// </summary>
        public int BetterLinearSearch<T>(T[] array, int size, T key) where T : IComparable<T>
        {
            for (int i = 0; i < size; i++)
            {
                if (array[i].CompareTo(key) == 0)
                    return i;
            }
            return NotFound();
        }

        /// <summary>
        /// A slightly enhanced version of linear search algorithm. One checking instead of two in the loop.
        /// Inputs and output: Same as LINEAR-SEARCH.
        /// </summary>
        public int SentinelLinearSearch<T>(T[] array, int size, T key) where T : IComparable<T>
        {
            var last = array[size - 1];
            array[size - 1] = key;

            int i = 0;
            while (array[i].CompareTo(key) != 0)
            {
                i++;
            }

            array[size - 1] = last;

            if (i < size - 1 || array[size - 1].CompareTo(key) == 0)
                return i;

            return NotFound();
        }


        /// <summary>
        /// An recursion version of linear search algorithm.
        /// Inputs: Same as LINEAR-SEARCH, but with an added parameter i.
        /// </summary>
        /// <param name="i"> The initial or current index. For algorithm internal usage. No need to change its default value.</param>
        /// <returns>
        /// The index of an element equaling key in the sub-array from array[i] through array[size - 1],
        /// or NOT-FOUND if key does not appear in this sub-array.
        /// </returns>
        public int RecursiveLinearSearch<T>(T[] array, int size, T key, int i = 0) where T : IComparable<T>
        {
            if (i >= size)
            {
                return NotFound();
            }

            if (array[i].CompareTo(key) == 0)
                return i;
            else
                return RecursiveLinearSearch(array, size, key, i + 1);
        }

        /// <summary>
        /// The binary search procedure.
        /// Inputs and output: Same as LINEAR-SEARCH.
        /// </summary>
        /// <param name="array">A sorted array of type T.</param>
        public int BinarySearch<T>(T[] array, int size, T key) where T : IComparable<T>
        {
            int p = 0,
                q = 0,
                r = size - 1;

            while (p <= r)
            {
                q = (p + r) / 2;

                if (array[q].CompareTo(key) == 0)
                    return q;
                else if (array[q].CompareTo(key) > 0)
                    r = q - 1;
                else
                    p = q + 1;
            }

            return NotFound();
        }

        /// <summary>
        /// The recursive version of binary search procedure.
        /// Inputs: (array and key) are the same as LINEAR-SEARCH, also the output. 
        /// </summary>
        /// <param name="p">The initial or current index. For algorithm internal usage, Set it always to 0.</param>
        /// <param name="r">The size of array in the function initial call.</param>
        public int RecursiveBinarySearch<T>(T[] array, int p, int r, T key) where T : IComparable<T>
        {
            if (p > r)
                return NotFound();

            int q = (p + r) / 2;

            if (array[q].CompareTo(key) == 0)
                return q;
            else if (array[q].CompareTo(key) > 0)
                return RecursiveBinarySearch(array, p, q - 1, key);
            else
                return RecursiveBinarySearch(array, q + 1, r, key);
        }

        /// <summary>
        /// A procedure to indicate that the search key does not exist.
        /// </summary>
        /// <returns>A special value which is invalid array index.</returns>
        public int NotFound()
        {
            return -1;
        }

        /// <summary>
        /// A recursive implementation for math factorial.
        /// </summary>
        /// <param name="n">An integer n greater than or equal to 0.</param>
        /// <returns>The value of (n)! .</returns>
        public uint Factorial(uint n)
        {
            if (n <= 0)
                return 1;
            else
                return n * Factorial(n - 1u);
        }
    }
}