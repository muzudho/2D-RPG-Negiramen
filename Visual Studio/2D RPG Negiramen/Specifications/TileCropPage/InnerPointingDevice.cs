namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.Models.Geometric;

/// <summary>
///     ポインティング・デバイス
/// </summary>
internal class InnerPointingDevice
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="owner"></param>
    internal InnerPointingDevice(
        ItsGardensideDoor gardensideDoor,
        IItsIndoor indoor)
    {
        this.GardensideDoor = gardensideDoor;
        this.Indoor = indoor;
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
    internal bool IsMouseDragging
    {
        get => isMouseDragging;
        set
        {
            if (isMouseDragging != value)
            {
                isMouseDragging = value;
                this.GardensideDoor.PageVM.InvalidateIsMouseDragging();
            }
        }
    }
    #endregion

    // - プライベート・プロパティ

    ItsGardensideDoor GardensideDoor { get; }
    IItsIndoor Indoor { get; }

    #region プロパティ（ポインティング・デバイス押下中か？）
    /// <summary>
    ///     ポインティング・デバイス押下中か？
    /// </summary>
    bool isMouseDragging { get; set; }
    #endregion
}
