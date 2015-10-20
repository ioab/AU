namespace Demo
{
    using AU.Cryptography.Ciphers;
    using AU.Cryptography.Helpers;
    using AU.Cryptography.Primes;
    using System;
    using System.Collections.Generic;

    internal class Cryptography
    {
        public static void Demo()
        {
            #region Ciphers

            var cipher = AliceSubstitute();
            BobSubstituteBack(cipher);

            Console.WriteLine();

            var cipher2 = AliceShift();
            BobShiftBack(cipher2);

            Console.WriteLine();

            var ciphet3 = AliceVigenered();
            BobVigeneredBack(ciphet3);

            Console.WriteLine();

            var cipher4 = AliceOneTimePadded();
            BobOneTimePadded(cipher4);

            Console.WriteLine("\nEve decrypted it: \t {0}  \t?!! Nice try though :)\n", Eve(cipher4));

            #endregion Ciphers

            #region RSA

            // Euclid //
            Console.WriteLine("(GCD) of 30, 18 Tuple<g, i, j>: {0}", Euclid.Compute(30, 18));

            // ModularExponentiation //
            Console.WriteLine("259^269 mod 493 = {0}", ModularExponentiation.Compute(259, 269, 493));

            RSA();

            #endregion RSA

            #region Primes

            //Console.WriteLine("\n-> Primes Tests and Factoring ...");
            //Console.WriteLine(NaivePrimalityTest.IsPrime(11));
            //Console.WriteLine(NaivePrimalityTest.IsPrime(541));
            //Console.WriteLine(NaivePrimalityTest.IsPrime(102));

            //Console.WriteLine();
            //foreach (var prime in SieveOfEratosthenes.ComputePrimes(100))
            //    Console.Write(prime + " | ");

            #endregion Primes

            Console.ReadKey();
        }

        private static void RSA()
        {
            Console.WriteLine("\nNow -->RSA ..");

            // The Book Example //
            var rsa = new RSA(17u, 29u);
            rsa.Compute();

            Console.WriteLine("PK : " + rsa.PublicKey + "\nSK : " + rsa.PrivateKey);

            var rsaCipherText = rsa.FS(327);
            Console.WriteLine("Encrypted : {0}", rsaCipherText);

            var rsaPlainText = rsa.FP(rsaCipherText);
            Console.WriteLine("Decrypted : {0}", rsaPlainText);

            Console.WriteLine(rsa);

            Console.WriteLine();

            // Another Example.. //
            var rsa2 = new RSA(500u);
            rsa2.Compute();

            var alice = rsa2.FS(49);

            var bob = rsa2.FP(alice);

            Console.WriteLine("Ecy : {0} \nDec : {1}.", alice, bob);

            Console.WriteLine(rsa2);
        }

        private static string AliceSubstitute()
        {
            var substituteBook = new Dictionary<char, char>()
            {
                {'A' , 'Z'},
                {'E', 'M'},
                {'M', 'E'},
                {'Z', 'Y'},
                {'Y', 'A'},
            };

            return (new SubstitutionCipher(useWhiteSpace: true).Encrypt(substituteBook, "Okay you win zzz"));
        }

        private static void BobSubstituteBack(string cipher)
        {
            var substituteBook = new Dictionary<char, char>()
            {
                {'A' , 'Z'},
                {'E', 'M'},
                {'M', 'E'},
                {'Z', 'Y'},
                {'Y', 'A'},
            };

            var whatDidSheSent = new SubstitutionCipher(useWhiteSpace: true).Decrypt(substituteBook, cipher);
            Console.WriteLine("Hehe, I got her substituted message secretly: \n {0}", whatDidSheSent);
        }

        private static string AliceShift()
        {
            return (new ShiftCipher(useWhiteSpace: true).Encrypt(3, "Hi Bob Love ya"));
        }

        private static void BobShiftBack(string cipher)
        {
            var whatDidSheSent = new ShiftCipher(useWhiteSpace: true).Decrypt(3, cipher);
            Console.WriteLine("Hehe, I got her shifted message secretly: \n {0}", whatDidSheSent);
        }

        private static string AliceVigenered()
        {
            return new OneTimePad(true, requireEqualKeyLength: false).Encrypt("My Secret Key", "I love you bob. See you on Friday night, dear.");
        }

        private static void BobVigeneredBack(string cipher)
        {
            var whatDidSheSent = new OneTimePad(true, requireEqualKeyLength: false).Decrypt("My Secret Key", cipher);
            Console.WriteLine("Hehe, I got her vigener coded message secretly: \n {0}", whatDidSheSent);
        }

        private static string AliceOneTimePadded()
        {
            return new OneTimePad(true).Encrypt("Ya We Know that", "I love you bob!");
        }

        private static void BobOneTimePadded(string cipher)
        {
            // P an K are both 15 characters //
            var whatDidSheSent = new OneTimePad(true).Decrypt("Ya We Know that", cipher);
            Console.WriteLine("Hehe, I got her one time padded message secretly: \n {0}", whatDidSheSent);
        }

        private static string Eve(string cipher)
        {
            return new OneTimePad(true).Decrypt("Eve is brillant", cipher);
        }
    }
}