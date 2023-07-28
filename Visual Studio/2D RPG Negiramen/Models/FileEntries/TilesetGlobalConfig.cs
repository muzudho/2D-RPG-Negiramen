namespace _2D_RPG_Negiramen.Models.FileEntries;

using TheFileEntryLocation = _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 タイルセット・グローバル構成
/// </summary>
internal class TilesetGlobalConfig
{
    // - その他

    #region その他（読込）
    /// <summary>
    ///     TODO ★ 読込
    /// </summary>
    internal TilesetGlobalConfig Load(TheFileEntryLocation.UnityAssets.ImagesTilesetToml location)
    {


        return new TilesetGlobalConfig();
    }
    #endregion

    // - インターナル静的プロパティ

    #region プロパティ（空オブジェクト）
    /// <summary>
    ///     空オブジェクト
    /// </summary>
    internal static TilesetGlobalConfig Empty = new TilesetGlobalConfig();
    #endregion

    // - インターナル・プロパティ

    #region プロパティ（拡張子）
    /// <summary>
    ///     拡張子
    /// </summary>
    internal FileExtension ExtensionObj { get; set; } = FileExtension.Empty;
    #endregion

    #region プロパティ（ファイル・ステム）
    /// <summary>
    ///     ファイル・ステム
    ///     
    ///     <list type="bullet">
    ///         <item><see cref="UUIDObj"/>が分かっているときは、ファイル・ステムは使わない</item>
    ///     </list>
    /// </summary>
    internal FileStem FileStemObj { get; set; } = FileStem.Empty;
    #endregion

    #region プロパティ（公開日）
    /// <summary>
    ///     公開日
    /// </summary>
    internal DateTime PublishDate { get; set; } = DateTime.MinValue;
    #endregion

    #region プロパティ（UUID）
    /// <summary>
    ///     UUID
    /// </summary>
    internal UUID UUIDObj { get; set; } = UUID.Empty;
    #endregion
}
