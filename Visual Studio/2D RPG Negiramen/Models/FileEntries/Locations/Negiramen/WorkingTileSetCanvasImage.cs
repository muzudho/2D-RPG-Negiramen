namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.Negiramen
{
    /// <summary>
    ///     😁 ネギラーメン・ワークスペースの作業中のタイル・セット・キャンバスPNG画像ファイルへのパス
    /// </summary>
    /// <example>"C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/Workspace/Temporary/Images/working_tile_set_canvas.png"</example>
    class WorkingTileSetCanvasImage : _2D_RPG_Negiramen.Models.FileEntries.Locations.Its

    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static WorkingTileSetCanvasImage Empty { get; } = new WorkingTileSetCanvasImage();

        // - その他

        /// <summary>
        ///     生成
        /// </summary>
        internal WorkingTileSetCanvasImage()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal WorkingTileSetCanvasImage(FileEntryPath fileEntryPath)
            : base(fileEntryPath)
        {
        }
    }
}
