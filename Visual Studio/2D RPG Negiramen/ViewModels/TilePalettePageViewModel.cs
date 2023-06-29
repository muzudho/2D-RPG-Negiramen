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
            get => _pointingPointOnImage.X.AsInt;
            set
            {
                if (_pointingPointOnImage.X.AsInt != value)
                {
                    _pointingPointOnImage = new Models.Point(new Models.X(value), _pointingPointOnImage.Y);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 画像上のポインティング座標ｙ
        /// </summary>
        public int PointingYOnImageAsInt
        {
            get => _pointingPointOnImage.Y.AsInt;
            set
            {
                if (_pointingPointOnImage.Y.AsInt != value)
                {
                    _pointingPointOnImage = new Models.Point(_pointingPointOnImage.X, new Models.Y(value));
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// ウィンドウ上のポインティング座標ｘ
        /// </summary>
        public int PointingXOnWindowAsInt
        {
            get => _pointingPointOnWindow.X.AsInt;
            set
            {
                if (_pointingPointOnWindow.X.AsInt != value)
                {
                    _pointingPointOnWindow = new Models.Point(new Models.X(value), _pointingPointOnWindow.Y);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// ウィンドウ上のポインティング座標ｙ
        /// </summary>
        public int PointingYOnWindowAsInt
        {
            get => _pointingPointOnWindow.Y.AsInt;
            set
            {
                if (_pointingPointOnWindow.Y.AsInt != value)
                {
                    _pointingPointOnWindow = new Models.Point(_pointingPointOnWindow.X, new Models.Y(value));
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
        /// <param name="pointingPointOnImage">画像上のポインティング座標</param>
        /// <param name="pointingPointOnWindow">ウィンドウ上のポインティング座標</param>
        /// <param name="tappedPointOnImage">画像上のタップ座標</param>
        internal TilePalettePageViewModel(
            Models.Point pointingPointOnImage,
            Models.Point pointingPointOnWindow,
            Models.Point tappedPointOnImage)
        {
            this._pointingPointOnImage = pointingPointOnImage;
            this._pointingPointOnWindow = pointingPointOnWindow;
            this._tappedPointOnImage = tappedPointOnImage;
        }

        // - プライベート・フィールド

        /// <summary>
        /// 画像上のポインティング座標
        /// </summary>
        Models.Point _pointingPointOnImage = Models.Point.Empty;

        /// <summary>
        /// ウィンドウ上のポインティング座標ｘ
        /// </summary>
        Models.Point _pointingPointOnWindow = Models.Point.Empty;

        /// <summary>
        /// 画像上のタップ座標
        /// </summary>
        Models.Point _tappedPointOnImage = Models.Point.Empty;
    }
}
