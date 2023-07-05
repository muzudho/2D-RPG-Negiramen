namespace _2D_RPG_Negiramen.Models.FileEntries.Locations
{
    /// <summary>
    ///     😁 Unityの Assets フォルダーへのパス
    /// </summary>
    class UnityAssetsFolder : Its
    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static UnityAssetsFolder Empty { get; } = new UnityAssetsFolder();

        /// <summary>
        ///     生成
        /// </summary>
        internal UnityAssetsFolder()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal UnityAssetsFolder(FileEntryPath path)
            :base(path)
        {
        }
    }
}
