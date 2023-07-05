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

        // - 静的その他

        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="filePath">ファイルへのパス</param>
        /// <param name="replaceSeparators">`\` を `/` へ置換</param>
        /// <returns>実例</returns>
        internal static TileSetImage FromString(
            string filePath,
            bool replaceSeparators = false)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (replaceSeparators)
            {
                filePath = filePath.Replace("\\", "/");
            }

            return new TileSetImage(filePath);
        }

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
        internal TileSetImage(string asStr)
            : base(asStr: asStr)
        {
        }
    }
}
