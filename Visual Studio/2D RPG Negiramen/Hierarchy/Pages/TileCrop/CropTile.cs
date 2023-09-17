namespace _2D_RPG_Negiramen.Hierarchy.Pages.TileCrop;

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
    /// <param name="colleagues"></param>
    internal CropTile(
        ItsMembers colleagues)
    {
        this.Colleagues = colleagues;
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
        get => recordVisually;
    }
    #endregion

    internal void SetRecordVisually(
        TileRecordVisually value,
        Action onVanished,
        Action onUpdated,
        LazyArgs.Set<string> setAddsButtonText,
        Action onDeleteButtonEnableChanged)
    {
        var oldTileVisually = recordVisually;

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
                UpdateByDifference(
                    setAddsButtonText: setAddsButtonText,
                    onDeleteButtonEnableChanged: onDeleteButtonEnableChanged,
                    // タイトル
                    tileTitle: TileTitle.Empty);

                // 末端にセット（変更通知を呼ぶために）
                // Ｉｄ
                SetIdOrEmpty(
                    value: TileIdOrEmpty.Empty,
                    setAddsButtonText: setAddsButtonText,
                    onDeleteButtonEnableChanged: onDeleteButtonEnableChanged);

                // 空にする
                recordVisually = TileRecordVisually.CreateEmpty();

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
                recordVisually = TileRecordVisually.CreateEmpty();
            }
            else
            {
                // ［切抜きカーソル］の指すタイルが有るなら構わない
            }

            // （変更通知を送っている）
            UpdateByDifference(
                setAddsButtonText: setAddsButtonText,
                onDeleteButtonEnableChanged: onDeleteButtonEnableChanged,
                // タイトル
                tileTitle: newValue.Title);

            SetIdOrEmpty(
                value: newValue.Id,
                setAddsButtonText: setAddsButtonText,
                onDeleteButtonEnableChanged: onDeleteButtonEnableChanged);
            // this.CropTileTitleAsStr = newValue.Title.AsStr;

            onUpdated();
        }
    }

    internal void SetRecordVisuallyNoGuiUpdate(TileRecordVisually value)
    {
        recordVisually = value;
    }

    #region プロパティ（Ｉｄ）
    /// <summary>
    ///     Ｉｄ
    /// </summary>
    public TileIdOrEmpty IdOrEmpty
    {
        get
        {
            var contents = RecordVisually;

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
        if (RecordVisually.Id == value)
            return;

        // 差分更新
        UpdateByDifference(
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
        var currentTileVisually = RecordVisually;

        // タイルＩｄ
        if (!(tileId is null) && currentTileVisually.Id != tileId)
        {
            RecordVisually.Id = tileId;

            // Ｉｄが入ることで、タイル登録扱いになる。いろいろ再描画する

            // ［追加／上書き］ボタン再描画
            setAddsButtonText(this.Colleagues.CalculateLabelOfAddsButton());

            // ［削除］ボタン再描画
            this.Colleagues.DeletesButtonRefreshEnabled(
                onEnableChanged: onDeleteButtonEnableChanged);
        }

        // タイル・タイトル
        if (!(tileTitle is null) && currentTileVisually.Title != tileTitle)
        {
            RecordVisually.Title = tileTitle;
        }

        // 論理削除フラグ
        if (!(logicalDelete is null) && currentTileVisually.LogicalDelete != logicalDelete)
        {
            RecordVisually.LogicalDelete = logicalDelete;
        }

        // Trace.WriteLine($"[CropTile.cs UpdateByDifference] SavesRecordVisually.Dump(): {this.SavesRecordVisually.Dump()}");
    }
    #endregion

    // - プライベート・プロパティ

    ItsMembers Colleagues { get; }

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
