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
        TilePaletteEditPageViewModel(Models.Size imageSize, Models.Point gridLeftTopPoint)
        {
            this._imageSize = imageSize;
            this._gridLeftTopPoint = gridLeftTopPoint;
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
    }
}
