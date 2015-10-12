using AU.Searching;
using System;

namespace Demo
{
    internal class Search
    {
        public static void Demo()
        {
            var s = new Searching();

            //Console.WriteLine(s.LinearSearch(new[] { 100, 10, 93, 15 }, 4, 15));
            //Console.WriteLine(s.LinearSearch(new[] { "Eve", "Sam", "Bell", "Kal" }, 4, "Eve"));

            //Console.WriteLine(s.BetterLinearSearch(new[] { 50, 540.54, 110.258, 0, 47.26 }, 5, 110.258));

            //Console.WriteLine(s.SentinelLinearSearch(new[] { "100", "af", "1" }, 3, "100"));

            //Console.WriteLine(s.RecursiveLinearSearch(new[] { 50, 540.54, 110.258, 0, 47.26 }, 5, 110.258));
            //Console.WriteLine(s.RecursiveLinearSearch(new[] { "100", "af", "1" }, 3, "se"));

            //Console.WriteLine(s.BinarySearch(new[] { 0, 47.26, 50, 54.54, 110.258, 400 }, 6, 110.258));
            //Console.WriteLine(s.BinarySearch(new[] { "avb", "ade", "bcs", "zx" }, 4, "bcs"));

            Console.WriteLine(s.RecursiveBinarySearch(new[] { 0, 47.26, 50, 54.54, 110.258, 400 }, 0, 6, 110.258));
            Console.WriteLine(s.RecursiveBinarySearch(new[] { "avb", "ade", "bcs", "zx" }, 0, 4, "bcs"));
        }
    }
}