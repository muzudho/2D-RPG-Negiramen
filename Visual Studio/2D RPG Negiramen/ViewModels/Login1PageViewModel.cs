namespace _2D_RPG_Negiramen.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;

/// <summary>
///     😁 ［ログイン１］ページ・ビューモデル
/// </summary>
internal class Login1PageViewModel : ObservableObject, ILogin1PageViewModel
{
    // - パブリック・プロパティ

    #region プロパティ（［サークル名］の文字数）
    /// <summary>
    ///     ［サークル名］の文字数
    /// </summary>
    public int YourCircleNameLength
    {
        get => this.yourCircleNameLength;
        set
        {
            if (this.yourCircleNameLength == value)
                return;
            
            this.yourCircleNameLength = value;
            OnPropertyChanged(nameof(CharacterLength));
        }
    }
    #endregion

    #region プロパティ（［作品名］の文字数）
    /// <summary>
    ///     ［作品名］の文字数
    /// </summary>
    public int YourWorkNameLength
    {
        get => this.yourWorkNameLength;
        set
        {
            if (this.yourWorkNameLength == value)
                return;

            this.yourWorkNameLength = value;
            OnPropertyChanged(nameof(CharacterLength));
        }
    }
    #endregion

    // - パブリック変更通知プロパティ

    #region 変更通知プロパティ（ロケール　関連）
    /// <summary>
    ///     現在選択中の文化情報。文字列形式
    /// </summary>
    public string CultureInfoAsStr
    {
        get => LocalizationResourceManager.Instance.CultureInfo.Name;
        set
        {
            if (LocalizationResourceManager.Instance.CultureInfo.Name != value)
            {
                LocalizationResourceManager.Instance.SetCulture(new CultureInfo(value));
                OnPropertyChanged(nameof(CultureInfoAsStr));
            }
        }
    }

    /// <summary>
    ///     ロケールＩｄのリスト
    /// </summary>
    public ObservableCollection<string> LocaleIdCollection => App.LocaleIdCollection;
    #endregion

    #region 変更通知プロパティ（［文字数］）
    /// <summary>
    ///     ［文字数］
    /// </summary>
    public int CharacterLength => this.yourCircleNameLength + this.yourWorkNameLength;
    #endregion

    // - パブリック・メソッド

    #region メソッド（ロケール変更による再描画）
    /// <summary>
    ///     ロケール変更による再描画
    ///     
    ///     <list type="bullet">
    ///         <item>動的にテキストを変えている部分に対応するため</item>
    ///     </list>
    /// </summary>
    public void InvalidateLocale()
    {
        // this.InvalidateAddsButton();
    }
    #endregion

    // - プライベート・プロパティ

    int yourCircleNameLength;
    int yourWorkNameLength;
}
