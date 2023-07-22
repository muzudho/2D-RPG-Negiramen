namespace _2D_RPG_Negiramen.Models.FileEntries.Locations
{
    using _2D_RPG_Negiramen.Coding;

    /// <summary>
    ///     😁 Unityの 📂 `Assets` フォルダーへのパス
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///         <item><see cref="_2D_RPG_Negiramen.App.Configuration"/></item>
    ///     </list>
    /// </summary>
    class UnityAssetsFolder : Its
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        internal UnityAssetsFolder()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal UnityAssetsFolder(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
            : base(pathSource, convert)
        {
        }
        #endregion

        // - インターナル静的プロパティ

        #region プロパティ（空オブジェクト）
        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static UnityAssetsFolder Empty { get; } = new UnityAssetsFolder();
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（Unityの 📂 `Assets/{Your Circle Name}` フォルダーの場所）
        /// <summary>
        ///     Unityの 📂 `Assets/{Your Circle Name}` フォルダーの場所
        /// </summary>
        internal UnityAssetsYourCircleNameFolder YourCircleNameFolder
        {
            get
            {
                if (yourCircleNameFolder == null)
                {
                    yourCircleNameFolder = new UnityAssetsYourCircleNameFolder(
                        pathSource: FileEntryPathSource.FromString(
                            System.IO.Path.Combine(this.Path.AsStr, App.GetOrLoadConfiguration().YourCircleName.AsStr)),
                        convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                    replaceSeparators: true));
                }

                return yourCircleNameFolder;
            }
        }
        #endregion

        // - プライベート・フィールド

        UnityAssetsYourCircleNameFolder yourCircleNameFolder;
    }
}
