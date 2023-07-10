namespace _2D_RPG_Negiramen
{
    using _2D_RPG_Negiramen.Resources.Languages;
    using System.ComponentModel;
    using System.Globalization;

    /// <summary>
    ///     <pre>
    ///         ローカライゼーション資源管理
    ///         
    ///         📺 [Localization in .NET MAUI - Adding Multi-Language to Your Apps](https://www.youtube.com/watch?v=cf4sXULR7os)
    ///         📖 [.NET MAUI Localization Sample](https://github.com/jfversluis/MauiLocalizationSample)  
    ///     </pre>
    /// </summary>
    public class LocalizationResourceManager : INotifyPropertyChanged
    {
        // - パブリック静的プロパティ

        #region プロパティ（インスタンス）
        /// <summary>
        ///     インスタンス
        /// </summary>
        public static LocalizationResourceManager Instance { get; } = new();
        #endregion

        // - その他

        #region その他（生成）
        /// <summary>
        ///     生成
        /// </summary>
        private LocalizationResourceManager()
        {
            AppResources.Culture = CultureInfo.CurrentCulture;
        }
        #endregion

        // - パブリック・プロパティ

        #region プロパティ（インデクサ）
        /// <summary>
        ///     インデクサ
        /// </summary>
        /// <param name="resourceKey">リソース・キー</param>
        /// <returns></returns>
        public object this[string resourceKey]
            => AppResources.ResourceManager.GetObject(resourceKey, AppResources.Culture) ?? Array.Empty<byte>();
        #endregion

        // - パブリック・イベント

        #region イベント（プロパティ変更時）
        /// <summary>
        ///     プロパティ変更時
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        // - パブリック・メソッド

        #region メソッド（文化設定）
        /// <summary>
        ///     文化設定
        /// </summary>
        /// <param name="culture">文化</param>
        public void SetCulture(CultureInfo culture)
        {
            AppResources.Culture = culture;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（文化情報）
        /// <summary>
        ///     文化情報
        /// </summary>
        internal CultureInfo CultureInfo => AppResources.Culture;
        #endregion
    }
}
