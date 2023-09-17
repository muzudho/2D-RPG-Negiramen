namespace _2D_RPG_Negiramen.Hierarchy.Pages.TileCrop;

/// <summary>
///     ボタン
/// </summary>
internal class Button
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    internal Button()
    {
    }
    #endregion

    // - インターナル・プロパティ

    #region プロパティ（活性性）
    /// <summary>
    ///     活性性
    /// </summary>
    public bool IsEnabled => isEnabled;
    #endregion

    // - インターナル・メソッド

    #region メソッド（活性性の設定）
    /// <summary>
    ///     活性性の設定
    /// </summary>
    /// <param name="value"></param>
    /// <param name="onChanged"></param>
    internal void SetEnabled(
        bool value,
        Action onChanged)
    {
        if (isEnabled == value) return;

        isEnabled = value;

        onChanged();
    }
    #endregion

    // - プライベート・フィールド

    #region フィールド（活性性）
    /// <summary>
    ///     活性性
    /// </summary>
    bool isEnabled;
    #endregion
}
