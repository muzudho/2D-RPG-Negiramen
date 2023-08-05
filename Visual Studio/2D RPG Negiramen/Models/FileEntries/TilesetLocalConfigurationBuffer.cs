namespace _2D_RPG_Negiramen.Models.FileEntries;

/// <summary>
///     😁 タイルセット・ローカル構成バッファー
///     
///     <list type="bullet">
///         <item>ミュータブル</item>
///     </list>
/// </summary>
public class TilesetLocalConfigurationBuffer
{
    // - インターナル・プロパティ

    #region プロパティ（タイトル）
    /// <summary>
    ///     タイトル
    /// </summary>
    internal TilesetTitle? Title { get; set; }
    #endregion

    #region プロパティ（著者）
    /// <summary>
    ///     著者
    /// </summary>
    internal Author? Author { get; set; }
    #endregion
}
