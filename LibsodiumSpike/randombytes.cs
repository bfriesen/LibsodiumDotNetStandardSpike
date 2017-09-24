using System;
using System.Runtime.InteropServices;

namespace LibsodiumSpike
{
    partial class Sodium
    {        
        private static readonly Lazy<randombytes_buf_> _randombytes_buf;

        private static void init_randombytes(out Lazy<randombytes_buf_> randombytes_buf)
        {
            randombytes_buf = _embeddedLibsodium.GetLazyDelegate<randombytes_buf_>(nameof(Sodium.randombytes_buf));
        }

        /// <summary>
        /// The <see cref="randombytes_buf"/> function fills <paramref name="size"/> bytes starting
        /// at <paramref name="buf"/> with an unpredictable sequence of bytes.
        /// </summary>
        /// <param name="buf">The buffer.</param>
        /// <param name="size">The number of bytes to fill.</param>
        public static void randombytes_buf(byte[] buf, int size) => _randombytes_buf.Value.Invoke(buf, size);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void randombytes_buf_(byte[] buf, int size);
    }
}
