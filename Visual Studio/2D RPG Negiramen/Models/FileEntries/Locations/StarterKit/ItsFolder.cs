namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.StarterKit
{
    using _2D_RPG_Negiramen.Coding;

    /// <summary>
    ///     😁 ネギラーメンの 📂 `Starter Kit` フォルダのパス
    /// </summary>
    class ItsFolder : _2D_RPG_Negiramen.Models.FileEntries.Locations.Its
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        internal ItsFolder()
            : base()
        {
        }

        /// <summary>
        ///     <pre>
        ///         生成
        ///         
        ///         以下の２箇所で生成される。
        ///     </pre>
        ///     <list type="bullet">
        ///         <item>構成ページのテキストボックス</item>
        ///         <item>構成ファイル</item>
        ///     </list>
        /// </summary>
        internal ItsFolder(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
            : base(pathSource, convert)
        {
        }
        #endregion

        // - インターナル静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static ItsFolder Empty { get; } = new ItsFolder();

        // - インターナル・プロパティ

        #region プロパティ（ネギラーメンの 📂 `Starter Kit` フォルダ下の 📄 `user_configuration.toml` ファイルの場所）
        /// <summary>
        ///     ネギラーメンの 📂 `Starter Kit` フォルダ下の 📄 `user_configuration.toml` ファイルの場所
        /// </summary>
        internal UserConfigurationFile UserConfigurationFile
        {
            get
            {
                if (userConfigurationFile == null)
                {
                    userConfigurationFile = new UserConfigurationFile(
                        pathSource: FileEntryPathSource.FromString(
                            System.IO.Path.Combine(Path.AsStr, "user_configuration.toml")),
                        convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                    replaceSeparators: true));
                }

                return userConfigurationFile;
            }
        }
        #endregion

        // - プライベート・フィールド

        UserConfigurationFile? userConfigurationFile;
    }
}
