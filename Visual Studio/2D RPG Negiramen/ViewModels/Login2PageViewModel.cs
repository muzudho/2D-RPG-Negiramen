namespace _2D_RPG_Negiramen.ViewModels;

using _2D_RPG_Negiramen.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;
using TheFileEntryLocations = _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 ［ログイン２］ページ・ビューモデル
/// </summary>
internal class Login2PageViewModel : ObservableObject, ILogin2PageViewModel
{
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

    #region 変更通知プロパティ（ネギラーメンの 📂 `Starter Kit` フォルダの場所）
    /// <summary>
    ///     ネギラーメンの 📂 `Starter Kit` フォルダの場所
    /// </summary>
    /// <example>"C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/Starter Kit"</example>
    public string StarterKitFolderPathAsStr
    {
        get => this.starterKitFolder.Path.AsStr;
        set
        {
            if (this.starterKitFolder.Path.AsStr == value)
                return;

            // 循環参照しないようにフィールドにセットします
            this.starterKitFolder = new TheFileEntryLocations.StarterKit.ItsFolder(
                                        pathSource: FileEntryPathSource.FromString(value),
                                        convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                                    replaceSeparators: true));
            OnPropertyChanged(nameof(StarterKitFolderPathAsStr));
        }
    }
    #endregion

    #region 変更通知プロパティ（Unity の 📂 `Assets` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets` フォルダの場所
    /// </summary>
    /// <example>"C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/Assets"</example>
    public string UnityAssetsFolderPathAsStr
    {
        get => this.unityAssetsFolder.Path.AsStr;
        set
        {
            if (this.unityAssetsFolder.Path.AsStr == value)
                return;

            // 循環参照しないようにフィールドにセットします
            this.unityAssetsFolder = new TheFileEntryLocations.UnityAssets.ItsFolder(
                                        pathSource: FileEntryPathSource.FromString(value),
                                        convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                                    replaceSeparators: true));
            OnPropertyChanged(nameof(UnityAssetsFolderPathAsStr));
        }
    }
    #endregion

    // - パブリック・プロパティ

    #region プロパティ（ネギラーメンの 📂 `Starter Kit` フォルダの場所）
    /// <summary>
    ///     ネギラーメンの 📂 `Starter Kit` フォルダの場所
    /// </summary>
    /// <example>"C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/Starter Kit"</example>
    public TheFileEntryLocations.StarterKit.ItsFolder StarterKitFolder
    {
        get => this.starterKitFolder;
        set
        {
            if (this.starterKitFolder == value)
                return;

            this.starterKitFolder = value;
            OnPropertyChanged(nameof(StarterKitFolderPathAsStr));
        }
    }
    #endregion

    #region プロパティ（Unity の 📂 `Assets` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets` フォルダの場所
    /// </summary>
    /// <example>"C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/Assets"</example>
    public TheFileEntryLocations.UnityAssets.ItsFolder UnityAssetsFolder
    {
        get => this.unityAssetsFolder;
        set
        {
            if (this.unityAssetsFolder == value)
                return;

            this.unityAssetsFolder = value;
            OnPropertyChanged(nameof(UnityAssetsFolderPathAsStr));
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

    // - インターナル・プロパティ

    TheFileEntryLocations.StarterKit.ItsFolder starterKitFolder = TheFileEntryLocations.StarterKit.ItsFolder.Empty;
    TheFileEntryLocations.UnityAssets.ItsFolder unityAssetsFolder = TheFileEntryLocations.UnityAssets.ItsFolder.Empty;
}
