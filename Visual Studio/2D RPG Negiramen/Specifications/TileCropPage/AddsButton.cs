namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.ViewHistory.TileCropPage;

/// <summary>
///     追加ボタン
/// </summary>
internal class AddsButton
{
    /// <summary>
    ///     兄弟ドア
    /// </summary>
    ItsSiblingDoors SiblingDoors { get; }

    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="specObj"></param>
    internal AddsButton(
        ItsSiblingDoors siblingDoors,
        IItsOutdoor outdoor, // TODO これを削除したい
        IItsIndoor indoor) // TODO これを削除したい
    {
        this.SiblingDoors = siblingDoors;
        this.Outdoor = outdoor; // TODO これを削除したい
        this.Indoor = indoor; // TODO これを削除したい
    }
    #endregion

    // - インターナル・メソッド

    /// <summary>
    ///     ［追加］
    /// </summary>
    internal void AddTile()
    {
        var contents = this.Indoor.CropTileTargetTileRecordVisually;

        TileIdOrEmpty tileIdOrEmpty;

        // Ｉｄが空欄
        // ［追加］（新規作成）だ

        // ［切抜きカーソル］にサイズがなければ、何もしない
        if (contents.IsNone)
            return;

        // 新しいタイルＩｄを発行
        tileIdOrEmpty = this.Outdoor.TilesetSettingsVMUsableId;
        this.Outdoor.TilesetSettingsVMIncreaseUsableId();

        // 追加でも、上書きでも、同じ処理でいける
        // ［登録タイル追加］処理
        App.History.Do(new AddRegisteredTileProcessing(
            outdoor: this.Outdoor,
            spec: this.Indoor,
            croppedCursorVisually: contents,
            tileIdOrEmpty: tileIdOrEmpty,
            workingRectangle: contents.SourceRectangle.Do(this.SiblingDoors.Zoom.Value)));

        Outdoor.InvalidateForHistory();
    }

    /// <summary>
    ///     ［上書き］
    /// </summary>
    public void OverwriteTile()
    {
        var contents = this.Indoor.CropTileTargetTileRecordVisually;

        TileIdOrEmpty tileIdOrEmpty;

        // ［切抜きカーソル］にサイズがなければ、何もしない
        if (contents.IsNone)
            return;

        // Ｉｄが空欄でない
        // ［上書き］（更新）だ
        tileIdOrEmpty = this.Indoor.CropTileIdOrEmpty;

        // 追加でも、上書きでも、同じ処理でいける
        // ［登録タイル追加］処理
        App.History.Do(new AddRegisteredTileProcessing(
            outdoor: this.Outdoor,
            spec: this.Indoor,
            croppedCursorVisually: contents,
            tileIdOrEmpty: tileIdOrEmpty,
            workingRectangle: contents.SourceRectangle.Do(this.Indoor.RoomsideDoors.ZoomValue)));

        Outdoor.InvalidateForHistory();
    }

    /// <summary>
    ///     再描画
    /// </summary>
    internal void Refresh()
    {
        // 切抜きカーソルが、登録済みタイルのいずれかと交差しているか？
        if (this.Indoor.HasIntersectionBetweenCroppedCursorAndRegisteredTile)
        {
            // 合同のときは「交差中」とは表示しない
            if (!this.Indoor.IsCongruenceBetweenCroppedCursorAndRegisteredTile)
            {
                // 「交差中」
                // Trace.WriteLine("[TileCropPage.xml.cs InvalidateAddsButton] 交差中だ");

                this.Outdoor.AddsButtonText = (string)LocalizationResourceManager.Instance["Intersecting"];
                return;
            }
        }

        var contents = this.Indoor.CropTileSavesRecordVisually;

        if (contents.IsNone)
        {
            // ［切抜きカーソル］の指すタイル無し時

            // 「追加」
            this.Outdoor.AddsButtonText = (string)LocalizationResourceManager.Instance["Add"];
        }
        else
        {
            // 切抜きカーソル有り時
            // Ｉｄ未設定時

            if (this.Indoor.CropTileIdOrEmpty == TileIdOrEmpty.Empty)
            {
                // Ｉｄが空欄
                // ［追加］（新規作成）だ

                // ［追加」
                this.Outdoor.AddsButtonText = (string)LocalizationResourceManager.Instance["Add"];
            }
            else
            {
                // ［復元」
                this.Outdoor.AddsButtonText = (string)LocalizationResourceManager.Instance["Restore"];
            }
        }

        // ［追加／復元］ボタンの活性性
        this.Outdoor.InvalidateAddsButton();
    }

    // - プライベート・プロパティ

    IItsOutdoor Outdoor { get; }
    IItsIndoor Indoor { get; }
}
