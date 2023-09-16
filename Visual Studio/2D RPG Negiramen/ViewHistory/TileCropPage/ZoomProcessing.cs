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
    /// <param name="obsoletedIndoor"></param>
    /// <param name="oldValue">変更前の値</param>
    /// <param name="newValue">変更後の値</param>
    internal ZoomProcessing(
        IItsTwoWayDoor twoWayDoor,
        ItsGardensideDoor gardensideDoor,
        ItsRoomsideDoors roomsideDoors,
        Zoom oldValue,
        Zoom newValue)
    {
        this.TwoWayDoor = twoWayDoor;
        this.GardensideDoor = gardensideDoor;
        this.RoomsideDoors = roomsideDoors;
        this.OldValue = oldValue;
        this.NewValue = newValue;
    }

    // - パブリック・メソッド

    /// <summary>
    ///     ドゥ
    /// </summary>
    public void Do()
    {
        this.GardensideDoor.PageVM.ZoomAsFloat = this.NewValue.AsFloat;

        this.AfterChanged();
    }

    /// <summary>
    ///     アンドゥ
    /// </summary>
    public void Undo()
    {
        this.GardensideDoor.PageVM.ZoomAsFloat = this.OldValue.AsFloat;

        this.AfterChanged();
    }

    // - プライベート・プロパティ

    /// <summary>内部クラス</summary>
    IItsTwoWayDoor TwoWayDoor { get; }
    ItsGardensideDoor GardensideDoor { get; }
    ItsRoomsideDoors RoomsideDoors { get; }

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
            this.TwoWayDoor.RemakeWorkingTilesetImage();
        }

        // ［元画像グリッド］の更新
        {
            // キャンバス画像の再作成
            this.TwoWayDoor.RemakeGridCanvasImage();
        }

        // ［作業グリッド］の再計算
        {
            // 横幅
            this.RoomsideDoors.CropCursor.RecalculateWorkingGridTileWidth(
                setValue: (value) =>
                {
                    this.GardensideDoor.PageVM.WorkingGridTileWidthAsFloat = this.GardensideDoor.PageVM.ZoomAsFloat * value;
                    // this.Owner.Owner.InvalidateWorkingGrid();
                });

            // 縦幅
            this.RoomsideDoors.CropCursor.RecalculateWorkingGridTileHeight(
                setValue: (value) =>
                {
                    this.GardensideDoor.PageVM.WorkingGridTileHeightAsFloat = this.GardensideDoor.PageVM.ZoomAsFloat * value;
                    // this.Owner.Owner.InvalidateWorkingGrid();
                });
        }

        // ［切抜きカーソルが指すタイル］更新
        {
            //// 位置
            //this.Owner.CroppedCursorPointedTileWorkingLocation = new TheGeometric.PointFloat(
            //    x: new TheGeometric.XFloat(this.Owner.ZoomAsFloat * this.Owner.CroppedCursorPointedTileSourceRect.Location.X.AsInt),
            //    y: new TheGeometric.YFloat(this.Owner.ZoomAsFloat * this.Owner.CroppedCursorPointedTileSourceRect.Location.Y.AsInt));

            // サイズ
            this.RoomsideDoors.CropCursor.WorkingWidthWithoutTrick = new TheGeometric.WidthFloat(this.GardensideDoor.PageVM.ZoomAsFloat * this.GardensideDoor.PageVM.CroppedCursorPointedTileSourceRect.Size.Width.AsInt);
            this.GardensideDoor.PageVM.CroppedCursorPointedTileWorkingHeight = new TheGeometric.HeightFloat(this.GardensideDoor.PageVM.ZoomAsFloat * this.GardensideDoor.PageVM.CroppedCursorPointedTileSourceRect.Size.Height.AsInt);
        }

        // 全ての［登録タイル］の更新
        foreach (var registeredTileVM in this.GardensideDoor.TilesetSettingsVM.TileRecordVisuallyList)
        {
            // ズーム
            registeredTileVM.Zoom = this.RoomsideDoors.ZoomProperties.Value;
        }

        // 変更通知
        this.GardensideDoor.PageVM.InvalidateForHistory();
    }
}
