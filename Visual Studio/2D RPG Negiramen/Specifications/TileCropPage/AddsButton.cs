namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.Coding;
using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.Visually;

/// <summary>
///     追加ボタン
/// </summary>
internal class AddsButton
{
    /// <summary>
    ///     室内側ドア
    /// </summary>
    ItsRoomsideDoors RoomsideDoors { get; }

    // - その他

    #region その他（生成）
    private AddsButton()
    {

    }

    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="roomsideDoors"></param>
    internal AddsButton(
        ItsRoomsideDoors roomsideDoors)
    {
        this.RoomsideDoors = roomsideDoors;
    }
    #endregion

    // - インターナル・デリゲート

    /// <summary>
    ///     上書きする
    /// </summary>
    internal delegate void DoAddRegisteredTIle(TileRecordVisually contents);

    // - インターナル・メソッド

    /// <summary>
    ///     ［追加］
    /// </summary>
    internal void AddTile(
        DoAddRegisteredTIle doAddRegisteredTIle)
    {
        doAddRegisteredTIle(
            contents: this.RoomsideDoors.CropTile.RecordVisually);
    }

    /// <summary>
    ///     ［上書き］
    /// </summary>
    public void OverwriteTile(
        DoAddRegisteredTIle doAddRegisteredTIle)
    {
        doAddRegisteredTIle(
            contents: this.RoomsideDoors.CropTile.RecordVisually);
    }

    /// <summary>
    ///     状態監視
    /// </summary>
    internal void MonitorState(
        LazyArgs.Set<string> setAddsButtonText)
    {
        // 切抜きカーソルが、登録済みタイルのいずれかと交差しているか？
        if (this.RoomsideDoors.HasIntersectionBetweenCroppedCursorAndRegisteredTile)
        {
            // 合同のときは「交差中」とは表示しない
            if (!this.RoomsideDoors.IsCongruenceBetweenCroppedCursorAndRegisteredTile)
            {
                // Trace.WriteLine("[TileCropPage.xml.cs InvalidateAddsButton] 交差中だ");

                // 「交差中」
                setAddsButtonText((string)LocalizationResourceManager.Instance["Intersecting"]);
                return;
            }
        }

        var contents = this.RoomsideDoors.CropTile.RecordVisually;

        if (contents.IsNone)
        {
            // ［切抜きカーソル］の指すタイル無し時

            // 「追加」
            setAddsButtonText((string)LocalizationResourceManager.Instance["Add"]);
        }
        else
        {
            // 切抜きカーソル有り時
            // Ｉｄ未設定時

            if (this.RoomsideDoors.CropTile.IdOrEmpty == TileIdOrEmpty.Empty)
            {
                // Ｉｄが空欄
                // ［追加］（新規作成）だ

                // ［追加」
                setAddsButtonText((string)LocalizationResourceManager.Instance["Add"]);
            }
            else
            {
                // ［復元」
                setAddsButtonText((string)LocalizationResourceManager.Instance["Restore"]);
            }
        }
    }
}
