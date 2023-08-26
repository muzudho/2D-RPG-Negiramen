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
        IItsOutdoor outdoor,
        IItsSpec spec,
        TileIdOrEmpty tileIdOrEmpty)
    {
        this.Outdoor = outdoor;
        this.Spec = spec;
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
        if (this.Outdoor.TilesetSettingsVMDeleteLogical(
            // 現在選択中のタイルのＩｄ
            id: this.TileIdOrEmpty))
        {
            // タイルセット設定ビューモデルに変更あり
            this.Outdoor.InvalidateTilesetSettingsVM();
        }

        Trace.WriteLine($"[TileCropPage.xml.cs DeletesButton_Clicked] タイルを論理削除 TileId: [{this.TileIdOrEmpty.AsBASE64}]");

        //
        // 設定ファイルの保存
        // ==================
        //
        if (this.Outdoor.TilesetSettingsVMSaveCsv(this.Outdoor.TilesetDatatableFileLocation))
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
        this.Outdoor.RefreshForTileAdd();
    }

    public void Undo()
    {
        //
        // 設定ファイルの編集
        // ==================
        //
        //      - 選択中のタイルの論理削除の取消
        //
        if (this.Outdoor.TilesetSettingsVMDeleteLogical(
            // 現在選択中のタイルのＩｄ
            id: this.TileIdOrEmpty))
        {
            // タイルセット設定ビューモデルに変更あり
            this.Outdoor.InvalidateTilesetSettingsVM();
        }

        Trace.WriteLine($"[TileCropPage.xml.cs DeletesButton_Clicked] タイルを論理削除 TileId: [{this.TileIdOrEmpty.AsBASE64}]");

        //
        // 設定ファイルの保存
        // ==================
        //
        if (this.Outdoor.TilesetSettingsVMSaveCsv(this.Outdoor.TilesetDatatableFileLocation))
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
        this.Outdoor.RefreshForTileAdd();
    }

    // - プライベート・プロパティ

    /// <summary>
    ///     内部クラス
    /// </summary>
    IItsOutdoor Outdoor { get; }
    IItsSpec Spec { get; }

    /// <summary>
    ///     ［タイル］のＩｄ
    /// </summary>
    TileIdOrEmpty TileIdOrEmpty { get; }
}
