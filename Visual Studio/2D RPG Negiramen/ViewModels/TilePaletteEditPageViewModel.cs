namespace _2D_RPG_Negiramen.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;

    /// <summary>
    ///     😁 ［タイル・パレット編集ページ］ビューモデル
    /// </summary>
    [QueryProperty(nameof(TileSetImageFilePathAsStr), queryId: "TileSetImageFilePathAsStr")]
    [QueryProperty(nameof(ImageSize), queryId: "ImageSize")]
    [QueryProperty(nameof(InternalGridImageSize), queryId: "InternalGridImageSize")]
    [QueryProperty(nameof(GridLeftTop), queryId: "GridLeftTop")]
    [QueryProperty(nameof(GridTileSize), queryId: "GridTileSize")]
    class TilePaletteEditPageViewModel : ObservableObject
    {
        // - 変更通知プロパティ

        #region 変更通知プロパティ（画像サイズ）
        /// <summary>
        ///     画像のサイズ
        /// </summary>
        public Models.Size ImageSize
        {
            get => _imageSize;
            set
            {
                if (_imageSize != value)
                {
                    // 差分判定
                    var dirtyWidth = _imageSize.Width != value.Width;
                    var dirtyHeight = _imageSize.Height != value.Height;

                    // 更新
                    _imageSize = value;

                    // 変更通知
                    if (dirtyWidth)
                    {
                        OnPropertyChanged(nameof(ImageWidthAsInt));
                    }

                    if (dirtyHeight)
                    {
                        OnPropertyChanged(nameof(ImageHeightAsInt));
                    }
                }
            }
        }

        /// <summary>
        ///     画像の横幅
        /// </summary>
        public int ImageWidthAsInt
        {
            get => _imageSize.Width.AsInt;
            set
            {
                if (_imageSize.Width.AsInt != value)
                {
                    _imageSize = new Models.Size(new Models.Width(value), _imageSize.Height);
                    OnPropertyChanged(nameof(ImageWidthAsInt));
                }
            }
        }

        /// <summary>
        ///     画像の縦幅
        /// </summary>
        public int ImageHeightAsInt
        {
            get => _imageSize.Height.AsInt;
            set
            {
                if (_imageSize.Height.AsInt != value)
                {
                    _imageSize = new Models.Size(_imageSize.Width, new Models.Height(value));
                    OnPropertyChanged(nameof(ImageHeightAsInt));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（内部的グリッド画像のサイズ）
        /// <summary>
        ///     <pre>
        ///         内部的グリッド画像のサイズ
        ///         
        ///         グリッドの線の太さを 2px と想定しているので、グリッドの線が画像の端っこで切れないように、内部的グリッド画像のサイズを 2px 広げる
        ///     </pre>
        /// </summary>
        public Models.Size InternalGridImageSize
        {
            get => _internalGridImageSize;
            set
            {
                if (_internalGridImageSize != value)
                {
                    // 差分判定
                    var dirtyWidth = _internalGridImageSize.Width != value.Width;
                    var dirtyHeight = _internalGridImageSize.Height != value.Height;

                    // 更新
                    _internalGridImageSize = value;

                    // 変更通知
                    if (dirtyWidth)
                    {
                        OnPropertyChanged(nameof(InternalGridImageWidthAsInt));
                    }

                    if (dirtyHeight)
                    {
                        OnPropertyChanged(nameof(InternalGridImageHeightAsInt));
                    }
                }
            }
        }

        /// <summary>
        ///     <pre>
        ///         画像の横幅
        ///         
        ///         グリッドの線の太さを 2px と想定しているので、グリッドの線が画像の端っこで切れないように、内部的グリッド画像のサイズを 2px 広げる
        ///     </pre>
        /// </summary>
        public int InternalGridImageWidthAsInt
        {
            get => _internalGridImageSize.Width.AsInt;
            set
            {
                if (_internalGridImageSize.Width.AsInt != value)
                {
                    _internalGridImageSize = new Models.Size(new Models.Width(value), _internalGridImageSize.Height);
                    OnPropertyChanged(nameof(InternalGridImageWidthAsInt));
                }
            }
        }

        /// <summary>
        ///     <pre>
        ///         画像の縦幅
        ///         
        ///         グリッドの線の太さを 2px と想定しているので、グリッドの線が画像の端っこで切れないように、内部的グリッド画像のサイズを 2px 広げる
        ///     </pre>
        /// </summary>
        public int InternalGridImageHeightAsInt
        {
            get => _internalGridImageSize.Height.AsInt;
            set
            {
                if (_internalGridImageSize.Height.AsInt != value)
                {
                    _internalGridImageSize = new Models.Size(_internalGridImageSize.Width, new Models.Height(value));
                    OnPropertyChanged(nameof(InternalGridImageHeightAsInt));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（グリッド全体の左上表示位置）
        /// <summary>
        ///     グリッド全体の左上表示位置
        /// </summary>
        public Models.Point GridLeftTop
        {
            get => App.WorkingGridLeftTop;
            set
            {
                if (App.WorkingGridLeftTop != value)
                {
                    // 差分判定
                    var dirtyX = App.WorkingGridLeftTop.X != value.X;
                    var dirtyY = App.WorkingGridLeftTop.Y != value.Y;

                    // 更新
                    App.WorkingGridLeftTop = value;

                    // 変更通知
                    if (dirtyX)
                    {
                        OnPropertyChanged(nameof(GridLeftAsInt));
                    }

                    if (dirtyY)
                    {
                        OnPropertyChanged(nameof(GridTopAsInt));
                    }
                }
            }
        }

        /// <summary>
        ///     グリッド全体の左上表示位置ｘ
        /// </summary>
        public int GridLeftAsInt
        {
            get => App.WorkingGridLeftTop.X.AsInt;
            set
            {
                if (App.WorkingGridLeftTop.X.AsInt != value)
                {
                    App.WorkingGridLeftTop = new Models.Point(new Models.X(value), App.WorkingGridLeftTop.Y);
                    OnPropertyChanged(nameof(GridLeftAsInt));

                    // グリッドを再描画
                    RefreshGraphicsViewOfGrid();
                }
            }
        }

        /// <summary>
        ///     グリッド全体の左上表示位置ｙ
        /// </summary>
        public int GridTopAsInt
        {
            get => App.WorkingGridLeftTop.Y.AsInt;
            set
            {
                if (App.WorkingGridLeftTop.Y.AsInt != value)
                {
                    App.WorkingGridLeftTop = new Models.Point(App.WorkingGridLeftTop.X, new Models.Y(value));
                    OnPropertyChanged(nameof(GridLeftAsInt));

                    // グリッドを再描画
                    RefreshGraphicsViewOfGrid();
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（グリッド・タイルのサイズ）
        /// <summary>
        ///     グリッド・タイルのサイズ
        /// </summary>
        public Models.Size GridTileSize
        {
            get => App.WorkingGridTileSize;
            set
            {
                if (App.WorkingGridTileSize != value)
                {
                    // 差分判定
                    var dirtyWidth = App.WorkingGridTileSize.Width != value.Width;
                    var dirtyHeight = App.WorkingGridTileSize.Height != value.Height;

                    // 更新
                    App.WorkingGridTileSize = value;

                    // 変更通知
                    if (dirtyWidth)
                    {
                        OnPropertyChanged(nameof(GridTileWidthAsInt));
                    }

                    if (dirtyHeight)
                    {
                        OnPropertyChanged(nameof(GridTileHeightAsInt));
                    }
                }
            }
        }

        /// <summary>
        ///     グリッド・タイルの横幅
        /// </summary>
        public int GridTileWidthAsInt
        {
            get => App.WorkingGridTileSize.Width.AsInt;
            set
            {
                if (App.WorkingGridTileSize.Width.AsInt != value)
                {
                    App.WorkingGridTileSize = new Models.Size(new Models.Width(value), App.WorkingGridTileSize.Height);
                    OnPropertyChanged(nameof(GridTileWidthAsInt));

                    // グリッドを再描画
                    RefreshGraphicsViewOfGrid();
                }
            }
        }

        /// <summary>
        ///     グリッド・タイルの縦幅
        /// </summary>
        public int GridTileHeightAsInt
        {
            get => App.WorkingGridTileSize.Height.AsInt;
            set
            {
                if (App.WorkingGridTileSize.Height.AsInt != value)
                {
                    App.WorkingGridTileSize = new Models.Size(App.WorkingGridTileSize.Width, new Models.Height(value));
                    OnPropertyChanged(nameof(GridTileHeightAsInt));

                    // グリッドを再描画
                    RefreshGraphicsViewOfGrid();
                }
            }
        }
        #endregion

        #region タイルの矩形
        /// <summary>
        ///     タイルの位置ｘ
        /// </summary>
        public int TileLeftAsInt
        {
            get => _tileRect.Point.X.AsInt;
            set
            {
                if (_tileRect.Point.X.AsInt != value)
                {
                    _tileRect = new Models.Rectangle(new Models.Point(new Models.X(value), _tileRect.Point.Y), _tileRect.Size);
                    OnPropertyChanged(nameof(TileLeftAsInt));
                }
            }
        }

        /// <summary>
        ///     タイルの位置ｙ
        /// </summary>
        public int TileTopAsInt
        {
            get => _tileRect.Point.Y.AsInt;
            set
            {
                if (_tileRect.Point.Y.AsInt != value)
                {
                    _tileRect = new Models.Rectangle(new Models.Point(_tileRect.Point.X, new Models.Y(value)), _tileRect.Size);
                    OnPropertyChanged(nameof(TileTopAsInt));
                }
            }
        }

        /// <summary>
        ///     タイルの横幅
        /// </summary>
        public int TileWidthAsInt
        {
            get => _tileRect.Size.Width.AsInt;
            set
            {
                if (_tileRect.Size.Width.AsInt != value)
                {
                    _tileRect = new Models.Rectangle(_tileRect.Point, new Models.Size(new Models.Width(value), _tileRect.Size.Height));
                    OnPropertyChanged(nameof(TileWidthAsInt));
                }
            }
        }

        /// <summary>
        ///     タイルの縦幅
        /// </summary>
        public int TileHeightAsInt
        {
            get => _tileRect.Size.Height.AsInt;
            set
            {
                if (_tileRect.Size.Height.AsInt != value)
                {
                    _tileRect = new Models.Rectangle(_tileRect.Point, new Models.Size(_tileRect.Size.Width, new Models.Height(value)));
                    OnPropertyChanged(nameof(TileHeightAsInt));
                }
            }
        }
        #endregion

        #region タイルのコメント
        /// <summary>
        ///     コメント
        /// </summary>
        public string CommentAsStr
        {
            get => _comment.AsStr;
            set
            {
                if (_comment.AsStr != value)
                {
                    _comment = new Models.Comment(value);
                    OnPropertyChanged(nameof(CommentAsStr));
                }
            }
        }

        /// <summary>
        ///     コメント
        /// </summary>
        public string TileSetImageFilePathAsStr
        {
            get => _tileSetImageFilePath.AsStr;
            set
            {
                if (_tileSetImageFilePath.AsStr != value)
                {
                    _tileSetImageFilePath = new Models.TileSetImageFilePath(value);
                    OnPropertyChanged(nameof(TileSetImageFilePathAsStr));
                }
            }
        }
        #endregion

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
        public TilePaletteEditPageViewModel()
        {
        }

        // - プライベート・フィールド

        /// <summary>
        ///     画像サイズ
        /// </summary>
        Models.Size _imageSize = Models.Size.Empty;

        /// <summary>
        ///     内部的グリッド画像サイズ
        /// </summary>
        Models.Size _internalGridImageSize = Models.Size.Empty;

        /// <summary>
        ///     タイル矩形
        /// </summary>
        Models.Rectangle _tileRect = Models.Rectangle.Empty;

        /// <summary>
        ///     コメント
        /// </summary>
        Models.Comment _comment = Models.Comment.Empty;

        /// <summary>
        ///     タイル・セット画像ファイルへのパス
        /// </summary>
        Models.TileSetImageFilePath _tileSetImageFilePath = Models.TileSetImageFilePath.Empty;

        /// <summary>
        ///     ウィンドウ上のタイル・カーソル座標
        /// </summary>
        Models.Point _tileCursorPointOnWindow = Models.Point.Empty;

        /// <summary>
        ///     ウィンドウ上のタイル・カーソルのマージン
        /// </summary>
        Thickness _tileCursorThickness = Thickness.Zero;

        // - プライベート・メソッド

        /// <summary>
        ///     <pre>
        ///         グリッドの再描画
        /// 
        ///         TRICK:  GraphicsView を再描画させたいが、ビューモデルから要求する方法が分からない。
        ///                 そこで、内部的なグリッド画像の横幅が偶数のときは +1、奇数のときは -1 して
        ///                 振動させることで、再描画を呼び起こすことにする
        ///     </pre>
        /// </summary>
        void RefreshGraphicsViewOfGrid()
        {
            if (this.InternalGridImageWidthAsInt % 2 == 1)
            {
                this.InternalGridImageWidthAsInt--;
            }
            else
            {
                this.InternalGridImageWidthAsInt++;
            }
        }

    }
}
