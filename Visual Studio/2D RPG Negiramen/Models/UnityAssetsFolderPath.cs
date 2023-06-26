namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    /// Unityの Assets フォルダーへのパス
    /// </summary>
    class UnityAssetsFolderPath
    {
        // - 静的プロパティ

        /// <summary>
        /// 空オブジェクト
        /// </summary>
        internal static UnityAssetsFolderPath Empty { get; } = new UnityAssetsFolderPath();

        /// <summary>
        /// 文字列を与えて初期化
        /// </summary>
        /// <param name="folderPath">フォルダーへのパス</param>
        /// <returns>実例</returns>
        internal static UnityAssetsFolderPath FromString(string folderPath)
        {
            if (folderPath == null)
            {
                throw new ArgumentNullException(nameof(folderPath));
            }

            return new UnityAssetsFolderPath(folderPath);
        }

        /// <summary>
        /// 生成
        /// </summary>
        internal UnityAssetsFolderPath()
        {
            this.AsStr = string.Empty;
        }

        /// <summary>
        /// 生成
        /// </summary>
        internal UnityAssetsFolderPath(string asStr)
        {
            this.AsStr = asStr;
        }

        /// <summary>
        /// 文字列形式
        /// </summary>
        internal string AsStr { get; }
    }
}
