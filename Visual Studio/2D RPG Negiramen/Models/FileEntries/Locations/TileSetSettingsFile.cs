namespace _2D_RPG_Negiramen.Models.FileEntries.Locations
{
    using _2D_RPG_Negiramen.Coding;

    /// <summary>
    ///     😁 タイルセットCSVファイル・ロケーション
    /// </summary>
    class TilesetSettingsFile : Its
    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static TilesetSettingsFile Empty { get; } = new TilesetSettingsFile();

        // - その他

        /// <summary>
        ///     生成
        /// </summary>
        internal TilesetSettingsFile()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal TilesetSettingsFile(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
            : base(pathSource, convert)
        {
        }
    }
}
