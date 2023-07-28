namespace _2D_RPG_Negiramen.Models.FileEntries;

/// <summary>
///     😁 タイルセット・ローカル構成
/// </summary>
internal class TilesetLocalConfig
{
    // - インターナル静的プロパティ

    #region プロパティ（空オブジェクト）
    /// <summary>
    ///     空オブジェクト
    /// </summary>
    internal static TilesetLocalConfig Empty = new TilesetLocalConfig();
    #endregion

    // - インターナル・プロパティ

    #region プロパティ（タイルセット・タイトル）
    /// <summary>
    ///     タイルセット・タイトル
    /// </summary>
    internal TilesetTitle TilesetTitleObj { get; set; } = TilesetTitle.Empty;
    #endregion

    #region プロパティ（著者）
    /// <summary>
    ///     著者
    /// </summary>
    internal Author AuthorObj { get; set; } = Author.Empty;
    #endregion
}
