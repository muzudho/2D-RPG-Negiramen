namespace _2D_RPG_Negiramen.Hierarchy.TileCropPage;

using _2D_RPG_Negiramen.Models;
using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;
using TheHierarchyTileCropPage = _2D_RPG_Negiramen.Hierarchy.TileCropPage;

/// <summary>
///     メンバー・ネットワーク
///     
///     <list type="bullet">
///         <item>密結合を認めるオブジェクトのコレクション</item>
///     </list>
/// </summary>
internal class ItsMemberNetwork
{
    internal ItsMemberNetwork(
        TheHierarchyTileCropPage.ItsCommon hierarchyCommon)
    {
        this.CommonOfHierarchy = hierarchyCommon;
    }

    /// <summary>文化情報</summary>
    internal InnerCultureInfo IndoorCultureInfo
    {
        get
        {
            if (this.indoorCultureInfo == null)
            {
                this.indoorCultureInfo = new InnerCultureInfo();
            }

            return this.indoorCultureInfo;
        }
    }
    InnerCultureInfo indoorCultureInfo;

    /// <summary>ポインティング・デバイス</summary>
    internal InnerPointingDevice PointingDevice
    {
        get
        {
            if (this.pointingDevice == null)
            {
                this.pointingDevice = new InnerPointingDevice();
            }

            return this.pointingDevice;
        }
    }
    InnerPointingDevice pointingDevice;

    /// <summary>ズーム</summary>
    internal ZoomProperties ZoomProperties
    {
        get
        {
            if (this.zoomProperties == null)
            {
                this.zoomProperties = new ZoomProperties(
                    memberNetwork: this);
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
            if (this.gridUnit == null)
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
            if (this.cropCursor == null)
            {
                this.cropCursor = new CropCursor(this);
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
                this.addsButton = new AddsButton(
                    commonOfHierarchy: this.CommonOfHierarchy,
                    roomsideDoors: this);
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
                this.deletesButton = new DeletesButton(this);
            }

            return this.deletesButton;
        }
    }
    DeletesButton deletesButton;

    #region プロパティ（［タイルセット元画像］　関連）
    /// <summary>
    ///     ［タイルセット元画像］のサイズ
    /// </summary>
    internal TheGeometric.SizeInt TilesetSourceImageSize { get; set; } = TheGeometric.SizeInt.Empty;
    #endregion

    /// <summary>
    ///     変更通知プロパティ <see cref="HalfThicknessOfGridLineAsInt"/> に関わる
    ///     ［元画像グリッド］の線の半分の太さ
    /// </summary>
    internal ThicknessOfLine HalfThicknessOfGridLine
    {
        get => this.halfThicknessOfGridLine;
    }

    //internal void SetHalfThicknessOfGridLine(ThicknessOfLine value)
    //{
    //    if (this.halfThicknessOfGridLine != value)
    //    {
    //        this.halfThicknessOfGridLine = value;

    //        // 屋外を更新
    //        this.Corridor.OwnerPageVM.InvalidateHalfThicknessOfGridLineAsInt();
    //    }
    //}

    /// <summary>［元画像グリッド］の線の太さの半分</summary>
    ThicknessOfLine halfThicknessOfGridLine = new(1);

    // - プライベート・プロパティ

    TheHierarchyTileCropPage.ItsCommon CommonOfHierarchy { get; }
}
