namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.AppData
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
    ///     "C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalState"
    /// </example>
    internal class ItsFolder : Its
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        internal ItsFolder()
            : base(
                  pathSource: new FileEntryPathSource(FileSystem.Current.AppDataDirectory),
                  convert: (pathSource) => FileEntryPath.From(pathSource,
                                                              replaceSeparators: true))
        {
        }
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（OSの 📂 キャッシュ・フォルダ の `{あたなのサークル名}` フォルダの場所）
        /// <summary>
        ///     OSの 📂 キャッシュ・フォルダ の `{あたなのサークル名}` フォルダの場所
        ///     
        ///     <list type="bullet">
        ///         <item>構成ファイルを作る前に　このプロパティを使うと、循環参照します</item>
        ///     </list>
        /// </summary>
        internal YourCircleNameFolder YourCircleNameFolder
        {
            get
            {
                if (yourCircleNameFolder == null)
                {
                    yourCircleNameFolder = new YourCircleNameFolder(
                        pathSource: FileEntryPathSource.FromString(
                            System.IO.Path.Combine(Path.AsStr, App.GetOrLoadConfiguration().YourCircleName.AsStr)),
                        convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                    replaceSeparators: true));
                }

                return yourCircleNameFolder;
            }
        }
        #endregion

        // - インターナル・メソッド

        #region メソッド（Unity の 📄 `C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalState\Doujin Circle Negiramen` ファイルの場所）
        /// <summary>
        ///     Unity の 📄 `C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalState\Doujin Circle Negiramen` ファイルの場所
        /// </summary>
        /// <param name="yourCircleName">サークル名</param>
        internal YourCircleNameFolder CreateAndOverwriteYourCircleNameFolder(string yourCircleName)
        {
            this.yourCircleNameFolder = new YourCircleNameFolder(
                pathSource: FileEntryPathSource.FromString(
                    System.IO.Path.Combine(Path.AsStr, yourCircleName)),
                convert: (pathSource) => FileEntryPath.From(pathSource,
                                                            replaceSeparators: true));

            return this.yourCircleNameFolder;
        }
        #endregion

        // - プライベート・フィールド

        YourCircleNameFolder? yourCircleNameFolder;
    }
}
