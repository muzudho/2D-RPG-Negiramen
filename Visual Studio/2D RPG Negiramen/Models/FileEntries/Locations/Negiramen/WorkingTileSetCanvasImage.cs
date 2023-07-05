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

        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="folderPath">フォルダーへのパス</param>
        /// <param name="replaceSeparators">`\` を `/` へ置換</param>
        /// <returns>実例</returns>
        internal static WorkingTileSetCanvasImage FromString(
            string folderPath,
            bool replaceSeparators = false)
        {
            if (folderPath == null)
            {
                throw new ArgumentNullException(nameof(folderPath));
            }

            if (replaceSeparators)
            {
                folderPath = folderPath.Replace("\\", "/");
            }

            return new WorkingTileSetCanvasImage(folderPath);
        }

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
        internal WorkingTileSetCanvasImage(string asStr)
            : base(asStr)
        {
        }
    }
}
