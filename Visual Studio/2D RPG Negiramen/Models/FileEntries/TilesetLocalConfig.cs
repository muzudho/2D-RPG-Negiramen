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
    public FileExtension ExtensionObj { get; set; } = FileExtension.Empty;

    /// <summary>
    ///     ファイル・ステム
    ///     
    ///     <list type="bullet">
    ///         <item><see cref="UUIDObj"/>が分かっているときは、ファイル・ステムは使わない</item>
    ///     </list>
    /// </summary>
    public FileStem FileStemObj { get; set; } = FileStem.Empty;

    /// <summary>
    ///     公開日
    /// </summary>
    public DateTime PublishDate { get; set; } = DateTime.MinValue;

    /// <summary>
    ///     UUID
    /// </summary>
    public UUID UUIDObj { get; set; } = UUID.Empty;
}
