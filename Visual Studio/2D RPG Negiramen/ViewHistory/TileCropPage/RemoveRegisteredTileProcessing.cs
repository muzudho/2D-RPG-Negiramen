namespace _2D_RPG_Negiramen.ViewHistory.TileCropPage;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.History;
using _2D_RPG_Negiramen.Specifications.TileCropPage;
using System.Diagnostics;

/// <summary>
///     ［登録タイル削除］処理
/// </summary>
internal class RemoveRegisteredTileProcessing : IProcessing
{
    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="owner"></param>
    internal RemoveRegisteredTileProcessing(
        ItsGardensideDoor gardensideDoor,
        IItsCorridorOutdoorDirection obsoletedOutdoor,
        IItsIndoor indoor,
        TileIdOrEmpty tileIdOrEmpty)
    {
        this.GardensideDoor = gardensideDoor;
        this.ObsoletedOutdoor = obsoletedOutdoor;
        this.Indoor = indoor;
        TileIdOrEmpty = tileIdOrEmpty;
    }

    public void Do()
    {
        //
        // 設定ファイルの編集
        // ==================
        //
        //      - 選択中のタイルを論理削除
        //
        if (this.GardensideDoor.TilesetSettingsVM.DeleteLogical(
            // 現在選択中のタイルのＩｄ
            id: this.TileIdOrEmpty))
        {
            // タイルセット設定ビューモデルに変更あり
            this.ObsoletedOutdoor.ObsoletedPageVMInvalidateTilesetSettingsVM();
        }

        Trace.WriteLine($"[TileCropPage.xml.cs DeletesButton_Clicked] タイルを論理削除 TileId: [{this.TileIdOrEmpty.AsBASE64}]");

        //
        // 設定ファイルの保存
        // ==================
        //
        if (this.GardensideDoor.TilesetSettingsVM.SaveCsv(this.GardensideDoor.PageVM.TilesetDatatableFileLocation))
        {
            // 保存成功
        }
        else
        {
            // TODO 保存失敗時のエラー対応
        }

        //
        // カラーマップの再描画
        // ====================
        //
        this.ObsoletedOutdoor.RefreshForTileAdd();
    }

    public void Undo()
    {
        //
        // 設定ファイルの編集
        // ==================
        //
        //      - 選択中のタイルの論理削除の取消
        //
        if (this.GardensideDoor.TilesetSettingsVM.DeleteLogical(
            // 現在選択中のタイルのＩｄ
            id: this.TileIdOrEmpty))
        {
            // タイルセット設定ビューモデルに変更あり
            this.ObsoletedOutdoor.ObsoletedPageVMInvalidateTilesetSettingsVM();
        }

        Trace.WriteLine($"[TileCropPage.xml.cs DeletesButton_Clicked] タイルを論理削除 TileId: [{this.TileIdOrEmpty.AsBASE64}]");

        //
        // 設定ファイルの保存
        // ==================
        //
        if (this.GardensideDoor.TilesetSettingsVM.SaveCsv(this.GardensideDoor.PageVM.TilesetDatatableFileLocation))
        {
            // 保存成功
        }
        else
        {
            // TODO 保存失敗時のエラー対応
        }

        //
        // カラーマップの再描画
        // ====================
        //
        this.ObsoletedOutdoor.RefreshForTileAdd();
    }

    // - プライベート・プロパティ

    /// <summary>内部クラス</summary>
    ItsGardensideDoor GardensideDoor { get; }
    IItsCorridorOutdoorDirection ObsoletedOutdoor { get; }
    IItsIndoor Indoor { get; }

    /// <summary>
    ///     ［タイル］のＩｄ
    /// </summary>
    TileIdOrEmpty TileIdOrEmpty { get; }
}
