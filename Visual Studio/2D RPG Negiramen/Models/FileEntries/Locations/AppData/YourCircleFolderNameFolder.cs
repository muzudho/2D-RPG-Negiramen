namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.AppData
{
    using _2D_RPG_Negiramen;
    using _2D_RPG_Negiramen.Coding;
    using _2D_RPG_Negiramen.Models;

    /// <summary>
    ///     😁 OSの 📂 アプリケーション・ディレクトリー の `{あたなのサークル・フォルダ名}` フォルダ―の場所
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///         <item><see cref="App.Configuration"/></item>
    ///     </list>
    /// </summary>
    /// <example>
    ///     "C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalState\Doujin Circle Negiramen"
    /// </example>
    internal class YourCircleFolderNameFolder : Its
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        internal YourCircleFolderNameFolder()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal YourCircleFolderNameFolder(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
            : base(pathSource, convert)
        {
        }
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（OSの 📂 キャッシュ・ディレクトリー の `{あたなのサークル・フォルダ名}/{あなたの作品フォルダ名}` フォルダの場所）
        /// <summary>
        ///     OSの 📂 キャッシュ・ディレクトリー の `{あたなのサークル・フォルダ名}/{あなたの作品フォルダ名}` フォルダの場所
        ///     
        ///     <list type="bullet">
        ///         <item>構成ファイルを作る前に　このプロパティを使うと、循環参照します</item>
        ///     </list>
        /// </summary>
        internal YourWorkFolderNameFolder YourWorkFolderNameFolder
        {
            get
            {
                if (yourWorkFolderNameFolder == null)
                {
                    yourWorkFolderNameFolder = new YourWorkFolderNameFolder(
                        pathSource: FileEntryPathSource.FromString(
                            System.IO.Path.Combine(Path.AsStr, App.GetOrLoadConfiguration().RememberYourWorkFolderName.AsStr)),
                        convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                    replaceSeparators: true));
                }

                return yourWorkFolderNameFolder;
            }
        }
        #endregion

        // - インターナル・メソッド

        #region メソッド（OSの 📂 アプリケーション・ディレクトリー の `{あたなのサークル名}/{あなたの作品名}` フォルダ―の場所）
        /// <summary>
        ///     OSの 📂 アプリケーション・ディレクトリー の `{あたなのサークル名}/{あなたの作品名}` フォルダ―の場所
        /// </summary>
        /// <param name="yourWorkFolderName">作品名</param>
        internal YourWorkFolderNameFolder CreateAndOverwriteYourWorkFolderNameFolder(string yourWorkFolderName)
        {
            this.yourWorkFolderNameFolder = new YourWorkFolderNameFolder(
                pathSource: FileEntryPathSource.FromString(
                    System.IO.Path.Combine(Path.AsStr, yourWorkFolderName)),
                convert: (pathSource) => FileEntryPath.From(pathSource,
                                                            replaceSeparators: true));

            return this.yourWorkFolderNameFolder;
        }
        #endregion

        // - プライベート・フィールド

        YourWorkFolderNameFolder? yourWorkFolderNameFolder;
    }
}
