﻿using RockLib.Interop;
using System;
using System.Runtime.InteropServices;

namespace LibsodiumSpike
{
    /// <summary>
    /// Defines the various libsodium functions and constants.
    /// </summary>
    public static partial class Sodium
    {
#if USE_FUNCTION_POINTER
        private static readonly EmbeddedNativeLibrary _embeddedLibsodium;
#endif

        static Sodium()
        {
#if USE_FUNCTION_POINTER
            _embeddedLibsodium = new EmbeddedNativeLibrary("libsodium", NativeLibraryOptions.PreferEmbeddedOverInstalled,
                new DllInfo(TargetRuntime.Win32, "LibsodiumSpike.Win32.libsodium.dll", "LibsodiumSpike.Win32.msvcr120.dll"),
                new DllInfo(TargetRuntime.Win64, "LibsodiumSpike.Win64.libsodium.dll", "LibsodiumSpike.Win64.msvcr120.dll"),
                new DllInfo(TargetRuntime.Linux, "LibsodiumSpike.Linux.libsodium.so"),
                new DllInfo(TargetRuntime.Mac, "LibsodiumSpike.Mac.libsodium.dylib"));
#else
            EmbeddedNativeLibrary.Load("libsodium", NativeLibraryOptions.PreferEmbeddedOverInstalled,
                new DllInfo(TargetRuntime.Win32, "LibsodiumSpike.Win32.libsodium.dll", "LibsodiumSpike.Win32.msvcr120.dll"),
                new DllInfo(TargetRuntime.Win64, "LibsodiumSpike.Win64.libsodium.dll", "LibsodiumSpike.Win64.msvcr120.dll"));
#endif

            if (sodium_init() == -1)
                throw new InvalidOperationException("The libsodium library failed to initialize: sodium_init() returned -1.");

#if USE_FUNCTION_POINTER
            init_randombytes(out _randombytes_buf);
            init_crypto_secretbox(out _crypto_secretbox_easy, out _crypto_secretbox_open_easy);
#endif
        }

        /// <summary>
        /// <para>
        /// <see cref="sodium_init"/> initializes the library and should be called before any other
        /// function provided by Sodium. The function can be called more than once, but it should not
        /// be executed by multiple threads simultaneously. Add appropriate locks around the function
        /// call if this scenario can happen in your application.
        /// </para>
        /// <para>
        /// After this function returns, all of the other functions provided by Sodium will be
        /// thread-safe.
        /// </para>
        /// <para>
        /// <see cref="sodium_init"/> doesn't perform any memory allocations. However, on Unix systems,
        /// it opens /dev/urandom and keeps the descriptor open so that the device remains accessible
        /// after a chroot() call. Multiple calls to <see cref="sodium_init"/> do not cause additional
        /// descriptors to be opened.
        /// </para>
        /// </summary>
        /// <returns>-1 if the initialization fails, 0 on success.</returns>
#if USE_FUNCTION_POINTER
        private static int sodium_init() => _embeddedLibsodium.GetDelegate<sodium_init_>(nameof(sodium_init)).Invoke();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int sodium_init_();
#else
        [DllImport("libsodium")]
        private static extern int sodium_init();
#endif
    }
}
