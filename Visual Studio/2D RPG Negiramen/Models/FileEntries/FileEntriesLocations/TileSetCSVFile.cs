namespace _2D_RPG_Negiramen.Models.FileEntries.FileEntriesLocations
{
    /// <summary>
    ///     😁 タイル・セットCSVファイル・ロケーション
    /// </summary>

    /* プロジェクト '2D RPG Negiramen (net7.0-android)' からのマージされていない変更
    前:
        class TileSetCSVFilePath : Models.FilePath
    後:
        class TileSetCSVFilePath : FilePath
    */

    /* プロジェクト '2D RPG Negiramen (net7.0-android)' からのマージされていない変更
    前:
        class TileSetCSVFile : FileEntriesLocations.Its
    後:
        class TileSetCSVFile : Its
    */
    class TileSetCSVFile : Its
    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static TileSetCSVFile Empty { get; } = new TileSetCSVFile();

        // - 静的その他

        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="filePath">ファイルへのパス</param>
        /// <param name="replaceSeparators">`\` を `/` へ置換</param>
        /// <returns>実例</returns>
        internal static TileSetCSVFile FromString(
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

            return new TileSetCSVFile(filePath);
        }

        // - その他

        /// <summary>
        ///     生成
        /// </summary>
        internal TileSetCSVFile()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal TileSetCSVFile(string asStr)
            : base(asStr: asStr)
        {
        }
    }
}
