namespace AU.AlgorithmsOnStrings
{
    using System;
    using System.Text;

    /// <summary>
    /// The common longest sequence algorithms.
    /// </summary>
    public static class LCS
    {
        /// <summary>
        /// The compute-LCS-table procedure.
        /// Time Complexity:
        ///     O(m . n)
        /// </summary>
        /// <param name="x">A string of arbitrary length m.</param>
        /// <param name="y">A string of arbitrary length n.</param>
        /// <returns>
        /// The array l[0 .. m, 0 .. n].
        /// The value of l[m, n] is the length of a longest common subsequence of X and Y.
        /// </returns>
        public static int[,] ComputeLCSTable(string x, string y)
        {
            int m = x.Length,
                n = y.Length;

            var l = new int[m == 0 ? 1 : m, n == 0 ? 1 : n];    // Handle empty string edge case //

            /**
             *  The next 2 loops not needed. Int[] initialized to 0s by default.
             *  kept them for clarity ..
             */
            for (int i = 0; i < m; ++i)
                l[i, 0] = 0;

            for (int j = 0; j < n; ++j)
                l[0, j] = 0;

            /**
             *  PseudoCode in book produces a bug, that is, ignoring the first letter in LCS ..
             *  (Workaround) : start the array from 0 instead of 1 ..
             *  The following lambda is to ensure safe indexing.
             */
            Func<int, int> ind = (d) => d == -1 ? 0 : d;

            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (x[i] == y[j])
                        l[i, j] = l[ind(i - 1), ind(j - 1)] + 1;
                    else
                        l[i, j] = Math.Max(l[i, ind(j - 1)], l[ind(i - 1), j]);
                }
            }
            return l;
        }

        /// <summary>
        /// The assemble-LCS procedure.
        /// Time Complexity:
        ///     O(m + n)
        /// </summary>
        /// <param name="x">A string of arbitrary length m.</param>
        /// <param name="y">A string of arbitrary length n.</param>
        /// <param name="l">The array filled in by the COMPUTE-LCS-TABLE() procedure.</param>
        /// <param name="i">An index into X, as well as into l, (i.e : X.Length - 1).</param>
        /// <param name="j">An index into Y, as well as into l, (i.e : Y.Length - 1).</param>
        /// <returns>An LCS of Xi and Yj.</returns>
        public static string AssembleLCS(string x, string y, int[,] l, int i, int j)
        {
            if (i < 0 || j < 0 || l[i, j] == 0)
                return string.Empty;

            if (x[i] == y[j])
            {
                return new StringBuilder(AssembleLCS(x, y, l, i - 1, j - 1)).Append(x[i]).ToString();
            }

            if (l[i, j - 1] > l[i - 1, j])
            {
                return AssembleLCS(x, y, l, i, j - 1);
            }
            else
            {
                return AssembleLCS(x, y, l, i - 1, j);
            }
        }
    }
}