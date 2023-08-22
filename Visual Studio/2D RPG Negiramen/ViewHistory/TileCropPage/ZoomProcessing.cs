namespace _2D_RPG_Negiramen.ViewHistory.TileCropPage;

using _2D_RPG_Negiramen.Models.Geometric;
using _2D_RPG_Negiramen.Models.History;
using _2D_RPG_Negiramen.ViewInnerModels;
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
    /// <param name="oldValue">変更前の値</param>
    /// <param name="newValue">変更後の値</param>
    internal ZoomProcessing(TileCropPageViewInnerModel inner, Zoom oldValue, Zoom newValue)
    {
        this.Inner = inner;
        this.OldValue = oldValue;
        this.NewValue = newValue;
    }

    // - パブリック・メソッド

    /// <summary>
    ///     ドゥ
    /// </summary>
    public void Do()
    {
        this.Inner.Zoom = this.NewValue;

        this.AfterChanged();
    }

    /// <summary>
    ///     アンドゥ
    /// </summary>
    public void Undo()
    {
        this.Inner.Zoom = this.OldValue;

        this.AfterChanged();
    }

    // - プライベート・プロパティ

    /// <summary>
    ///     内部クラス
    /// </summary>
    TileCropPageViewInnerModel Inner { get; }

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
            this.Inner.RemakeWorkingTilesetImage();
        }

        // ［元画像グリッド］の更新
        {
            // キャンバス画像の再作成
            this.Inner.RemakeGridCanvasImage();
        }

        // ［作業グリッド］の再計算
        {
            // 横幅
            this.Inner.RefreshWorkingGridTileWidth();
            // 縦幅
            this.Inner.RefreshWorkingGridTileHeight();
        }

        // ［切抜きカーソルが指すタイル］更新
        {
            //// 位置
            //this.Owner.CroppedCursorPointedTileWorkingLocation = new TheGeometric.PointFloat(
            //    x: new TheGeometric.XFloat(this.Owner.ZoomAsFloat * this.Owner.CroppedCursorPointedTileSourceRect.Location.X.AsInt),
            //    y: new TheGeometric.YFloat(this.Owner.ZoomAsFloat * this.Owner.CroppedCursorPointedTileSourceRect.Location.Y.AsInt));

            // サイズ
            this.Inner.CroppedCursorPointedTileWorkingWidthWithoutTrick = new TheGeometric.WidthFloat(this.Inner.ZoomAsFloat * this.Inner.CroppedCursorPointedTileSourceRect.Size.Width.AsInt);
            this.Inner.CroppedCursorPointedTileWorkingHeight = new TheGeometric.HeightFloat(this.Inner.ZoomAsFloat * this.Inner.CroppedCursorPointedTileSourceRect.Size.Height.AsInt);
        }

        // 全ての［登録タイル］の更新
        foreach (var registeredTileVM in this.Inner.TilesetSettingsVM.TileRecordVisuallyList)
        {
            // ズーム
            registeredTileVM.Zoom = this.Inner.Zoom;
        }

        // 変更通知
        this.Inner.InvalidateForHistory();
    }
}
