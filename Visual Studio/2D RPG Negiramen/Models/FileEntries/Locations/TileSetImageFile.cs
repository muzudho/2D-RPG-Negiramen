namespace _2D_RPG_Negiramen.Models.FileEntries.Locations
{
    using _2D_RPG_Negiramen.Coding;

    /// <summary>
    ///     😁 タイル・セット画像ファイル・パス
    /// </summary>
    class TileSetImageFile : Its
    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static TileSetImageFile Empty { get; } = new TileSetImageFile();

        // - その他

        /// <summary>
        ///     生成
        /// </summary>
        internal TileSetImageFile()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal TileSetImageFile(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
            : base(pathSource, convert)
        {
        }
    }
}
