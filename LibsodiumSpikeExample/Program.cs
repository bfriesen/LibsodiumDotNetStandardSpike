using LibsodiumSpike;
using System;
using System.Text;

namespace LibsodiumSpikeExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var message = Encoding.UTF8.GetBytes("Hello, world!");

            var nonce = new byte[Sodium.crypto_secretbox_NONCEBYTES];
            var key = new byte[Sodium.crypto_secretbox_KEYBYTES];
            var ciphertext = new byte[Sodium.crypto_secretbox_MACBYTES + message.Length];

            Sodium.randombytes_buf(nonce, nonce.Length);
            Sodium.randombytes_buf(key, key.Length);
            Sodium.crypto_secretbox_easy(ciphertext, message, message.Length, nonce, key);

            var decrypted = new byte[message.Length];
            if (Sodium.crypto_secretbox_open_easy(decrypted, ciphertext, ciphertext.Length, nonce, key) != 0)
            {
                Console.WriteLine("Message forged!");
            }

            Console.WriteLine("Key, base-64 encoded: {0}", Convert.ToBase64String(key));
            Console.WriteLine("Nonce, base-64 encoded: {0}", Convert.ToBase64String(nonce));
            Console.WriteLine("Original message, UTF-8 decoded: {0}", Encoding.UTF8.GetString(message));
            Console.WriteLine("Original message, base-64 encoded: {0}", Convert.ToBase64String(message));
            Console.WriteLine("Encrypted message, base-64 encoded: {0}", Convert.ToBase64String(ciphertext));
            Console.WriteLine("Decrypted message, base-64 encoded: {0}", Convert.ToBase64String(decrypted));
            Console.WriteLine("Decrypted message, UTF-8 decoded: {0}", Encoding.UTF8.GetString(decrypted));
        }
    }
}
