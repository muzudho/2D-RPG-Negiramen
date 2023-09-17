﻿namespace _2D_RPG_Negiramen.Hierarchy.HistoryOfPages.TileCrop;

using _2D_RPG_Negiramen.Coding;
using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.Geometric;
using _2D_RPG_Negiramen.Models.History;
using _2D_RPG_Negiramen.Models.Visually;
using TheHistoryOfTileCropPage = _2D_RPG_Negiramen.Hierarchy.HistoryOfPages.TileCrop;
using TheTileCropPage = _2D_RPG_Negiramen.Hierarchy.Pages.TileCrop;

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
        TheHistoryOfTileCropPage.Common commonOfHierarchy,
        MemberNetworkOfTileCropPage colleagues,
        TheTileCropPage.ItsMemberNetwork subordinates,
        TileRecordVisually croppedCursorVisually,
        TileIdOrEmpty tileIdOrEmpty,
        RectangleFloat workingRectangle)
    {
        this.CommonOfHierarchy = commonOfHierarchy;
        this.Colleagues = colleagues;
        this.SetAddsButtonText = (text) =>
        {
            this.Colleagues.PageVM.AddsButtonText = text;
            this.Colleagues.PageVM.InvalidateAddsButton();
        };

        this.Subordinates = subordinates;

        this.CroppedCursorVisually = croppedCursorVisually;
        this.TileIdOrEmpty = tileIdOrEmpty;
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
        this.Subordinates.CropTile.SetIdOrEmpty(
            value: this.TileIdOrEmpty,
            setAddsButtonText: this.SetAddsButtonText,
            onDeleteButtonEnableChanged: () =>
            {
                this.Colleagues.PageVM.InvalidateDeletesButton();
            });

        // ビューの再描画（タイルＩｄ更新）
        this.Colleagues.PageVM.InvalidateTileIdChange();

        // リストに登録済みか確認
        if (!this.Colleagues.PageVM.TilesetSettingsVM.TryGetTileById(this.TileIdOrEmpty, out TileRecordVisually? registeredTileVisuallyOrNull))
        {
            // リストに無ければ、ダミーのタイルを追加（あとですぐ上書きする）
            this.Colleagues.PageVM.TilesetSettingsVM.AddTileVisually(
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
        if (this.Colleagues.PageVM.TilesetSettingsVM.TryGetTileById(this.TileIdOrEmpty, out registeredTileVisuallyOrNull))
        {
            TileRecordVisually registeredTileVisually = registeredTileVisuallyOrNull ?? throw new NullReferenceException(nameof(registeredTileVisuallyOrNull));

            // 新・元画像の位置とサイズ
            registeredTileVisually.SourceRectangle = this.CroppedCursorVisually.SourceRectangle;

            // 新・作業画像の位置とサイズ
            registeredTileVisually.Zoom = this.Subordinates.ZoomProperties.Value;

            // 新・タイル・タイトル
            registeredTileVisually.Title = this.CroppedCursorVisually.Title;

            // 新・論理削除
            registeredTileVisually.LogicalDelete = this.CroppedCursorVisually.LogicalDelete;
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
        this.Subordinates.CropTile.SetIdOrEmpty(
            value: TileIdOrEmpty.Empty,
            setAddsButtonText: this.SetAddsButtonText,
            onDeleteButtonEnableChanged: () =>
            {
                this.Colleagues.PageVM.InvalidateDeletesButton();
            });

        // ビューの再描画（タイルＩｄ更新）
        this.Colleagues.PageVM.InvalidateTileIdChange();

        // リストから削除
        if (!this.Colleagues.PageVM.TilesetSettingsVM.TryRemoveTileById(this.TileIdOrEmpty, out TileRecordVisually? tileRecordVisualBufferOrNull))
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
        this.Subordinates.DeletesButton.Refresh(
            onEnableChanged: () =>
            {
                this.Colleagues.PageVM.InvalidateDeletesButton();
            });

        //
        // カラーマップの再描画
        // ====================
        //
        //this.coloredMapGraphicsView1.Invalidate();
        this.Colleagues.PageVM.RefreshForTileAdd();
    }
    #endregion

    // - プライベート・プロパティ

    TheHistoryOfTileCropPage.Common CommonOfHierarchy { get; }

    /// <summary>メンバー・ネットワーク</summary>
    MemberNetworkOfTileCropPage Colleagues { get; }
    TheTileCropPage.ItsMemberNetwork Subordinates { get; }

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
