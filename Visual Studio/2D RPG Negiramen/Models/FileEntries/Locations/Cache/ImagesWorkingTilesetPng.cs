namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.Cache;

using _2D_RPG_Negiramen.Coding;

/// <summary>
///     😁 OSの 📂 キャッシュ・ディレクトリー の `{あたなのサークル名}/{あなたの作品名}/Images/working_tileset.png` ファイルの場所
///     
///     <list type="bullet">
///         <item>イミュータブル</item>
///         <item><see cref="App.Configuration"/></item>
///     </list>
/// </summary>
/// <example>
///     "C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalCache\Doujin Circle Negiramen\Negiramen Quest\Images\working_tileset.png"
/// </example>
internal class ImagesWorkingTilesetPng : Its
{
    // - その他

    #region その他（生成　関連）
    /// <summary>
    ///     生成
    /// </summary>
    internal ImagesWorkingTilesetPng()
        : base()
    {
    }

    /// <summary>
    ///     生成
    /// </summary>
    internal ImagesWorkingTilesetPng(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
        : base(pathSource, convert)
    {
    }
    #endregion

    // - インターナル静的プロパティ

    #region プロパティ（空オブジェクト）
    /// <summary>
    ///     空オブジェクト
    /// </summary>
    internal static ImagesWorkingTilesetPng Empty { get; } = new ImagesWorkingTilesetPng();
    #endregion
}
