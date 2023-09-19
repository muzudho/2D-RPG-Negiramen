namespace _2D_RPG_Negiramen.Hierarchy.HistoryOfPages.TileCrop;

using _2D_RPG_Negiramen.Coding;
using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.Geometric;
using _2D_RPG_Negiramen.Models.History;

/// <summary>
///     ［登録タイル追加］処理
/// </summary>
internal class AddRegisteredTileProcessing : IProcessing
{
    // - その他

    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="specObj"></param>
    /// <param name="tileRecord"></param>
    /// <param name="newTileIdOrEmpty"></param>
    /// <param name="workingRectangle"></param>
    internal AddRegisteredTileProcessing(
        MembersOfTileCropPage colleagues,
        TileRecord tileRecord,
        TileIdOrEmpty newTileIdOrEmpty,
        RectangleFloat workingRectangle)
    {
        this.Colleagues = colleagues;
        this.SetAddsButtonText = (text) =>
        {
            this.Colleagues.PageVM.AddsButton_Text = text;
            this.Colleagues.PageVM.InvalidateAddsButton();
        };

        this.SelectedTileRecord = tileRecord;
        this.TileIdOrEmpty = newTileIdOrEmpty;
        this.WorkingRectangle = workingRectangle;
    }

    LazyArgs.Set<string> SetAddsButtonText { get; }

    // - パブリック・メソッド

    #region メソッド（ドゥ―）
    /// <summary>
    ///     ドゥ―
    /// </summary>
    public void Do()
    {
        // ［タイル］のＩｄ変更
        this.Colleagues.PageVM.Subordinates.SelectedTile.SetIdOrEmpty(
            value: this.TileIdOrEmpty,
            onTileIdOrEmpty: (TileIdOrEmpty tileIdOrEmpty) =>
            {
                this.Colleagues.PageVM.UpdateByDifference(
                    setAddsButtonText: this.SetAddsButtonText,
                    onDeleteButtonEnableChanged: () =>
                    {
                        this.Colleagues.PageVM.InvalidateDeletesButton();
                    },
                    tileId: tileIdOrEmpty);
            });

        // ビューの再描画（タイルＩｄ更新）
        this.Colleagues.PageVM.InvalidateTileIdChange();

        // リストに登録済みか確認
        if (!this.Colleagues.PageVM.TilesetSettingsVM.TryGetTileById(this.TileIdOrEmpty, out TileRecord? registeredTileOrNull))
        {
            // リストに無ければ、ダミーのタイルを追加（あとですぐ上書きする）
            this.Colleagues.PageVM.TilesetSettingsVM.AddTile(
                id: this.TileIdOrEmpty,
                rect: RectangleInt.Empty,
                title: Models.TileTitle.Empty);
        }

        //
        // この時点で、タイルは必ず登録されている
        //

        // リストに必ず登録されているはずなので、選択タイルＩｄを使って、タイル・レコードを取得、その内容に、登録タイルを上書き
        if (this.Colleagues.PageVM.TilesetSettingsVM.TryRemoveTileById(this.TileIdOrEmpty, out registeredTileOrNull))
        {
            TileRecord registeredTile = registeredTileOrNull ?? throw new NullReferenceException(nameof(registeredTileOrNull));

            // 差替え
            this.Colleagues.PageVM.TilesetSettingsVM.AddTile(new TileRecord(
                id: this.TileIdOrEmpty,
                rect: this.SelectedTileRecord.Rectangle,
                title: this.SelectedTileRecord.Title));
        }

        //
        // 設定ファイルの保存
        // ==================
        //
        if (!this.Colleagues.PageVM.TilesetSettingsVM.SaveCsv(this.Colleagues.PageVM.TilesetDatatableFileLocation))
        {
            // TODO 保存失敗時のエラー対応
        }

        //
        // カラーマップの再描画
        // ====================
        //
        this.Colleagues.PageVM.RefreshForTileAdd();
    }
    #endregion

    #region メソッド（アンドゥ―）
    /// <summary>
    ///     アンドゥ―
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Undo()
    {
        // ［タイル］のＩｄ消去
        this.Colleagues.PageVM.Subordinates.SelectedTile.SetIdOrEmpty(
            value: TileIdOrEmpty.Empty,
            onTileIdOrEmpty: (TileIdOrEmpty tileIdOrEmpty) =>
            {
                this.Colleagues.PageVM.UpdateByDifference(
                    setAddsButtonText: this.SetAddsButtonText,
                    onDeleteButtonEnableChanged: () =>
                    {
                        this.Colleagues.PageVM.InvalidateDeletesButton();
                    },
                    tileId: tileIdOrEmpty);
            });

        // ビューの再描画（タイルＩｄ更新）
        this.Colleagues.PageVM.InvalidateTileIdChange();

        // リストから削除
        if (!this.Colleagues.PageVM.TilesetSettingsVM.TryRemoveTileById(this.TileIdOrEmpty, out TileRecord? tileRecordBufferOrNull))
        {
            // TODO 成功しなかったら異常
            throw new Exception();
        }

        //
        // 設定ファイルの保存
        // ==================
        //
        if (!this.Colleagues.PageVM.TilesetSettingsVM.SaveCsv(this.Colleagues.PageVM.TilesetDatatableFileLocation))
        {
            // TODO 保存失敗時のエラー対応
        }

        //  ［削除］ボタンの再描画
        this.Colleagues.PageVM.InvalidateDeletesButton();

        //
        // カラーマップの再描画
        // ====================
        //
        //this.coloredMapGraphicsView1.Invalidate();
        this.Colleagues.PageVM.RefreshForTileAdd();
    }
    #endregion

    // - プライベート・プロパティ

    /// <summary>メンバー・ネットワーク</summary>
    MembersOfTileCropPage Colleagues { get; }

    /// <summary>
    ///     ［切抜きカーソル］に対応
    /// </summary>
    TileRecord SelectedTileRecord { get; }

    /// <summary>
    ///     ［タイル］のＩｄ
    /// </summary>
    TileIdOrEmpty TileIdOrEmpty { get; }

    /// <summary>
    ///     タイルセット作業画像の位置とサイズ
    /// </summary>
    RectangleFloat WorkingRectangle { get; }
}
