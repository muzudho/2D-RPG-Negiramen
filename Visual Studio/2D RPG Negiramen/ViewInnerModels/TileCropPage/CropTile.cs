namespace _2D_RPG_Negiramen.ViewInnerModels.TileCropPage;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.Visually;
using System.Diagnostics;

/// <summary>
///     切抜きカーソルが指すタイル
/// </summary>
internal class CropTile
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="owner"></param>
    internal CropTile(TileCropPageViewInnerModel owner)
    {
        this.Owner = owner;
    }
    #endregion

    // - インターナル・プロパティ

    #region プロパティ（保存データ）
    /// <summary>
    ///     保存データ
    ///     
    ///     <list type="bullet">
    ///         <item>★循環参照しやすいので注意</item>
    ///         <item>［切抜きカーソル］が指すタイルが未確定のときも、指しているタイルにアクセスできることに注意</item>
    ///     </list>
    /// </summary>
    internal TileRecordVisually SavesRecordVisually { get; set; } = TileRecordVisually.CreateEmpty();
    #endregion

    /// <summary>
    ///     Ｉｄ
    /// </summary>
    public TileIdOrEmpty IdOrEmpty
    {
        get
        {
            var contents = this.SavesRecordVisually;

            // ［切抜きカーソル］の指すタイル無し時
            if (contents.IsNone)
                return TileIdOrEmpty.Empty;

            return contents.Id;
        }
        set
        {
            if (this.SavesRecordVisually.Id == value)
                return;

            // 差分更新
            this.UpdateByDifference(
                tileId: value);
        }
    }

    // - インターナル・メソッド

    #region メソッド（［切抜きカーソルが指すタイル］を差分更新）
    /// <summary>
    ///     ［切抜きカーソルが指すタイル］を差分更新
    /// </summary>
    /// <returns></returns>
    public void UpdateByDifference(
        TileIdOrEmpty? tileId = null,
        TileTitle? tileTitle = null,
        LogicalDelete? logicalDelete = null)
    {
        var currentTileVisually = this.SavesRecordVisually;

        // タイルＩｄ
        if (!(tileId is null) && currentTileVisually.Id != tileId)
        {
            this.SavesRecordVisually.Id = tileId;

            // Ｉｄが入ることで、タイル登録扱いになる。いろいろ再描画する

            // ［追加／上書き］ボタン再描画
            this.Owner.AddsButton.Refresh();

            // ［削除］ボタン再描画
            this.Owner.DeletesButton.Refresh();
        }

        // タイル・タイトル
        if (!(tileTitle is null) && currentTileVisually.Title != tileTitle)
        {
            this.SavesRecordVisually.Title = tileTitle;
        }

        // 論理削除フラグ
        if (!(logicalDelete is null) && currentTileVisually.LogicalDelete != logicalDelete)
        {
            this.SavesRecordVisually.LogicalDelete = logicalDelete;
        }

        // 変更通知を送る
        Owner.InvalidateTileIdChange();

        Trace.WriteLine($"[CropTile.cs UpdateByDifference] SavesRecordVisually.Dump(): {this.SavesRecordVisually.Dump()}");
    }
    #endregion

    // - プライベート・プロパティ

    TileCropPageViewInnerModel Owner { get; }
}
