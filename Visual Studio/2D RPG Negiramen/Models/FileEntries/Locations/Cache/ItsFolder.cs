namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.Cache
{
    using _2D_RPG_Negiramen;
    using _2D_RPG_Negiramen.Coding;
    using _2D_RPG_Negiramen.Models;

    /// <summary>
    ///     😁 OSの 📂 キャッシュ・フォルダの場所
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///         <item><see cref="App.Configuration"/></item>
    ///     </list>
    /// </summary>
    /// <example>
    ///     "C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalCache"
    /// </example>
    internal class ItsFolder : Its
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        internal ItsFolder()
            : base(pathSource: FileEntryPathSource.FromString(FileSystem.CacheDirectory),
                   convert: (pathSource) => FileEntryPath.From(pathSource,
                                                               replaceSeparators: true))
        {
        }
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（OSの 📂 キャッシュ・フォルダ の `{あたなのサークル・フォルダ名}` フォルダの場所）
        /// <summary>
        ///     OSの 📂 キャッシュ・フォルダ の `{あたなのサークル・フォルダ名}` フォルダの場所
        /// </summary>
        internal YourCircleFolderNameFolder YourCircleFolderNameFolder
        {
            get
            {
                if (yourCircleFolderNameFolder == null)
                {
                    yourCircleFolderNameFolder = new YourCircleFolderNameFolder(
                        pathSource: FileEntryPathSource.FromString(
                            System.IO.Path.Combine(Path.AsStr, App.GetOrLoadConfiguration().RememberYourCircleFolderName.AsStr)),
                        convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                    replaceSeparators: true));
                }

                return yourCircleFolderNameFolder;
            }
        }
        #endregion

        // - プライベート・フィールド

        YourCircleFolderNameFolder? yourCircleFolderNameFolder;
    }
}
