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
        internal UserConfiguration(FileEntryPath fileEntryPath)
            : base(fileEntryPath)
        {
        }
    }
}
