namespace AU.Cryptography.Primes
{
    using System;

    /// <summary>
    /// The naive approach for testing primality, which is generally a trial division technique.
    /// Complexity:
    ///     O(√n / ln n).
    /// </summary>
    /// <see cref="https://en.wikipedia.org/wiki/Primality_test"/>
    public static class NaivePrimalityTest
    {
        public static bool IsPrime(uint number)
        {
            if (number <= 1)
            {
                return false;
            }
            else if (number <= 3)
            {
                return true;
            }
            else if (number % 2 == 0 || number % 3 == 0)
            {
                return false;
            }
            else
            {
                for (uint d = 5; d <= (uint)Math.Sqrt((double)number); d = d + 2)
                    if (number % d == 0)
                        return false;

                return true;
            }
        }
    }
}