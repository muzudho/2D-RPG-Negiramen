namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;

/// <summary>
///     タイル切抜きページ
/// </summary>
interface IItsIndoor
{
    /// <summary>屋内（ページの各要素）</summary>
    ItsRoomsideDoors RoomsideDoors { get; }

    // - パブリック・メソッド

    int TilesetSourceImageWidthAsInt { get; }
    int TilesetSourceImageHeightAsInt { get; }
}
