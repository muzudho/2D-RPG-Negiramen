namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.Models.Geometric;
using _2D_RPG_Negiramen.ViewHistory.TileCropPage;

/// <summary>
///     ズーム
/// </summary>
internal class InnerZoom
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="specObj"></param>
    internal InnerZoom(
        IItsOutdoor outdoor,
        IItsIndoor indoor)
    {
        this.Outdoor = outdoor;
        this.Indoor = indoor;
    }
    #endregion

    // - インターナル・プロパティ
    /// <summary>
    ///     ズーム
    ///     
    ///     <list type="bullet">
    ///         <item>セッターは画像を再生成する重たい処理なので、スパムしないように注意</item>
    ///         <item>コード・ビハインドで使用</item>
    ///     </list>
    /// </summary>
    public Zoom Value
    {
        get => value;
        set
        {
            if (this.value == value)
                return;

            // TODO 循環参照しやすいから、良くないコード
            this.Outdoor.ZoomAsFloat = value.AsFloat;
        }
    }

    /// <summary>
    ///     ［ズーム］整数形式
    ///     
    ///     <list type="bullet">
    ///         <item>セッターは画像を再生成する重たい処理なので、スパムしないように注意</item>
    ///     </list>
    /// </summary>
    public float AsFloat
    {
        get => value.AsFloat;
        set
        {
            if (this.value.AsFloat != value)
            {
                if (this.Outdoor.ZoomMinAsFloat <= value && value <= this.Outdoor.ZoomMaxAsFloat)
                {
                    Zoom oldValue = this.value;
                    Zoom newValue = new Zoom(value);

                    this.value = newValue;
                    this.Indoor.CropCursorRefreshCanvasTrick("[TileCropPageViewModel.cs ZoomAsFloat]");

                    // 再帰的にズーム再変更、かつ変更後の影響を処理
                    App.History.Do(new ZoomProcessing(
                        this.Outdoor,
                        this.Indoor,
                        oldValue,
                        newValue));
                }
            }
        }
    }

    /// <summary>
    ///     ズーム最大
    /// </summary>
    public float MaxAsFloat => maxValue.AsFloat;

    /// <summary>
    ///     ズーム最小
    /// </summary>
    public float MinAsFloat => minValue.AsFloat;

    // - プライベート・プロパティ

    IItsOutdoor Outdoor { get; }
    IItsIndoor Indoor { get; }

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
