namespace _2D_RPG_Negiramen.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;

/// <summary>
///     😁 ［メイン・ページ］ビューモデル
/// </summary>
internal class MainPageViewModel : ObservableObject, IMainPageViewModel
{
    // - 変更通知プロパティ

    #region 変更通知プロパティ（現在選択中の文化情報。文字列形式）
    /// <summary>
    ///     現在選択中の文化情報。文字列形式
    /// </summary>
    public CultureInfo SelectedCultureInfo
    {
        get
        {
            return LocalizationResourceManager.Instance.CultureInfo;
        }
        set
        {
            if (LocalizationResourceManager.Instance.CultureInfo != value)
            {
                LocalizationResourceManager.Instance.SetCulture(value);
                OnPropertyChanged(nameof(SelectedCultureInfo));
            }
        }
    }
    #endregion

    #region 変更通知プロパティ（文化情報のリスト）
    /// <summary>
    ///     文化情報のリスト
    /// </summary>
    public ObservableCollection<CultureInfo> CultureInfoCollection => App.CultureInfoCollection;
    #endregion

    // - パブリック・メソッド

    #region メソッド（画面遷移でこの画面に戻ってきた時）
    /// <summary>
    ///     画面遷移でこの画面に戻ってきた時
    /// </summary>
    public void ReactOnVisited()
    {
        // ロケールが変わってるかもしれないので反映
        OnPropertyChanged(nameof(SelectedCultureInfo));
    }
    #endregion
}
