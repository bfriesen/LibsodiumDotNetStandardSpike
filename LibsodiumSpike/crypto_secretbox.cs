using System.Runtime.InteropServices;

namespace LibsodiumSpike
{
    partial class Sodium
    {
        private const int crypto_secretbox_ZEROBYTES = 32;

        private const int crypto_secretbox_BOXZEROBYTES = 16;

        /// <summary>
        /// The number of bytes in a secretbox key.
        /// </summary>
        public const int crypto_secretbox_KEYBYTES = 32;

        /// <summary>
        /// The number of bytes in a secretbox mac.
        /// </summary>
        public const int crypto_secretbox_MACBYTES = crypto_secretbox_ZEROBYTES - crypto_secretbox_BOXZEROBYTES;

        /// <summary>
        /// The number of bytes in a secretbox nonce.
        /// </summary>
        public const int crypto_secretbox_NONCEBYTES = 24;

        /// <summary>
        /// <para>
        /// The <see cref="crypto_secretbox_easy"/> function encrypts a message <paramref name="m"/> whose 
        /// length is <paramref name="mlen"/> bytes, with a key <paramref name="k"/> and a nonce
        /// <paramref name="n"/>.
        /// </para>
        /// <para>
        /// <paramref name="k"/> should be <see cref="crypto_secretbox_KEYBYTES"/> bytes and
        /// <paramref name="n"/> should be <see cref="crypto_secretbox_NONCEBYTES"/> bytes.
        /// </para>
        /// <para>
        /// <paramref name="c"/> should be at least <see cref="crypto_secretbox_MACBYTES"/> +
        /// <paramref name="mlen"/> bytes long.
        /// </para>
        /// <para>
        /// This function writes the authentication tag, whose length is <see cref="crypto_secretbox_MACBYTES"/>
        /// bytes, in <paramref name="c"/>, immediately followed by the encrypted message, whose length is the
        /// same as the plaintext: <paramref name="mlen"/>.
        /// </para>
        /// <para>
        /// <paramref name="c"/> and <paramref name="m"/> can overlap, making in-place encryption possible.
        /// However do not forget that <see cref="crypto_secretbox_MACBYTES"/> extra bytes are required to
        /// prepend the tag.
        /// </para>
        /// </summary>
        /// <param name="c">
        /// The resulting ciphertext. Should be at least <see cref="crypto_secretbox_MACBYTES"/> +
        /// <paramref name="mlen"/> bytes long.
        /// </param>
        /// <param name="m">The message to encrypt.</param>
        /// <param name="mlen">The length of message <paramref name="m"/>.</param>
        /// <param name="n">The nonce. Should be <see cref="crypto_secretbox_NONCEBYTES"/> bytes.</param>
        /// <param name="k">The key. Should be <see cref="crypto_secretbox_KEYBYTES"/> bytes.</param>
        /// <returns></returns>
        [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
        public static extern int crypto_secretbox_easy(byte[] c, byte[] m, long mlen, byte[] n, byte[] k);

        /// <summary>
        /// <para>
        /// The <see cref="crypto_secretbox_open_easy"/> function verifies and decrypts a ciphertext
        /// produced by <see cref="crypto_secretbox_easy"/>.
        /// </para>
        /// <para>
        /// <paramref name="c"/> is a pointer to an authentication tag + encrypted message combination,
        /// as produced by <see cref="crypto_secretbox_easy"/>. <paramref name="clen"/> is the length
        /// of this authentication tag + encrypted message combination. Put differently,
        /// <paramref name="clen"/> is the number of bytes written by <see cref="crypto_secretbox_easy"/>,
        /// which is <see cref="crypto_secretbox_MACBYTES"/> + the length of the message.
        /// </para>
        /// <para>
        /// The nonce <paramref name="n"/> and the key <paramref name="k"/> have to match the used to
        /// encrypt and authenticate the message.
        /// </para>
        /// <para>
        /// The function returns -1 if the verification fails, and 0 on success. On success, the
        /// decrypted message is stored into <paramref name="m"/>.
        /// </para>
        /// <para>
        /// <paramref name="m"/> and <paramref name="c"/> can overlap, making in-place decryption possible.
        /// </para>
        /// </summary>
        /// <param name="m">
        /// The resulting message. Should be at least <paramref name="clen"/> -
        /// <see cref="crypto_secretbox_MACBYTES"/> bytes long.
        /// </param>
        /// <param name="c">
        /// A pointer to an authentication tag + encrypted message combination, as produced by
        /// <see cref="crypto_secretbox_easy"/>.
        /// </param>
        /// <param name="clen">
        /// The length of the authentication tag + encrypted message combination.
        /// </param>
        /// <param name="n">
        /// The nonce. Must match the nonce used to encrypt and authenticate the message.
        /// </param>
        /// <param name="k">
        /// The key. Must match the key used to encrypt and authenticate the message.
        /// </param>
        /// <returns>-1 if the verification fails, 0 on success.</returns>
        [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
        public static extern int crypto_secretbox_open_easy(byte[] m, byte[] c, long clen, byte[] n, byte[] k);
    }
}
