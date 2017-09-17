using System.Runtime.InteropServices;

namespace LibsodiumSpike
{
    partial class Sodium
    {
        /// <summary>
        /// The number of zero bytes that are padded at the beginning of the message for the
        /// <see cref="crypto_secretbox"/> and <see cref="crypto_secretbox_open"/> methods.
        /// </summary>
        public const int crypto_secretbox_ZEROBYTES = 32;

        /// <summary>
        /// The number of zero bytes that are padded at the beginning of the ciphertext for the
        /// <see cref="crypto_secretbox"/> and <see cref="crypto_secretbox_open"/> methods.
        /// </summary>
        public const int crypto_secretbox_BOXZEROBYTES = 16;

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

        /// <summary>
        /// The <see cref="crypto_secretbox_detached"/> function encrypts a message <paramref name="m"/>
        /// of length <paramref name="mlen"/> with a key <paramref name="k"/> and a nonce
        /// <paramref name="n"/>, and puts the encrypted message into <paramref name="c"/>. Exactly
        /// <paramref name="mlen"/> bytes will be put into <paramref name="c"/>, since this function
        /// does not prepend the authentication tag. The tag, whose size is
        /// <see cref="crypto_secretbox_MACBYTES"/> bytes, will be put into <paramref name="mac"/>.
        /// </summary>
        /// <param name="c">
        /// The resulting ciphertext. Should be at least <paramref name="mlen"/> bytes long.
        /// </param>
        /// <param name="mac">
        /// The resulting tag. Should be <see cref="crypto_secretbox_MACBYTES"/> bytes.
        /// </param>
        /// <param name="m">The message.</param>
        /// <param name="mlen">The length of the message.</param>
        /// <param name="n">The nonce. Should be <see cref="crypto_secretbox_NONCEBYTES"/> bytes.</param>
        /// <param name="k">The key. Should be <see cref="crypto_secretbox_KEYBYTES"/> bytes.</param>
        /// <returns></returns>
        [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
        public static extern int crypto_secretbox_detached(byte[] c, byte[] mac, byte[] m, long mlen, byte[] n, byte[] k);

        /// <summary>
        /// <para>
        /// The <see cref="crypto_secretbox_open_detached"/> function verifies and decrypts an encrypted
        /// message <paramref name="c"/> whose length is <paramref name="clen"/>. <paramref name="clen"/>
        /// doesn't include the tag, so this length is the same as the plaintext.
        /// </para>
        /// <para>
        /// The plaintext is put into <paramref name="m"/> after verifying that <paramref name="mac"/> is
        /// a valid authentication tag for this ciphertext, with the given nonce <paramref name="n"/> and
        /// key <paramref name="k"/>.
        /// </para>
        /// <para>
        /// The function returns -1 if the verification fails, or 0 on success.
        /// </para>
        /// </summary>
        /// <param name="m">
        /// The resulting message. Should be at least <paramref name="clen"/> bytes long.
        /// </param>
        /// <param name="c">The ciphertext, as produced by <see cref="crypto_secretbox_detached"/>.</param>
        /// <param name="mac">
        /// The authentication tag, as produced by <see cref="crypto_secretbox_detached"/>. Should be
        /// <see cref="crypto_secretbox_MACBYTES"/> bytes.
        /// </param>
        /// <param name="clen">The length of the ciphertext.</param>
        /// <param name="n">The nonce. Should be <see cref="crypto_secretbox_NONCEBYTES"/> bytes.</param>
        /// <param name="k">The key. Should be <see cref="crypto_secretbox_KEYBYTES"/> bytes.</param>
        /// <returns>-1 if the verification fails, 0 on success.</returns>
        [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
        public static extern int crypto_secretbox_open_detached(byte[] m, byte[] c, byte[] mac, long clen, byte[] n, byte[] k);

        /// <summary>
        /// <para>
        /// The <see cref="crypto_secretbox"/> function encrypts and authenticates a message
        /// <paramref name="m"/> using a secret key <paramref name="k"/> and a nonce
        /// <paramref name="n"/>. The <see cref="crypto_secretbox"/> function puts the ciphertext
        /// into <paramref name="c"/>. It then returns 0.
        /// <para>
        /// </para>
        /// The caller must ensure, before calling the <see cref="crypto_secretbox"/> function,
        /// that the first <see cref="crypto_secretbox_ZEROBYTES"/> bytes of the message
        /// <paramref name="m"/> are all 0. Typical higher-level applications will work with the
        /// remaining bytes of the message; note, however, that <paramref name="mlen"/> counts
        /// all of the bytes, including the bytes required to be 0.
        /// <para>
        /// </para>
        /// Similarly, the <see cref="crypto_secretbox"/> function ensures that the first
        /// <see cref="crypto_secretbox_BOXZEROBYTES"/> bytes of the ciphertext <paramref name="c"/>
        /// are all 0.
        /// </para>
        /// </summary>
        /// <param name="c">
        /// The resulting ciphertext. Should be at least <paramref name="mlen"/> bytes long.
        /// </param>
        /// <param name="m">
        /// The message. Should be padded at the beginning with <see cref="crypto_secretbox_ZEROBYTES"/>
        /// zero bytes.
        /// </param>
        /// <param name="mlen">The length of the message, including zero bytes.</param>
        /// <param name="n">The nonce. Should be <see cref="crypto_secretbox_NONCEBYTES"/> bytes.</param>
        /// <param name="k">The key. Should be <see cref="crypto_secretbox_KEYBYTES"/> bytes.</param>
        /// <returns></returns>
        [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
        public static extern int crypto_secretbox(byte[] c, byte[] m, long mlen, byte[] n, byte[] k);

        /// <summary>
        /// <para>
        /// The <see cref="crypto_secretbox_open"/> function verifies and decrypts a ciphertext
        /// <paramref name="c"/> using a secret key <paramref name="k"/> and a nonce
        /// <paramref name="n"/>. The <see cref="crypto_secretbox_open"/> function puts the
        /// plaintext into <paramref name="m"/>. It then returns 0.
        /// </para>
        /// <para>
        /// If the ciphertext fails verification, <see cref="crypto_secretbox_open"/> instead
        /// returns -1, possibly after modifying <paramref name="m"/>.
        /// </para>
        /// <para>
        /// The caller must ensure, before calling the <see cref="crypto_secretbox_open"/> function,
        /// that the first <see cref="crypto_secretbox_BOXZEROBYTES"/> bytes of the ciphertext
        /// <paramref name="c"/> are all 0. The <see cref="crypto_secretbox_open"/> function ensures
        /// (in case of success) that the first <see cref="crypto_secretbox_ZEROBYTES"/> bytes of
        /// the plaintext <paramref name="m"/> are all 0.
        /// </para>
        /// </summary>
        /// <param name="m">
        /// The resulting message. Should be at least <paramref name="clen"/> bytes long.
        /// </param>
        /// <param name="c">
        /// The ciphertext. Should be padded at the beginning with
        /// <see cref="crypto_secretbox_BOXZEROBYTES"/> zero bytes.
        /// </param>
        /// <param name="clen">The length of the ciphertext, includeing zero bytes.</param>
        /// <param name="n">The nonce. Should be <see cref="crypto_secretbox_NONCEBYTES"/> bytes.</param>
        /// <param name="k">The key. Should be <see cref="crypto_secretbox_KEYBYTES"/> bytes.</param>
        /// <returns>-1 if the verification fails, 0 on success.</returns>
        [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
        public static extern int crypto_secretbox_open(byte[] m, byte[] c, long clen, byte[] n, byte[] k);
    }
}
