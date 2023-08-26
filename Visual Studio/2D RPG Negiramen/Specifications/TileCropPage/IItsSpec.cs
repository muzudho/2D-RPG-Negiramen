namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.Geometric;
using _2D_RPG_Negiramen.Models.Visually;
using System.Globalization;
using TheFileEntryLocations = Models.FileEntries.Locations;
using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;

/// <summary>
///     タイル切抜きページ
/// </summary>
interface IItsSpec
{
    // - パブリック・プロパティ

    CultureInfo IndoorCultureInfoSelected { set; }

    TileRecordVisually IndoorCropTileSavesRecordVisually { get; }

    TileRecordVisually IndoorCropTileTargetTileRecordVisually { get; }

    TileIdOrEmpty IndoorCropTileIdOrEmpty { set; get; }

    Zoom IndoorZoomValue { get; set; }

    void IndoorCropCursorRefreshCanvasTrick(string codePlace);



    float OutdoorZoomAsFloat { get; set; }

    int IndoorGridUnitSourceValueWidthAsInt { get; }
    int IndoorGridUnitSourceValueHeightAsInt { get; }


    /// <summary>
    ///     ［切抜きカーソルが指すタイル］の元画像ベースの矩形
    ///     
    ///     <list type="bullet">
    ///         <item>カーソルが無いとき、大きさの無いカーソルを返す</item>
    ///     </list>
    /// </summary>
    RectangleInt OutdoorCroppedCursorPointedTileSourceRect { get; set; }

    void OutdoorInvalidateDeletesButton();

    void OutdoorInvalidateCultureInfo();


    bool HasIntersectionBetweenCroppedCursorAndRegisteredTile { get; set; }

    bool IsCongruenceBetweenCroppedCursorAndRegisteredTile { get; set; }

    // - パブリック・メソッド

    void WholeRefreshForTileAdd();

    void AddsButtonRefresh();

    void DeletesButtonRefresh();



    void WholeRemakeWorkingTilesetImage();
    void WholeRemakeGridCanvasImage();

    void CropCursorRecalculateWorkingGridTileWidth();
    void CropCursorRecalculateWorkingGridTileHeight();

    TheGeometric.WidthFloat CropCursorWorkingWidthWithoutTrick { set; }
    HeightFloat OutdoorCroppedCursorPointedTileWorkingHeight { set; }

    List<TileRecordVisually> OutdoorTilesetSettingsVMTileRecordVisuallyList { get; }

}
