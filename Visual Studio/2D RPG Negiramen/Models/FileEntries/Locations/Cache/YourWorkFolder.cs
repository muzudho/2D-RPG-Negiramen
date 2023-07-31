namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.Cache
{
    using _2D_RPG_Negiramen;
    using _2D_RPG_Negiramen.Models;

    /// <summary>
    ///     😁 OSの 📂 キャッシュ・ディレクトリー の `{あたなのサークル・フォルダ名}/{あなたの作品フォルダ名}` フォルダの場所
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///         <item><see cref="App.Configuration"/></item>
    ///     </list>
    /// </summary>
    /// <example>
    ///     "C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalCache\Doujin Circle Negiramen\Negiramen Quest"
    /// </example>
    internal class YourWorkFolder : Its
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        internal YourWorkFolder(FileEntryPath parentPath)
            : base(pathSource: FileEntryPathSource.FromString(System.IO.Path.Combine(parentPath.AsStr, App.GetOrLoadConfiguration().RememberYourWorkFolderName.AsStr)),
                   convert: (pathSource) => FileEntryPath.From(pathSource,
                                                               replaceSeparators: true))
        {
        }
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（OSの 📂 キャッシュ・ディレクトリー の `{あたなのサークル・フォルダ名}/{あなたの作品フォルダ名}/Images` フォルダの場所）
        /// <summary>
        ///     OSの 📂 キャッシュ・ディレクトリー の `{あたなのサークル・フォルダ名}/{あなたの作品フォルダ名}/Images` フォルダの場所
        /// </summary>
        internal ImagesFolder ImagesFolder
        {
            get
            {
                if (imagesFolder == null)
                {
                    imagesFolder = new ImagesFolder(Path);
                }

                return imagesFolder;
            }
        }
        #endregion

        // - プライベート・フィールド

        ImagesFolder? imagesFolder;
    }
}
