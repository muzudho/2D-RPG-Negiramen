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
        ///     画像上のポインティング位置ｘ
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
        ///     画像上のポインティング位置ｙ
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
        ///     画像上のタップ位置ｘ
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
        ///     画像上のタップ位置ｙ
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
        ///     ウィンドウ上のポインティング位置ｘ
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
        ///     ウィンドウ上のポインティング位置ｙ
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
        ///     選択タイルの位置ｘ
        /// </summary>
        public int SelectedTileLeftAsInt
        {
            get => _tileCursorPoint.X.AsInt;
            set
            {
                if (_tileCursorPoint.X.AsInt != value)
                {
                    _tileCursorPoint = new Models.Point(new Models.X(value), _tileCursorPoint.Y);
                    OnPropertyChanged(nameof(SelectedTileLeftAsInt));

                    this.TileCursorPointAsMargin = new Thickness(
                        // 左
                        this.SelectedTileLeftAsInt,
                        // 上
                        this.SelectedTileTopAsInt,
                        // 右
                        0,
                        // 下
                        0);
                }
            }
        }

        /// <summary>
        ///     選択タイルの位置ｙ
        /// </summary>
        public int SelectedTileTopAsInt
        {
            get => _tileCursorPoint.Y.AsInt;
            set
            {
                if (_tileCursorPoint.Y.AsInt != value)
                {
                    _tileCursorPoint = new Models.Point(_tileCursorPoint.X, new Models.Y(value));
                    OnPropertyChanged(nameof(SelectedTileTopAsInt));

                    this.TileCursorPointAsMargin = new Thickness(
                        // 左
                        this.SelectedTileLeftAsInt,
                        // 上
                        this.SelectedTileTopAsInt,
                        // 右
                        0,
                        // 下
                        0);
                }
            }
        }

        /// <summary>
        ///     タイル・カーソルの位置（マージンとして）
        /// </summary>
        public Thickness TileCursorPointAsMargin
        {
            get => _tileCursorPointAsMargin;
            private set
            {
                if (_tileCursorPointAsMargin != value)
                {
                    _tileCursorPointAsMargin = value;
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
        ///     画像上のポインティング位置
        /// </summary>
        Models.Point _pointingPointOnImage = Models.Point.Empty;

        /// <summary>
        ///     画像上のタップ位置
        /// </summary>
        Models.Point _tappedPointOnImage = Models.Point.Empty;

        /// <summary>
        ///     ウィンドウ上のポインティング位置
        /// </summary>
        Models.Point _pointingPointOnWindow = Models.Point.Empty;

        /// <summary>
        ///     タイル・カーソル位置
        /// </summary>
        Models.Point _tileCursorPoint = Models.Point.Empty;

        /// <summary>
        ///     タイル・カーソルの位置（マージンとして）
        /// </summary>
        Thickness _tileCursorPointAsMargin = Thickness.Zero;
    }
}
