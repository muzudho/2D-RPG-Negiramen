namespace _2D_RPG_Negiramen.Models.FileEntries;

using TheFileEntryLocations = _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 タイルセット・グローバル構成バッファー
///     
///     <list type="bullet">
///         <item>ミュータブル</item>
///     </list>
/// </summary>
internal class TilesetGlobalConfigurationBuffer
{
    // - インターナル・プロパティ

    #region プロパティ（タイルセット・グローバル構成ファイルの場所）
    /// <summary>
    ///     タイルセット・グローバル構成ファイルの場所
    /// </summary>
    /// <example>"C:\Users\むずでょ\Documents\Unity Projects\Negiramen Practice\Assets\Doujin Circle Negiramen\Negiramen Quest\Auto Generated\Images\Tilesets\86A25699-E391-4D61-85A5-356BA8049881.toml"</example>
    internal TheFileEntryLocations.UnityAssets.ImagesTilesetToml? Location { get; set; }
    #endregion
}
