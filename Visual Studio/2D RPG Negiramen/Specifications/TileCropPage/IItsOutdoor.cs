namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.Models;
using TheFileEntryLocations = Models.FileEntries.Locations;

interface IItsOutdoor
{
    void InvalidateAddsButton();
    void InvalidateTilesetSettingsVM();
    void InvalidateForHistory();
    bool TilesetSettingsVMDeleteLogical(TileIdOrEmpty id);
    bool TilesetSettingsVMUndeleteLogical(TileIdOrEmpty id);

    /// <summary>
    ///     ［タイルセット・データテーブル］ファイルの場所
    ///     <list type="bullet">
    ///         <item>ページの引数として使用</item>
    ///     </list>
    /// </summary>
    TheFileEntryLocations.UnityAssets.DataCsvTilesetCsv TilesetDatatableFileLocation { get; }

    #region 変更通知メソッド（［選択タイル］　関連）
    /// <summary>
    ///     ［選択タイル］Ｉｄの再描画
    /// </summary>
    void InvalidateTileIdChange();
    #endregion

    void TilesetSettingsVMIncreaseUsableId();
    TileIdOrEmpty TilesetSettingsVMUsableId { get; }
}
