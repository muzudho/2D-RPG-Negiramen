namespace _2D_RPG_Negiramen.Hierarchy.Pages.TileCrop;

using _2D_RPG_Negiramen.Models.Geometric;

/// <summary>
///     ポインティング・デバイス
/// </summary>
internal class PointingDevice
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    internal PointingDevice()
    {
    }
    #endregion

    // - インターナル・プロパティ

    #region プロパティ（ポインティング・デバイス押下開始位置）
    /// <summary>
    ///     ポインティング・デバイス押下開始位置
    /// </summary>
    internal PointFloat StartPoint { get; set; } = PointFloat.Zero;
    #endregion

    #region プロパティ（ポインティング・デバイス現在位置）
    /// <summary>
    ///     ポインティング・デバイス現在位置
    /// </summary>
    internal PointFloat CurrentPoint { get; set; } = PointFloat.Zero;
    #endregion

    #region プロパティ（ポインティング・デバイス押下中か？）
    /// <summary>
    ///     ポインティング・デバイス押下中か？
    /// 
    ///     <list type="bullet">
    ///         <item>タイルを選択開始していて、まだ未確定だ</item>
    ///         <item>マウスじゃないと思うけど</item>
    ///     </list>
    /// </summary>
    internal bool IsMouseDragging { get; private set; }
    #endregion

    internal void SetMouseDragging(
        bool value,
        Action onChanged)
    {
        if (this.IsMouseDragging == value) return;

        this.IsMouseDragging = value;
        onChanged();
    }
}
