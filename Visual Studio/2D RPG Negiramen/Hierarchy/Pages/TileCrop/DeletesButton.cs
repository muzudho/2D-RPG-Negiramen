namespace _2D_RPG_Negiramen.Hierarchy.Pages.TileCrop;

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
    /// <param name="colleagues"></param>
    internal DeletesButton(
        ItsMembers colleagues)
    {
        this.Colleagues = colleagues;
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
    public bool IsEnabled => isEnabled;
    #endregion

    // - インターナル・メソッド

    #region メソッド（再描画）
    /// <summary>
    ///     再描画
    /// </summary>
    internal void RefreshEnabled(
        Action onEnableChanged)
    {
        var contents = this.Colleagues.CropTile.RecordVisually;

        if (
            // 切抜きカーソル無し時
            contents.IsNone
            // 論理削除時
            || contents.LogicalDelete.AsBool)
        {
            // 不活性
            SetEnabled(
                value: false,
                onChanged: onEnableChanged);
            return;
        }

        if (contents.Id == TileIdOrEmpty.Empty)
        {
            // Ｉｄ未設定時
            SetEnabled(
                value: false,
                onChanged: onEnableChanged);
            return;
        }

        // タイル登録済み時
        SetEnabled(
            value: true,
            onChanged: onEnableChanged);
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

    ItsMembers Colleagues { get; }

    // - プライベート・メソッド

    void SetEnabled(
        bool value,
        Action onChanged)
    {
        if (isEnabled == value) return;

        isEnabled = value;

        onChanged();
    }
}
