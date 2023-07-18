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
        ///     選択タイルの位置ｘ
        /// </summary>
        public int SourceSelectedTileLeftAsInt
        {
            get => _tileCursorPoint.X.AsInt;
            set
            {
                if (_tileCursorPoint.X.AsInt != value)
                {
                    _tileCursorPoint = new Models.Geometric.PointInt(new Models.Geometric.XInt(value), _tileCursorPoint.Y);
                    OnPropertyChanged(nameof(SourceSelectedTileLeftAsInt));

                    this.WorkingRectCursorPointAsMargin = new Thickness(
                        // 左
                        this.SourceSelectedTileLeftAsInt,
                        // 上
                        this.SourceSelectedTileTopAsInt,
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
        public int SourceSelectedTileTopAsInt
        {
            get => _tileCursorPoint.Y.AsInt;
            set
            {
                if (_tileCursorPoint.Y.AsInt != value)
                {
                    _tileCursorPoint = new Models.Geometric.PointInt(_tileCursorPoint.X, new Models.Geometric.YInt(value));
                    OnPropertyChanged(nameof(SourceSelectedTileTopAsInt));

                    this.WorkingRectCursorPointAsMargin = new Thickness(
                        // 左
                        this.SourceSelectedTileLeftAsInt,
                        // 上
                        this.SourceSelectedTileTopAsInt,
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
        public Thickness WorkingRectCursorPointAsMargin
        {
            get => workingRectCursorPointAsMargin;
            private set
            {
                if (workingRectCursorPointAsMargin != value)
                {
                    workingRectCursorPointAsMargin = value;
                    OnPropertyChanged(nameof(WorkingRectCursorPointAsMargin));
                }
            }
        }

        #region 変更通知プロパティ（グリッド全体の左上表示位置）

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
        Models.PointInt workingGridLeftTop = Models.PointInt.Empty;
後:
        PointInt workingGridLeftTop = PointInt.Empty;
*/
        Models.Geometric.PointInt workingGridLeftTop = Models.Geometric.PointInt.Empty;

        /// <summary>
        ///     グリッド全体の左上表示位置
        /// </summary>

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
        public Models.PointInt WorkingGridLeftTop
後:
        public PointInt WorkingGridLeftTop
*/
        public Models.Geometric.PointInt WorkingGridLeftTop
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
        ///     タイル・カーソル位置
        /// </summary>
        Models.Geometric.PointInt _tileCursorPoint = Models.Geometric.PointInt.Empty;

        /// <summary>
        ///     矩形カーソル。ズーム済みの位置（マージンとして）
        /// </summary>
        Thickness workingRectCursorPointAsMargin = Thickness.Zero;
    }
}
