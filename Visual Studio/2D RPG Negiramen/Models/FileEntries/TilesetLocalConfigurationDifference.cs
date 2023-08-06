namespace _2D_RPG_Negiramen.Models.FileEntries;

/// <summary>
///     😁 タイルセット・ローカル構成の差分
///     
///     <list type="bullet">
///         <item>ミュータブル</item>
///     </list>
/// </summary>
public class TilesetLocalConfigurationDifference
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
