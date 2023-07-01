namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    /// タイル・セット画像ファイル・パス
    /// </summary>
    class TileSetImageFilePath
    {
        // - 静的プロパティ

        /// <summary>
        /// 空オブジェクト
        /// </summary>
        internal static TileSetImageFilePath Empty { get; } = new TileSetImageFilePath();

        /// <summary>
        /// 文字列を与えて初期化
        /// </summary>
        /// <param name="filePath">ファイルへのパス</param>
        /// <returns>実例</returns>
        internal static TileSetImageFilePath FromStringAndReplaceSeparators(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            filePath = filePath.Replace("\\", "/");

            return new TileSetImageFilePath(filePath);
        }

        /// <summary>
        /// 生成
        /// </summary>
        internal TileSetImageFilePath()
        {
            this.AsStr = string.Empty;
        }

        /// <summary>
        /// 生成
        /// </summary>
        internal TileSetImageFilePath(string asStr)
        {
            this.AsStr = asStr;
        }

        /// <summary>
        /// 文字列形式
        /// </summary>
        internal string AsStr { get; }

        /// <summary>
        /// 暗黙的な文字列形式
        /// </summary>
        public override string ToString() => AsStr;
    }
}
