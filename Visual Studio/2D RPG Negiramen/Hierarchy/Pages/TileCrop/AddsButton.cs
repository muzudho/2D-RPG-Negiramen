namespace _2D_RPG_Negiramen.Hierarchy.Pages.TileCrop;

using _2D_RPG_Negiramen.Coding;
using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.ViewHistory.TileCropPage;
using TheHierarchyTileCropPage = _2D_RPG_Negiramen.Hierarchy.Pages.TileCrop;

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
        ItsCommon commonOfHierarchy,
        ItsMemberNetwork roomsideDoors)
    {
        CommonOfHierarchy = commonOfHierarchy;
        RoomsideDoors = roomsideDoors;
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
        if (CommonOfHierarchy.HasIntersectionBetweenCroppedCursorAndRegisteredTile)
        {
            // 合同のときは「交差中」とは表示しない
            if (!CommonOfHierarchy.IsCongruenceBetweenCroppedCursorAndRegisteredTile)
            {
                // Trace.WriteLine("[TileCropPage.xml.cs InvalidateAddsButton] 交差中だ");

                // 「交差中」
                setAddsButtonText((string)LocalizationResourceManager.Instance["Intersecting"]);
                return;
            }
        }

        var contents = RoomsideDoors.CropTile.RecordVisually;

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

            if (RoomsideDoors.CropTile.IdOrEmpty == TileIdOrEmpty.Empty)
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

    ItsCommon CommonOfHierarchy { get; }

    /// <summary>
    ///     室内側ドア
    /// </summary>
    ItsMemberNetwork RoomsideDoors { get; }
}
