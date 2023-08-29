namespace _2D_RPG_Negiramen.ViewHistory.TileCropPage;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.Geometric;
using _2D_RPG_Negiramen.Models.History;
using _2D_RPG_Negiramen.Models.Visually;
using _2D_RPG_Negiramen.Specifications.TileCropPage;

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
    /// <param name="croppedCursorVisually"></param>
    /// <param name="tileIdOrEmpty"></param>
    /// <param name="workingRectangle"></param>
    internal AddRegisteredTileProcessing(
        ItsGardensideDoor gardensideDoor,
        IItsCorridorOutdoorDirection obsoletedOutdoor,
        IItsIndoor spec,
        TileRecordVisually croppedCursorVisually,
        TileIdOrEmpty tileIdOrEmpty,
        RectangleFloat workingRectangle)
    {
        this.GardensideDoor = gardensideDoor;

        this.ObsoletedOutdoor = obsoletedOutdoor;
        this.Indoor = spec;
        this.CroppedCursorVisually = croppedCursorVisually;
        this.TileIdOrEmpty = tileIdOrEmpty;
        this.WorkingRectangle = workingRectangle;
    }

    // - パブリック・メソッド

    #region メソッド（ドゥ―）
    /// <summary>
    ///     ドゥ―
    /// </summary>
    public void Do()
    {
        // ［タイル］のＩｄ変更
        this.Indoor.CropTileIdOrEmpty = this.TileIdOrEmpty;

        // ビューの再描画（タイルＩｄ更新）
        this.ObsoletedOutdoor.ObsoletedInvalidateTileIdChange();

        // リストに登録済みか確認
        if (!this.ObsoletedOutdoor.ObsoletedTilesetSettingsVMTryGetTileById(this.TileIdOrEmpty, out TileRecordVisually? registeredTileVisuallyOrNull))
        {
            // リストに無ければ、ダミーのタイルを追加（あとですぐ上書きする）
            this.ObsoletedOutdoor.ObsoletedTilesetSettingsVMAddTileVisually(
                id: this.TileIdOrEmpty,
                rect: RectangleInt.Empty,
                zoom: Zoom.IdentityElement,
                title: Models.TileTitle.Empty,
                logicalDelete: Models.LogicalDelete.False);
        }

        //
        // この時点で、タイルは必ず登録されている
        //

        // リストに必ず登録されているはずなので、選択タイルＩｄを使って、タイル・レコードを取得、その内容に、登録タイルを上書き
        if (this.ObsoletedOutdoor.ObsoletedTilesetSettingsVMTryGetTileById(this.TileIdOrEmpty, out registeredTileVisuallyOrNull))
        {
            TileRecordVisually registeredTileVisually = registeredTileVisuallyOrNull ?? throw new NullReferenceException(nameof(registeredTileVisuallyOrNull));

            // 新・元画像の位置とサイズ
            registeredTileVisually.SourceRectangle = this.CroppedCursorVisually.SourceRectangle;

            // 新・作業画像の位置とサイズ
            registeredTileVisually.Zoom = this.Indoor.RoomsideDoors.ZoomValue;

            // 新・タイル・タイトル
            registeredTileVisually.Title = this.CroppedCursorVisually.Title;

            // 新・論理削除
            registeredTileVisually.LogicalDelete = this.CroppedCursorVisually.LogicalDelete;
        }

        //
        // 設定ファイルの保存
        // ==================
        //
        if (!this.ObsoletedOutdoor.ObsoletedTilesetSettingsVMSaveCsv(this.GardensideDoor.PageVM.TilesetDatatableFileLocation))
        {
            // TODO 保存失敗時のエラー対応
        }

        //
        // カラーマップの再描画
        // ====================
        //
        this.ObsoletedOutdoor.RefreshForTileAdd();
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
        this.Indoor.CropTileIdOrEmpty = TileIdOrEmpty.Empty;

        // ビューの再描画（タイルＩｄ更新）
        this.ObsoletedOutdoor.ObsoletedInvalidateTileIdChange();

        // リストから削除
        if (!this.ObsoletedOutdoor.ObsoletedTilesetSettingsVMTryRemoveTileById(this.TileIdOrEmpty, out TileRecordVisually? tileRecordVisualBufferOrNull))
        {
            // TODO 成功しなかったら異常
            throw new Exception();
        }

        //
        // 設定ファイルの保存
        // ==================
        //
        if (!this.ObsoletedOutdoor.ObsoletedTilesetSettingsVMSaveCsv(this.GardensideDoor.PageVM.TilesetDatatableFileLocation))
        {
            // TODO 保存失敗時のエラー対応
        }

        //  ［削除］ボタンの再描画
        this.Indoor.DeletesButtonRefresh();

        //
        // カラーマップの再描画
        // ====================
        //
        //this.coloredMapGraphicsView1.Invalidate();
        this.ObsoletedOutdoor.RefreshForTileAdd();
    }
    #endregion

    // - プライベート・プロパティ

    /// <summary>内部モデル</summary>
    ItsGardensideDoor GardensideDoor { get; }
    IItsCorridorOutdoorDirection ObsoletedOutdoor { get; }
    IItsIndoor Indoor { get; }

    /// <summary>
    ///     ［切抜きカーソル］に対応
    /// </summary>
    TileRecordVisually CroppedCursorVisually { get; }

    /// <summary>
    ///     ［タイル］のＩｄ
    /// </summary>
    TileIdOrEmpty TileIdOrEmpty { get; }

    /// <summary>
    ///     タイルセット作業画像の位置とサイズ
    /// </summary>
    RectangleFloat WorkingRectangle { get; }
}
