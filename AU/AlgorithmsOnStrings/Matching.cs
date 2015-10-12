namespace AU.AlgorithmsOnStrings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Matching
    {
        /// <summary>
        /// The simple string matcher procedure.
        /// Running time: 
        ///     Θ((n - m) . m)
        /// </summary>
        /// <param name="text">The haystack string to search for pattern into.</param>
        /// <param name="pattern">The needle string.</param>
        /// <returns>An enumerable of the matches found and there shifts amount.</returns>
        public static IEnumerable<string> SimpleMatcher(string text, string pattern)
        {
            int n = text.Length,
                m = pattern.Length;

            if (n == 0 || m == 0 || m > n)
                yield break;

            for (int i = 0; i < n; ++i)
            {
                int j = 0,
                    k = i;

                while (j < m)
                {
                    if (pattern[j] != text[k])
                        break;

                    ++j;
                    ++k;
                }

                if (j == m)
                    yield return string.Format("Pattern {0} occurs with shift {1} in text {2}", pattern, i, text);
            }
        }

        /// <summary>
        /// The finite automaton string matcher.
        /// Time Complexity:
        ///     Θ(n)
        /// </summary>
        /// <param name="text">A text string represents the input.</param>
        /// <param name="n">The length of text string.</param>
        /// <param name="m">the length of the pattern string.
        /// The next-state table has rows indexed by 0 to m, and columns indexed by the characters that may occur in the text.
        /// </param>
        /// <param name="nextState">the table of state transitions, formed according to the pattern being matched.</param>
        /// <returns>An enumerable of all the shift amounts for which the pattern occurs in the text.</returns>
        public static IEnumerable<string> FAStringMatcher(string text, int n, int m, int[,] nextState)
        {
            int state = 0;

            for (int i = 0; i < n; ++i)
            {
                state = nextState[state, text[i]];

                if (state == m)
                    yield return string.Format("The pattern occurs with shift: {0}", (i - m) + 1);  // +1 because our index starts at 0. //
            }
        }

        /// <summary>
        /// The build next-state table procedure.
        /// Required to run finite-automaton string matcher.
        /// Time Complexity:
        ///     Θ(m^3 . q)
        /// </summary>
        /// <returns>the table of state transitions of FA, formed according to the pattern being matched.</returns>
        public static int[,] BuildNextStateTable(string pattern)
        {
            // Alphabet is ASCII characters. //
            char[] alphabet = Enumerable.Range(0, 127).Select(c => (char)c).ToArray();

            int n = alphabet.Length,
                m = pattern.Length;

            int[,] nextState = new int[m + 1, n + 1];

            foreach (var ch in alphabet)
            {
                for (int k = 0; k <= m; ++k)
                {
                    string concat = pattern.Substring(0, k) + ch;

                    int i = Math.Min(k + 1, m);

                    while (!concat.EndsWith(pattern.Substring(0, i)))
                        --i;

                    nextState[k, ch] = i;
                }
            }

            return nextState;
        }
    }
}