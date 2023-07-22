namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets
{
    using _2D_RPG_Negiramen;
    using _2D_RPG_Negiramen.Coding;
    using _2D_RPG_Negiramen.Models;
    using TheFileEntryLocation = _2D_RPG_Negiramen.Models.FileEntries.Locations;

    /// <summary>
    ///     😁 Unityの 📂 `Assets` フォルダーへのパス
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///         <item><see cref="App.Configuration"/></item>
    ///     </list>
    /// </summary>
    class ItsFolder : TheFileEntryLocation.Its
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
        ///     生成
        /// </summary>
        internal ItsFolder(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
            : base(pathSource, convert)
        {
        }
        #endregion

        // - インターナル静的プロパティ

        #region プロパティ（空オブジェクト）
        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static ItsFolder Empty { get; } = new ItsFolder();
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（Unityの 📂 `Assets/{Your Circle Name}` フォルダーの場所）
        /// <summary>
        ///     Unityの 📂 `Assets/{Your Circle Name}` フォルダーの場所
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

        // - プライベート・フィールド

        YourCircleNameFolder? yourCircleNameFolder;
    }
}
