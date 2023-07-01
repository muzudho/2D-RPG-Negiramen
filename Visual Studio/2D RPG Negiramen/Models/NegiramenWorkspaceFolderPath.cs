namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     😁 ネギラーメン・ワークスペース・フォルダーのパス
    /// </summary>
    class NegiramenWorkspaceFolderPath
    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static NegiramenWorkspaceFolderPath Empty { get; } = new NegiramenWorkspaceFolderPath();

        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="folderPath">フォルダーへのパス</param>
        /// <returns>実例</returns>
        internal static NegiramenWorkspaceFolderPath FromStringAndReplaceSeparators(string folderPath)
        {
            if (folderPath == null)
            {
                throw new ArgumentNullException(nameof(folderPath));
            }

            folderPath = folderPath.Replace("\\", "/");

            return new NegiramenWorkspaceFolderPath(folderPath);
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal NegiramenWorkspaceFolderPath()
        {
            this.AsStr = string.Empty;
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal NegiramenWorkspaceFolderPath(string asStr)
        {
            this.AsStr = asStr;
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
