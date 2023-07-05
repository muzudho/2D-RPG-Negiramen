namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.Negiramen
{
    /// <summary>
    ///     😁 ユーザー構成ファイルへのパス
    ///     
    ///     <list type="bullet">
    ///         <item>配置場所は、構成ファイルで変更可能。既定ではネギラーメン・ワークスペースの直下に置く想定</item>
    ///     </list>
    /// </summary>
    class UserConfiguration : _2D_RPG_Negiramen.Models.FileEntries.Locations.Its

    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static UserConfiguration Empty { get; } = new UserConfiguration();

        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="folderPath">フォルダーへのパス</param>
        /// <param name="replaceSeparators">`\` を `/` へ置換</param>
        /// <returns>実例</returns>
        internal static UserConfiguration FromString(
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

            return new UserConfiguration(folderPath);
        }

        // - その他

        /// <summary>
        ///     生成
        /// </summary>
        internal UserConfiguration()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal UserConfiguration(string asStr)
            : base(asStr)
        {
        }
    }
}
