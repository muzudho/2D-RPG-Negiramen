namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.Cache
{
    using _2D_RPG_Negiramen;
    using _2D_RPG_Negiramen.Models;

    /// <summary>
    ///     😁 OSの 📂 キャッシュ・ディレクトリー の `{あたなのサークル名}/{あなたの作品名}/Images/Tilesets/Thumbnails` フォルダの場所
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///         <item><see cref="App.Configuration"/></item>
    ///     </list>
    /// </summary>
    /// <example>
    ///     "C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalCache\Doujin Circle Negiramen\Negiramen Quest\Images\Tileset\Thumbnails"
    /// </example>
    internal class ImagesTilesetsThumbnailsFolder : Its
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        internal ImagesTilesetsThumbnailsFolder(FileEntryPath parentPath)
            : base(pathSource: FileEntryPathSource.FromString(System.IO.Path.Combine(parentPath.AsStr, "Thumbnails")),
                   convert: (pathSource) => FileEntryPath.From(pathSource,
                                                               replaceSeparators: true))
        {
        }
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（OSの 📂 キャッシュ・ディレクトリー の `{あたなのサークル名}/{あなたの作品名}/Images/Tilesets/Thumbnails/{名前}.png` ファイルの場所）
        /// <summary>
        ///     OSの 📂 キャッシュ・ディレクトリー の `{あたなのサークル名}/{あなたの作品名}/Images/Tilesets/Thumbnails/{名前}.png` ファイルの場所
        /// </summary>
        internal ImagesTilesetsThumbnailsPng CreateTilesetThumbnailPng(string fileStem)
        {
            return new ImagesTilesetsThumbnailsPng(
                pathSource: FileEntryPathSource.FromString(
                    System.IO.Path.Combine(Path.AsStr, $"{fileStem}.png")),
                convert: (pathSource) => FileEntryPath.From(pathSource,
                                                            replaceSeparators: true));
        }
        #endregion
    }
}
