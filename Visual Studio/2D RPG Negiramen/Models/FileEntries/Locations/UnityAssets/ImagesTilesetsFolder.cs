namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets
{
    using _2D_RPG_Negiramen.Coding;
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.FileEntries.Locations;

    /// <summary>
    ///     😁 Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tilesets` フォルダの場所
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///         <item><see cref="ItsFolder"/></item>
    ///     </list>
    /// </summary>
    class ImagesTilesetsFolder : Its
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        internal ImagesTilesetsFolder()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal ImagesTilesetsFolder(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
            : base(pathSource, convert)
        {
        }
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（Unity の 📄 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tilesets/{名前}.png` ファイルの場所）
        /// <summary>
        ///     Unity の 📄 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tilesets/{名前}.png` ファイルの場所
        /// </summary>
        internal ImagesTilesetPng CreateTilesetPng(string fileStem)
        {
            return new ImagesTilesetPng(
                pathSource: FileEntryPathSource.FromString(
                    System.IO.Path.Combine(Path.AsStr, $"{fileStem}.png")),
                convert: (pathSource) => FileEntryPath.From(pathSource,
                                                            replaceSeparators: true));
        }
        #endregion
    }
}
