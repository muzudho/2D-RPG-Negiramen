namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.Geometric;
using _2D_RPG_Negiramen.Models.Visually;
using System.Globalization;
using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;

/// <summary>
///     タイル切抜きページ
/// </summary>
interface IItsIndoor
{
    // - パブリック・プロパティ

    CultureInfo IndoorCultureInfoSelected { set; }

    TileRecordVisually IndoorCropTileSavesRecordVisually { get; }

    TileRecordVisually IndoorCropTileTargetTileRecordVisually { get; }

    TileIdOrEmpty IndoorCropTileIdOrEmpty { set; get; }

    Zoom IndoorZoomValue { get; set; }

    void IndoorCropCursorRefreshCanvasTrick(string codePlace);

    int IndoorGridUnitSourceValueWidthAsInt { get; }
    int IndoorGridUnitSourceValueHeightAsInt { get; }


    bool HasIntersectionBetweenCroppedCursorAndRegisteredTile { get; set; }

    bool IsCongruenceBetweenCroppedCursorAndRegisteredTile { get; set; }

    // - パブリック・メソッド

    void AddsButtonRefresh();

    void DeletesButtonRefresh();




    void CropCursorRecalculateWorkingGridTileWidth();
    void CropCursorRecalculateWorkingGridTileHeight();

    TheGeometric.WidthFloat CropCursorWorkingWidthWithoutTrick { set; }
}
