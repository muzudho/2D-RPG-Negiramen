namespace _2D_RPG_Negiramen.Models.FileSpace.Negiramen
{
    /// <summary>
    ///     😁 ネギラーメン・ワークスペース・フォルダーのパス
    /// </summary>
    class WorkspaceFolderPath

    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static WorkspaceFolderPath Empty { get; } = new WorkspaceFolderPath();

        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="folderPath">フォルダーへのパス</param>
        /// <returns>実例</returns>
        internal static WorkspaceFolderPath FromStringAndReplaceSeparators(string folderPath)
        {
            if (folderPath == null)
            {
                throw new ArgumentNullException(nameof(folderPath));
            }

            folderPath = folderPath.Replace("\\", "/");

            return new WorkspaceFolderPath(folderPath);
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal WorkspaceFolderPath()
        {
            AsStr = string.Empty;
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal WorkspaceFolderPath(string asStr)
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
