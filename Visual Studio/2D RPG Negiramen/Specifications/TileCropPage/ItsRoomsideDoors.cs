﻿namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;

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
        this.Corridor = corridor;
    }

    /// <summary>廊下</summary>
    ItsCorridor Corridor { get; }

    /// <summary>文化情報</summary>
    internal InnerCultureInfo IndoorCultureInfo
    {
        get
        {
            if (this.indoorCultureInfo == null)
            {
                this.indoorCultureInfo = new InnerCultureInfo(
                    gardensideDoor: this.Corridor.GardensideDoor,
                    roomsideDoors: this.Corridor.RoomsideDoors);
            }

            return this.indoorCultureInfo;
        }
    }
    InnerCultureInfo indoorCultureInfo;

    /// <summary>ズーム</summary>
    internal ZoomProperties ZoomProperties
    {
        get
        {
            if (this.zoomProperties == null)
            {
                this.zoomProperties = new ZoomProperties(
                    twoWayDoor: this.Corridor.TwoWayDoor,
                    gardensideDoor: this.Corridor.GardensideDoor,
                    roomsideDoors: this.Corridor.RoomsideDoors);
            }

            return this.zoomProperties;
        }
    }
    ZoomProperties zoomProperties;

    /// <summary>グリッド単位</summary>
    internal GridUnit GridUnit
    {
        get
        {
            if (this.gridUnit==null)
            {
                this.gridUnit = new GridUnit();
            }

            return this.gridUnit;
        }
    }
    GridUnit gridUnit;

    /// <summary>切抜きカーソル</summary>
    internal CropCursor CropCursor
    {
        get
        {
            if (this.cropCursor==null)
            {
                this.cropCursor = new CropCursor(this.Corridor.GardensideDoor, this);
            }

            return this.cropCursor;
        }
    }
    CropCursor cropCursor;

    #region プロパティ（切抜きカーソルが指すタイル）
    /// <summary>切抜きカーソルが指すタイル</summary>
    internal CropTile CropTile
    {
        get
        {
            if (this.cropTile == null)
            {
                this.cropTile = new CropTile(
                    gardensideDoor: this.Corridor.GardensideDoor,
                    roomsideDoors: this);
            }

            return this.cropTile;
        }
    }
    CropTile cropTile;
    #endregion

    /// <summary>追加ボタン</summary>
    internal AddsButton AddsButton
    {
        get
        {
            if (this.addsButton == null)
            {
                this.addsButton = new AddsButton(this.Corridor.GardensideDoor, this);
            }

            return this.addsButton;
        }
    }
    AddsButton addsButton;

    /// <summary>削除ボタン</summary>
    internal DeletesButton DeletesButton
    {
        get
        {
            if (this.deletesButton == null)
            {
                this.deletesButton = new DeletesButton(this.Corridor.GardensideDoor, this);
            }

            return this.deletesButton;
        }
    }
    DeletesButton deletesButton;

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

    #region プロパティ（［タイルセット元画像］　関連）
    /// <summary>
    ///     ［タイルセット元画像］のサイズ
    /// </summary>
    internal TheGeometric.SizeInt TilesetSourceImageSize { get; set; } = TheGeometric.SizeInt.Empty;
    #endregion
}
