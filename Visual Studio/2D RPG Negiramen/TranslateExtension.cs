namespace _2D_RPG_Negiramen;

/// <summary>
///     <pre>
///         翻訳拡張
///         
///         📺 [Localization in .NET MAUI - Adding Multi-Language to Your Apps](https://www.youtube.com/watch?v=cf4sXULR7os)
///         📖 [.NET MAUI Localization Sample](https://github.com/jfversluis/MauiLocalizationSample)  
///     </pre>
/// </summary>
[ContentProperty(nameof(Name))]
public class TranslateExtension : IMarkupExtension<BindingBase>
{
    // - パブリック・プロパティ

    #region プロパティ（名前）
    /// <summary>
    ///     名前
    /// </summary>
    public string Name { get; set; }
    #endregion

    #region プロパティ（値の提供）
    /// <summary>
    ///     値の提供
    /// </summary>
    /// <param name="serviceProvider">サービス提供機</param>
    /// <returns>束縛</returns>
    public BindingBase ProvideValue(IServiceProvider serviceProvider)
    {
        return new Binding
        {
            Mode = BindingMode.OneWay,
            Path = $"[{Name}]",
            Source = LocalizationResourceManager.Instance
        };
    }
    #endregion

    // - プライベート・メソッド

    #region メソッド（値の提供）
    /// <summary>
    ///     値の提供
    /// </summary>
    /// <param name="serviceProvider">サービス提供機</param>
    /// <returns></returns>
    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return ProvideValue(serviceProvider);
    }
    #endregion
}
