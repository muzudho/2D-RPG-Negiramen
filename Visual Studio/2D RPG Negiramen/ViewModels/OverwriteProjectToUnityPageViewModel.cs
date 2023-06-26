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
    /// ［Unityへプロジェクトを上書きする］ページ用のビューモデル
    /// </summary>
    class OverwriteProjectToUnityPageViewModel : ObservableObject
    {
        /// <summary>
        /// Unity の Assets フォルダーへのパス。文字列形式
        /// </summary>
        private Models.UnityAssetsFolderPath _unityAssetsFolderPath;

        /// <summary>
        /// それをするコマンド
        /// </summary>
        public ICommand DoItCommand { get; }

        /// <summary>
        /// 生成
        /// 
        /// <list type="bullet">
        ///     <item>ビュー・モデルのデフォルト・コンストラクターは public 修飾にする必要がある</item>
        /// </list>
        /// </summary>
        public OverwriteProjectToUnityPageViewModel()
        {
            //
            // TOML形式ファイルの読取
            // ======================
            //
            // 📖　[Tomlyn　＞　Documentation](https://github.com/xoofx/Tomlyn/blob/main/doc/readme.md)
            //

            string unity_assets_folder_path = string.Empty;

            try
            {
                // フォルダー名は自動的に与えられているので、これを使う
                string appDataDirAsStr = FileSystem.Current.AppDataDirectory;
                // Example: `C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalState`

                // 読取たいファイルへのパス
                var configurationFilePath = System.IO.Path.Combine(appDataDirAsStr, "configuration.toml");

                // 設定ファイルの読取
                var configurationText = System.IO.File.ReadAllText(configurationFilePath);

                // TOML
                var model = Toml.ToModel(configurationText);

                unity_assets_folder_path = (string)((TomlTable)model["paths"]!)["unity_assets_folder_path"];
            }
            catch (Exception ex)
            {
                // TODO 例外対応、何したらいい（＾～＾）？
            }

            _unityAssetsFolderPath = Models.UnityAssetsFolderPath.FromString(unity_assets_folder_path);

            DoItCommand = new AsyncRelayCommand(DoIt);
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

        // - コマンド

        /// <summary>
        /// ［それをする］コマンドを実行
        /// </summary>
        /// <returns></returns>
        async Task DoIt()
        {
            await Task.Run(() =>
            {
                // テキスト・ボックスから、Unity エディターの Assets フォルダーへのパスを取得
                var assetsFolderPath = this.UnityAssetsFolderPathAsStr;

                if (!Directory.Exists(assetsFolderPath))
                {
                    // TODO ディレクトリー・パスでなければ失敗
                    return;
                }

                // `Assets/Doujin Circle Grayscale/2D RPG Negiramen` ディレクトリーの有無をチェック
                var doujinCircleGrayscaleFolderPath = Path.Combine(assetsFolderPath, "Doujin Circle Grayscale");

                if (!Directory.Exists(doujinCircleGrayscaleFolderPath))
                {
                    // 無ければ作成
                    Directory.CreateDirectory(doujinCircleGrayscaleFolderPath);
                }

                UnityAssetsFolder.PushStartupMemberToUnityAssetsFolder(doujinCircleGrayscaleFolderPath);

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
