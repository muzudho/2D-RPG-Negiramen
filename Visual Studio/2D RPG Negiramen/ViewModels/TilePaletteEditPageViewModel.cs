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
        TilePaletteEditPageViewModel(Models.Size imageSize)
        {
            this._imageSize = imageSize;
        }

        // - プライベート・フィールド

        /// <summary>
        /// 画像サイズ
        /// </summary>
        Models.Size _imageSize = Models.Size.Empty;
    }
}
