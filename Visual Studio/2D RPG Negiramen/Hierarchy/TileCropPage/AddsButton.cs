namespace _2D_RPG_Negiramen.Hierarchy.TileCropPage;

using _2D_RPG_Negiramen.Coding;
using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.ViewHistory.TileCropPage;
using TheHierarchyTileCropPage = _2D_RPG_Negiramen.Hierarchy.TileCropPage;

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
    private AddsButton()
    {
    }

    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="commonOfHierarchy"></param>
    /// <param name="roomsideDoors"></param>
    internal AddsButton(
        TheHierarchyTileCropPage.ItsCommon commonOfHierarchy,
        ItsMemberNetwork roomsideDoors)
    {
        this.CommonOfHierarchy = commonOfHierarchy;
        this.RoomsideDoors = roomsideDoors;
    }
    #endregion

    // - インターナル・メソッド

    /// <summary>
    ///     状態監視
    /// </summary>
    internal void MonitorStateOfAddsButton(
        LazyArgs.Set<string> setAddsButtonText)
    {
        // 切抜きカーソルが、登録済みタイルのいずれかと交差しているか？
        if (this.CommonOfHierarchy.HasIntersectionBetweenCroppedCursorAndRegisteredTile)
        {
            // 合同のときは「交差中」とは表示しない
            if (!this.CommonOfHierarchy.IsCongruenceBetweenCroppedCursorAndRegisteredTile)
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

    // - プライベート・プロパティ

    TheHierarchyTileCropPage.ItsCommon CommonOfHierarchy { get; }

    /// <summary>
    ///     室内側ドア
    /// </summary>
    ItsMemberNetwork RoomsideDoors { get; }
}
