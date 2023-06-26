namespace _2D_RPG_Negiramen.ViewModels
{
    using _2D_RPG_Negiramen.Models;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Microsoft.Maui.Storage;
    using System.Windows.Input;
    using Tomlyn;
    using Tomlyn.Model;

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
                var configuration = Configuration.LoadToml();

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

                //
                // マルチプラットフォームの MAUI では、
                // パソコンだけではなく、スマホなどのサンドボックス環境などでの使用も想定されている
                // 
                // そのため、設定の保存／読込の操作は最小限のものしかない
                //
                // 📖　[Where to save .Net MAUI user settings](https://stackoverflow.com/questions/70599331/where-to-save-net-maui-user-settings)
                //
                // // getter
                // var value = Preferences.Get("nameOfSetting", "defaultValueForSetting");
                //
                // // setter
                // Preferences.Set("nameOfSetting", value);
                //
                //
                // しかし、2D RPG は　Windows PC で開発すると想定する。
                // そこで、 MAUI の範疇を外れ、Windows 固有のファイル・システムの API を使用することにする
                //
                // 📖　[File system helpers](https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/storage/file-system-helpers?tabs=windows)
                //

                // フォルダー名は自動的に与えられているので、これを使う
                string appDataDirAsStr = FileSystem.Current.AppDataDirectory;
                // Example: `C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalState`

                // 保存したいファイルへのパス
                var configurationFilePath = System.IO.Path.Combine(appDataDirAsStr, "configuration.toml");

                var escapedAssetsFolderPath = assetsFolderPath.Replace("\\", "/");

                // 設定ファイルの保存
                System.IO.File.WriteAllText(configurationFilePath, $@"[paths]
unity_assets_folder_path = ""{escapedAssetsFolderPath}""");

            });
        }
    }
}
