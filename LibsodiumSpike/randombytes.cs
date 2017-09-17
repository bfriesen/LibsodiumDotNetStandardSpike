using System.Runtime.InteropServices;

namespace LibsodiumSpike
{
    partial class Sodium
    {
        /// <summary>
        /// The <see cref="randombytes_random"/> function returns an unpredictable value between
        /// 0 and 0xffffffff (included).
        /// </summary>
        /// <returns>A value between 0 and 0xffffffff (included).</returns>
        [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
        public extern static uint randombytes_random();

        /// <summary>
        /// The <see cref="randombytes_uniform"/> function returns an unpredictable value between
        /// 0 and <paramref name="upper_bound"/> (excluded). Unlike randombytes_random() % upper_bound,
        /// it does its best to guarantee a uniform distribution of the possible output values.
        /// </summary>
        /// <param name="upper_bound">The exclusive upper bound.</param>
        /// <returns>A value between 0 and <paramref name="upper_bound"/> (excluded).</returns>
        [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
        public extern static uint randombytes_uniform(uint upper_bound);

        /// <summary>
        /// The <see cref="randombytes_buf"/> function fills <paramref name="size"/> bytes starting
        /// at <paramref name="buf"/> with an unpredictable sequence of bytes.
        /// </summary>
        /// <param name="buf">The buffer.</param>
        /// <param name="size">The number of bytes to fill.</param>
        [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
        public extern static void randombytes_buf(byte[] buf, int size);

        /// <summary>
        /// This deallocates the global resources used by the pseudo-random number generator. More
        /// specifically, when the /dev/urandom device is used, it closes the descriptor. Explicitly
        /// calling this function is almost never required.
        /// </summary>
        /// <returns></returns>
        [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
        public extern static int randombytes_close();

        /// <summary>
        /// The <see cref="randombytes_stir"/> function reseeds the pseudo-random number generator,
        /// if it supports this operation.
        /// </summary>
        [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
        public extern static void randombytes_stir();
    }
}
