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
    internal Zoom Value => value;

    internal void SetValue(Zoom value) => this.value = value;

    /// <summary>
    ///     ［ズーム］整数形式
    ///     
    ///     <list type="bullet">
    ///         <item>セッターは画像を再生成する重たい処理なので、スパムしないように注意</item>
    ///     </list>
    /// </summary>
    internal float AsFloat => value.AsFloat;

    /// <summary>
    ///     ズーム最大
    /// </summary>
    internal float MaxAsFloat => maxValue.AsFloat;

    /// <summary>
    ///     ズーム最小
    /// </summary>
    internal float MinAsFloat => minValue.AsFloat;

    // - プライベート・フィールド

    /// <summary>
    ///     ［ズーム］
    /// </summary>
    Zoom value = Zoom.IdentityElement;

    /// <summary>
    ///     ［ズーム］最大
    /// </summary>
    Zoom maxValue = new(4.0f);

    /// <summary>
    ///     ［ズーム］最小
    /// </summary>
    Zoom minValue = new(0.5f);
}
