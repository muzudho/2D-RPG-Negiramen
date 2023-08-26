namespace _2D_RPG_Negiramen.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using System.Diagnostics;

    /// <summary>
    ///     😁 ［タイル・パレット・ページ］ビューモデル
    /// </summary>
    internal class TilePalettePageViewModel : ObservableObject
    {

        // - その他

        #region その他（生成）
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
        #endregion

        // - パブリック変更通知プロパティ

        #region 変更通知プロパティ（画像上のポインティング位置ｘ）
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
        #endregion

        #region 変更通知プロパティ（画像上のポインティング位置ｙ）
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
        #endregion

        #region 変更通知プロパティ（画像上のタップ位置ｘ）
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
        #endregion

        #region 変更通知プロパティ（画像上のタップ位置ｙ）
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
        #endregion

        #region 変更通知プロパティ（ウィンドウ上のポインティング位置ｘ）
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
        #endregion

        #region 変更通知プロパティ（ウィンドウ上のポインティング位置ｙ）
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
        #endregion

        #region 変更通知プロパティ（切抜きカーソル。元画像ベースの位置ｘ）
        /// <summary>
        ///     切抜きカーソル。元画像ベースの位置ｘ
        /// </summary>
        public int CroppedCursorPointedTileSourceLeftAsInt
        {
            get => sourceCroppedCursorPoint.X.AsInt;
            set
            {
                if (sourceCroppedCursorPoint.X.AsInt != value)
                {
                    sourceCroppedCursorPoint = new Models.Geometric.PointInt(new Models.Geometric.XInt(value), sourceCroppedCursorPoint.Y);
                    OnPropertyChanged(nameof(CroppedCursorPointedTileSourceLeftAsInt));

                    this.croppedCursorWorkingPointAsMargin = new Thickness(
                        // 左
                        this.CroppedCursorPointedTileSourceLeftAsInt,
                        // 上
                        this.CroppedCursorPointedTileSourceTopAsInt,
                        // 右
                        0,
                        // 下
                        0);
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（切抜きカーソル。元画像ベースの位置ｙ）
        /// <summary>
        ///     切抜きカーソル。元画像ベースの位置ｙ
        /// </summary>
        public int CroppedCursorPointedTileSourceTopAsInt
        {
            get => sourceCroppedCursorPoint.Y.AsInt;
            set
            {
                if (sourceCroppedCursorPoint.Y.AsInt != value)
                {
                    sourceCroppedCursorPoint = new Models.Geometric.PointInt(sourceCroppedCursorPoint.X, new Models.Geometric.YInt(value));
                    OnPropertyChanged(nameof(CroppedCursorPointedTileSourceTopAsInt));

                    this.croppedCursorWorkingPointAsMargin = new Thickness(
                        // 左
                        this.CroppedCursorPointedTileSourceLeftAsInt,
                        // 上
                        this.CroppedCursorPointedTileSourceTopAsInt,
                        // 右
                        0,
                        // 下
                        0);
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（矩形カーソル。ズーム済みの位置（マージンとして））
        /// <summary>
        ///     矩形カーソル。ズーム済みの位置（マージンとして）
        /// </summary>
        public Thickness CroppedCursorWorkingPointAsMargin
        {
            get => this.croppedCursorWorkingPointAsMargin;
            private set
            {
                if (this.croppedCursorWorkingPointAsMargin != value)
                {
                    this.croppedCursorWorkingPointAsMargin = value;
                    OnPropertyChanged(nameof(CroppedCursorWorkingPointAsMargin));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（グリッド全体の左上表示位置）
        Models.Geometric.PointFloat workingGridLeftTop = Models.Geometric.PointFloat.Zero;

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

        // - パブリック・イベントハンドラ

        #region イベントハンドラ（ポインター・ムーブ時）
        /// <summary>
        ///     ポインター・ムーブ時
        /// </summary>
        public void OnPointedMove(Image image, Point pointerPosition)
        {
            this.PointingXOnImageAsInt = (int)pointerPosition.X;
            this.PointingYOnImageAsInt = (int)pointerPosition.Y;

            //Trace.WriteLine($"[TilePalettePage PointerGestureRecognizer_PointerMoved] image.X = {image.X}");
            //Trace.WriteLine($"[TilePalettePage PointerGestureRecognizer_PointerMoved] image.Y = {image.Y}");

            this.PointingXOnWindowAsInt = this.PointingXOnImageAsInt + (int)image.X;
            this.PointingYOnWindowAsInt = this.PointingYOnImageAsInt + (int)image.Y;
        }
        #endregion

        #region イベントハンドラ（タップ時）
        /// <summary>
        ///     タップ時
        /// </summary>
        public void OnTapped(Point tappedPosition)
        {
            // タップした位置
            var tapped = new Models.Geometric.PointFloat(
                new Models.Geometric.XFloat((float)tappedPosition.X),
                new Models.Geometric.YFloat((float)tappedPosition.Y));
            Trace.WriteLine($"[TilePalettePage TapGestureRecognizer_Tapped] tapped x:{tapped.X.AsFloat} y:{tapped.Y.AsFloat}");

            // タイル・カーソルの位置
            var tileCursor = Models.CoordinateHelper.TranslateTappedPointToTileCursorPoint(
                tapped: tapped,
                gridLeftTop: this.WorkingGridLeftTop,
                gridTile: new Models.Geometric.SizeFloat(new Models.Geometric.WidthFloat(32), new Models.Geometric.HeightFloat(32)));

            //
            // 計算値の反映
            // ============
            //
            this.TappedXOnImageAsInt = (int)tapped.X.AsFloat;
            this.TappedYOnImageAsInt = (int)tapped.Y.AsFloat;
            this.CroppedCursorPointedTileSourceLeftAsInt = (int)tileCursor.X.AsFloat;
            this.CroppedCursorPointedTileSourceTopAsInt = (int)tileCursor.Y.AsFloat;
        }
        #endregion

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
        Thickness croppedCursorWorkingPointAsMargin = Thickness.Zero;
    }
}
