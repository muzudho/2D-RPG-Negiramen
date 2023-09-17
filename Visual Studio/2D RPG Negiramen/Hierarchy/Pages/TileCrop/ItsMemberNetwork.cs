namespace _2D_RPG_Negiramen.Hierarchy.Pages.TileCrop;

using _2D_RPG_Negiramen.Models;
using TheGeometric = Models.Geometric;

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
        ItsCommon commonOfHierarchy)
    {
        CommonOfHierarchy = commonOfHierarchy;
    }

    /// <summary>文化情報</summary>
    internal InnerCultureInfo IndoorCultureInfo
    {
        get
        {
            if (indoorCultureInfo == null)
            {
                indoorCultureInfo = new InnerCultureInfo();
            }

            return indoorCultureInfo;
        }
    }
    InnerCultureInfo indoorCultureInfo;

    /// <summary>ポインティング・デバイス</summary>
    internal InnerPointingDevice PointingDevice
    {
        get
        {
            if (pointingDevice == null)
            {
                pointingDevice = new InnerPointingDevice();
            }

            return pointingDevice;
        }
    }
    InnerPointingDevice pointingDevice;

    /// <summary>ズーム</summary>
    internal ZoomProperties ZoomProperties
    {
        get
        {
            if (zoomProperties == null)
            {
                zoomProperties = new ZoomProperties(
                    memberNetwork: this);
            }

            return zoomProperties;
        }
    }
    ZoomProperties zoomProperties;

    /// <summary>グリッド単位</summary>
    internal GridUnit GridUnit
    {
        get
        {
            if (gridUnit == null)
            {
                gridUnit = new GridUnit();
            }

            return gridUnit;
        }
    }
    GridUnit gridUnit;

    /// <summary>切抜きカーソル</summary>
    internal CropCursor CropCursor
    {
        get
        {
            if (cropCursor == null)
            {
                cropCursor = new CropCursor(this);
            }

            return cropCursor;
        }
    }
    CropCursor cropCursor;

    #region プロパティ（切抜きカーソルが指すタイル）
    /// <summary>切抜きカーソルが指すタイル</summary>
    internal CropTile CropTile
    {
        get
        {
            if (cropTile == null)
            {
                cropTile = new CropTile(
                    memberNetwork: this);
            }

            return cropTile;
        }
    }
    CropTile cropTile;
    #endregion

    /// <summary>追加ボタン</summary>
    internal AddsButton AddsButton
    {
        get
        {
            if (addsButton == null)
            {
                addsButton = new AddsButton(
                    commonOfHierarchy: CommonOfHierarchy,
                    memberNetwork: this);
            }

            return addsButton;
        }
    }
    AddsButton addsButton;

    /// <summary>削除ボタン</summary>
    internal DeletesButton DeletesButton
    {
        get
        {
            if (deletesButton == null)
            {
                deletesButton = new DeletesButton(this);
            }

            return deletesButton;
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
        get => halfThicknessOfGridLine;
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


    /* プロジェクト '2D RPG Negiramen (net7.0-maccatalyst)' からのマージされていない変更
    前:
        TheHierarchyTileCropPage.ItsCommon CommonOfHierarchy { get; }
    後:
        ItsCommon CommonOfHierarchy { get; }
    */
    ItsCommon CommonOfHierarchy { get; }
}
