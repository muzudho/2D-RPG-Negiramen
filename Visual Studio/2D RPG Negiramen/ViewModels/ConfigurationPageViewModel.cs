﻿namespace _2D_RPG_Negiramen.ViewModels
{
    using _2D_RPG_Negiramen.Models;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Windows.Input;
    using TheFileEntryLocations = Models.FileEntries.Locations;
    using TheLocationOfUnityAssets = _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets;

    /// <summary>
    ///     😁 ［構成］ページ用のビューモデル
    /// </summary>
    class ConfigurationPageViewModel
        : ObservableObject, IConfigurationPageViewModel
    {
        // - その他

        #region その他（生成）
        /// <summary>
        ///     生成
        ///     
        ///     <list type="bullet">
        ///         <item>ビュー・モデルのデフォルト・コンストラクターは public 修飾にする必要がある</item>
        ///     </list>
        /// </summary>
        public ConfigurationPageViewModel()
        {
            // TODO ★ 構成ファイルを読込済みか、または、そうではないか？

            // 構成ファイル取得
            var configuration = App.GetOrLoadConfiguration();

            NegiramenStarterKitFolderPathAsStr = configuration.StarterKitFolder.Path.AsStr;

            this.UnityAssetsFolder = configuration.UnityAssetsFolder;
            UnityAssetsFolderPathAsStr = this.UnityAssetsFolder.Path.AsStr;

            YourCircleFolderNameAsStr = configuration.RememberYourCircleFolderName.AsStr;
            YourWorkFolderNameAsStr = configuration.RememberYourWorkFolderName.AsStr;

            // Unity の Assets フォルダ―へ初期設定をコピーするコマンド
            PushStartupToUnityAssetsFolderCommand = new AsyncRelayCommand(PushStartupToUnityAssetsFolder);
        }
        #endregion

        // - パブリック・プロパティ

        #region プロパティ（Unity の Assets フォルダ―へ初期設定をコピーするコマンド）
        /// <summary>
        ///     Unity の Assets フォルダ―へ初期設定をコピーするコマンド
        /// </summary>
        public ICommand PushStartupToUnityAssetsFolderCommand { get; }
        #endregion

        // - パブリック変更通知プロパティ

        #region 変更通知プロパティ（ロケール　関連）
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

        /// <summary>
        ///     ロケールＩｄのリスト
        /// </summary>
        public ObservableCollection<string> LocaleIdCollection => App.LocaleIdCollection;
        #endregion

        #region 変更通知プロパティ（ネギラーメン 📂 `Starter Kit` フォルダ）
        /// <summary>
        ///     ネギラーメン 📂 `Starter Kit` フォルダへのパス。文字列形式
        /// </summary>
        public string NegiramenStarterKitFolderPathAsStr
        {
            get => negiramenStarterKitFolder.Path.AsStr;
            set
            {
                if (negiramenStarterKitFolder.Path.AsStr != value)
                {
                    negiramenStarterKitFolder = new TheFileEntryLocations.StarterKit.ItsFolder(
                        pathSource: FileEntryPathSource.FromString(value),
                        convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                    replaceSeparators: true));
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（Unity の Assets フォルダへのパス。文字列形式）
        /// <summary>
        ///     Unity の 📂 `Assets` フォルダへのパス。文字列形式
        /// </summary>
        /// <example>"C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/Assets"</example>
        public string UnityAssetsFolderPathAsStr
        {
            get => _unityAssetsFolder.Path.AsStr;
            set
            {
                if (_unityAssetsFolder.Path.AsStr == value)
                    return;

                _unityAssetsFolder = new TheLocationOfUnityAssets.ItsFolder(
                    pathSource: FileEntryPathSource.FromString(value),
                    convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                replaceSeparators: true));
                OnPropertyChanged();
            }
        }
        #endregion

        #region 変更通知プロパティ（あなたのサークル・フォルダ名）
        /// <summary>
        ///     あなたのサークル・フォルダ名
        /// </summary>
        public string YourCircleFolderNameAsStr
        {
            get => _yourCircleFolderName.AsStr;
            set
            {
                if (_yourCircleFolderName.AsStr == value)
                    return;

                _yourCircleFolderName = Models.YourCircleFolderName.FromString(value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region 変更通知プロパティ（あなたの作品フォルダ名）
        /// <summary>
        ///     あなたの作品フォルダ名
        /// </summary>
        public string YourWorkFolderNameAsStr
        {
            get => _yourWorkFolderName.AsStr;
            set
            {
                if (_yourWorkFolderName.AsStr == value)
                    return;

                _yourWorkFolderName = Models.YourWorkFolderName.FromString(value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region 変更通知プロパティ（キャッシュ・ディレクトリー）
        /// <summary>
        ///     キャッシュ・ディレクトリー
        /// </summary>
        public string CacheDirectoryAsStr => App.CacheFolder.Path.AsStr;
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

        #region プロパティ（Unity の 📂 `Assets` フォルダの場所）
        /// <summary>
        ///     Unity の 📂 `Assets` フォルダの場所
        /// </summary>
        internal TheLocationOfUnityAssets.ItsFolder UnityAssetsFolder { get; private set; }
        #endregion

        // - プライベート・フィールド

        #region フィールド（ネギラーメンの 📂 `Starter Kit` フォルダへのパス）
        /// <summary>
        ///     ネギラーメンの 📂 `Starter Kit` フォルダへのパス
        /// </summary>
        TheFileEntryLocations.StarterKit.ItsFolder negiramenStarterKitFolder = TheFileEntryLocations.StarterKit.ItsFolder.Empty;
        #endregion

        #region フィールド（Unity の Assets フォルダへのパス）
        /// <summary>
        ///     Unity の Assets フォルダへのパス
        /// </summary>
        TheLocationOfUnityAssets.ItsFolder _unityAssetsFolder = TheLocationOfUnityAssets.ItsFolder.Empty;
        #endregion

        #region フィールド（あなたのサークル・フォルダ名）
        /// <summary>
        ///     あなたのサークル・フォルダ名
        /// </summary>
        YourCircleFolderName _yourCircleFolderName = YourCircleFolderName.Empty;
        #endregion

        #region フィールド（あなたの作品フォルダ名）
        /// <summary>
        ///     あなたの作品フォルダ名
        /// </summary>
        YourWorkFolderName _yourWorkFolderName = YourWorkFolderName.Empty;
        #endregion

        // - プライベート・メソッド

        #region メソッド（［Unity の Assets フォルダ―へ初期設定をコピーする］コマンドを実行）
        /// <summary>
        ///     ［Unity の Assets フォルダ―へ初期設定をコピーする］コマンドを実行
        /// </summary>
        /// <returns>なし</returns>
        async Task PushStartupToUnityAssetsFolder()
        {
            await Task.Run(() =>
            {
                // 構成ファイルの更新差分
                var configurationDifference = new Models.FileEntries.ConfigurationBuffer()
                {
                    StarterKitFolder = this.negiramenStarterKitFolder,
                    UnityAssetsFolder = this._unityAssetsFolder,
                    RememberYourCircleFolderName = _yourCircleFolderName,
                    RememberYourWorkFolderName = _yourWorkFolderName,
                };

                // 構成ファイルの保存
                if (Models.FileEntries.Configuration.SaveTOML(App.GetOrLoadConfiguration(), configurationDifference, out Models.FileEntries.Configuration newConfiguration))
                {
                    // グローバル変数を更新
                    App.SetConfiguration(newConfiguration);

                    // ネギラーメンのスターターキット・フォルダの内容を確認
                    var isOk = Models.FileEntries.Deployments.StarterKitDeployment.CheckForUnityAssets();
                    if (!isOk)
                    {
                        // TODO 異常時の処理
                        return;
                    }

                    // アプリケーション・フォルダへ初期設定をコピー
                    if (!Models.FileEntries.Deployments.ApplicationProjectDeployment.MakeFolder())
                    {
                        // TODO 異常時の処理
                        return;
                    }

                    // Unity の Assets フォルダへ初期設定をコピー
                    if (!Models.FileEntries.Deployments.UnityAssetsDeployment.MakeFolder(this.UnityAssetsFolder))
                    {
                        // TODO 異常時の処理
                        return;
                    }
                }
                else
                {
                    // TODO 異常時の処理
                }
            });

            // 画面遷移、戻る
            await Shell.Current.GoToAsync("..");

            // 履歴は戻しておく
            var shellNavigationState = App.NextPage.Pop();

            // 全ての入力が準備できているなら、画面遷移する
            var newConfiguration = App.GetOrLoadConfiguration();
            if (newConfiguration.IsReady())
            {
                await Shell.Current.GoToAsync(shellNavigationState);
            }
        }
        #endregion
    }
}
