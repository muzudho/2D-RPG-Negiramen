namespace _2D_RPG_Negiramen.Models.FileEntries.Locations
{
    /// <summary>
    ///     😁 タイル・セットCSVファイル・ロケーション
    /// </summary>
    class TileSetSettingsFile : Its
    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static TileSetSettingsFile Empty { get; } = new TileSetSettingsFile();

        // - その他

        /// <summary>
        ///     生成
        /// </summary>
        internal TileSetSettingsFile()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal TileSetSettingsFile(FileEntryPath path)
            : base(path)
        {
        }
    }
}
