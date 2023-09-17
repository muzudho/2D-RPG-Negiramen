namespace _2D_RPG_Negiramen.Hierarchy.Pages.TileCrop;

using _2D_RPG_Negiramen.Models.Geometric;

/// <summary>
///     ズーム関連
/// </summary>
internal class ZoomProperties
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="memberNetwork"></param>
    internal ZoomProperties()
    {
        this.Value = Zoom.IdentityElement;
        this.MaxValue = new(4.0f);
        this.MinValue = new(0.5f);
    }
    #endregion

    // - インターナル・デリゲート

    internal delegate void DoZoomProcessing(
        Zoom oldValue,
        Zoom newValue);

    // - インターナル・プロパティ

    /// <summary>
    ///     ズーム
    ///     
    ///     <list type="bullet">
    ///         <item>セッターは画像を再生成する重たい処理なので、スパムしないように注意</item>
    ///         <item>コード・ビハインドで使用</item>
    ///     </list>
    /// </summary>
    internal Zoom Value { get; set; }

    /// <summary>
    ///     ［ズーム］整数形式
    ///     
    ///     <list type="bullet">
    ///         <item>セッターは画像を再生成する重たい処理なので、スパムしないように注意</item>
    ///     </list>
    /// </summary>
    internal float AsFloat => this.Value.AsFloat;

    /// <summary>
    ///     ［ズーム］最大
    /// </summary>
    internal Zoom MaxValue { get; }

    /// <summary>
    ///     ズーム最大
    /// </summary>
    internal float MaxAsFloat => this.MaxValue.AsFloat;

    /// <summary>
    ///     ［ズーム］最小
    /// </summary>
    internal Zoom MinValue { get; }

    /// <summary>
    ///     ズーム最小
    /// </summary>
    internal float MinAsFloat => this.MinValue.AsFloat;
}
