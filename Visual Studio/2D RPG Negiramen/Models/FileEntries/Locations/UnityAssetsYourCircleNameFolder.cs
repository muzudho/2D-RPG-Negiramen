namespace _2D_RPG_Negiramen.Models.FileEntries.Locations
{
    using _2D_RPG_Negiramen.Coding;

    /// <summary>
    ///     😁 Unityの 📂 `Assets/{Your Circle Name}` フォルダーの場所
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///         <item><see cref="_2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssetsFolder"/></item>
    ///     </list>
    /// </summary>
    internal class UnityAssetsYourCircleNameFolder : Its
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        internal UnityAssetsYourCircleNameFolder()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal UnityAssetsYourCircleNameFolder(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
            : base(pathSource, convert)
        {
        }
        #endregion

        // - インターナル静的プロパティ

        #region プロパティ（空オブジェクト）
        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static UnityAssetsYourCircleNameFolder Empty { get; } = new UnityAssetsYourCircleNameFolder();
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝`フォルダーの場所）
        /// <summary>
        ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝`フォルダーの場所
        /// </summary>
        internal UnityAssetsYourWorkNameFolder YourWorkNameFolder
        {
            get
            {
                if (yourWorkNameFolder == null)
                {
                    yourWorkNameFolder = new UnityAssetsYourWorkNameFolder(
                        pathSource: FileEntryPathSource.FromString(
                            System.IO.Path.Combine(this.Path.AsStr, App.GetOrLoadConfiguration().YourWorkName.AsStr)),
                        convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                    replaceSeparators: true));
                }

                return yourWorkNameFolder;
            }
        }
        #endregion

        // - プライベート・フィールド

        UnityAssetsYourWorkNameFolder yourWorkNameFolder;
    }
}
