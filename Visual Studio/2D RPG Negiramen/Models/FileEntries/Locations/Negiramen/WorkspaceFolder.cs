namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.Negiramen
{
    /// <summary>
    ///     😁 ネギラーメン・ワークスペース・フォルダーのパス
    /// </summary>
    class WorkspaceFolder : _2D_RPG_Negiramen.Models.FileEntries.Locations.Its

    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static WorkspaceFolder Empty { get; } = new WorkspaceFolder();

        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="folderPath">フォルダーへのパス</param>
        /// <param name="replaceSeparators">`\` を `/` へ置換</param>
        /// <returns>実例</returns>
        internal static WorkspaceFolder FromString(
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

            return new WorkspaceFolder(folderPath);
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal WorkspaceFolder()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal WorkspaceFolder(string asStr)
            : base(asStr)
        {
        }
    }
}
