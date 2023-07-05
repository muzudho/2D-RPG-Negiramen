namespace _2D_RPG_Negiramen.Models.FileSpace.Negiramen
{
    /// <summary>
    ///     😁 ネギラーメン・ワークスペースの作業中のタイル・セット・キャンバスPNG画像ファイルへのパス
    /// </summary>
    /// <example>"C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/Workspace/Temporary/Images/working_tile_set_canvas.png"</example>
    class WorkingTileSetCanvasImageFilePath

    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static WorkingTileSetCanvasImageFilePath Empty { get; } = new WorkingTileSetCanvasImageFilePath();

        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="folderPath">フォルダーへのパス</param>
        /// <returns>実例</returns>
        internal static WorkingTileSetCanvasImageFilePath FromStringAndReplaceSeparators(string folderPath)
        {
            if (folderPath == null)
            {
                throw new ArgumentNullException(nameof(folderPath));
            }

            folderPath = folderPath.Replace("\\", "/");

            return new WorkingTileSetCanvasImageFilePath(folderPath);
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal WorkingTileSetCanvasImageFilePath()
        {
            AsStr = string.Empty;
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal WorkingTileSetCanvasImageFilePath(string asStr)
        {
            AsStr = asStr;
        }

        /// <summary>
        ///     文字列形式
        /// </summary>
        internal string AsStr { get; }

        /// <summary>
        ///     暗黙的な文字列形式
        /// </summary>
        public override string ToString() => AsStr;
    }
}
