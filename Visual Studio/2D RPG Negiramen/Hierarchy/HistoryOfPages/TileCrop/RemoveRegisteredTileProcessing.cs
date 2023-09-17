namespace _2D_RPG_Negiramen.Hierarchy.HistoryOfPages.TileCrop;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.History;
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
        MembersOfTileCropPage colleagues,
        TileIdOrEmpty tileIdOrEmpty)
    {
        this.Colleagues = colleagues;
        this.TileIdOrEmpty = tileIdOrEmpty;
    }

    public void Do()
    {
        //
        // 設定ファイルの編集
        // ==================
        //
        //      - 選択中のタイルを論理削除
        //
        if (this.Colleagues.PageVM.TilesetSettingsVM.DeleteLogical(
            // 現在選択中のタイルのＩｄ
            id: this.TileIdOrEmpty))
        {
            // タイルセット設定ビューモデルに変更あり
            this.Colleagues.PageVM.InvalidateTilesetSettingsVM();
        }

        Trace.WriteLine($"［タイル削除］ 　Do　タイルを論理削除 TileId: [{this.TileIdOrEmpty.AsBASE64}]");

        //
        // 設定ファイルの保存
        // ==================
        //
        if (this.Colleagues.PageVM.TilesetSettingsVM.SaveCsv(this.Colleagues.PageVM.TilesetDatatableFileLocation))
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
        this.Colleagues.PageVM.RefreshForTileAdd();

        // 履歴ボタンの変更通知
        this.Colleagues.PageVM.InvalidateForHistory();
    }

    public void Undo()
    {
        //
        // 設定ファイルの編集
        // ==================
        //
        //      - 選択中のタイルの論理削除の取消
        //
        if (this.Colleagues.PageVM.TilesetSettingsVM.UndeleteLogical(
            // 現在選択中のタイルのＩｄ
            id: this.TileIdOrEmpty))
        {
            // タイルセット設定ビューモデルに変更あり
            this.Colleagues.PageVM.InvalidateTilesetSettingsVM();
        }

        Trace.WriteLine($"［タイル削除］　Undo　タイルを論理削除 TileId: [{this.TileIdOrEmpty.AsBASE64}]");

        //
        // 設定ファイルの保存
        // ==================
        //
        if (this.Colleagues.PageVM.TilesetSettingsVM.SaveCsv(this.Colleagues.PageVM.TilesetDatatableFileLocation))
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
        this.Colleagues.PageVM.RefreshForTileAdd();

        // 履歴ボタンの変更通知
        this.Colleagues.PageVM.InvalidateForHistory();
    }

    // - プライベート・プロパティ

    /// <summary>メンバー・ネットワーク</summary>
    MembersOfTileCropPage Colleagues { get; }

    /// <summary>
    ///     ［タイル］のＩｄ
    /// </summary>
    TileIdOrEmpty TileIdOrEmpty { get; }
}
