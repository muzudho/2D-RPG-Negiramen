namespace _2D_RPG_Negiramen.Models.FileEntries.Locations
{
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
        internal TileSetImageFile(FileEntryPath path)
            : base(path)
        {
        }
    }
}
