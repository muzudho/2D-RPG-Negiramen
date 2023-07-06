namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.Negiramen
{
    using _2D_RPG_Negiramen.Coding;

    /// <summary>
    ///     😁 ネギラーメン・ワークスペースの作業中のタイル・セット・キャンバスPNG画像ファイルへのパス
    /// </summary>
    /// <example>"C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/Workspace/Temporary/Images/working_tile_set_canvas.png"</example>
    class WorkingTileSetCanvasImageFile : _2D_RPG_Negiramen.Models.FileEntries.Locations.Its

    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static WorkingTileSetCanvasImageFile Empty { get; } = new WorkingTileSetCanvasImageFile();

        // - その他

        /// <summary>
        ///     生成
        /// </summary>
        internal WorkingTileSetCanvasImageFile()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal WorkingTileSetCanvasImageFile(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
            : base(pathSource, convert)
        {
        }
    }
}
