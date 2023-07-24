namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.Negiramen
{
    using _2D_RPG_Negiramen.Coding;

    /// <summary>
    ///     😁 ネギラーメン・ワークスペースの作業中のタイルセット画像ファイルへのパス
    /// </summary>
    /// <example>
    ///     "C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalCache\Doujin Circle Negiramen\Negiramen Quest\Images\working_tileset.png"
    /// </example>
    class WorkingTilesetImageFile : _2D_RPG_Negiramen.Models.FileEntries.Locations.Its

    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static WorkingTilesetImageFile Empty { get; } = new WorkingTilesetImageFile();

        // - その他

        /// <summary>
        ///     生成
        /// </summary>
        internal WorkingTilesetImageFile()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal WorkingTilesetImageFile(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
            : base(pathSource, convert)
        {
        }
    }
}
