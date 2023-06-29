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
        /// 画像上のポインティング座標ｘ
        /// </summary>
        public int PointingXOnImageAsInt
        {
            get => _pointingXOnImage.AsInt;
            set
            {
                if (_pointingXOnImage.AsInt != value)
                {
                    _pointingXOnImage = new Models.X(value);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 画像上のポインティング座標ｙ
        /// </summary>
        public int PointingYOnImageAsInt
        {
            get => _pointingYOnImage.AsInt;
            set
            {
                if (_pointingYOnImage.AsInt != value)
                {
                    _pointingYOnImage = new Models.Y(value);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// ウィンドウ上のポインティング座標ｘ
        /// </summary>
        public int PointingXOnWindowAsInt
        {
            get => _pointingXOnWindow.AsInt;
            set
            {
                if (_pointingXOnWindow.AsInt != value)
                {
                    _pointingXOnWindow = new Models.X(value);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// ウィンドウ上のポインティング座標ｙ
        /// </summary>
        public int PointingYOnWindowAsInt
        {
            get => _pointingYOnWindow.AsInt;
            set
            {
                if (_pointingYOnWindow.AsInt != value)
                {
                    _pointingYOnWindow = new Models.Y(value);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 画像上のタップ座標ｘ
        /// </summary>
        public int TappedXOnImageAsInt
        {
            get => _tappedPointOnImage.X.AsInt;
            set
            {
                if (_tappedPointOnImage.X.AsInt != value)
                {
                    _tappedPointOnImage = new Models.Point(new Models.X(value), _tappedPointOnImage.Y);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 画像上のタップ座標ｙ
        /// </summary>
        public int TappedYOnImageAsInt
        {
            get => _tappedPointOnImage.Y.AsInt;
            set
            {
                if (_tappedPointOnImage.Y.AsInt != value)
                {
                    _tappedPointOnImage = new Models.Point(_tappedPointOnImage.X, new Models.Y(value));
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
        /// <param name="pointingXOnImage">画像上のポインティング座標ｘ</param>
        /// <param name="pointingYOnImage">画像上のポインティング座標ｙ</param>
        /// <param name="pointingXOnWindow">ウィンドウ上のポインティング座標ｘ</param>
        /// <param name="pointingYOnWindow">ウィンドウ上のポインティング座標ｙ</param>
        /// <param name="tappedPointOnImage">画像上のタップ座標</param>
        internal TilePalettePageViewModel(
            Models.X pointingXOnImage,
            Models.Y pointingYOnImage,
            Models.X pointingXOnWindow,
            Models.Y pointingYOnWindow,
            Models.Point tappedPointOnImage)
        {
            this.PointingXOnImageAsInt = pointingXOnImage.AsInt;
            this.PointingYOnImageAsInt = pointingYOnImage.AsInt;
            this.PointingXOnWindowAsInt = pointingXOnWindow.AsInt;
            this.PointingYOnWindowAsInt = pointingYOnWindow.AsInt;
            this._tappedPointOnImage = tappedPointOnImage;
        }

        // - プライベート・フィールド

        /// <summary>
        /// 画像上のポインティング座標ｘ
        /// </summary>
        private Models.X _pointingXOnImage = Models.X.Empty;

        /// <summary>
        /// 画像上のポインティング座標ｙ
        /// </summary>
        private Models.Y _pointingYOnImage = Models.Y.Empty;

        /// <summary>
        /// ウィンドウ上のポインティング座標ｘ
        /// </summary>
        private Models.X _pointingXOnWindow = Models.X.Empty;

        /// <summary>
        /// ウィンドウ上のポインティング座標ｙ
        /// </summary>
        private Models.Y _pointingYOnWindow = Models.Y.Empty;

        /// <summary>
        /// 画像上のタップ座標
        /// </summary>
        private Models.Point _tappedPointOnImage = Models.Point.Empty;
    }
}
