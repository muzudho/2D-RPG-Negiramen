namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.Geometric;
using _2D_RPG_Negiramen.Models.Visually;
using TheFileEntryLocations = Models.FileEntries.Locations;
using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;

/// <summary>
///     タイル切抜きページ
/// </summary>
interface IItsSpec
{
    // - パブリック・プロパティ

    TileRecordVisually CropTileTargetTileRecordVisually { get; }

    TileIdOrEmpty CropTileIdOrEmpty { set; get; }

    Zoom ZoomValue { get; }

    /// <summary>
    ///     ［タイルセット・データテーブル］ファイルの場所
    ///     <list type="bullet">
    ///         <item>ページの引数として使用</item>
    ///     </list>
    /// </summary>
    TheFileEntryLocations.UnityAssets.DataCsvTilesetCsv WholeTilesetDatatableFileLocation { get; }

    #region 変更通知メソッド（［選択タイル］　関連）
    /// <summary>
    ///     ［選択タイル］Ｉｄの再描画
    /// </summary>
    void WholeInvalidateTileIdChange();
    #endregion

    bool WholeTilesetSettingsVMSaveCsv(TheFileEntryLocations.UnityAssets.DataCsvTilesetCsv tileSetSettingsFile);

    bool WholeTilesetSettingsVMTryGetTileById(TileIdOrEmpty tileId, out TileRecordVisually? resultVisuallyOrNull);

    void WholeTilesetSettingsVMAddTileVisually(TileIdOrEmpty id,
        TheGeometric.RectangleInt rect,
        Zoom zoom,
        TileTitle title,
        LogicalDelete logicalDelete);

    bool WholeTilesetSettingsVMTryRemoveTileById(TileIdOrEmpty tileId, out TileRecordVisually? resultVisuallyOrNull);

    string WholePageVMAddsButtonText { get; set; }
    string WholePageVMAddsButtonHint { get; }

    // - パブリック・メソッド

    void WholeRefreshForTileAdd();

    void DeletesButtonRefresh();

}
