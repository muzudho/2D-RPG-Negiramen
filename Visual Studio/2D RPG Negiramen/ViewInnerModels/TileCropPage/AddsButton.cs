namespace _2D_RPG_Negiramen.ViewInnerModels.TileCropPage;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.ViewHistory.TileCropPage;

/// <summary>
///     追加ボタン
/// </summary>
internal class AddsButton
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="owner"></param>
    internal AddsButton(TileCropPageViewInnerModel owner)
    {
        this.Owner = owner;
    }
    #endregion

    // - インターナル・メソッド

    /// <summary>
    ///     ［追加］
    /// </summary>
    internal void AddTile()
    {
        var contents = this.Owner.TargetTileRecordVisually;

        TileIdOrEmpty tileIdOrEmpty;

        // Ｉｄが空欄
        // ［追加］（新規作成）だ

        // ［切抜きカーソル］にサイズがなければ、何もしない
        if (contents.IsNone)
            return;

        // 新しいタイルＩｄを発行
        tileIdOrEmpty = Owner.TilesetSettingsVM.UsableId;
        Owner.TilesetSettingsVM.IncreaseUsableId();

        // 追加でも、上書きでも、同じ処理でいける
        // ［登録タイル追加］処理
        App.History.Do(new AddRegisteredTileProcessing(
            inner: this.Owner,
            croppedCursorVisually: contents,
            tileIdOrEmpty: tileIdOrEmpty,
            workingRectangle: contents.SourceRectangle.Do(this.Owner.Zoom.Value)));

        Owner.InvalidateForHistory();
    }

    /// <summary>
    ///     ［上書き］
    /// </summary>
    public void OverwriteTile()
    {
        var contents = this.Owner.TargetTileRecordVisually;

        TileIdOrEmpty tileIdOrEmpty;

        // ［切抜きカーソル］にサイズがなければ、何もしない
        if (contents.IsNone)
            return;

        // Ｉｄが空欄でない
        // ［上書き］（更新）だ
        tileIdOrEmpty = this.Owner.CropTile.IdOrEmpty;

        // 追加でも、上書きでも、同じ処理でいける
        // ［登録タイル追加］処理
        App.History.Do(new AddRegisteredTileProcessing(
            inner: this.Owner,
            croppedCursorVisually: contents,
            tileIdOrEmpty: tileIdOrEmpty,
            workingRectangle: contents.SourceRectangle.Do(this.Owner.Zoom.Value)));

        Owner.InvalidateForHistory();
    }

    /// <summary>
    ///     再描画
    /// </summary>
    internal void Refresh()
    {
        // 切抜きカーソルが、登録済みタイルのいずれかと交差しているか？
        if (this.Owner.HasIntersectionBetweenCroppedCursorAndRegisteredTile)
        {
            // 合同のときは「交差中」とは表示しない
            if (!this.Owner.IsCongruenceBetweenCroppedCursorAndRegisteredTile)
            {
                // 「交差中」
                // Trace.WriteLine("[TileCropPage.xml.cs InvalidateAddsButton] 交差中だ");

                this.Owner.Owner.AddsButtonText = (string)LocalizationResourceManager.Instance["Intersecting"];
                return;
            }
        }

        var contents = this.Owner.CropTile.SavesRecordVisually;

        if (contents.IsNone)
        {
            // ［切抜きカーソル］の指すタイル無し時

            // 「追加」
            this.Owner.Owner.AddsButtonText = (string)LocalizationResourceManager.Instance["Add"];
        }
        else
        {
            // 切抜きカーソル有り時
            // Ｉｄ未設定時

            if (this.Owner.CropTile.IdOrEmpty == TileIdOrEmpty.Empty)
            {
                // Ｉｄが空欄
                // ［追加］（新規作成）だ

                // ［追加」
                this.Owner.Owner.AddsButtonText = (string)LocalizationResourceManager.Instance["Add"];
            }
            else
            {
                // ［復元」
                this.Owner.Owner.AddsButtonText = (string)LocalizationResourceManager.Instance["Restore"];
            }
        }

        // ［追加／復元］ボタンの活性性
        this.Owner.Owner.InvalidateAddsButton();
    }

    // - プライベート・プロパティ

    TileCropPageViewInnerModel Owner { get; }
}
