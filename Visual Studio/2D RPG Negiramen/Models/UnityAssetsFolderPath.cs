namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    /// Unityの Assets フォルダーへのパス
    /// </summary>
    class UnityAssetsFolderPath
    {
        /// <summary>
        /// 文字列を与えて初期化
        /// </summary>
        /// <param name="folderPath">フォルダーへのパス</param>
        /// <returns>実例</returns>
        internal static UnityAssetsFolderPath FromString(string folderPath)
        {
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
