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

        // - 静的その他

        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="filePath">ファイルへのパス</param>
        /// <param name="replaceSeparators">`\` を `/` へ置換</param>
        /// <returns>実例</returns>
        internal static TileSetSettings FromString(
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

            return new TileSetSettings(filePath);
        }

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
        internal TileSetSettings(string asStr)
            : base(asStr: asStr)
        {
        }
    }
}
