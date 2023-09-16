﻿namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.ViewHistory.TileCropPage;

/// <summary>
///     削除ボタン
/// </summary>
internal class DeletesButton
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="specObj"></param>
    internal DeletesButton(
        ItsGardensideDoor gardensideDoor,
        ItsRoomsideDoors roomsideDoors)
    {
        this.GardensideDoor = gardensideDoor;
        this.RoomsideDoors = roomsideDoors;
    }
    #endregion

    // - インターナル・プロパティ

    #region プロパティ（活性性）
    /// <summary>
    ///     活性性
    /// </summary>
    public bool IsEnabled
    {
        get => this.isEnabled;
    }
    #endregion

    internal void SetEnabled(
        bool value,
        Action onChanged)
    {
        if (this.isEnabled == value)
            return;

        this.isEnabled = value;

        onChanged();
    }

    // - インターナル・メソッド

    #region メソッド（再描画）
    /// <summary>
    ///     再描画
    /// </summary>
    internal void Refresh(
        Action onEnableChanged)
    {
        var contents = this.RoomsideDoors.CropTile.RecordVisually;

        if (contents.IsNone)
        {
            // 切抜きカーソル無し時
            this.SetEnabled(
                value: false,
                onChanged: onEnableChanged);
            return;
        }

        // 切抜きカーソル有り時
        if (contents.Id == TileIdOrEmpty.Empty)
        {
            // Ｉｄ未設定時
            this.SetEnabled(
                value: false,
                onChanged: onEnableChanged);
        }
        else
        {
            // タイル登録済み時
            this.SetEnabled(
                value: true,
                onChanged: onEnableChanged);
        }
    }
    #endregion

    #region メソッド（タイル削除）
    /// <summary>
    ///     タイル削除
    /// </summary>
    public void RemoveTile()
    {
        App.History.Do(new RemoveRegisteredTileProcessing(
            gardensideDoor: this.GardensideDoor,
            tileIdOrEmpty: this.RoomsideDoors.CropTile.IdOrEmpty));

        this.GardensideDoor.PageVM.InvalidateForHistory();
    }
    #endregion

    // - プライベート・フィールド

    #region フィールド（活性性）
    /// <summary>
    ///     活性性
    /// </summary>
    bool isEnabled;
    #endregion

    // - プライベート・プロパティ

    ItsGardensideDoor GardensideDoor { get; }
    ItsRoomsideDoors RoomsideDoors { get; }
}
