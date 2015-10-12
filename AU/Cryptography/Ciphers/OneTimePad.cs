namespace AU.Cryptography.Ciphers
{
    using System;
    using System.Text;

    /// <summary>
    /// The one time pad cryptographic system.
    /// </summary>
    public class OneTimePad : Cipher
    {
        private bool RequireEqualKeyLength { get; set; }

        /// <summary>
        /// Initializes the one time pad cipher cryptosystem.
        /// </summary>
        /// <param name="useWhiteSpace">true to include white-space in the alphabet.</param>
        /// <param name="requireEqualKeyLength">
        /// - True to force the key be the same length as the (P) or (C) or longer,
        /// which is one side of the perfect secrecy requirements.
        /// - False to allow key to be shorter than (P) or (C). Wrapping around as needed. (Vegener's Code).
        /// </param>
        public OneTimePad(bool useWhiteSpace = false, bool requireEqualKeyLength = true)
            : base(useWhiteSpace)
        {
            this.RequireEqualKeyLength = requireEqualKeyLength;
        }

        /// <summary>
        /// The one time pad cipher encryption algorithm.
        /// </summary>
        /// <param name="key">An string to mask the plain-text. Must be used when decrypting the cipher later.</param>
        /// <param name="plainText">The message to be encrypted.</param>
        /// <returns>A cipher text converted from the given message.</returns>
        public override string Encrypt(object key, string plainText)
        {
            string pad = CheckPadLength((string)key, plainText);

            var cipher = new StringBuilder();

            for (int i = 0; i < plainText.Length; ++i)
                cipher.Append((char)(pad[(i % pad.Length)] ^ plainText[i]));

            return cipher.ToString();
        }

        /// <summary>
        /// The one time pad cipher decryption algorithm.
        /// </summary>
        /// <param name="key">An string to unmask the cipher. Must be the same used to encrypt the message.</param>
        /// <param name="cipherText">The cipher to be decrypted.</param>
        /// <returns>A plain text converted from the given cipher.</returns>
        public override string Decrypt(object key, string cipherText)
        {
            string pad = CheckPadLength((string)key, cipherText);

            var message = new StringBuilder();

            for (int i = 0; i < cipherText.Length; ++i)
                message.Append((char)(pad[(i % pad.Length)] ^ cipherText[i]));

            return message.ToString();
        }

        /// <summary>
        /// Validates the pad length according to (P) or (C) text, if consumer forced it.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// If consumer enabled RequireEqualKeyLength flag, and pad is shorter than the text.
        /// </exception>
        private string CheckPadLength(string pad, string text)
        {
            if (!RequireEqualKeyLength)
                return pad;

            if (pad.Length < text.Length)
                throw new ArgumentException("Key has to have the same length as the plain/cipher text or longer.");

            return pad;
        }
    }
}