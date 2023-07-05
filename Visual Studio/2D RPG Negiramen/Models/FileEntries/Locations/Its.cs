namespace _2D_RPG_Negiramen.Models.FileEntries.Locations
{
    /// <summary>
    ///     😁 ファイル・ロケーション
    /// </summary>
    abstract class Its
    {
        // - その他

        /// <summary>
        ///     未設定
        /// </summary>
        internal Its()
        {
            Path = FileEntryPath.Empty;
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal Its(FileEntryPath path)
        {
            Path = path;
        }

        // - インターナル・プロパティー

        /// <summary>
        ///     ファイル・エントリー・パス
        ///     
        ///     <list type="bullet">
        ///         <item>ファイル・パスや、フォルダー・パスのこと</item>
        ///     </list>
        /// </summary>
        internal FileEntryPath Path { get; }
    }
}
