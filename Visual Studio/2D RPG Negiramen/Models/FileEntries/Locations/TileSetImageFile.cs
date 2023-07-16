namespace _2D_RPG_Negiramen.Models.FileEntries.Locations
{
    using _2D_RPG_Negiramen.Coding;

    /// <summary>
    ///     😁 タイルセット画像ファイル・パス
    /// </summary>
    class TilesetImageFile : Its
    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static TilesetImageFile Empty { get; } = new TilesetImageFile();

        // - その他

        /// <summary>
        ///     生成
        /// </summary>
        internal TilesetImageFile()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal TilesetImageFile(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
            : base(pathSource, convert)
        {
        }
    }
}
