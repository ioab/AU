namespace AU.Cryptography.Ciphers
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Cipher
    {
        /// <summary>
        /// The defined plain-text (PL) and cipher-text (CT) alphabet dictionary.
        /// The characters are case-insensitive.
        /// </summary>
        public List<char> Alphabet { get; protected set; }

        /// <summary>
        /// Initializes an abstract cipher cryptosystem requirements.
        /// </summary>
        /// <param name="useWhiteSpace">true to include white-space in the alphabet.</param>
        public Cipher(bool useWhiteSpace = false)
        {
            // Add all English letters to our dictionary of texts //
            this.Alphabet = new List<char>(Enumerable.Range('A', 26).Select(x => (char)x));

            // Add white-space, if useSpace flag is true //
            if (useWhiteSpace)
                this.Alphabet.Add(' ');
        }

        public abstract string Encrypt(object key, string plainText);

        public abstract string Decrypt(object key, string cipherText);
    }
}