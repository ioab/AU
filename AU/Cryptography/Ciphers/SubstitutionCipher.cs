namespace AU.Cryptography.Ciphers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The substitution cipher cryptographic system.
    /// </summary>
    public class SubstitutionCipher : Cipher
    {
        public SubstitutionCipher(bool useWhiteSpace = false)
            : base(useWhiteSpace)
        { }

        /// <summary>
        /// The substitution cipher encryption algorithm.
        /// </summary>
        /// <param name="key">A dictionary with type parameters &lt;char,char&gt; represents the substitution book the parties agree upon.</param>
        /// <param name="plainText">The message to be encrypted.</param>
        /// <returns>A cipher text converted from the given message.</returns>
        public override string Encrypt(object key, string plainText)
        {
            var substitutionBook = key as Dictionary<char, char>;
            if (key == null)
                throw new ArgumentException("Substitution Book is not a valid dictionary<char, char> type.");

            var cipher = new StringBuilder();

            foreach (var letter in plainText.ToUpper())
            {
                /**
                 *  Follow the substitution book.
                 *  Keep the original as a fallback if no entry found in the book
                 */
                if (substitutionBook.ContainsKey(letter))
                    cipher.Append(substitutionBook[letter]);
                else
                    cipher.Append(letter);
            }
            return cipher.ToString();
        }

        /// <summary>
        /// The substitution cipher decryption algorithm.
        /// </summary>
        /// <param name="key">A dictionary with type parameters &lt;char,char&gt; represents the substitution book the parties agree upon.</param>
        /// <param name="cipherText">The cipher to be decrypted.</param>
        /// <returns>A plain text converted from the given cipher.</returns>
        public override string Decrypt(object key, string cipherText)
        {
            var substitutionBook = key as Dictionary<char, char>;
            if (key == null)
                throw new ArgumentException("Substitution Book is not a valid dictionary<char, char> type.");

            var message = new StringBuilder();

            foreach (var letter in cipherText)
            {
                /**
                 *  Follow the substitution book.
                 *  Keep the original as a fallback if no entry found in the book
                 */
                var result = substitutionBook.Values.Where(v => v == letter);
                if (result.Count() > 0)
                    message.Append(substitutionBook.First(x => x.Value == letter).Key);
                else
                    message.Append(letter);
            }

            return message.ToString().ToLower();
        }
    }
}