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

    // - インターナル・メソッド

    /// <summary>
    ///     ［追加］
    /// </summary>
    internal void AddTile(
        DoRegisteredTIle doRegisteredTIle)
    {
        doRegisteredTIle(
            contents: this.RoomsideDoors.CropTile.RecordVisually);
    }

    /// <summary>
    ///     上書きする
    /// </summary>
    internal delegate void DoRegisteredTIle(TileRecordVisually contents);

    /// <summary>
    ///     ［上書き］
    /// </summary>
    public void OverwriteTile(
        DoRegisteredTIle doRegisteredTIle)
    {
        doRegisteredTIle(
            contents: this.RoomsideDoors.CropTile.RecordVisually);
    }

    /// <summary>
    ///     再描画
    /// </summary>
    internal void Refresh(
        LazyArgs.Set<string> setAddsButtonText)
    {
        // 切抜きカーソルが、登録済みタイルのいずれかと交差しているか？
        if (this.RoomsideDoors.HasIntersectionBetweenCroppedCursorAndRegisteredTile)
        {
            // 合同のときは「交差中」とは表示しない
            if (!this.RoomsideDoors.IsCongruenceBetweenCroppedCursorAndRegisteredTile)
            {
                // 「交差中」
                // Trace.WriteLine("[TileCropPage.xml.cs InvalidateAddsButton] 交差中だ");

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
