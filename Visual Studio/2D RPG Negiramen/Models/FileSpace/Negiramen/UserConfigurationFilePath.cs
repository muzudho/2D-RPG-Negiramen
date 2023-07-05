namespace _2D_RPG_Negiramen.Models.FileSpace.Negiramen
{
    /// <summary>
    ///     😁 ユーザー構成ファイルへのパス
    ///     
    ///     <list type="bullet">
    ///         <item>配置場所は、構成ファイルで変更可能。既定ではネギラーメン・ワークスペースの直下に置く想定</item>
    ///     </list>
    /// </summary>
    class UserConfigurationFilePath

    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static UserConfigurationFilePath Empty { get; } = new UserConfigurationFilePath();

        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="folderPath">フォルダーへのパス</param>
        /// <param name="replaceSeparators">`\` を `/` へ置換</param>
        /// <returns>実例</returns>
        internal static UserConfigurationFilePath FromString(
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

            return new UserConfigurationFilePath(folderPath);
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal UserConfigurationFilePath()
        {
            AsStr = string.Empty;
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal UserConfigurationFilePath(string asStr)
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
