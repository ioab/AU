namespace AU.Cryptography.Helpers
{
    using System;

    /// <summary>
    /// The modular exponential algorithm applies repeated squaring strategy
    /// which raises input to (d)th power in time of O(log d).
    /// </summary>
    public static class ModularExponentiation
    {
        /// <summary>
        /// The modular exponentiation procedure.
        /// </summary>
        /// <param name="x">A nonnegative ulongeger.</param>
        /// <param name="d">A nonnegative ulongeger.</param>
        /// <param name="n">A positive ulongeger (&gt; 0).</param>
        /// <returns>The value of x^d mod n.</returns>
        public static ulong Compute(ulong x, ulong d, ulong n)
        {
            if (d == 0) return 1;

            ulong z;
            if (d % 2 == 0)
            {
                z = ModularExponentiation.Compute(x, d / 2, n);
                return (ulong)Math.Pow(z, 2) % n;
            }
            else
            {
                z = ModularExponentiation.Compute(x, (d - 1) / 2, n);
                return (ulong)(Math.Pow(z, 2) * x) % n;
            }
        }
    }
}