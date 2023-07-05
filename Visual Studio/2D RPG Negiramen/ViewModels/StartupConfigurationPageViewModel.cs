namespace _2D_RPG_Negiramen.ViewModels
{
    using _2D_RPG_Negiramen.Models;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using System.Windows.Input;

    /// <summary>
    ///     😁 ［初期設定］ページ用のビューモデル
    /// </summary>
    class StartupConfigurationPageViewModel : ObservableObject
    {
        // - プロパティ

        /// <summary>
        ///     Unity の Assets フォルダ―へ初期設定をコピーするコマンド
        /// </summary>
        public ICommand PushStartupToUnityAssetsFolderCommand { get; }

        // - その他

        /// <summary>
        ///     生成
        ///     
        ///     <list type="bullet">
        ///         <item>ビュー・モデルのデフォルト・コンストラクターは public 修飾にする必要がある</item>
        ///     </list>
        /// </summary>
        public StartupConfigurationPageViewModel()
        {
            // 構成ファイル取得
            var configuration = App.GetOrLoadConfiguration();

            NegiramenWorkspaceFolderPathAsStr = configuration.NegiramenWorkspaceFolderPath.AsStr;
            UnityAssetsFolderPathAsStr = configuration.UnityAssetsFolderPath.AsStr;
            YourCircleNameAsStr = configuration.YourCircleName.AsStr;
            YourWorkNameAsStr = configuration.YourWorkName.AsStr;

            // Unity の Assets フォルダ―へ初期設定をコピーするコマンド
            PushStartupToUnityAssetsFolderCommand = new AsyncRelayCommand(PushStartupToUnityAssetsFolder);
        }

        // - 変更通知プロパティ

        /// <summary>
        ///     ネギラーメン・ワークスペース・フォルダーへのパス。文字列形式
        /// </summary>
        public string NegiramenWorkspaceFolderPathAsStr
        {
            get => _negiramenWorkspaceFolderPath.AsStr;
            set
            {
                if (_negiramenWorkspaceFolderPath.AsStr != value)
                {

/* プロジェクト '2D RPG Negiramen (net7.0-android)' からのマージされていない変更
前:
                    _negiramenWorkspaceFolderPath = Models.FileEntriesLocations.Negiramen.WorkspaceFolderPath.FromString(
後:
                    _negiramenWorkspaceFolderPath = WorkspaceFolderPath.FromString(
*/
                    _negiramenWorkspaceFolderPath = Models.FileEntries.FileEntriesLocations.Negiramen.WorkspaceFolderPath.FromString(
                        value,
                        replaceSeparators: true);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        ///     Unity の Assets フォルダーへのパス。文字列形式
        /// </summary>
        /// <example>"C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/Assets"</example>
        public string UnityAssetsFolderPathAsStr
        {
            get => _unityAssetsFolderPath.AsStr;
            set
            {
                if (_unityAssetsFolderPath.AsStr != value)
                {

/* プロジェクト '2D RPG Negiramen (net7.0-android)' からのマージされていない変更
前:
                    _unityAssetsFolderPath = Models.FileEntriesLocations.UnityAssetsFolderPath.FromString(
後:
                    _unityAssetsFolderPath = UnityAssetsFolderPath.FromString(
*/
                    _unityAssetsFolderPath = Models.FileEntries.FileEntriesLocations.UnityAssetsFolderPath.FromString(
                        value,
                        replaceSeparators: true);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        ///     あなたのサークル名
        /// </summary>
        public string YourCircleNameAsStr
        {
            get => _yourCircleName.AsStr;
            set
            {
                if (_yourCircleName.AsStr != value)
                {
                    _yourCircleName = Models.YourCircleName.FromString(value);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        ///     あなたの作品名
        /// </summary>
        public string YourWorkNameAsStr
        {
            get => _yourWorkName.AsStr;
            set
            {
                if (_yourWorkName.AsStr != value)
                {
                    _yourWorkName = Models.YourWorkName.FromString(value);
                    OnPropertyChanged();
                }
            }
        }

        // - コマンド

        /// <summary>
        ///     ［Unity の Assets フォルダ―へ初期設定をコピーする］コマンドを実行
        /// </summary>
        /// <returns>なし</returns>
        async Task PushStartupToUnityAssetsFolder()
        {
            await Task.Run(() =>
            {
                // テキスト・ボックスから、Unity エディターの Assets フォルダーへのパスを取得
                var assetsFolderPathAsStr = this.UnityAssetsFolderPathAsStr;

                // 構成ファイルの更新差分
                var configurationDifference = new Models.FileEntries.ConfigurationBuffer()
                {
                    NegiramenWorkspaceFolderPath = this._negiramenWorkspaceFolderPath,
                    UnityAssetsFolderPath = this._unityAssetsFolderPath,
                    YourCircleName = _yourCircleName,
                    YourWorkName = _yourWorkName,
                };

                // 設定ファイルの保存
                if (Models.FileEntries.Configuration.SaveTOML(App.GetOrLoadConfiguration(), configurationDifference, out Models.FileEntries.Configuration newConfiguration))
                {
                    // グローバル変数を更新
                    App.SetConfiguration(newConfiguration);

                    // ネギラーメンのワークスペース・フォルダーの内容を確認

/* プロジェクト '2D RPG Negiramen (net7.0-android)' からのマージされていない変更
前:
                    var isOk = Models.FileLocations.Negiramen.WorkspaceFolder.CheckForUnityAssets();
後:
                    var isOk = WorkspaceFolder.CheckForUnityAssets();
*/
                    var isOk = Models.FileEntries.WorkspaceFolder.CheckForUnityAssets();
                    if (!isOk)
                    {
                        // TODO 異常時の処理
                        return;
                    }

                    // Unity の Assets フォルダ―へ初期設定をコピー

/* プロジェクト '2D RPG Negiramen (net7.0-android)' からのマージされていない変更
前:
                    if (!Models.FileLocations.UnityAssetsFolder.PushStartupMemberToUnityAssetsFolder(assetsFolderPathAsStr))
後:
                    if (!UnityAssetsFolder.PushStartupMemberToUnityAssetsFolder(assetsFolderPathAsStr))
*/
                    if (!Models.FileEntries.UnityAssetsFolder.PushStartupMemberToUnityAssetsFolder(assetsFolderPathAsStr))
                    {
                        // TODO 異常時の処理
                        return;
                    }
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

        // - プライベート・フィールド

        /// <summary>
        ///     ネギラーメンの 📂 `Workspace` フォルダーへのパス
        /// </summary>

/* プロジェクト '2D RPG Negiramen (net7.0-android)' からのマージされていない変更
前:
        private Models.FileEntriesLocations.Negiramen.WorkspaceFolderPath _negiramenWorkspaceFolderPath = Models.FileEntriesLocations.Negiramen.WorkspaceFolderPath.Empty;
後:
        private WorkspaceFolderPath _negiramenWorkspaceFolderPath = WorkspaceFolderPath.Empty;
*/
        private Models.FileEntries.FileEntriesLocations.Negiramen.WorkspaceFolderPath _negiramenWorkspaceFolderPath = Models.FileEntries.FileEntriesLocations.Negiramen.WorkspaceFolderPath.Empty;

        /// <summary>
        ///     Unity の Assets フォルダーへのパス
        /// </summary>

/* プロジェクト '2D RPG Negiramen (net7.0-android)' からのマージされていない変更
前:
        private Models.FileEntriesLocations.UnityAssetsFolderPath _unityAssetsFolderPath = Models.FileEntriesLocations.UnityAssetsFolderPath.Empty;
後:
        private UnityAssetsFolderPath _unityAssetsFolderPath = UnityAssetsFolderPath.Empty;
*/
        private Models.FileEntries.FileEntriesLocations.UnityAssetsFolderPath _unityAssetsFolderPath = Models.FileEntries.FileEntriesLocations.UnityAssetsFolderPath.Empty;

        /// <summary>
        ///     あなたのサークル名
        /// </summary>
        private YourCircleName _yourCircleName = YourCircleName.Empty;

        /// <summary>
        ///     あなたの作品名
        /// </summary>
        private YourWorkName _yourWorkName = YourWorkName.Empty;
    }
}
