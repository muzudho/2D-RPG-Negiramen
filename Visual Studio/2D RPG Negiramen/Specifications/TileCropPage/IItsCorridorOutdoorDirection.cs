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

    void InvalidateAddsButton();
    void ObsoletedInvalidateTilesetSettingsVM();
    void ObsoletedInvalidateForHistory();

    #region 変更通知メソッド（［選択タイル］　関連）
    /// <summary>
    ///     ［選択タイル］Ｉｄの再描画
    /// </summary>
    void ObsoletedInvalidateTileIdChange();
    #endregion

    bool ObsoletedTilesetSettingsVMTryGetTileById(TileIdOrEmpty tileId, out TileRecordVisually? resultVisuallyOrNull);
    void ObsoletedTilesetSettingsVMAddTileVisually(TileIdOrEmpty id,
        TheGeometric.RectangleInt rect,
        Zoom zoom,
        TileTitle title,
        LogicalDelete logicalDelete);

    bool ObsoletedTilesetSettingsVMTryRemoveTileById(TileIdOrEmpty tileId, out TileRecordVisually? resultVisuallyOrNull);

    string AddsButtonText { get; set; }
    string AddsButtonHint { get; }
    void InvalidateWorkingTargetTile();

    float WorkingGridTileWidthAsFloat { set; }
    float WorkingGridTileHeightAsFloat { set; }

    bool CropTileLogicalDeleteAsBool { set; }

    int CropTileSourceLeftAsInt { set; }
    int CropTileSourceTopAsInt { set; }
    int CropTileSourceWidthAsInt { set; }
    int CropTileSourceHeightAsInt { set; }

    void InvalidateIsMouseDragging();

    float ObsoletedPageVMZoomMinAsFloat { get; }
    float ObsoletedPageVMZoomMaxAsFloat { get; }



    float ObsoletedPageVMZoomAsFloat { get; set; }

    /// <summary>
    ///     ［切抜きカーソルが指すタイル］の元画像ベースの矩形
    ///     
    ///     <list type="bullet">
    ///         <item>カーソルが無いとき、大きさの無いカーソルを返す</item>
    ///     </list>
    /// </summary>
    RectangleInt CroppedCursorPointedTileSourceRect { get; set; }

    void ObsoletedInvalidateDeletesButton();

    void ObsoletedInvalidateCultureInfo();

    HeightFloat CroppedCursorPointedTileWorkingHeight { set; }

    void RefreshForTileAdd();

    void RemakeWorkingTilesetImage();
    void RemakeGridCanvasImage();
}
