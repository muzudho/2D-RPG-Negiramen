namespace _2D_RPG_Negiramen.Models.FileEntries.Locations
{
    /// <summary>
    ///     😁 タイル・セット画像ファイル・パス
    /// </summary>
    class TileSetImage : Its
    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static TileSetImage Empty { get; } = new TileSetImage();

        // - その他

        /// <summary>
        ///     生成
        /// </summary>
        internal TileSetImage()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal TileSetImage(FileEntryPath fileEntryPath)
            : base(fileEntryPath)
        {
        }
    }
}
