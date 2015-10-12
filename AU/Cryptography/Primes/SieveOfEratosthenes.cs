namespace AU.Cryptography.Primes
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A simple, ancient algorithm for finding all prime numbers up to any given limit.
    /// Time Complexity: 
    ///     O(n log (log n))
    /// </summary>
    /// <remarks>
    /// It does so by iteratively marking as composite (i.e., not prime) the multiples of each prime, starting with the multiples of 2.
    /// </remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes"/>
    /// <seealso cref="http://www.algolist.net/Algorithms/Number_theoretic/Sieve_of_Eratosthenes"/>
    public static class SieveOfEratosthenes
    {
        public static IEnumerable<uint> ComputePrimes(uint limit)
        {
            uint squareRoot = (uint)Math.Sqrt(limit);

            bool[] isComposite = new bool[limit + 1];

            for (uint i = 2; i <= squareRoot; ++i)
            {
                if (!isComposite[i])
                {
                    for (uint k = i * i; k <= limit; k += i)
                        isComposite[k] = true;
                }
            }

            for (uint i = 2; i <= limit; ++i)
                if (!isComposite[i])
                    yield return i;
        }
    }
}