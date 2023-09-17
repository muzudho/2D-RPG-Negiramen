namespace _2D_RPG_Negiramen.Hierarchy.TileCropPage;

using _2D_RPG_Negiramen.Models;

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
    /// <param name="roomsideDoors"></param>
    internal DeletesButton(
        ItsMemberNetwork roomsideDoors)
    {
        this.RoomsideDoors = roomsideDoors;
    }
    #endregion

    // - インターナル・デリゲート

    /// <summary>
    ///     上書きする
    /// </summary>
    internal delegate void DoRemoveRegisteredTIle(TileIdOrEmpty tileIdOrEmpty);

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
    public void RemoveTile(
        DoRemoveRegisteredTIle doRemoveRegisteredTIle)
    {
        doRemoveRegisteredTIle(
            tileIdOrEmpty: this.RoomsideDoors.CropTile.IdOrEmpty);
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

    ItsMemberNetwork RoomsideDoors { get; }
}
