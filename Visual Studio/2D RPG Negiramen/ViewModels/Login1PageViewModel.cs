namespace _2D_RPG_Negiramen.ViewModels;

using _2D_RPG_Negiramen.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

/// <summary>
///     😁 ［ログイン１］ページ・ビューモデル
/// </summary>
internal class Login1PageViewModel : ObservableObject, ILogin1PageViewModel
{
    // - パブリック・プロパティ

    #region プロパティ（あなたのサークル名）
    /// <summary>
    ///     あなたのサークル名
    /// </summary>
    public YourCircleName YourCircleName
    {
        get => this.yourCircleName;
        set
        {
            if (this.yourCircleName == value)
                return;

            this.YourCircleNameAsStr = value.AsStr;
        }
    }
    #endregion

    #region プロパティ（あなたの作品名）
    /// <summary>
    ///     あなたの作品名
    /// </summary>
    public YourWorkName YourWorkName
    {
        get => this.yourWorkName;
        set
        {
            if (this.yourWorkName == value)
                return;

            this.YourWorkNameAsStr = value.AsStr;
        }
    }
    #endregion

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
            OnPropertyChanged(nameof(NumberOfCharacters));
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
            OnPropertyChanged(nameof(NumberOfCharacters));
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

    #region 変更通知プロパティ（あなたのサークル名）
    /// <summary>
    ///     あなたのサークル名
    /// </summary>
    public string YourCircleNameAsStr
    {
        get => this.yourCircleName.AsStr;
        set
        {
            if (this.yourCircleName.AsStr == value)
                return;

            this.yourCircleName = new YourCircleName(value);
            OnPropertyChanged(nameof(YourCircleNameAsStr));

            // this.IsVisibleOfNextButton = !this.IsExistsEntryInList();
            OnPropertyChanged(nameof(IsVisibleOfNextButton));
        }
    }
    #endregion

    #region 変更通知プロパティ（あなたの作品名）
    /// <summary>
    ///     あなたの作品名
    /// </summary>
    public string YourWorkNameAsStr
    {
        get => this.yourWorkName.AsStr;
        set
        {
            if (this.yourWorkName.AsStr == value)
                return;

            this.yourWorkName = new YourWorkName(value);
            OnPropertyChanged(nameof(YourWorkNameAsStr));

            // this.IsVisibleOfNextButton = !this.IsExistsEntryInList();
            OnPropertyChanged(nameof(IsVisibleOfNextButton));
        }
    }
    #endregion

    #region 変更通知プロパティ（［文字数］）
    /// <summary>
    ///     ［文字数］
    /// </summary>
    public int NumberOfCharacters => this.yourCircleNameLength + this.yourWorkNameLength;
    #endregion

    #region 変更通知プロパティ（［続きから］ボタンの可視性）
    /// <summary>
    ///     ［次へ］ボタンの可視性
    /// </summary>
    public bool IsVisibleOfContinueButton
    {
        get => this.isVisibleOfContinueButton;
        set
        {
            if (this.isVisibleOfContinueButton == value)
                return;

            this.isVisibleOfContinueButton = value;
            OnPropertyChanged(nameof(IsVisibleOfContinueButton));
        }
    }
    #endregion

    #region 変更通知プロパティ（［次へ］ボタンの可視性）
    /// <summary>
    ///     ［次へ］ボタンの可視性
    ///     
    ///     <list type="bullet">
    ///         <item>サークル名と作品名が、エントリー・リストに既存なら偽</item>
    ///     </list>
    /// </summary>
    public bool IsVisibleOfNextButton
    {
        get => !this.IsExistsEntryInList();
        //get => this.isVisibleOfNextButton;
        //set
        //{
        //    if (this.isVisibleOfNextButton == value)
        //        return;

        //    this.isVisibleOfNextButton = value;
        //    OnPropertyChanged(nameof(IsVisibleOfNextButton));
        //}
    }
    #endregion

    #region 変更通知プロパティ（［次へ］ボタンの活性性）
    /// <summary>
    ///     ［次へ］ボタンの活性性
    /// </summary>
    public bool IsEnabledOfNextButton
    {
        get => this.isEnabledOfNextButton;
        set
        {
            if (this.isEnabledOfNextButton == value)
                return;

            this.isEnabledOfNextButton = value;
            OnPropertyChanged(nameof(IsEnabledOfNextButton));
        }
    }
    #endregion

    #region 変更通知プロパティ（エントリー・リスト　関連）
    /// <summary>
    ///     エントリー・リスト
    /// </summary>
    public List<ConfigurationEntry> EntryList => App.GetOrLoadConfiguration().EntryList;

    /// <summary>
    ///     選択エントリー
    /// </summary>
    public ConfigurationEntry? SelectedEntry
    {
        get => this.selectedEntry;
        set
        {
            if (this.selectedEntry == value)
                return;

            this.selectedEntry = value;
            OnPropertyChanged(nameof(SelectedEntry));
        }
    }
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

    YourCircleName yourCircleName = YourCircleName.Empty;
    YourWorkName yourWorkName = YourWorkName.Empty;

    int yourCircleNameLength;
    int yourWorkNameLength;

    // bool isVisibleOfNextButton;
    bool isVisibleOfContinueButton;
    bool isEnabledOfNextButton;

    ConfigurationEntry? selectedEntry;

    // - プライベート・メソッド

    /// <summary>
    ///     入力したサークル名と作品名は、エントリー・リストに既存か？
    /// </summary>
    /// <returns>そうだ</returns>
    bool IsExistsEntryInList() => this.EntryList.Exists((entry) => entry.YourCircleName == this.yourCircleName && entry.YourWorkName == this.yourWorkName);

}
