namespace _2D_RPG_Negiramen.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;

    /// <summary>
    /// ［タイル・パレット・ページ］ビューモデル
    /// </summary>
    internal class TilePalettePageViewModel : ObservableObject
    {
        // - 変更通知プロパティ

        /// <summary>
        /// 画像上の座標ｘ
        /// </summary>
        public int XOnImageAsInt
        {
            get => _xOnImage.AsInt;
            set
            {
                if (_xOnImage.AsInt != value)
                {
                    _xOnImage = new Models.X(value);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 画像上の座標ｙ
        /// </summary>
        public int YOnImageAsInt
        {
            get => _yOnImage.AsInt;
            set
            {
                if (_yOnImage.AsInt != value)
                {
                    _yOnImage = new Models.Y(value);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// ウィンドウ上の座標ｘ
        /// </summary>
        public int XOnWindowAsInt
        {
            get => _xOnWindow.AsInt;
            set
            {
                if (_xOnWindow.AsInt != value)
                {
                    _xOnWindow = new Models.X(value);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// ウィンドウ上の座標ｙ
        /// </summary>
        public int YOnWindowAsInt
        {
            get => _yOnWindow.AsInt;
            set
            {
                if (_yOnWindow.AsInt != value)
                {
                    _yOnWindow = new Models.Y(value);
                    OnPropertyChanged();
                }
            }
        }

        // - その他

        /// <summary>
        ///     生成
        ///     
        ///     <list type="bullet">
        ///         <item>ビュー・モデルのデフォルト・コンストラクターは public 修飾にする必要がある</item>
        ///     </list>
        /// </summary>
        public TilePalettePageViewModel()
        {
        }

        /// <summary>
        ///     生成。初期値を指定したいときに
        /// </summary>
        /// <param name="xOnImage">画像上の座標ｘ</param>
        /// <param name="yOnImage">画像上の座標ｙ</param>
        /// <param name="xOnWindow">ウィンドウ上の座標ｘ</param>
        /// <param name="yOnWindow">ウィンドウ上の座標ｙ</param>
        internal TilePalettePageViewModel(Models.X xOnImage, Models.Y yOnImage, Models.X xOnWindow, Models.Y yOnWindow)
        {
            this.XOnImageAsInt = xOnImage.AsInt;
            this.YOnImageAsInt = yOnImage.AsInt;
            this.XOnWindowAsInt = xOnWindow.AsInt;
            this.YOnWindowAsInt = yOnWindow.AsInt;
        }

        // - プライベート・フィールド

        /// <summary>
        /// 画像上の座標ｘ
        /// </summary>
        private Models.X _xOnImage = Models.X.Empty;

        /// <summary>
        /// 画像上の座標ｙ
        /// </summary>
        private Models.Y _yOnImage = Models.Y.Empty;

        /// <summary>
        /// ウィンドウ上の座標ｘ
        /// </summary>
        private Models.X _xOnWindow = Models.X.Empty;

        /// <summary>
        /// ウィンドウ上の座標ｙ
        /// </summary>
        private Models.Y _yOnWindow = Models.Y.Empty;
    }
}
