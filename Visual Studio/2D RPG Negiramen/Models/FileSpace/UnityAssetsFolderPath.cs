namespace _2D_RPG_Negiramen.Models.FileSpace
{
    /// <summary>
    ///     😁 Unityの Assets フォルダーへのパス
    /// </summary>
    class UnityAssetsFolderPath
    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static UnityAssetsFolderPath Empty { get; } = new UnityAssetsFolderPath();

        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="folderPath">フォルダーへのパス</param>
        /// <param name="replaceSeparators">`\` を `/` へ置換</param>
        /// <returns>実例</returns>
        internal static UnityAssetsFolderPath FromString(
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

            return new UnityAssetsFolderPath(folderPath);
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal UnityAssetsFolderPath()
        {
            this.AsStr = string.Empty;
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal UnityAssetsFolderPath(string asStr)
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
