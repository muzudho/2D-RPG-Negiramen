namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.Models;
using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;

/// <summary>
///     タイル切抜きページ
/// </summary>
interface IItsIndoor
{
    /// <summary>屋内（ページの各要素）</summary>
    ItsRoomsideDoors RoomsideDoors { get; }

    // - パブリック・プロパティ

    void CropCursorRefreshCanvasTrick(string codePlace);

    int GridUnitSourceValueWidthAsInt { get; }
    int GridUnitSourceValueHeightAsInt { get; }


    bool HasIntersectionBetweenCroppedCursorAndRegisteredTile { get; set; }

    bool IsCongruenceBetweenCroppedCursorAndRegisteredTile { get; set; }

    // - パブリック・メソッド

    void CropCursorRecalculateWorkingGridTileWidth();
    void CropCursorRecalculateWorkingGridTileHeight();

    TheGeometric.WidthFloat CropCursorWorkingWidthWithoutTrick { set; }

    int TilesetSourceImageWidthAsInt { get; }
    int TilesetSourceImageHeightAsInt { get; }
}
