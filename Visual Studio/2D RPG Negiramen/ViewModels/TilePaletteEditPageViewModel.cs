namespace _2D_RPG_Negiramen.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;

    /// <summary>
    ///     😁 ［タイル・パレット編集ページ］ビューモデル
    /// </summary>
    [QueryProperty(nameof(TileSetImageFilePathAsStr), queryId: "TileSetImageFilePathAsStr")]
    [QueryProperty(nameof(ImageSize), queryId: "ImageSize")]
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

        #region 変更通知プロパティ（グリッドの左上位置）
        /// <summary>
        ///     グリッドの左上位置
        /// </summary>
        public Models.Point GridLeftTop
        {
            get => _gridLeftTop;
            set
            {
                if (_gridLeftTop != value)
                {
                    // 差分判定
                    var dirtyX = _gridLeftTop.X != value.X;
                    var dirtyY = _gridLeftTop.Y != value.Y;

                    // 更新
                    _gridLeftTop = value;

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
        ///     グリッドの左上位置ｘ
        /// </summary>
        public int GridLeftAsInt
        {
            get => _gridLeftTop.X.AsInt;
            set
            {
                if (_gridLeftTop.X.AsInt != value)
                {
                    _gridLeftTop = new Models.Point(new Models.X(value), _gridLeftTop.Y);
                    OnPropertyChanged(nameof(GridLeftAsInt));
                }
            }
        }

        /// <summary>
        ///     グリッドの左上位置ｙ
        /// </summary>
        public int GridTopAsInt
        {
            get => _gridLeftTop.Y.AsInt;
            set
            {
                if (_gridLeftTop.Y.AsInt != value)
                {
                    _gridLeftTop = new Models.Point(_gridLeftTop.X, new Models.Y(value));
                    OnPropertyChanged(nameof(GridLeftAsInt));
                }
            }
        }
        #endregion

        #region グリッド・タイルのサイズ
        /// <summary>
        ///     グリッド・タイルのサイズ
        /// </summary>
        public Models.Size GridTileSize
        {
            get => _gridTileSize;
            set
            {
                if (_gridTileSize != value)
                {
                    // 差分判定
                    var dirtyWidth = _gridTileSize.Width != value.Width;
                    var dirtyHeight = _gridTileSize.Height != value.Height;

                    // 更新
                    _gridTileSize = value;

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
            get => _gridTileSize.Width.AsInt;
            set
            {
                if (_gridTileSize.Width.AsInt != value)
                {
                    _gridTileSize = new Models.Size(new Models.Width(value), _gridTileSize.Height);
                    OnPropertyChanged(nameof(GridTileWidthAsInt));
                }
            }
        }

        /// <summary>
        ///     グリッド・タイルの縦幅
        /// </summary>
        public int GridTileHeightAsInt
        {
            get => _gridTileSize.Height.AsInt;
            set
            {
                if (_gridTileSize.Height.AsInt != value)
                {
                    _gridTileSize = new Models.Size(_gridTileSize.Width, new Models.Height(value));
                    OnPropertyChanged(nameof(GridTileHeightAsInt));
                }
            }
        }
        #endregion

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
        ///     グリッドの左上位置
        /// </summary>
        Models.Point _gridLeftTop = Models.Point.Empty;

        /// <summary>
        ///     グリッド・タイル・サイズ
        /// </summary>
        Models.Size _gridTileSize = Models.Size.Empty;

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
    }
}
