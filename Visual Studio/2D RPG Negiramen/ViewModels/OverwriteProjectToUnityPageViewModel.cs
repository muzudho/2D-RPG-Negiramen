namespace _2D_RPG_Negiramen.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;

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
        /// 生成
        /// 
        /// <list type="bullet">
        ///     <item>ビュー・モデルのデフォルト・コンストラクターは public 修飾にする必要がある</item>
        /// </list>
        /// </summary>
        public OverwriteProjectToUnityPageViewModel()
        {
            _unityAssetsFolderPath = Models.UnityAssetsFolderPath.FromString(string.Empty);
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
    }
}
