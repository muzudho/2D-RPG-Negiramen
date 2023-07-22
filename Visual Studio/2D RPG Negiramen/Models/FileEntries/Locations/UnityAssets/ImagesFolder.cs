namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets
{
    using _2D_RPG_Negiramen.Coding;
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.FileEntries.Locations;

    /// <summary>
    ///     😁 Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images` フォルダーの場所
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///         <item><see cref="ItsFolder"/></item>
    ///     </list>
    /// </summary>
    internal class ImagesFolder : Its
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        internal ImagesFolder()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal ImagesFolder(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
            : base(pathSource, convert)
        {
        }
        #endregion

        // - インターナル静的プロパティ

        #region プロパティ（空オブジェクト）
        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static ImagesFolder Empty { get; } = new ImagesFolder();
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tileset` フォルダーの場所）
        /// <summary>
        ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tileset` フォルダーの場所
        /// </summary>
        internal UnityAssetsImagesTilesetFolder TilesetFolder
        {
            get
            {
                if (tilesetFolder == null)
                {
                    tilesetFolder = new UnityAssetsImagesTilesetFolder(
                        pathSource: FileEntryPathSource.FromString(
                            System.IO.Path.Combine(Path.AsStr, "Tileset")),
                        convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                    replaceSeparators: true));
                }

                return tilesetFolder;
            }
        }
        #endregion

        // - プライベート・フィールド

        UnityAssetsImagesTilesetFolder? tilesetFolder;
    }
}
