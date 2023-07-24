namespace _2D_RPG_Negiramen.Models.FileEntries.Locations
{
    using _2D_RPG_Negiramen.Coding;

    /// <summary>
    ///     😁 ファイル・ロケーション
    /// </summary>
    abstract class Its
    {
        // - その他

        #region その他（生成　関連）
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
        internal Its(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
        {
            PathSource = pathSource;
            Path = convert(PathSource);
        }
        #endregion

        // - インターナル・プロパティー

        #region プロパティ（ファイル・エントリー・パス）
        /// <summary>
        ///     ファイル・エントリー・パス
        ///     
        ///     <list type="bullet">
        ///         <item>ファイル・パスや、フォルダ・パスのこと</item>
        ///         <item>セパレーター置換後、変数展開後</item>
        ///     </list>
        /// </summary>
        internal FileEntryPath Path { get; }
        #endregion

        #region プロパティ（ファイル・エントリー・パス・ソース）
        /// <summary>
        ///     ファイル・エントリー・パス・ソース
        ///     
        ///     <list type="bullet">
        ///         <item>設定ファイルに記入されているファイル・パスや、フォルダ・パスのこと</item>
        ///         <item>セパレーター置換前、変数展開前</item>
        ///     </list>
        /// </summary>
        internal FileEntryPathSource PathSource { get; }
        #endregion

        // - インターナル・メソッド

        #region メソッド（このディレクトリーが存在しないなら、作成する）
        /// <summary>
        ///     このディレクトリーが存在しないなら、作成する
        /// </summary>
        internal void CreateThisDirectoryIfItDoesNotExist()
        {
            if (!Directory.Exists(this.Path.AsStr))
            {
                Directory.CreateDirectory(this.Path.AsStr);
            }
        }
        #endregion
    }
}
