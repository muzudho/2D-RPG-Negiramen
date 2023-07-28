namespace _2D_RPG_Negiramen.Models.FileEntries;

/// <summary>
///     😁 タイルセット・ローカル構成
/// </summary>
internal class TilesetLocalConfig
{
    // - パブリック・メソッド

    /// <summary>
    ///     拡張子
    /// </summary>
    public string Extension { get; set; } = string.Empty;

    /// <summary>
    ///     ファイル・ステム
    ///     
    ///     <list type="bullet">
    ///         <item><see cref="UUID"/>が分かっているときは、ファイル・ステムは使わない</item>
    ///     </list>
    /// </summary>
    public string FileStem { get; set; } = string.Empty;

    /// <summary>
    ///     公開日
    /// </summary>
    public DateTime PublishDate { get; set; } = DateTime.MinValue;

    /// <summary>
    ///     UUID
    /// </summary>
    public string UUID { get; set; } = string.Empty;
}
