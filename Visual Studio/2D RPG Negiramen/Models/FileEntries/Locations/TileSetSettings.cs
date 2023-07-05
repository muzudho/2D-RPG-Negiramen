namespace _2D_RPG_Negiramen.Models.FileEntries.Locations
{
    /// <summary>
    ///     😁 タイル・セットCSVファイル・ロケーション
    /// </summary>
    class TileSetSettings : Its
    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static TileSetSettings Empty { get; } = new TileSetSettings();

        // - その他

        /// <summary>
        ///     生成
        /// </summary>
        internal TileSetSettings()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal TileSetSettings(FileEntryPath fileEntryPath)
            : base(fileEntryPath)
        {
        }
    }
}
