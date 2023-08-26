﻿namespace _2D_RPG_Negiramen.ViewHistory.TileCropPage;

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
        IItsSpec spec,
        TileRecordVisually croppedCursorVisually,
        TileIdOrEmpty tileIdOrEmpty,
        RectangleFloat workingRectangle)
    {
        this.Spec = spec;
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
        this.Spec.CropTileIdOrEmpty = this.TileIdOrEmpty;

        // ビューの再描画（タイルＩｄ更新）
        this.Spec.WholeInvalidateTileIdChange();

        // リストに登録済みか確認
        if (!this.Spec.WholeTilesetSettingsVMTryGetTileById(this.TileIdOrEmpty, out TileRecordVisually? registeredTileVisuallyOrNull))
        {
            // リストに無ければ、ダミーのタイルを追加（あとですぐ上書きする）
            this.Spec.WholeTilesetSettingsVMAddTileVisually(
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
        if (this.Spec.WholeTilesetSettingsVMTryGetTileById(this.TileIdOrEmpty, out registeredTileVisuallyOrNull))
        {
            TileRecordVisually registeredTileVisually = registeredTileVisuallyOrNull ?? throw new NullReferenceException(nameof(registeredTileVisuallyOrNull));

            // 新・元画像の位置とサイズ
            registeredTileVisually.SourceRectangle = this.CroppedCursorVisually.SourceRectangle;

            // 新・作業画像の位置とサイズ
            registeredTileVisually.Zoom = this.Spec.ZoomValue;

            // 新・タイル・タイトル
            registeredTileVisually.Title = this.CroppedCursorVisually.Title;

            // 新・論理削除
            registeredTileVisually.LogicalDelete = this.CroppedCursorVisually.LogicalDelete;
        }

        //
        // 設定ファイルの保存
        // ==================
        //
        if (!this.Spec.WholeTilesetSettingsVMSaveCsv(this.Spec.WholeTilesetDatatableFileLocation))
        {
            // TODO 保存失敗時のエラー対応
        }

        //
        // カラーマップの再描画
        // ====================
        //
        this.Spec.WholeRefreshForTileAdd();
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
        this.Spec.CropTileIdOrEmpty = TileIdOrEmpty.Empty;

        // ビューの再描画（タイルＩｄ更新）
        this.Spec.WholeInvalidateTileIdChange();

        // リストから削除
        if (!this.Spec.WholeTilesetSettingsVMTryRemoveTileById(this.TileIdOrEmpty, out TileRecordVisually? tileRecordVisualBufferOrNull))
        {
            // TODO 成功しなかったら異常
            throw new Exception();
        }

        //
        // 設定ファイルの保存
        // ==================
        //
        if (!this.Spec.WholeTilesetSettingsVMSaveCsv(this.Spec.WholeTilesetDatatableFileLocation))
        {
            // TODO 保存失敗時のエラー対応
        }

        //  ［削除］ボタンの再描画
        this.Spec.DeletesButtonRefresh();

        //
        // カラーマップの再描画
        // ====================
        //
        //this.coloredMapGraphicsView1.Invalidate();
        this.Spec.WholeRefreshForTileAdd();
    }
    #endregion

    // - プライベート・プロパティ

    /// <summary>
    ///     内部モデル
    /// </summary>
    IItsSpec Spec { get; }

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
