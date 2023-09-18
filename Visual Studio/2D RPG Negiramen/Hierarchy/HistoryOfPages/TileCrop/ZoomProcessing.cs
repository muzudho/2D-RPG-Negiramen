namespace _2D_RPG_Negiramen.Hierarchy.HistoryOfPages.TileCrop;

using _2D_RPG_Negiramen.Models.Geometric;
using _2D_RPG_Negiramen.Models.History;
using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;
using TheTileCropPage = _2D_RPG_Negiramen.Hierarchy.Pages.TileCrop;

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
        MembersOfTileCropPage colleagues,
        TheTileCropPage.ItsMembers subordinates,
        Zoom oldValue,
        Zoom newValue)
    {
        this.Colleagues = colleagues;
        this.Subordinates = subordinates;
        this.OldValue = oldValue;
        this.NewValue = newValue;
    }

    // - パブリック・メソッド

    /// <summary>
    ///     ドゥ
    /// </summary>
    public void Do()
    {
        this.Colleagues.PageVM.ZoomAsFloat = this.NewValue.AsFloat;

        this.AfterChanged();
    }

    /// <summary>
    ///     アンドゥ
    /// </summary>
    public void Undo()
    {
        this.Colleagues.PageVM.ZoomAsFloat = this.OldValue.AsFloat;

        this.AfterChanged();
    }

    // - プライベート・プロパティ

    MembersOfTileCropPage Colleagues { get; }
    TheTileCropPage.ItsMembers Subordinates { get; }

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
            this.Colleagues.PageVM.RemakeWorkingTilesetImage();
        }

        // ［元画像グリッド］の更新
        {
            // キャンバス画像の再作成
            this.Colleagues.PageVM.RemakeGridCanvasImage();
        }

        // ［作業グリッド］の再計算
        {
            // 横幅
            var width = this.Subordinates.GridUnit.SourceValue.Width.AsInt;
            this.Colleagues.PageVM.WorkingGridTileWidthAsFloat = this.Colleagues.PageVM.ZoomAsFloat * width;
            // this.Owner.Owner.InvalidateWorkingGrid();

            // 縦幅
            var height = this.Subordinates.GridUnit.SourceValue.Height.AsInt;
            this.Colleagues.PageVM.WorkingGridTileHeightAsFloat = this.Colleagues.PageVM.ZoomAsFloat * height;
            // this.Owner.Owner.InvalidateWorkingGrid();
        }

        // ［切抜きカーソルが指すタイル］更新
        {
            //// 位置
            //this.Owner.CroppedCursorPointedTileWorkingLocation = new TheGeometric.PointFloat(
            //    x: new TheGeometric.XFloat(this.Owner.ZoomAsFloat * this.Owner.SelectedTile_SourceLeftAsInt.Location.X.AsInt),
            //    y: new TheGeometric.YFloat(this.Owner.ZoomAsFloat * this.Owner.SelectedTile_SourceLeftAsInt.Location.Y.AsInt));

            // サイズ
            this.Subordinates.SelectedTile.WorkingWidthWithoutTrick = new TheGeometric.WidthFloat(this.Colleagues.PageVM.ZoomAsFloat * this.Colleagues.PageVM.SelectedTile_SourceRect.Size.Width.AsInt);
            this.Colleagues.PageVM.SelectedTile_WorkingHeight = new TheGeometric.HeightFloat(this.Colleagues.PageVM.ZoomAsFloat * this.Colleagues.PageVM.SelectedTile_SourceRect.Size.Height.AsInt);
        }

        // 全ての［登録タイル］の更新
        foreach (var registeredTileVM in this.Colleagues.PageVM.TilesetSettingsVM.TileRecordVisuallyList)
        {
            // ズーム
            registeredTileVM.Zoom = this.Subordinates.ZoomProperties.Value;
        }

        // 変更通知
        this.Colleagues.PageVM.InvalidateForHistory();
    }
}
