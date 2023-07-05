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
            FileEntryPath = FileEntryPath.Empty;
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal Its(FileEntryPath fileEntryPath)
        {
            FileEntryPath = fileEntryPath;
        }

        // - インターナル・プロパティー

        /// <summary>
        ///     文字列形式
        /// </summary>
        internal FileEntryPath FileEntryPath { get; }
    }
}
