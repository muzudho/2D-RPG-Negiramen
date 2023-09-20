namespace _2D_RPG_Negiramen.Hierarchy.HistoryOfPages.TileCrop;

using _2D_RPG_Negiramen.Models.Geometric;
using _2D_RPG_Negiramen.Models.History;
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
    /// <param name="colleagues"></param>
    /// <param name="oldValue">変更前の値</param>
    /// <param name="newValue">変更後の値</param>
    internal ZoomProcessing(
        MembersOfTileCropPage colleagues,
        Zoom oldValue,
        Zoom newValue)
    {
        this.Colleagues = colleagues;
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
        // ［タイルセット作業画像］の再作成
        this.Colleagues.PageVM.RemakeWorkingTilesetImage();

        // ［元画像グリッド］のキャンバス画像の再作成
        this.Colleagues.PageVM.RemakeGridCanvasImage();

        // ［作業グリッド］の横幅の再計算
        this.Colleagues.PageVM.WorkingGridTileWidthAsFloat = this.Colleagues.PageVM.ZoomAsFloat * this.Colleagues.PageVM.Subordinates.GridUnit_SourceValue.Width.AsInt;

        // ［作業グリッド］の縦幅の再計算
        this.Colleagues.PageVM.WorkingGridTileHeightAsFloat = this.Colleagues.PageVM.ZoomAsFloat * this.Colleagues.PageVM.Subordinates.GridUnit_SourceValue.Height.AsInt;

        // ［選択タイル］の幅更新
        this.Colleagues.PageVM.Subordinates.SelectedTile.WorkingWidthWithoutTrick = new TheGeometric.WidthFloat(this.Colleagues.PageVM.ZoomAsFloat * this.Colleagues.PageVM.Colleagues.PageVM.Subordinates.SelectedTile.SourceRectangle.Size.Width.AsInt);

        // 履歴の変更通知
        this.Colleagues.PageVM.InvalidateForHistory();
    }
}
