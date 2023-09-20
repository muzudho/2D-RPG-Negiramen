namespace _2D_RPG_Negiramen.Hierarchy.HistoryOfPages.TileCrop;

using _2D_RPG_Negiramen.Coding;
using _2D_RPG_Negiramen.Models;
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
    /// <param name="tileBackup"></param>
    /// <param name="newTileIdOrEmpty"></param>
    internal AddRegisteredTileProcessing(
        MembersOfTileCropPage colleagues,
        TileRecord tileBackup,
        TileIdOrEmpty newTileIdOrEmpty)
    {
        this.Colleagues = colleagues;
        this.SetAddsButtonText = (text) =>
        {
            this.Colleagues.PageVM.AddsButton_Text = text;
            this.Colleagues.PageVM.InvalidateAddsButton();
        };

        this.TileBackup = tileBackup;
        this.TileIdOrEmpty = newTileIdOrEmpty;
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

        // 追加。リストに無いから追加するはず。上書きはあり得ない
        this.Colleagues.PageVM.TilesetSettingsVM.AddTile(new TileRecord(
            id: this.TileIdOrEmpty,
            rect: this.TileBackup.Rectangle,
            title: this.TileBackup.Title));

        //
        // 設定ファイルの保存
        // ==================
        //
        if (!this.Colleagues.PageVM.TilesetSettingsVM.SaveCsv(this.Colleagues.PageVM.TilesetDatatableFileLocation))
        {
            // TODO 保存失敗時のエラー対応
        }

        this.InvalidateGui();
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

        this.InvalidateGui();
    }
    #endregion

    // - プライベート・プロパティ

    /// <summary>メンバー・ネットワーク</summary>
    MembersOfTileCropPage Colleagues { get; }

    /// <summary>
    ///     選択タイルのバックアップ
    /// </summary>
    TileRecord TileBackup { get; }

    /// <summary>
    ///     ［タイル］のＩｄ
    /// </summary>
    TileIdOrEmpty TileIdOrEmpty { get; }

    // - プライベート・メソッド

    /// <summary>
    ///     GUIに変更通知を送るもの
    /// </summary>
    void InvalidateGui()
    {
        // ［追加］ボタンの変更通知
        this.Colleagues.PageVM.InvalidateAddsButton();

        // ［削除］ボタンの変更通知
        this.Colleagues.PageVM.InvalidateDeletesButton();

        // ［タイルＩｄ］の変更通知
        this.Colleagues.PageVM.InvalidateTileIdChange();

        // ［タイル タイトル］の変更通知
        this.Colleagues.PageVM.InvalidateTileTitle();

        // ［タイルセット作業画像］のサイズ変更
        this.Colleagues.PageVM.TrickChangeWorkingImageSize(
            onFinished: () =>{});

        // ［タイルセット作業画像］の変更通知
        this.Colleagues.PageVM.InvalidateTilesetWorkingImage();
    }
}
