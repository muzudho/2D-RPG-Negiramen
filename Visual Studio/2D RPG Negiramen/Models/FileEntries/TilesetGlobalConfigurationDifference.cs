namespace _2D_RPG_Negiramen.Models.FileEntries;

/// <summary>
///     😁 タイルセット・グローバル構成の差分
///     
///     <list type="bullet">
///         <item>ミュータブル</item>
///     </list>
/// </summary>
internal class TilesetGlobalConfigurationDifference
{
    // - インターナル・プロパティ

    #region プロパティ（UUID）
    /// <summary>
    ///     UUID
    /// </summary>
    internal UUID? Uuid { get; set; }
    #endregion

    #region プロパティ（拡張子）
    /// <summary>
    ///     拡張子
    /// </summary>
    internal FileExtension? Extension { get; set; }
    #endregion
}
