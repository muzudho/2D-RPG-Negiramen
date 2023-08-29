namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.Geometric;
using _2D_RPG_Negiramen.Models.Visually;
using TheFileEntryLocations = Models.FileEntries.Locations;
using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;

/// <summary>
///     廊下の、屋外への向き
/// </summary>
interface IItsCorridorOutdoorDirection
{
    /// <summary>
    ///     屋外側のドア
    /// </summary>
    ItsGardensideDoor GardensideDoor { get; }

    void ObsoletedPageVMInvalidateAddsButton();
    void ObsoletedPageVMInvalidateTilesetSettingsVM();
    void ObsoletedPageVMInvalidateForHistory();

    #region 変更通知メソッド（［選択タイル］　関連）
    /// <summary>
    ///     ［選択タイル］Ｉｄの再描画
    /// </summary>
    void ObsoletedPageVMInvalidateTileIdChange();
    #endregion

    string ObsoletedPageVMAddsButtonText { get; set; }
    string ObsoletedPageVMAddsButtonHint { get; }
    void ObsoletedPageVMInvalidateWorkingTargetTile();

    float ObsoletedPageVMWorkingGridTileWidthAsFloat { set; }
    float ObsoletedPageVMWorkingGridTileHeightAsFloat { set; }

    bool ObsoletedPageVMCropTileLogicalDeleteAsBool { set; }

    int ObsoletedPageVMCropTileSourceLeftAsInt { set; }
    int ObsoletedPageVMCropTileSourceTopAsInt { set; }
    int ObsoletedPageVMCropTileSourceWidthAsInt { set; }
    int ObsoletedPageVMCropTileSourceHeightAsInt { set; }

    void ObsoletedPageVMInvalidateIsMouseDragging();

    float ObsoletedPageVMZoomMinAsFloat { get; }
    float ObsoletedPageVMZoomMaxAsFloat { get; }



    float ObsoletedPageVMZoomAsFloat { get; set; }

    void ObsoletedPageVMInvalidateDeletesButton();

    void ObsoletedPageVMInvalidateCultureInfo();

    void RefreshForTileAdd();

    void RemakeWorkingTilesetImage();
    void RemakeGridCanvasImage();
}
