namespace _2D_RPG_Negiramen.Models.FileEntries.Locations
{
    using _2D_RPG_Negiramen.Coding;

    /// <summary>
    ///     😁 Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images` フォルダーの場所
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///         <item><see cref="_2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssetsFolder"/></item>
    ///     </list>
    /// </summary>
    internal class UnityAssetsImagesFolder : Its
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        internal UnityAssetsImagesFolder()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal UnityAssetsImagesFolder(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
            : base(pathSource, convert)
        {
        }
        #endregion

        // - インターナル静的プロパティ

        #region プロパティ（空オブジェクト）
        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static UnityAssetsImagesFolder Empty { get; } = new UnityAssetsImagesFolder();
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tileset` フォルダーの場所）
        /// <summary>
        ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tileset` フォルダーの場所
        /// </summary>
        internal UnityAssetsImagesTilesetFolder ImagesTilesetFolder
        {
            get
            {
                if (imagesTilesetFolder == null)
                {
                    imagesTilesetFolder = new UnityAssetsImagesTilesetFolder(
                        pathSource: FileEntryPathSource.FromString(
                            System.IO.Path.Combine(this.Path.AsStr, "Tileset")),
                        convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                    replaceSeparators: true));
                }

                return imagesTilesetFolder;
            }
        }
        #endregion

        // - プライベート・フィールド

        UnityAssetsImagesTilesetFolder imagesTilesetFolder;
    }
}
