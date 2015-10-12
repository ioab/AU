namespace AU.Lib
{
    using System;

    /// <summary>
    /// The Rabin-Miller primality test algorithm.
    /// </summary>
    /// <see cref="http://rosettacode.org/wiki/Miller-Rabin_primality_test#Csharp"/>
    public static class RabinMiller
    {
        /// <summary>
        /// Runs the Rabin-Miller primality check.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns>True means &quot;probably prime&quot;. False means &quot;definitely composite.&quot;</returns>
        public static bool IsPrime(uint n, uint k)
        {
            if (n < 2)
            {
                return false;
            }

            if (n != 2 && n % 2 == 0)
            {
                return false;
            }

            uint s = n - 1;

            while (s % 2 == 0)
            {
                s >>= 1;
            }

            Random r = new Random();
            for (uint i = 0; i < k; i++)
            {
                double a = r.Next((int)n - 1) + 1;
                uint temp = s;
                uint mod = (uint)Math.Pow(a, (double)temp) % n;

                while (temp != n - 1 && mod != 1 && mod != n - 1)
                {
                    mod = (mod * mod) % n;
                    temp = temp * 2;
                }

                if (mod != n - 1 && temp % 2 == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}