namespace LibsodiumSpike
{
    /// <summary>
    /// Provides a mechanism for changing the loading behavior of the native library.
    /// </summary>
    public static class NativeLibraryOptions
    {
        /// <summary>
        /// The default value of <see cref="PreferEmbeddedOverInstalled"/>.
        /// </summary>
        public const bool DefaultPreferEmbeddedOverInstalled = true;

        /// <summary>
        /// <para>Gets or sets a value that determines the order that paths are searched when loading the
        /// native library. If true, loading the embedded native library is attempted first and if it fails,
        /// then loading the native library from the operating system's default load paths is attempted. If
        /// false, the installed library is attempted first and the embedded library is attempted second.</para>
        /// <para>NOTE: This value must be set before the <see cref="Sodium"/> class is accessed for the first time.</para>
        /// </summary>
        public static bool PreferEmbeddedOverInstalled { get; set; } = DefaultPreferEmbeddedOverInstalled;
    }
}
