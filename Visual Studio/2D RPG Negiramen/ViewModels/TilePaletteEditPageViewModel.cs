namespace _2D_RPG_Negiramen.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;

    /// <summary>
    /// ［タイル・パレット編集ページ］ビューモデル
    /// </summary>
    class TilePaletteEditPageViewModel : ObservableObject
    {
        // - 変更通知プロパティ

        /// <summary>
        /// 画像の横幅
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
        /// 画像の縦幅
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

        /// <summary>
        /// グリッドの左上位置ｘ
        /// </summary>
        public int GridLeftAsInt
        {
            get => _gridLeftTopPoint.X.AsInt;
            set
            {
                if (_gridLeftTopPoint.X.AsInt != value)
                {
                    _gridLeftTopPoint = new Models.Point(new Models.X(value), _gridLeftTopPoint.Y);
                    OnPropertyChanged(nameof(GridLeftAsInt));
                }
            }
        }

        /// <summary>
        /// グリッドの左上位置ｙ
        /// </summary>
        public int GridTopAsInt
        {
            get => _gridLeftTopPoint.Y.AsInt;
            set
            {
                if (_gridLeftTopPoint.Y.AsInt != value)
                {
                    _gridLeftTopPoint = new Models.Point(_gridLeftTopPoint.X, new Models.Y(value));
                    OnPropertyChanged(nameof(GridLeftAsInt));
                }
            }
        }

        /// <summary>
        /// グリッド・タイルの横幅
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
        /// グリッド・タイルの縦幅
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











        /// <summary>
        /// タイルの位置ｘ
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
        /// タイルの位置ｙ
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
        /// タイルの横幅
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
        /// タイルの縦幅
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
        /// コメント
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

        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="imageSize">画像サイズ</param>
        /// <param name="gridLeftTopPoint">グリッドの左上の座標</param>
        /// <param name="gridTileSize">グリッド・タイル・サイズ</param>
        /// <param name="tileRect">タイル矩形</param>
        /// <param name="commentAsStr">コメント</param>
        TilePaletteEditPageViewModel(
            Models.Size imageSize,
            Models.Point gridLeftTopPoint,
            Models.Size gridTileSize,
            Models.Rectangle tileRect,
            string commentAsStr)
        {
            this._imageSize = imageSize;
            this._gridLeftTopPoint = gridLeftTopPoint;
            this._gridTileSize = gridTileSize;
            this._tileRect = tileRect;
            CommentAsStr = commentAsStr;
        }

        // - プライベート・フィールド

        /// <summary>
        /// 画像サイズ
        /// </summary>
        Models.Size _imageSize = Models.Size.Empty;

        /// <summary>
        /// グリッドの左上位置
        /// </summary>
        Models.Point _gridLeftTopPoint = Models.Point.Empty;

        /// <summary>
        /// グリッド・タイル・サイズ
        /// </summary>
        Models.Size _gridTileSize = Models.Size.Empty;

        /// <summary>
        /// タイル矩形
        /// </summary>
        Models.Rectangle _tileRect = Models.Rectangle.Empty;

        /// <summary>
        /// コメント
        /// </summary>
        Models.Comment _comment = Models.Comment.Empty;
    }
}
