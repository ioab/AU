namespace AU.Cryptography.Ciphers
{
    using AU.Cryptography.Helpers;
    using AU.Cryptography.Primes;
    using System;

    /// <summary>
    /// A simulated implementation of RSA.
    /// To meet RSA requirements for large primes (at least 1024 bits = 309 decimals),
    /// we would need integer types bigger than uint (e.g.: System.Numeric.BigInteger).
    /// </summary>
    public class RSA
    {
        #region Properties and Fields

        /// <summary>
        /// A set of fields used internally by RSA.
        /// </summary>
        private uint _p, _q, _n, _r, _e, _d;

        /// <summary>
        /// The RSA public Key pair P = (e, n).
        /// Pass it to anyone.
        /// </summary>
        public Tuple<uint, uint> PublicKey
        {
            get
            {
                return new Tuple<uint, uint>(_e, _n);
            }
        }

        /// <summary>
        /// The RSA secret Key pair S = (d, n).
        /// Keep it secret. Accessibility is public, so the owner can review it.
        /// </summary>
        public Tuple<uint, uint> PrivateKey
        {
            get
            {
                return new Tuple<uint, uint>(_d, _n);
            }
        }

        /// <summary>
        /// The Public-Key function.
        /// Used to encrypt plain-texts by any party who wants to contact with the owner.
        /// </summary>
        /// <remarks>Known publicly. Pass it to anyone.</remarks>
        public Func<uint, uint> FP { get; private set; }

        /// <summary>
        /// The Private-Key function.
        /// Used to decrypt cipher-texts by the owner only.
        /// </summary>
        /// <remarks>Revealed to nobody. Accessibility is public, so the owner can use it for decrypt.</remarks>
        public Func<uint, uint> FS { get; private set; }

        /// <summary>
        /// A pluggable primality test function to used internally by RSA.
        /// If not supplied during construction, a fallback to Naive-Division occurs.
        /// </summary>
        public Func<uint, bool> PrimesTest { get; protected set; }

        /// <summary>
        /// The range to limit the length of primes generation step in the algorithm.
        /// Should be at least 1024 bits = 309 decimal,
        /// But uint limits us to much smaller value.
        /// </summary>
        private uint Range { get; set; }

        #endregion Properties and Fields

        #region Constructors

        /// <summary>
        /// The initializer of RSA cryptosystem.
        /// A version enables the encipherer to provide her initial p and q.
        /// </summary>
        /// <param name="p">A prime integer.</param>
        /// <param name="q">Another prime integer.</param>
        /// <param name="primalityTest">An optional plugin primality test algorithm. Defaults to naive division.</param>
        public RSA(uint p, uint q, Func<uint, bool> primalityTest = null)
        {
            /**
             * fallback to default primality test function if no one is provided
             */
            PrimesTest = primalityTest != null ? primalityTest : (n) => NaivePrimalityTest.IsPrime(n);

            if (!PrimesTest(p))
                throw new ArgumentException("p is not a prime number");

            if (!PrimesTest(q))
                throw new ArgumentException("q is not a prime number");

            this._p = p;
            this._q = q;
        }

        /// <summary>
        /// The initializer of RSA cryptosystem.
        /// p and q are generated automatically in this overload.
        /// </summary>
        /// <param name="range">A range m to search for prime numbers under it.</param>
        /// <param name="primalityTest">An optional plugin primality test algorithm. Defaults to naive division.</param>
        /// <remarks>p and q will be generated pseudorandomly.</remarks>
        public RSA(uint range, Func<uint, bool> primalityTest = null)
        {
            PrimesTest = primalityTest != null ? primalityTest : (n) => NaivePrimalityTest.IsPrime(n);

            this.Range = range;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Runs the RSA algorithm and produce the needed encryption and decryption functions.
        /// </summary>
        public void Compute()
        {
            // Generate private large integers p, q if invalid ones supplied //
            if (this._p == 0 || this._q == 0)
                GeneratePrimes();

            // Calculate the values n and r //
            _n = _p * _q;
            _r = (_p - 1) * (_q - 1);

            // Compute the values of e and its (*) inverse d //
            ComputeEAndD();

            // Define the RSA cryptosystem (E/D) functions //
            FS = (x) => (uint)ModularExponentiation.Compute(x, _e, _n);
            FP = (x) => (uint)ModularExponentiation.Compute(x, _d, _n);
        }

        /// <summary>
        /// Generates the private primes p and q if the user didn't provide one.
        /// </summary>
        private void GeneratePrimes()
        {

            do
            {
                uint i = (uint)new Random().Next(3, (int)Range - 1);

                if (PrimesTest(i))
                    _p = i;

            } while (_p == 0);

            do
            {
                uint i = (uint)new Random().Next(3, (int)Range - 1);

                if (PrimesTest(i) && i != _p)
                    _q = i;

            } while (_q == 0);
        }

        /// <summary>
        /// Calculates a small odd integer e that is relatively prime to r.
        /// The only common divisor of e and r should be 1.
        /// </summary>
        private void ComputeEAndD()
        {
            for (uint i = 5; i < this._r; i = i + 2)
            {
                var candidateE = Euclid.Compute((long)this._r, (long)i);

                if (candidateE.Item1 == 1)
                {
                    this._e = i;

                    /**
                     *  d = j % r, in which j is in: GCD of (r,e) = (a.i + b.j) ..
                     */
                    long d = candidateE.Item3 % (uint)this._r;

                    /**
                     *  Avoid nonnegative j cases ..
                     */
                    while (d < 0)
                    {
                        d = (d + (uint)this._r) % (uint)this._r;
                    }

                    this._d = (uint)d;

                    break;
                }
            }
        }

        /// <summary>
        /// Overloading ToString() to provide informative form of the public-key set to others
        /// </summary>
        /// <returns>A string contains the PK pairs and the PK function.</returns>
        public override string ToString()
        {
            return string.Format("[My Pk is: (e = {0}, n = {1})], \n[PK function is: PK(x) = x^e xor n ,where x: your message]", _e, _n);
        }

        #endregion Methods
    }
}