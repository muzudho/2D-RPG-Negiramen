namespace _2D_RPG_Negiramen.Models.FileEntries.FileEntriesLocations
{
    /// <summary>
    ///     😁 タイル・セット画像ファイル・パス
    /// </summary>

    /* プロジェクト '2D RPG Negiramen (net7.0-android)' からのマージされていない変更
    前:
        class TileSetImageFilePath : Models.FilePath
    後:
        class TileSetImageFilePath : FilePath
    */

    /* プロジェクト '2D RPG Negiramen (net7.0-android)' からのマージされていない変更
    前:
        class TileSetImageFile : FileEntriesLocations.Its
    後:
        class TileSetImageFile : Its
    */
    class TileSetImageFile : Its
    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static TileSetImageFile Empty { get; } = new TileSetImageFile();

        // - 静的その他

        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="filePath">ファイルへのパス</param>
        /// <param name="replaceSeparators">`\` を `/` へ置換</param>
        /// <returns>実例</returns>
        internal static TileSetImageFile FromString(
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

            return new TileSetImageFile(filePath);
        }

        // - その他

        /// <summary>
        ///     生成
        /// </summary>
        internal TileSetImageFile()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal TileSetImageFile(string asStr)
            : base(asStr: asStr)
        {
        }
    }
}
