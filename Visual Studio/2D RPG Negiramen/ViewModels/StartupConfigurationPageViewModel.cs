namespace _2D_RPG_Negiramen.ViewModels
{
    using _2D_RPG_Negiramen.Models;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using System.Windows.Input;

    /// <summary>
    /// ［初期設定］ページ用のビューモデル
    /// </summary>
    class StartupConfigurationPageViewModel : ObservableObject
    {
        /// <summary>
        /// ネギラーメンの 📂 `Workspace` フォルダーへのパス
        /// </summary>
        private NegiramenWorkspaceFolderPath _negiramenWorkspaceFolderPath = NegiramenWorkspaceFolderPath.Empty;

        /// <summary>
        /// Unity の Assets フォルダーへのパス
        /// </summary>
        private UnityAssetsFolderPath _unityAssetsFolderPath = UnityAssetsFolderPath.Empty;

        /// <summary>
        /// あなたのサークル名
        /// </summary>
        private YourCircleName _yourCircleName = YourCircleName.Empty;

        /// <summary>
        /// あなたの作品名
        /// </summary>
        private YourWorkName _yourWorkName = YourWorkName.Empty;

        /// <summary>
        /// Unity の Assets フォルダ―へ初期設定をコピーするコマンド
        /// </summary>
        public ICommand PushStartupToUnityAssetsFolderCommand { get; }

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
        /// ネギラーメン・ワークスペース・フォルダーへのパス。文字列形式
        /// </summary>
        public string NegiramenWorkspaceFolderPathAsStr
        {
            get => _negiramenWorkspaceFolderPath.AsStr;
            set
            {
                if (_negiramenWorkspaceFolderPath.AsStr != value)
                {
                    _negiramenWorkspaceFolderPath = Models.NegiramenWorkspaceFolderPath.FromStringAndReplaceSeparators(value);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Unity の Assets フォルダーへのパス。文字列形式
        /// </summary>
        public string UnityAssetsFolderPathAsStr
        {
            get => _unityAssetsFolderPath.AsStr;
            set
            {
                if (_unityAssetsFolderPath.AsStr != value)
                {
                    _unityAssetsFolderPath = Models.UnityAssetsFolderPath.FromStringAndReplaceSeparators(value);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// あなたのサークル名
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
        /// あなたの作品名
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
        /// ［Unity の Assets フォルダ―へ初期設定をコピーする］コマンドを実行
        /// </summary>
        /// <returns></returns>
        async Task PushStartupToUnityAssetsFolder()
        {
            await Task.Run(() =>
            {
                // テキスト・ボックスから、Unity エディターの Assets フォルダーへのパスを取得
                var assetsFolderPathAsStr = this.UnityAssetsFolderPathAsStr;

                // 構成ファイルの更新差分
                var configurationDifference = new ConfigurationBuffer()
                {
                    NegiramenWorkspaceFolderPath = this._negiramenWorkspaceFolderPath,
                    UnityAssetsFolderPath = this._unityAssetsFolderPath,
                    YourCircleName = _yourCircleName,
                    YourWorkName = _yourWorkName,
                };

                // 設定ファイルの保存
                if (Configuration.SaveTOML(App.GetOrLoadConfiguration(), configurationDifference, out Configuration newConfiguration))
                {
                    // グローバル変数を更新
                    App.SetConfiguration(newConfiguration);

                    // Unity の Assets フォルダ―へ初期設定をコピー
                    if (!UnityAssetsFolder.PushStartupMemberToUnityAssetsFolder(assetsFolderPathAsStr))
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
    }
}
