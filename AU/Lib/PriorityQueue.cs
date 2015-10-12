﻿namespace AU.Lib
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A min-heap priority queue implementation.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the Queue. Comparer type required.</typeparam>
    /// <author>Allan Riordan</author>
    /// <seealso cref="http://allanrbo.blogspot.com/2011/12/simple-heap-implementation-priority.html"/>
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> data = new List<T>();

        public void Insert(T o)
        {
            data.Add(o);

            int i = data.Count - 1;
            while (i > 0)
            {
                int j = (i + 1) / 2 - 1;

                // Check if the invariant holds for the element in data[i]
                T v = data[j];
                if (v.CompareTo(data[i]) < 0 || v.CompareTo(data[i]) == 0)
                {
                    break;
                }

                // Swap the elements
                T tmp = data[i];
                data[i] = data[j];
                data[j] = tmp;

                i = j;
            }
        }

        public T ExtractMin()
        {
            if (data.Count < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            T min = data[0];
            data[0] = data[data.Count - 1];
            data.RemoveAt(data.Count - 1);
            this.MinHeapify(0);
            return min;
        }

        public T Peek()
        {
            return data[0];
        }

        public int Count
        {
            get { return data.Count; }
        }

        /// <summary>
        /// performs whatever bookkeeping is necessary in Q to record that shortest[v]
        /// was decreased by a call of RELAX (In Dijkstra’s algorithm).
        /// </summary>
        public void DecreaseKey(int index)
        {
            this.MinHeapify(index);
        }

        private void MinHeapify(int i)
        {
            int smallest;
            int l = 2 * (i + 1) - 1;
            int r = 2 * (i + 1) - 1 + 1;

            if (l < data.Count && (data[l].CompareTo(data[i]) < 0))
            {
                smallest = l;
            }
            else
            {
                smallest = i;
            }

            if (r < data.Count && (data[r].CompareTo(data[smallest]) < 0))
            {
                smallest = r;
            }

            if (smallest != i)
            {
                T tmp = data[i];
                data[i] = data[smallest];
                data[smallest] = tmp;
                this.MinHeapify(smallest);
            }
        }
    }
}