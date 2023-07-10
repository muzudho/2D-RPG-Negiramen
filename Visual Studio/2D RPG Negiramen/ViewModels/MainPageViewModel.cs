namespace _2D_RPG_Negiramen.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using System.Globalization;

    /// <summary>
    ///     😁 メイン・ページ・ビューモデル
    /// </summary>
    internal class MainPageViewModel : ObservableObject
    {
        // - 変更通知プロパティ

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
    }
}
