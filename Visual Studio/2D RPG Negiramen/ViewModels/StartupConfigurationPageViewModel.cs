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
        /// Unity の Assets フォルダーへのパス
        /// </summary>
        private Models.UnityAssetsFolderPath _unityAssetsFolderPath;

        /// <summary>
        /// あなたのサークル名
        /// </summary>
        private Models.YourCircleName _yourCircleName;

        /// <summary>
        /// あなたの作品名
        /// </summary>
        private Models.YourWorkName _yourWorkName;

        /// <summary>
        /// Unity の Assets フォルダ―へ初期設定をコピーするコマンド
        /// </summary>
        public ICommand PushStartupToUnityAssetsFolderCommand { get; }

        /// <summary>
        /// 生成
        /// 
        /// <list type="bullet">
        ///     <item>ビュー・モデルのデフォルト・コンストラクターは public 修飾にする必要がある</item>
        /// </list>
        /// </summary>
        public StartupConfigurationPageViewModel()
        {
            try
            {
                // 構成ファイル
                var configuration = Configuration.LoadTOML();

                UnityAssetsFolderPathAsStr = configuration.UnityAssetsFolderPath.AsStr;
                YourCircleNameAsStr = configuration.YourCircleName.AsStr;
                YourWorkNameAsStr = configuration.YourWorkName.AsStr;
            }
            catch (Exception ex)
            {
                // TODO 例外対応、何したらいい（＾～＾）？
            }

            // Unity の Assets フォルダ―へ初期設定をコピーするコマンド
            PushStartupToUnityAssetsFolderCommand = new AsyncRelayCommand(PushStartupToUnityAssetsFolder);
        }

        // - 変更通知プロパティ

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
                    _unityAssetsFolderPath = Models.UnityAssetsFolderPath.FromString(value);
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
                var assetsFolderPath = this.UnityAssetsFolderPathAsStr;

                if (!UnityAssetsFolder.PushStartupMemberToUnityAssetsFolder(assetsFolderPath))
                {
                    // TODO 異常時の処理
                    return;
                }

                // TODO Unityへプロジェクトを上書き

                // ここまでくれば成功
                // ==================


                var escapedAssetsFolderPathAsStr = assetsFolderPath.Replace("\\", "/");

                // 現在の構成ファイル
                var configuration = Configuration.LoadTOML();

                // 構成ファイルの更新差分
                var configurationDifference = new ConfigurationDifference()
                {
                    UnityAssetsFolderPath = UnityAssetsFolderPath.FromString(escapedAssetsFolderPathAsStr),
                    YourCircleName = _yourCircleName,
                    YourWorkName = _yourWorkName,
                };

                // 設定ファイルの保存
                Configuration.SaveTOML(configuration, configurationDifference);
            });
        }
    }
}
