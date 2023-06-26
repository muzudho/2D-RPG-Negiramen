namespace _2D_RPG_Negiramen.ViewModels
{
    using _2D_RPG_Negiramen.Models;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using System.Windows.Input;

    /// <summary>
    /// ［Unityへプロジェクトを上書きする］ページ用のビューモデル
    /// </summary>
    class OverwriteProjectToUnityPageViewModel : ObservableObject
    {
        /// <summary>
        /// Unity の Assets フォルダーへのパス
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
            // 構成ファイルの再読込
            if (Configuration.LoadTOML(out Configuration configuration))
            {
                App.Configuration = configuration;

                _unityAssetsFolderPath = configuration.UnityAssetsFolderPath;
            }

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

                if (!UnityAssetsFolder.PushStartupMemberToUnityAssetsFolder(assetsFolderPath))
                {
                    // TODO 異常時の処理
                    return;
                }

                // TODO Unityへプロジェクトを上書き

                // ここまでくれば成功
                // ==================

                var escapedAssetsFolderPathAsStr = assetsFolderPath.Replace("\\", "/");

                // 構成ファイルの更新差分
                var configurationDifference = new ConfigurationBuffer()
                {
                    UnityAssetsFolderPath = UnityAssetsFolderPath.FromString(escapedAssetsFolderPathAsStr)
                };

                // 設定ファイルの保存
                if(Configuration.SaveTOML(App.Configuration, configurationDifference, out Configuration newConfiguration))
                {
                    App.Configuration = newConfiguration;
                }
            });
        }
    }
}
