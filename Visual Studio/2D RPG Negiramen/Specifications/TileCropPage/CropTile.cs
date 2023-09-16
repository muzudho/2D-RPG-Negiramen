namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.Coding;
using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.Visually;

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
    /// <param name="roomsideDoors"></param>
    internal CropTile(
        ItsRoomsideDoors roomsideDoors)
    {
        this.RoomsideDoors = roomsideDoors;
    }
    #endregion

    // - インターナル・プロパティ

    #region プロパティ（［切抜きカーソルが指すタイル］　関連）
    /// <summary>
    ///     ［切抜きカーソル］が指すタイル
    ///     
    ///     <list type="bullet">
    ///         <item>［切抜きカーソル］が未確定のときも、指しているタイルにアクセスできることに注意</item>
    ///         <item>TODO ★ ［切抜きカーソル］が指すタイルが無いとき、無いということをセットするのを忘れている？</item>
    ///     </list>
    /// </summary>
    public TileRecordVisually RecordVisually
    {
        get => this.recordVisually;
    }
    #endregion

    internal void SetRecordVisually(
        TileRecordVisually value,
        Action onVanished,
        Action onUpdated,
        LazyArgs.Set<string> setAddsButtonText,
        Action onDeleteButtonEnableChanged)
    {
        var oldTileVisually = this.recordVisually;

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
                    setAddsButtonText: setAddsButtonText,
                    onDeleteButtonEnableChanged: onDeleteButtonEnableChanged,
                    // タイトル
                    tileTitle: TileTitle.Empty);

                // 末端にセット（変更通知を呼ぶために）
                // Ｉｄ
                this.SetIdOrEmpty(
                    value: TileIdOrEmpty.Empty,
                    setAddsButtonText: setAddsButtonText,
                    onDeleteButtonEnableChanged: onDeleteButtonEnableChanged);

                // 空にする
                this.recordVisually = TileRecordVisually.CreateEmpty();

                onVanished();
            }
        }
        else
        {
            var newValue = value;

            if (oldTileVisually.IsNone)
            {
                // ［切抜きカーソル］の指すタイル無し時

                // 新規作成
                this.recordVisually = TileRecordVisually.CreateEmpty();
            }
            else
            {
                // ［切抜きカーソル］の指すタイルが有るなら構わない
            }

            // （変更通知を送っている）
            this.UpdateByDifference(
                setAddsButtonText: setAddsButtonText,
                onDeleteButtonEnableChanged: onDeleteButtonEnableChanged,
                // タイトル
                tileTitle: newValue.Title);

            this.SetIdOrEmpty(
                value: newValue.Id,
                setAddsButtonText: setAddsButtonText,
                onDeleteButtonEnableChanged: onDeleteButtonEnableChanged);
            // this.CropTileTitleAsStr = newValue.Title.AsStr;

            onUpdated();
        }
    }

    internal void SetRecordVisuallyNoGuiUpdate(TileRecordVisually value)
    {
        this.recordVisually = value;
    }

    #region プロパティ（Ｉｄ）
    /// <summary>
    ///     Ｉｄ
    /// </summary>
    public TileIdOrEmpty IdOrEmpty
    {
        get
        {
            var contents = this.RecordVisually;

            // ［切抜きカーソル］の指すタイル無し時
            if (contents.IsNone)
                return TileIdOrEmpty.Empty;

            return contents.Id;
        }
    }
    #endregion

    internal void SetIdOrEmpty(
        TileIdOrEmpty value,
        LazyArgs.Set<string> setAddsButtonText,
        Action onDeleteButtonEnableChanged)
    {
        if (this.RecordVisually.Id == value)
            return;

        // 差分更新
        this.UpdateByDifference(
            setAddsButtonText: setAddsButtonText,
            onDeleteButtonEnableChanged: onDeleteButtonEnableChanged,
            tileId: value);
    }

    // - インターナル・メソッド

    #region メソッド（［切抜きカーソルが指すタイル］を差分更新）
    /// <summary>
    ///     ［切抜きカーソルが指すタイル］を差分更新
    /// </summary>
    /// <returns></returns>
    public void UpdateByDifference(
        LazyArgs.Set<string> setAddsButtonText,
        Action onDeleteButtonEnableChanged,
        TileIdOrEmpty? tileId = null,
        TileTitle? tileTitle = null,
        LogicalDelete? logicalDelete = null)
    {
        var currentTileVisually = this.RecordVisually;

        // タイルＩｄ
        if (!(tileId is null) && currentTileVisually.Id != tileId)
        {
            this.RecordVisually.Id = tileId;

            // Ｉｄが入ることで、タイル登録扱いになる。いろいろ再描画する

            // ［追加／上書き］ボタン再描画
            this.RoomsideDoors.AddsButton.Refresh(
                setAddsButtonText: setAddsButtonText);

            // ［削除］ボタン再描画
            this.RoomsideDoors.DeletesButton.Refresh(
                onEnableChanged: onDeleteButtonEnableChanged);
        }

        // タイル・タイトル
        if (!(tileTitle is null) && currentTileVisually.Title != tileTitle)
        {
            this.RecordVisually.Title = tileTitle;
        }

        // 論理削除フラグ
        if (!(logicalDelete is null) && currentTileVisually.LogicalDelete != logicalDelete)
        {
            this.RecordVisually.LogicalDelete = logicalDelete;
        }

        // Trace.WriteLine($"[CropTile.cs UpdateByDifference] SavesRecordVisually.Dump(): {this.SavesRecordVisually.Dump()}");
    }
    #endregion

    // - プライベート・プロパティ

    ItsRoomsideDoors RoomsideDoors { get; }

    #region プロパティ（保存データ）
    /// <summary>
    ///     保存データ
    ///     
    ///     <list type="bullet">
    ///         <item>★循環参照しやすいので注意</item>
    ///         <item>［切抜きカーソル］が指すタイルが未確定のときも、指しているタイルにアクセスできることに注意</item>
    ///     </list>
    /// </summary>
    TileRecordVisually recordVisually = TileRecordVisually.CreateEmpty();
    #endregion
}
