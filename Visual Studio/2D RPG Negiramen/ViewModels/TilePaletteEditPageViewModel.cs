namespace _2D_RPG_Negiramen.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;

    /// <summary>
    ///     😁 ［タイル・パレット編集ページ］ビューモデル
    /// </summary>
    [QueryProperty(nameof(TileSetImageFilePathAsStr), queryId: "TileSetImageFilePathAsStr")]
    [QueryProperty(nameof(ImageSize), queryId: "ImageSize")]
    [QueryProperty(nameof(GridCanvasSize), queryId: "GridCanvasSize")]
    [QueryProperty(nameof(GridLeftTop), queryId: "GridLeftTop")]
    [QueryProperty(nameof(GridTileSize), queryId: "GridTileSize")]
    [QueryProperty(nameof(TileCursorCanvasSize), queryId: "TileCursorCanvasSize")]
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

        #region 変更通知プロパティ（グリッドのキャンバス・サイズ）
        /// <summary>
        ///     <pre>
        ///         グリッドのキャンバス・サイズ
        ///         
        ///         グリッドの線の太さを 2px と想定しているので、グリッドの線が画像の端っこで切れないように、グリッドの内部的なキャンバス・サイズを 2px 広げる
        ///     </pre>
        /// </summary>
        public Models.Size GridCanvasSize
        {
            get => _gridCanvasSize;
            set
            {
                if (_gridCanvasSize != value)
                {
                    // 差分判定
                    var dirtyWidth = _gridCanvasSize.Width != value.Width;
                    var dirtyHeight = _gridCanvasSize.Height != value.Height;

                    // 更新
                    _gridCanvasSize = value;

                    // 変更通知
                    if (dirtyWidth)
                    {
                        OnPropertyChanged(nameof(GridCanvasWidthAsInt));
                    }

                    if (dirtyHeight)
                    {
                        OnPropertyChanged(nameof(GridCanvasHeightAsInt));
                    }
                }
            }
        }

        /// <summary>
        ///     <pre>
        ///         グリッドのキャンバスの横幅
        ///         
        ///         グリッドの線の太さを 2px と想定しているので、グリッドの線が画像の端っこで切れないように、グリッドのキャンバス・サイズを 2px 広げる
        ///     </pre>
        /// </summary>
        public int GridCanvasWidthAsInt
        {
            get => _gridCanvasSize.Width.AsInt;
            set
            {
                if (_gridCanvasSize.Width.AsInt != value)
                {
                    _gridCanvasSize = new Models.Size(new Models.Width(value), _gridCanvasSize.Height);
                    OnPropertyChanged(nameof(GridCanvasWidthAsInt));
                }
            }
        }

        /// <summary>
        ///     <pre>
        ///         グリッドのキャンバスの縦幅
        ///         
        ///         グリッドの線の太さを 2px と想定しているので、グリッドの線が画像の端っこで切れないように、グリッドのキャンバス・サイズを 2px 広げる
        ///     </pre>
        /// </summary>
        public int GridCanvasHeightAsInt
        {
            get => _gridCanvasSize.Height.AsInt;
            set
            {
                if (_gridCanvasSize.Height.AsInt != value)
                {
                    _gridCanvasSize = new Models.Size(_gridCanvasSize.Width, new Models.Height(value));
                    OnPropertyChanged(nameof(GridCanvasHeightAsInt));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（タイル・カーソルのキャンバス・サイズ）
        /// <summary>
        ///     <pre>
        ///         タイル・カーソルのキャンバス・サイズ
        ///         
        ///         カーソルの線の幅が 4px なので、キャンバス・サイズは + 8px にする
        ///     </pre>
        /// </summary>
        public Models.Size TileCursorCanvasSize
        {
            get => _tileCursorCanvasSize;
            set
            {
                if (_tileCursorCanvasSize != value)
                {
                    this.TileCursorCanvasWidthAsInt = value.Width.AsInt;
                    this.TileCursorCanvasHeightAsInt = value.Height.AsInt;
                }
            }
        }

        /// <summary>
        ///     <pre>
        ///         タイル・カーソルのキャンバスの横幅
        ///         
        ///         カーソルの線の幅が 4px なので、画像サイズは + 8px にする
        ///     </pre>
        /// </summary>
        public int TileCursorCanvasWidthAsInt
        {
            get => _tileCursorCanvasSize.Width.AsInt;
            set
            {
                if (_tileCursorCanvasSize.Width.AsInt != value)
                {
                    _tileCursorCanvasSize = new Models.Size(new Models.Width(value), _tileCursorCanvasSize.Height);

                    // キャンバスを再描画
                    RefreshCanvasOfTileCursor();

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(TileCursorCanvasWidthAsInt));
                }
            }
        }

        /// <summary>
        ///     <pre>
        ///         タイル・カーソルのキャンバスの縦幅
        ///         
        ///         カーソルの線の幅が 4px なので、画像サイズは + 8px にする
        ///     </pre>
        /// </summary>
        public int TileCursorCanvasHeightAsInt
        {
            get => _tileCursorCanvasSize.Height.AsInt;
            set
            {
                if (_tileCursorCanvasSize.Height.AsInt != value)
                {
                    _tileCursorCanvasSize = new Models.Size(_tileCursorCanvasSize.Width, new Models.Height(value));

                    // キャンバスを再描画
                    RefreshCanvasOfTileCursor();

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(TileCursorCanvasHeightAsInt));
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
                    this.GridLeftAsInt = value.X.AsInt;
                    this.GridTopAsInt = value.Y.AsInt;

                    /*
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
                    */
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

                    // キャンバスを再描画
                    RefreshCanvasOfGrid();

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(GridLeftAsInt));
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

                    // キャンバスを再描画
                    RefreshCanvasOfGrid();

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(GridLeftAsInt));
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
                    this.GridTileWidthAsInt = value.Width.AsInt;
                    this.GridTileHeightAsInt = value.Height.AsInt;
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

                    // カーソルの線の幅が 4px なので、タイル・カーソルの画像サイズは + 8px にする
                    this.TileCursorCanvasWidthAsInt = App.WorkingGridTileSize.Width.AsInt + 4 * App.HalfThicknessOfTileCursorLine.AsInt;

                    // キャンバスを再描画
                    RefreshCanvasOfGrid();
                    RefreshCanvasOfTileCursor();

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(GridTileWidthAsInt));
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

                    // カーソルの線の幅が 4px なので、タイル・カーソルの画像サイズは + 8px にする
                    this.TileCursorCanvasHeightAsInt = App.WorkingGridTileSize.Height.AsInt + 4 * App.HalfThicknessOfTileCursorLine.AsInt;

                    // キャンバスを再描画
                    RefreshCanvasOfGrid();
                    RefreshCanvasOfTileCursor();

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(GridTileHeightAsInt));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（タイルの最大サイズ）
        /// <summary>
        ///     <pre>
        ///         タイルの最大サイズ
        ///         
        ///         設定ファイルから与えられる
        ///     </pre>
        /// </summary>
        public Models.Size GridMaxTileSize
        {
            get => App.GetOrLoadSettings().TileMaxSize;
        }

        /// <summary>
        ///     タイルの最大横幅
        /// </summary>
        public int TileMaxWidthAsInt
        {
            get => App.GetOrLoadSettings().TileMaxSize.Width.AsInt;
        }

        /// <summary>
        ///     タイルの最大縦幅
        /// </summary>
        public int TileMaxHeightAsInt
        {
            get => App.GetOrLoadSettings().TileMaxSize.Height.AsInt;
        }
        #endregion

        #region 変更通知プロパティ（タイル・カーソルの位置）
        /// <summary>
        ///     タイル・カーソルの位置（マージンとして）
        /// </summary>
        public Thickness TileCursorPointAsMargin
        {
            get => _tileCursorPointAsMargin;
            set
            {
                if (_tileCursorPointAsMargin != value)
                {
                    _tileCursorPointAsMargin = value;
                    OnPropertyChanged(nameof(TileCursorPointAsMargin));
                }
            }
        }

        /// <summary>
        ///     タイル・カーソルの位置ｘ
        /// </summary>
        public int TileCursorXAsInt
        {
            get => _tileCursorPoint.X.AsInt;
            set
            {
                if (_tileCursorPoint.X.AsInt != value)
                {
                    _tileCursorPoint = new Models.Point(new Models.X(value), _tileCursorPoint.Y);

                    this.TileCursorPointAsMargin = new Thickness(
                        // 左
                        this.TileCursorXAsInt,
                        // 上
                        this.TileCursorYAsInt,
                        // 右
                        0,
                        // 下
                        0);

                    OnPropertyChanged(nameof(TileCursorXAsInt));
                }
            }
        }

        /// <summary>
        ///     タイル・カーソルの位置ｙ
        /// </summary>
        public int TileCursorYAsInt
        {
            get => _tileCursorPoint.Y.AsInt;
            set
            {
                if (_tileCursorPoint.Y.AsInt != value)
                {
                    _tileCursorPoint = new Models.Point(_tileCursorPoint.X, new Models.Y(value));

                    this.TileCursorPointAsMargin = new Thickness(
                        // 左
                        this.TileCursorXAsInt,
                        // 上
                        this.TileCursorYAsInt,
                        // 右
                        0,
                        // 下
                        0);

                    OnPropertyChanged(nameof(TileCursorYAsInt));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（選択タイルの矩形）
        /// <summary>
        ///     選択タイルの位置ｘ
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
        ///     選択タイルの位置ｙ
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
        ///     選択タイルの横幅
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
        ///     選択タイルの縦幅
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

        #region 変更通知プロパティ（選択タイルのコメント）
        /// <summary>
        ///     選択コメント
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
        ///     選択コメント
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
        Models.Size _gridCanvasSize = Models.Size.Empty;

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
        ///     タイル・カーソルの位置
        /// </summary>
        Models.Point _tileCursorPoint = Models.Point.Empty;

        /// <summary>
        ///     タイル・カーソルの位置（マージンとして）
        /// </summary>
        Thickness _tileCursorPointAsMargin = Thickness.Zero;

        /// <summary>
        ///     <pre>
        ///         タイル・カーソルのサイズ
        ///     
        ///         カーソルの線の幅が 4px なので、画像サイズは + 8px にする
        ///     </pre>
        /// </summary>
        Models.Size _tileCursorCanvasSize = Models.Size.Empty;

        /// <summary>
        ///     <pre>
        ///         タイルの最大サイズ
        ///     
        ///         設定ファイルから与えられる
        ///     </pre>
        /// </summary>
        Models.Size _tileMaxSize = Models.Size.Empty;

        // - プライベート・メソッド

        #region プライベート・メソッド（キャンバスの再描画）
        /// <summary>
        ///     <pre>
        ///         グリッドのキャンバスの再描画
        /// 
        ///         TRICK:  GraphicsView を再描画させたいが、ビューモデルから要求する方法が分からない。
        ///                 そこで、内部的なグリッド画像の横幅が偶数のときは +1、奇数のときは -1 して
        ///                 振動させることで、再描画を呼び起こすことにする
        ///     </pre>
        /// </summary>
        void RefreshCanvasOfGrid()
        {
            if (this.GridCanvasWidthAsInt % 2 == 1)
            {
                this.GridCanvasWidthAsInt--;
            }
            else
            {
                this.GridCanvasWidthAsInt++;
            }
        }

        /// <summary>
        ///     <pre>
        ///         タイル・カーソルのキャンバスの再描画
        /// 
        ///         TRICK:  GraphicsView を再描画させたいが、ビューモデルから要求する方法が分からない。
        ///                 そこで、内部的なグリッド画像の横幅が偶数のときは +1、奇数のときは -1 して
        ///                 振動させることで、再描画を呼び起こすことにする
        ///     </pre>
        /// </summary>
        void RefreshCanvasOfTileCursor()
        {
            int offset;

            if (this._tileCursorCanvasSize.Width.AsInt % 2 == 1)
            {
                offset = -1;
            }
            else
            {
                offset = 1;
            }

            // 循環参照を避けるために、直接フィールドを変更
            this._tileCursorCanvasSize = new Models.Size(new Models.Width(this._tileCursorCanvasSize.Width.AsInt - offset), new Models.Height(this._tileCursorCanvasSize.Height.AsInt));
            OnPropertyChanged(nameof(TileCursorCanvasWidthAsInt));
        }
        #endregion
    }
}
