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
    /// <summary>屋内（ページの各要素）</summary>
    ItsRoomsideDoors RoomsideDoors { get; }

    // - パブリック・プロパティ

    TileRecordVisually RoomsideDoorsCropTileSavesRecordVisually { get; }

    TileRecordVisually RoomsideDoorsCropTileTargetTileRecordVisually { get; }

    TileIdOrEmpty RoomsideDoorsCropTileIdOrEmpty { set; get; }

    void CropCursorRefreshCanvasTrick(string codePlace);

    int GridUnitSourceValueWidthAsInt { get; }
    int GridUnitSourceValueHeightAsInt { get; }


    bool HasIntersectionBetweenCroppedCursorAndRegisteredTile { get; set; }

    bool IsCongruenceBetweenCroppedCursorAndRegisteredTile { get; set; }

    // - パブリック・メソッド

    void AddsButtonRefresh();

    void DeletesButtonRefresh();




    void CropCursorRecalculateWorkingGridTileWidth();
    void CropCursorRecalculateWorkingGridTileHeight();

    TheGeometric.WidthFloat CropCursorWorkingWidthWithoutTrick { set; }

    int TilesetSourceImageWidthAsInt { get; }
    int TilesetSourceImageHeightAsInt { get; }
}
