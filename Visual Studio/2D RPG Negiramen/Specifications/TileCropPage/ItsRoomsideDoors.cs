namespace _2D_RPG_Negiramen.Specifications.TileCropPage;
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
            roomsideDoors: corridor.RoomsideDoors);

        this.ZoomProperties = new ZoomProperties(
            twoWayDoor: corridor.TwoWayDoor,
            gardensideDoor: corridor.GardensideDoor,
            roomsideDoors: corridor.RoomsideDoors);

        this.GridUnit = new GridUnit(corridor);

        this.CropCursor = new CropCursor(corridor.GardensideDoor, corridor);

        this.CropTile = new CropTile(
            gardensideDoor: corridor.GardensideDoor,
            roomsideDoors: corridor.RoomsideDoors,
            indoor: corridor);

        this.AddsButton = new AddsButton(corridor.GardensideDoor, corridor.RoomsideDoors, corridor);
        this.DeletesButton = new DeletesButton(corridor.GardensideDoor, corridor.RoomsideDoors);
    }

    /// <summary>文化情報</summary>
    internal InnerCultureInfo IndoorCultureInfo { get; }

    /// <summary>ズーム</summary>
    internal ZoomProperties ZoomProperties { get; }

    /// <summary>グリッド単位</summary>
    internal GridUnit GridUnit { get; }

    /// <summary>切抜きカーソル</summary>
    internal CropCursor CropCursor { get; }

    #region プロパティ（切抜きカーソルが指すタイル）
    /// <summary>切抜きカーソルが指すタイル</summary>
    internal CropTile CropTile { get; }
    #endregion

    /// <summary>追加ボタン</summary>
    internal AddsButton AddsButton { get; }

    /// <summary>削除ボタン</summary>
    internal DeletesButton DeletesButton { get; }

    #region プロパティ（切抜きカーソルと、既存タイルが交差しているか？）
    /// <summary>
    ///     切抜きカーソルと、既存タイルが交差しているか？
    /// </summary>
    /// <returns>そうだ</returns>
    public bool HasIntersectionBetweenCroppedCursorAndRegisteredTile { get; set; }
    #endregion

    #region プロパティ（切抜きカーソルと、既存タイルは合同か？）
    /// <summary>
    ///     切抜きカーソルと、既存タイルは合同か？
    /// </summary>
    /// <returns>そうだ</returns>
    public bool IsCongruenceBetweenCroppedCursorAndRegisteredTile { get; set; }
    #endregion
}
