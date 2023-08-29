namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.Geometric;
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
    /// <param name="specObj"></param>
    internal CropTile(
        IItsCorridorOutdoorDirection outdoor,
        IItsIndoor indoor)
    {
        this.Outdoor = outdoor;
        this.Indoor = indoor;
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

    #region プロパティ（［切抜きカーソルが指すタイル］　関連）
    /// <summary>
    ///     ［切抜きカーソル］が指すタイル
    ///     
    ///     <list type="bullet">
    ///         <item>［切抜きカーソル］が未確定のときも、指しているタイルにアクセスできることに注意</item>
    ///         <item>TODO ★ ［切抜きカーソル］が指すタイルが無いとき、無いということをセットするのを忘れている？</item>
    ///     </list>
    /// </summary>
    public TileRecordVisually TargetTileRecordVisually
    {
        get => this.SavesRecordVisually;
        set
        {
            var oldTileVisually = this.SavesRecordVisually;

            // 値に変化がない
            if (oldTileVisually == value)
                return;

            if (value.IsNone)
            {
                // ［切抜きカーソルが指すタイル］を無しに設定する

                if (oldTileVisually.IsNone)
                {
                    // ［切抜きカーソルが指すタイル］がもともと無く、［切抜きカーソルが指すタイル］を無しに設定するのだから、何もしなくてよい
                }
                else
                {
                    // ［切抜きカーソルが指すタイル］がもともと有って、［切抜きカーソルが指すタイル］を無しに設定するのなら、消すという操作がいる
                    this.UpdateByDifference(
                        // タイトル
                        tileTitle: TileTitle.Empty);

                    // 末端にセット（変更通知を呼ぶために）
                    // Ｉｄ
                    this.IdOrEmpty = TileIdOrEmpty.Empty;

                    // 元画像の位置とサイズ
                    this.Outdoor.CroppedCursorPointedTileSourceRect = RectangleInt.Empty;

                    // 論理削除
                    this.Outdoor.CropTileLogicalDeleteAsBool = false;

                    // 空にする
                    this.SavesRecordVisually = TileRecordVisually.CreateEmpty();
                }
            }
            else
            {
                var newValue = value;

                if (oldTileVisually.IsNone)
                {
                    // ［切抜きカーソル］の指すタイル無し時

                    // 新規作成
                    this.SavesRecordVisually = TileRecordVisually.CreateEmpty();
                }
                else
                {
                    // ［切抜きカーソル］の指すタイルが有るなら構わない
                }

                // （変更通知を送っている）
                this.UpdateByDifference(
                    // タイトル
                    tileTitle: newValue.Title);

                // （変更通知を送っている）
                this.IdOrEmpty = newValue.Id;
                this.Outdoor.CropTileSourceLeftAsInt = newValue.SourceRectangle.Location.X.AsInt;
                this.Outdoor.CropTileSourceTopAsInt = newValue.SourceRectangle.Location.Y.AsInt;
                this.Outdoor.CropTileSourceWidthAsInt = newValue.SourceRectangle.Size.Width.AsInt;
                this.Outdoor.CropTileSourceHeightAsInt = newValue.SourceRectangle.Size.Height.AsInt;
                // this.CropTileTitleAsStr = newValue.Title.AsStr;
            }

            // 変更通知を送りたい
            this.Outdoor.ObsoletedInvalidateTileIdChange();
        }
    }
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
            this.Indoor.AddsButtonRefresh();

            // ［削除］ボタン再描画
            this.Indoor.DeletesButtonRefresh();
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
        this.Outdoor.ObsoletedInvalidateTileIdChange();

        Trace.WriteLine($"[CropTile.cs UpdateByDifference] SavesRecordVisually.Dump(): {this.SavesRecordVisually.Dump()}");
    }
    #endregion

    // - プライベート・プロパティ

    IItsCorridorOutdoorDirection Outdoor { get; }
    IItsIndoor Indoor { get; }
}
