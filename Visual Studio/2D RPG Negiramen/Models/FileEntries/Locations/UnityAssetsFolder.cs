namespace _2D_RPG_Negiramen.Models.FileEntries.Locations
{
    using _2D_RPG_Negiramen.Coding;

    /// <summary>
    ///     😁 Unityの Assets フォルダーへのパス
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///         <item><see cref="_2D_RPG_Negiramen.App.Configuration"/></item>
    ///     </list>
    /// </summary>
    class UnityAssetsFolder : Its
    {
        // - その他

        #region その他（生成　関連）
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
        internal UnityAssetsFolder(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
            : base(pathSource, convert)
        {
        }
        #endregion

        // - インターナル静的プロパティ

        #region プロパティ（空オブジェクト）
        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static UnityAssetsFolder Empty { get; } = new UnityAssetsFolder();
        #endregion
    }
}
