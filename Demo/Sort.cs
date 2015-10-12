using AU.Lib;
using AU.Sorting;
using System;
using System.Linq;

namespace Demo
{
    internal class Sort
    {
        public static void Demo()
        {
            var sort = new Sorting();

            var list = new[] { 100, 50, 1, 200, 23, 15 };
            foreach (var i in list)
                Console.Write(i + " ");

            //sort.Selection<int>(list, list.Length);
            //Console.WriteLine("\nAftert Sorting ...");
            //foreach (var i in list)
            //    Console.Write(i + " ");

            //sort.Insertion<int>(list, list.Length);
            //Console.WriteLine("\nAftert Sorting ...");
            //foreach (var i in list)
            //    Console.Write(i + " ");

            //
            // O(n) with insertion //
            //

            //var l = new[] { 1, 5, 14, 20, 30 };
            //foreach (var i in l)
            //    Console.Write(i + " ");

            //sort.Insertion<int>(l, l.Length);
            //Console.WriteLine("\nAftert Sorting ...");
            //foreach (var i in l)
            //    Console.Write(i + " ");

            //
            //

            //sort.MergSort<int>(list, 0, list.Length - 1);
            //Console.WriteLine("\nAftert Sorting ...");
            //foreach (var i in list)
            //    Console.Write(i + " ");

            //Console.WriteLine();
            //var listForMerg = new[] { 1.8, 1.5, 0.4, 12, 3.14 , 0};
            //sort.MergSort<double>(listForMerg, 0, listForMerg.Length - 1);
            //foreach (var i in listForMerg)
            //    Console.Write(i + " ");

            //sort.Quick<int>(list, 0, list.Length);
            //Console.WriteLine("\nAftert Sorting ...");
            //foreach (var i in list)
            //    Console.Write(i + " ");

            //Console.WriteLine();
            //var arr = new[] { 1, 2, 2, 1, 1, 2, 2, 1, 2, 1, 2, 1 };
            //arr.ToList().ForEach(i => Console.Write(i + " "));
            //Console.WriteLine();

            //sort.ReallySimpleSort(arr, arr.Length);
            //arr.ToList().ForEach(i => Console.Write(i + " "));

            //Console.WriteLine();
            //var list10 = new[] { 10, 2, 1, 2, 5, 0, 10, 1, 2, 3, 4, 5 };
            //list10.ToList().ForEach(i => Console.Write(i + " "));

            //Console.WriteLine();
            //var sorted = sort.Count(list10, list10.Length, 11);
            //sorted.ToList().ForEach(i => Console.Write(i + " "));

            //Console.WriteLine();
            //sort.Bubble(list, list.Length);
            //list.ToList().ForEach(i => Console.Write(i + " "));

            Console.WriteLine();
            var res = sort.HeapSort(list, list.Length, new PriorityQueue<int>());
            res.ToList().ForEach(i => Console.Write(i + " "));
        }
    }
}