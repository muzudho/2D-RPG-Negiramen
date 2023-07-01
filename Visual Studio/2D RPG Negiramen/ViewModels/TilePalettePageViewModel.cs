namespace _2D_RPG_Negiramen.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;

    /// <summary>
    ///     😁 ［タイル・パレット・ページ］ビューモデル
    /// </summary>
    internal class TilePalettePageViewModel : ObservableObject
    {
        // - 変更通知プロパティ

        /// <summary>
        ///     画像上のポインティング座標ｘ
        /// </summary>
        public int PointingXOnImageAsInt
        {
            get => _pointingPointOnImage.X.AsInt;
            set
            {
                if (_pointingPointOnImage.X.AsInt != value)
                {
                    _pointingPointOnImage = new Models.Point(new Models.X(value), _pointingPointOnImage.Y);
                    OnPropertyChanged(nameof(PointingXOnImageAsInt));
                }
            }
        }

        /// <summary>
        ///     画像上のポインティング座標ｙ
        /// </summary>
        public int PointingYOnImageAsInt
        {
            get => _pointingPointOnImage.Y.AsInt;
            set
            {
                if (_pointingPointOnImage.Y.AsInt != value)
                {
                    _pointingPointOnImage = new Models.Point(_pointingPointOnImage.X, new Models.Y(value));
                    OnPropertyChanged(nameof(PointingYOnImageAsInt));
                }
            }
        }

        /// <summary>
        ///     画像上のタップ座標ｘ
        /// </summary>
        public int TappedXOnImageAsInt
        {
            get => _tappedPointOnImage.X.AsInt;
            set
            {
                if (_tappedPointOnImage.X.AsInt != value)
                {
                    _tappedPointOnImage = new Models.Point(new Models.X(value), _tappedPointOnImage.Y);
                    OnPropertyChanged(nameof(TappedXOnImageAsInt));
                }
            }
        }

        /// <summary>
        ///     画像上のタップ座標ｙ
        /// </summary>
        public int TappedYOnImageAsInt
        {
            get => _tappedPointOnImage.Y.AsInt;
            set
            {
                if (_tappedPointOnImage.Y.AsInt != value)
                {
                    _tappedPointOnImage = new Models.Point(_tappedPointOnImage.X, new Models.Y(value));
                    OnPropertyChanged(nameof(TappedYOnImageAsInt));
                }
            }
        }

        /// <summary>
        ///     ウィンドウ上のポインティング座標ｘ
        /// </summary>
        public int PointingXOnWindowAsInt
        {
            get => _pointingPointOnWindow.X.AsInt;
            set
            {
                if (_pointingPointOnWindow.X.AsInt != value)
                {
                    _pointingPointOnWindow = new Models.Point(new Models.X(value), _pointingPointOnWindow.Y);
                    OnPropertyChanged(nameof(PointingXOnWindowAsInt));
                }
            }
        }

        /// <summary>
        ///     ウィンドウ上のポインティング座標ｙ
        /// </summary>
        public int PointingYOnWindowAsInt
        {
            get => _pointingPointOnWindow.Y.AsInt;
            set
            {
                if (_pointingPointOnWindow.Y.AsInt != value)
                {
                    _pointingPointOnWindow = new Models.Point(_pointingPointOnWindow.X, new Models.Y(value));
                    OnPropertyChanged(nameof(PointingYOnWindowAsInt));
                }
            }
        }

        /// <summary>
        ///     ウィンドウ上のタイル・カーソル座標ｘ
        /// </summary>
        public int TileCursorXOnWindowAsInt
        {
            get => _tileCursorPointOnWindow.X.AsInt;
            set
            {
                if (_tileCursorPointOnWindow.X.AsInt != value)
                {
                    _tileCursorPointOnWindow = new Models.Point(new Models.X(value), _tileCursorPointOnWindow.Y);
                    OnPropertyChanged(nameof(TileCursorXOnWindowAsInt));

                    this.TileCursorPointAsMargin = new Thickness(
                        // 左
                        this.TileCursorXOnWindowAsInt,
                        // 上
                        this.TileCursorYOnWindowAsInt,
                        // 右
                        0,
                        // 下
                        0);
                }
            }
        }

        /// <summary>
        ///     ウィンドウ上のタイル・カーソル座標ｙ
        /// </summary>
        public int TileCursorYOnWindowAsInt
        {
            get => _tileCursorPointOnWindow.Y.AsInt;
            set
            {
                if (_tileCursorPointOnWindow.Y.AsInt != value)
                {
                    _tileCursorPointOnWindow = new Models.Point(_tileCursorPointOnWindow.X, new Models.Y(value));
                    OnPropertyChanged(nameof(TileCursorYOnWindowAsInt));

                    this.TileCursorPointAsMargin = new Thickness(
                        // 左
                        this.TileCursorXOnWindowAsInt,
                        // 上
                        this.TileCursorYOnWindowAsInt,
                        // 右
                        0,
                        // 下
                        0);
                }
            }
        }

        /// <summary>
        ///     ウィンドウ上のタイル・カーソル座標
        /// </summary>
        public Thickness TileCursorPointAsMargin
        {
            get => _tileCursorThickness;
            set
            {
                if (_tileCursorThickness != value)
                {
                    _tileCursorThickness = value;
                    OnPropertyChanged(nameof(TileCursorPointAsMargin));
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

        // - プライベート・フィールド

        /// <summary>
        ///     画像上のポインティング座標
        /// </summary>
        Models.Point _pointingPointOnImage = Models.Point.Empty;

        /// <summary>
        ///     画像上のタップ座標
        /// </summary>
        Models.Point _tappedPointOnImage = Models.Point.Empty;

        /// <summary>
        ///     ウィンドウ上のポインティング座標
        /// </summary>
        Models.Point _pointingPointOnWindow = Models.Point.Empty;

        /// <summary>
        ///     ウィンドウ上のタイル・カーソル座標
        /// </summary>
        Models.Point _tileCursorPointOnWindow = Models.Point.Empty;

        /// <summary>
        ///     ウィンドウ上のタイル・カーソルのマージン
        /// </summary>
        Thickness _tileCursorThickness = Thickness.Zero;
    }
}
