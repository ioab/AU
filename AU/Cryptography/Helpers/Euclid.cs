namespace AU.Cryptography.Helpers
{
    using System;

    /// <summary>
    /// The Euclid algorithm which calculate the great common divisor (GCD) of two integers.
    /// </summary>
    public static class Euclid
    {
        /// <summary>
        /// Run the Euclid procedure.
        /// Input:
        ///     a,b : two integers.
        /// </summary>
        /// <returns>A triple Tuple&gt;g, i, j&lt; such that g is the greatest common divisor of a and b and g = a.i + b.j</returns>
        public static Tuple<long, long, long> Compute(long a, long b)
        {
            if (b == 0)
                return new Tuple<long, long, long>(a, 1, b);

            var result = Euclid.Compute(b, a % b);

            long g = result.Item1,
                _i = result.Item2,
                _j = result.Item3;

            long i = _j,
                j = _i - ((long)Math.Floor((double)a / b) * _j);

            return new Tuple<long, long, long>(g, i, j);
        }
    }
}