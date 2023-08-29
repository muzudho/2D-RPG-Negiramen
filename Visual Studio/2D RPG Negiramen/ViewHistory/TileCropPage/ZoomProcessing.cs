namespace _2D_RPG_Negiramen.ViewHistory.TileCropPage;

using _2D_RPG_Negiramen.Models.Geometric;
using _2D_RPG_Negiramen.Models.History;
using _2D_RPG_Negiramen.Specifications.TileCropPage;
using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;

/// <summary>
///     ［ズーム］処理
/// </summary>
internal class ZoomProcessing : IProcessing
{
    // - その他

    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="indoor"></param>
    /// <param name="oldValue">変更前の値</param>
    /// <param name="newValue">変更後の値</param>
    internal ZoomProcessing(
        ItsGardensideDoor gardensideDoor,
        IItsCorridorOutdoorDirection obsoletedOutdoor,
        IItsIndoor indoor,
        Zoom oldValue,
        Zoom newValue)
    {
        this.GardensideDoor = gardensideDoor;
        this.ObsoletedOutdoor = obsoletedOutdoor;
        this.Indoor = indoor;
        this.OldValue = oldValue;
        this.NewValue = newValue;
    }

    // - パブリック・メソッド

    /// <summary>
    ///     ドゥ
    /// </summary>
    public void Do()
    {
        this.Indoor.RoomsideDoors.ZoomValue = this.NewValue;

        this.AfterChanged();
    }

    /// <summary>
    ///     アンドゥ
    /// </summary>
    public void Undo()
    {
        this.Indoor.RoomsideDoors.ZoomValue = this.OldValue;

        this.AfterChanged();
    }

    // - プライベート・プロパティ

    /// <summary>
    ///     内部クラス
    /// </summary>
    ItsGardensideDoor GardensideDoor { get; }
    IItsCorridorOutdoorDirection ObsoletedOutdoor { get; }
    IItsIndoor Indoor { get; }

    /// <summary>
    ///     変更前の値
    /// </summary>
    Zoom OldValue { get; }

    /// <summary>
    ///     変更後の値
    /// </summary>
    Zoom NewValue { get; }

    // - プライベート・メソッド

    /// <summary>
    ///     ［ズーム］変更後の影響
    /// </summary>
    void AfterChanged()
    {
        // ［タイルセット作業画像］の更新
        {
            // 画像の再作成
            this.ObsoletedOutdoor.RemakeWorkingTilesetImage();
        }

        // ［元画像グリッド］の更新
        {
            // キャンバス画像の再作成
            this.ObsoletedOutdoor.RemakeGridCanvasImage();
        }

        // ［作業グリッド］の再計算
        {
            // 横幅
            this.Indoor.CropCursorRecalculateWorkingGridTileWidth();
            // 縦幅
            this.Indoor.CropCursorRecalculateWorkingGridTileHeight();
        }

        // ［切抜きカーソルが指すタイル］更新
        {
            //// 位置
            //this.Owner.CroppedCursorPointedTileWorkingLocation = new TheGeometric.PointFloat(
            //    x: new TheGeometric.XFloat(this.Owner.ZoomAsFloat * this.Owner.CroppedCursorPointedTileSourceRect.Location.X.AsInt),
            //    y: new TheGeometric.YFloat(this.Owner.ZoomAsFloat * this.Owner.CroppedCursorPointedTileSourceRect.Location.Y.AsInt));

            // サイズ
            this.Indoor.CropCursorWorkingWidthWithoutTrick = new TheGeometric.WidthFloat(this.ObsoletedOutdoor.ObsoletedZoomAsFloat * this.ObsoletedOutdoor.CroppedCursorPointedTileSourceRect.Size.Width.AsInt);
            this.ObsoletedOutdoor.CroppedCursorPointedTileWorkingHeight = new TheGeometric.HeightFloat(this.ObsoletedOutdoor.ObsoletedZoomAsFloat * this.ObsoletedOutdoor.CroppedCursorPointedTileSourceRect.Size.Height.AsInt);
        }

        // 全ての［登録タイル］の更新
        foreach (var registeredTileVM in this.GardensideDoor.TilesetSettingsVM.TileRecordVisuallyList)
        {
            // ズーム
            registeredTileVM.Zoom = this.Indoor.RoomsideDoors.ZoomValue;
        }

        // 変更通知
        this.ObsoletedOutdoor.ObsoletedInvalidateForHistory();
    }
}
