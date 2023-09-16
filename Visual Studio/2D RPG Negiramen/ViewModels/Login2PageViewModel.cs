namespace _2D_RPG_Negiramen.ViewModels;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.FileEntries;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Input;
using TheFileEntryLocations = _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 ［ログイン２］ページ・ビューモデル
/// </summary>
internal class Login2PageViewModel : ObservableObject, ILogin2PageViewModel
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    ///     
    ///     <list type="bullet">
    ///         <item>XAMLに記述するので、パブリック修飾である必要があります</item>
    ///     </list>
    /// </summary>
    public Login2PageViewModel()
    {
        this.MakeNewProjectAndGoNextCommand = new AsyncRelayCommand(MakeNewProjectAndGoNext);
    }
    #endregion

    // - パブリック・コマンド・プロパティ

    #region コマンド（新プロジェクト作成と画面遷移のコマンド）
    /// <summary>
    ///     新プロジェクト作成と画面遷移のコマンド
    /// </summary>
    public ICommand MakeNewProjectAndGoNextCommand { get; }
    #endregion

    // - パブリック変更通知プロパティ

    #region 変更通知プロパティ（ロケール　関連）
    /// <summary>
    ///     現在選択中の文化情報。文字列形式
    /// </summary>
    public CultureInfo SelectedCultureInfo
    {
        get => LocalizationResourceManager.Instance.CultureInfo;
        set
        {
            if (LocalizationResourceManager.Instance.CultureInfo != value)
            {
                LocalizationResourceManager.Instance.SetCulture(value);
                OnPropertyChanged(nameof(SelectedCultureInfo));
            }
        }
    }

    /// <summary>
    ///     文化情報のリスト
    /// </summary>
    public ObservableCollection<CultureInfo> CultureInfoCollection => App.CultureInfoCollection;
    #endregion

    #region 変更通知プロパティ（ネギラーメンの 📂 `Starter Kit` フォルダの場所）
    /// <summary>
    ///     ネギラーメンの 📂 `Starter Kit` フォルダの場所
    /// </summary>
    /// <example>"C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/Starter Kit"</example>
    public string StarterKitFolderPathAsStr
    {
        get => this.starterKitFolderLocation.Path.AsStr;
        set
        {
            if (this.starterKitFolderLocation.Path.AsStr == value)
                return;

            // 循環参照しないようにフィールドにセットします
            this.starterKitFolderLocation = new TheFileEntryLocations.StarterKit.ItsFolder(
                                        pathSource: FileEntryPathSource.FromString(value),
                                        convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                                    replaceSeparators: true));
            OnPropertyChanged(nameof(StarterKitFolderPathAsStr));

            OnPropertyChanged(nameof(IsEnabledOfNewProjectButton));
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
        get => this.unityAssetsFolderLocation.Path.AsStr;
        set
        {
            if (this.unityAssetsFolderLocation.Path.AsStr == value)
                return;

            // 循環参照しないようにフィールドにセットします
            this.unityAssetsFolderLocation = new TheFileEntryLocations.UnityAssets.ItsFolder(
                                        pathSource: FileEntryPathSource.FromString(value),
                                        convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                                    replaceSeparators: true));
            OnPropertyChanged(nameof(UnityAssetsFolderPathAsStr));

            OnPropertyChanged(nameof(IsEnabledOfNewProjectButton));
        }
    }
    #endregion

    #region 変更通知プロパティ（［新しく作る］ボタンの活性性）
    /// <summary>
    ///     <pre>
    ///         ［新しく作る］ボタンの活性性
    ///         
    ///         以下の条件を満たしたとき活性にする
    ///     </pre>
    ///     <list type="bullet">
    ///         <item>スターターキット・フォルダへのパスが入力されており、フォルダーが実在する</item>
    ///         <item>Unity の Assets フォルダへのパスが入力されており、フォルダーが実在する</item>
    ///     </list>
    /// </summary>
    public bool IsEnabledOfNewProjectButton
    {
        get
        {
            //Trace.WriteLine($"[Login2PageViewModel IsEnabledOfNewProjectButton] StarterKit: {this.StarterKitFolderLocation.Path.AsStr}");
            //Trace.WriteLine($"[Login2PageViewModel IsEnabledOfNewProjectButton] UnityAssets: {this.UnityAssetsFolderLocation.Path.AsStr}");
            //Trace.WriteLine($"[Login2PageViewModel IsEnabledOfNewProjectButton] StarterKit: {this.StarterKitFolderLocation.IsExists()}, UnityAssets: {this.UnityAssetsFolderLocation.IsExists()}");

            return this.StarterKitFolderLocation.IsExists() && this.UnityAssetsFolderLocation.IsExists();
        }
    }
    #endregion

    // - パブリック・プロパティ

    #region プロパティ（ネギラーメンの 📂 `Starter Kit` フォルダの場所）
    /// <summary>
    ///     ネギラーメンの 📂 `Starter Kit` フォルダの場所
    /// </summary>
    /// <example>"C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/Starter Kit"</example>
    public TheFileEntryLocations.StarterKit.ItsFolder StarterKitFolderLocation
    {
        get => this.starterKitFolderLocation;
        set
        {
            if (this.starterKitFolderLocation == value)
                return;

            this.starterKitFolderLocation = value;
            OnPropertyChanged(nameof(StarterKitFolderPathAsStr));

            OnPropertyChanged(nameof(IsEnabledOfNewProjectButton));
        }
    }
    #endregion

    #region プロパティ（Unity の 📂 `Assets` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets` フォルダの場所
    /// </summary>
    /// <example>"C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/Assets"</example>
    public TheFileEntryLocations.UnityAssets.ItsFolder UnityAssetsFolderLocation
    {
        get => this.unityAssetsFolderLocation;
        set
        {
            if (this.unityAssetsFolderLocation == value)
                return;

            this.unityAssetsFolderLocation = value;
            OnPropertyChanged(nameof(UnityAssetsFolderPathAsStr));

            OnPropertyChanged(nameof(IsEnabledOfNewProjectButton));
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

    TheFileEntryLocations.StarterKit.ItsFolder starterKitFolderLocation = TheFileEntryLocations.StarterKit.ItsFolder.Empty;
    TheFileEntryLocations.UnityAssets.ItsFolder unityAssetsFolderLocation = TheFileEntryLocations.UnityAssets.ItsFolder.Empty;

    // - プライベート・メソッド

    #region メソッド（新プロジェクト作成）
    /// <summary>
    ///     新プロジェクト作成と画面遷移
    /// </summary>
    async Task MakeNewProjectAndGoNext()
    {
        // フォルダー作成
        LoginHelper.MakeFolders();

        // プロジェクト構成ファイルの上書き更新
        if(ProjectConfiguration.SaveTOML(
            current: App.GetOrLoadProjectConfiguration(),
            difference: new ProjectConfigurationDifference()
            {
                StarterKitFolderLocation = this.StarterKitFolderLocation,
                UnityAssetsFolderLocation = this.UnityAssetsFolderLocation,
            },
            out var newConfig))
        {
            App.SetProjectConfiguration(newConfig);
        }


        await Shell.Current.GoToAsync(
            state: new ShellNavigationState("//HomePage"));
        // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
    }
    #endregion
}
