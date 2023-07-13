namespace _2D_RPG_Negiramen.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using System.Collections.ObjectModel;
    using System.Globalization;

    /// <summary>
    ///     😁 メイン・ページ・ビューモデル
    /// </summary>
    internal class MainPageViewModel : ObservableObject
    {
        // - 変更通知プロパティ

        #region 変更通知プロパティ（現在選択中の文化情報。文字列形式）
        /// <summary>
        ///     現在選択中の文化情報。文字列形式
        /// </summary>
        public string CultureInfoAsStr
        {
            get
            {
                return LocalizationResourceManager.Instance.CultureInfo.Name;
            }
            set
            {
                if (LocalizationResourceManager.Instance.CultureInfo.Name != value)
                {
                    LocalizationResourceManager.Instance.SetCulture(new CultureInfo(value));

                    OnPropertyChanged(nameof(CultureInfoAsStr));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（ロケールＩｄのリスト）
        /// <summary>
        ///     ロケールＩｄのリスト
        /// </summary>
        public ObservableCollection<string> LocaleIdCollection => App.LocaleIdCollection;
        #endregion
    }
}
