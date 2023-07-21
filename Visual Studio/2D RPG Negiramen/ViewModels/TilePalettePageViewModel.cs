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
                    _pointingPointOnImage = new Models.Geometric.PointInt(new Models.Geometric.XInt(value), _pointingPointOnImage.Y);
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
                    _pointingPointOnImage = new Models.Geometric.PointInt(_pointingPointOnImage.X, new Models.Geometric.YInt(value));
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
                    _tappedPointOnImage = new Models.Geometric.PointInt(new Models.Geometric.XInt(value), _tappedPointOnImage.Y);
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
                    _tappedPointOnImage = new Models.Geometric.PointInt(_tappedPointOnImage.X, new Models.Geometric.YInt(value));
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
                    _pointingPointOnWindow = new Models.Geometric.PointInt(new Models.Geometric.XInt(value), _pointingPointOnWindow.Y);
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
                    _pointingPointOnWindow = new Models.Geometric.PointInt(_pointingPointOnWindow.X, new Models.Geometric.YInt(value));
                    OnPropertyChanged(nameof(PointingYOnWindowAsInt));
                }
            }
        }

        /// <summary>
        ///     切抜きカーソル。元画像ベースの位置ｘ
        /// </summary>
        public int SourceCroppedCursorLeftAsInt
        {
            get => sourceCroppedCursorPoint.X.AsInt;
            set
            {
                if (sourceCroppedCursorPoint.X.AsInt != value)
                {
                    sourceCroppedCursorPoint = new Models.Geometric.PointInt(new Models.Geometric.XInt(value), sourceCroppedCursorPoint.Y);
                    OnPropertyChanged(nameof(SourceCroppedCursorLeftAsInt));

                    this.workingCroppedCursorPointAsMargin = new Thickness(
                        // 左
                        this.SourceCroppedCursorLeftAsInt,
                        // 上
                        this.SourceCroppedCursorTopAsInt,
                        // 右
                        0,
                        // 下
                        0);
                }
            }
        }

        /// <summary>
        ///     切抜きカーソル。元画像ベースの位置ｙ
        /// </summary>
        public int SourceCroppedCursorTopAsInt
        {
            get => sourceCroppedCursorPoint.Y.AsInt;
            set
            {
                if (sourceCroppedCursorPoint.Y.AsInt != value)
                {
                    sourceCroppedCursorPoint = new Models.Geometric.PointInt(sourceCroppedCursorPoint.X, new Models.Geometric.YInt(value));
                    OnPropertyChanged(nameof(SourceCroppedCursorTopAsInt));

                    this.workingCroppedCursorPointAsMargin = new Thickness(
                        // 左
                        this.SourceCroppedCursorLeftAsInt,
                        // 上
                        this.SourceCroppedCursorTopAsInt,
                        // 右
                        0,
                        // 下
                        0);
                }
            }
        }

        /// <summary>
        ///     矩形カーソル。ズーム済みの位置（マージンとして）
        /// </summary>
        public Thickness WorkingCroppedCursorPointAsMargin
        {
            get => this.workingCroppedCursorPointAsMargin;
            private set
            {
                if (this.workingCroppedCursorPointAsMargin != value)
                {
                    this.workingCroppedCursorPointAsMargin = value;
                    OnPropertyChanged(nameof(WorkingCroppedCursorPointAsMargin));
                }
            }
        }

        #region 変更通知プロパティ（グリッド全体の左上表示位置）
        Models.Geometric.PointFloat workingGridLeftTop = Models.Geometric.PointFloat.Empty;

        /// <summary>
        ///     グリッド全体の左上表示位置
        /// </summary>
        public Models.Geometric.PointFloat WorkingGridLeftTop
        {
            get => this.workingGridLeftTop;
            set
            {
                if (this.workingGridLeftTop != value)
                {
                    this.workingGridLeftTop = value;
                    OnPropertyChanged(nameof(WorkingGridLeftTop));
                }
            }
        }
        #endregion

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
        Models.Geometric.PointInt _pointingPointOnImage = Models.Geometric.PointInt.Empty;

        /// <summary>
        ///     画像上のタップ位置
        /// </summary>
        Models.Geometric.PointInt _tappedPointOnImage = Models.Geometric.PointInt.Empty;

        /// <summary>
        ///     ウィンドウ上のポインティング位置
        /// </summary>
        Models.Geometric.PointInt _pointingPointOnWindow = Models.Geometric.PointInt.Empty;

        /// <summary>
        ///     切抜きカーソル。元画像ベース
        /// </summary>
        Models.Geometric.PointInt sourceCroppedCursorPoint = Models.Geometric.PointInt.Empty;

        /// <summary>
        ///     切抜きカーソル。ズーム済みの位置（マージンとして）
        /// </summary>
        Thickness workingCroppedCursorPointAsMargin = Thickness.Zero;
    }
}
