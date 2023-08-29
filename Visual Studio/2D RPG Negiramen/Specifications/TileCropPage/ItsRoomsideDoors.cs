namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.Models.Geometric;

/// <summary>
///     各部屋
///     
///     <list type="bullet">
///         <item>各部屋（ページの各要素）を持つ</item>
///     </list>
/// </summary>
internal class ItsRoomsideDoors
{
    internal ItsRoomsideDoors(ItsCorridor corridor)
    {
        this.IndoorCultureInfo = new InnerCultureInfo(
            gardensideDoor: corridor.GardensideDoor,
            indoor: corridor);

        this.ZoomProperties = new ZoomProperties(
            twoWayDoor: corridor.TwoWayDoor,
            gardensideDoor: corridor.GardensideDoor,
            indoor: corridor);

        this.CropTile = new CropTile(
            gardensideDoor: corridor.GardensideDoor,
            indoor: corridor);
    }

    /// <summary>文化情報</summary>
    internal InnerCultureInfo IndoorCultureInfo { get; }

    /// <summary>ズーム</summary>
    internal ZoomProperties ZoomProperties { get; }

    #region プロパティ（切抜きカーソルが指すタイル）
    /// <summary>切抜きカーソルが指すタイル</summary>
    internal CropTile CropTile { get; }
    #endregion
}
