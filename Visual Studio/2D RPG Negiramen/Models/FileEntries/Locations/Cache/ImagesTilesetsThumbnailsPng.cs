namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.Cache
{
    using _2D_RPG_Negiramen.Coding;
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.FileEntries.Locations;

    /// <summary>
    ///     😁 OSの 📂 キャッシュ・ディレクトリー の `{あたなのサークル名}/{あなたの作品名}/Images/Tilesets/Thumbnails/{名前}.png` ファイルの場所
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///         <item><see cref="ItsFolder"/></item>
    ///     </list>
    /// </summary>
    internal class ImagesTilesetsThumbnailsPng : Its
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        internal ImagesTilesetsThumbnailsPng()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal ImagesTilesetsThumbnailsPng(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
            : base(pathSource, convert)
        {
        }
        #endregion

        // - インターナル静的プロパティ

        #region プロパティ（空オブジェクト）
        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static ImagesTilesetsThumbnailsPng Empty { get; } = new ImagesTilesetsThumbnailsPng();
        #endregion
    }
}
