namespace _2D_RPG_Negiramen.Hierarchy.HistoryOfPages.TileCrop;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.History;
using System.Diagnostics;

/// <summary>
///     ［登録タイル削除］処理
/// </summary>
internal class RemoveRegisteredTileProcessing : IProcessing
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="tileIdOrEmpty">タイルＩｄ</param>
    /// <returns></returns>
    internal static RemoveRegisteredTileProcessing FromTileId(
        MembersOfTileCropPage colleagues,
        TileIdOrEmpty tileIdOrEmpty)
    {
        // ※１ Id が一致する最初の１件を取得
        TileRecord? removeeTile = colleagues.PageVM.TilesetSettingsVM.TileRecordList.Find(item => item.Id == tileIdOrEmpty);

        if (removeeTile != null)
        {
            return new RemoveRegisteredTileProcessing(
                colleagues: colleagues,
                removeeTile: removeeTile);
        }

        throw new InvalidOperationException("削除するタイルがありません");
    }

    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="owner"></param>
    RemoveRegisteredTileProcessing(
        MembersOfTileCropPage colleagues,
        TileRecord removeeTile)
    {
        this.Colleagues = colleagues;
        this.RemoveeTile = removeeTile;
    }
    #endregion

    /// <summary>
    ///     タイル削除
    ///     
    ///     <list type="bullet">
    ///         <item>※１ あれば、タイルの削除</item>
    ///         <item>※２ 設定ファイルの保存</item>
    ///         <item>※３ カラーマップに変更通知</item>
    ///         <item>※４ タイル情報に変更通知</item>
    ///         <item>※５ 履歴ボタンの変更通知</item>
    ///     </list>
    /// </summary>
    public void Do()
    {
        Trace.WriteLine($"［タイル削除］　Do　TileId: [{this.RemoveeTile.Id.AsBASE64}]");

        // ※１
        if (this.Colleagues.PageVM.TilesetSettingsVM.RemoveTile(
            item: this.RemoveeTile))
        {
            // 削除に成功したら

            // ※２
            if (!this.Colleagues.PageVM.TilesetSettingsVM.SaveCsv(this.Colleagues.PageVM.TilesetDatatableFileLocation))
            {
                // TODO 保存失敗時のエラー対応
            }

            // ※３
            this.Colleagues.PageVM.InvalidateTilesetSettingsVM();

            // ※４
            this.Colleagues.PageVM.RefreshForTileAdd();

            // ※５
            this.Colleagues.PageVM.InvalidateForHistory();
        }
    }

    /// <summary>
    ///     タイル削除のアンドゥ
    ///     
    ///     <list type="bullet">
    ///         <item></item>
    ///         <item></item>
    ///         <item></item>
    ///         <item></item>
    ///         <item></item>
    ///     </list>
    /// </summary>
    public void Undo()
    {
        Trace.WriteLine($"［タイル削除］　Undo　TileId: [{this.RemoveeTile.Id.AsBASE64}]");

        // 削除したタイルを、再び追加
        this.Colleagues.PageVM.TilesetSettingsVM.AddTile(
            item: this.RemoveeTile);

        //
        // 設定ファイルの保存
        // ==================
        //
        if (!this.Colleagues.PageVM.TilesetSettingsVM.SaveCsv(this.Colleagues.PageVM.TilesetDatatableFileLocation))
        {
            // TODO 保存失敗時のエラー対応
        }

        // カラーマップに変更通知
        this.Colleagues.PageVM.InvalidateTilesetSettingsVM();

        // タイル情報の変更通知
        this.Colleagues.PageVM.RefreshForTileAdd();

        // 履歴ボタンの変更通知
        this.Colleagues.PageVM.InvalidateForHistory();
    }

    // - プライベート・プロパティ

    /// <summary>メンバー・ネットワーク</summary>
    MembersOfTileCropPage Colleagues { get; }

    /// <summary>
    ///     削除するタイル
    /// </summary>
    TileRecord RemoveeTile { get; }
}
