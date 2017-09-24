using System;
using System.Runtime.InteropServices;

namespace LibsodiumSpike
{
    partial class Sodium
    {
        /// <summary>
        /// The <see cref="randombytes_buf"/> function fills <paramref name="size"/> bytes starting
        /// at <paramref name="buf"/> with an unpredictable sequence of bytes.
        /// </summary>
        /// <param name="buf">The buffer.</param>
        /// <param name="size">The number of bytes to fill.</param>
        [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
        public extern static void randombytes_buf(byte[] buf, int size);
    }
}
