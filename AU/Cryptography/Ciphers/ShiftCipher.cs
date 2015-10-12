namespace AU.Cryptography.Ciphers
{
    using System;
    using System.Text;

    /// <summary>
    /// The shift cipher cryptographic system.
    /// </summary>
    public class ShiftCipher : Cipher
    {
        public ShiftCipher(bool useWhiteSpace = false)
            : base(useWhiteSpace)
        { }

        /// <summary>
        /// The shift cipher encryption algorithm.
        /// </summary>
        /// <param name="key">An integer for the shift amount that the parties agree upon.</param>
        /// <param name="plainText">The message to be encrypted.</param>
        /// <returns>A cipher text converted from the given message.</returns>
        public override string Encrypt(object key, string plainText)
        {
            int x = 0,
                y = 0;

            var cipher = new StringBuilder();

            foreach (var letter in plainText.ToUpper())
            {
                x = Convert(letter);
                y = (x + (int)key) % Alphabet.Count;

                cipher.Append(Map(y));
            }

            return cipher.ToString();
        }

        /// <summary>
        /// The shift cipher decryption algorithm.
        /// </summary>
        /// <param name="key">An integer for the shift amount that the parties agree upon.</param>
        /// <param name="cipherText">The cipher to be decrypted.</param>
        /// <returns>A plain text converted from the given cipher.</returns>
        public override string Decrypt(object key, string cipherText)
        {
            int x = 0,
                y = 0;

            var message = new StringBuilder();
            foreach (var letter in cipherText)
            {
                y = Convert(letter);
                x = (y - (int)key) % Alphabet.Count;

                message.Append(Map(x));
            }

            return message.ToString().ToLower();
        }

        /// <summary>
        /// Transforms the character to its numeric position in the alphabet dictionary.
        /// </summary>
        /// <param name="letter">A valid letter in the defined characters set.</param>
        /// <returns>A number that represents the letter position.</returns>
        private int Convert(char letter)
        {
            return Alphabet.FindIndex(l => l == letter);
        }

        /// <summary>
        /// Maps a numeric index to its corresponding letter in the alphabet dictionary.
        /// </summary>
        /// <param name="position">A valid number that represents a position in defined characters set.</param>
        /// <returns>A letter from the alphabet set in the requested position.</returns>
        private char Map(int position)
        {
            if (position < 0)
            {
                position = Alphabet.Count - Math.Abs(position);
            }

            return Alphabet[position];
        }
    }
}