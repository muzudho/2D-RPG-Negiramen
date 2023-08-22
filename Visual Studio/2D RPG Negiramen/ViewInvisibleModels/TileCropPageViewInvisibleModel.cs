namespace _2D_RPG_Negiramen.ViewInvisibleModels
{
    using _2D_RPG_Negiramen.Models.Visually;
    using _2D_RPG_Negiramen.ViewModels;

    /// <summary>
    ///     見えないモデル
    ///     
    ///     <list type="bullet">
    ///         <item>ミュータブル</item>
    ///     </list>
    /// </summary>
    class TileCropPageViewInvisibleModel
    {
        // - その他

        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="owner"></param>
        internal TileCropPageViewInvisibleModel(TileCropPageViewModel owner)
        {
            this.Owner = owner;
        }

        // - インターナル・プロパティ

        TileCropPageViewModel Owner { get; }

        #region プロパティ（［タイルセット元画像］　関連）
        /// <summary>
        ///     ［タイルセット元画像］のサイズ
        /// </summary>
        internal Models.Geometric.SizeInt TilesetSourceImageSize
        {
            get => tilesetSourceImageSize;
            set
            {
                this.tilesetSourceImageSize = value;
            }
        }
        #endregion

        #region プロパティ（［タイルセット作業画像］　関連）
        /// <summary>
        ///     ［タイルセット作業画像］ファイルへのパス（文字列形式）
        /// </summary>
        public string TilesetWorkingImageFilePathAsStr => App.CacheFolder.YourCircleFolder.YourWorkFolder.ImagesFolder.WorkingTilesetPng.Path.AsStr;
        #endregion

        #region プロパティ（［切抜きカーソル］　関連）
        /// <summary>
        ///     ［切抜きカーソル］が指すタイル
        ///     
        ///     <list type="bullet">
        ///         <item>★循環参照しやすいので注意</item>
        ///         <item>［切抜きカーソル］が指すタイルが未確定のときも、指しているタイルにアクセスできることに注意</item>
        ///     </list>
        /// </summary>
        internal TileRecordVisually CroppedCursorPointedTileRecordVisually { get; set; } = TileRecordVisually.CreateEmpty();
        #endregion

        // - プライベート・フィールド

        #region フィールド（［タイルセット元画像］　関連）
        /// <summary>
        ///     ［タイルセット元画像］サイズ
        /// </summary>
        Models.Geometric.SizeInt tilesetSourceImageSize = Models.Geometric.SizeInt.Empty;
        #endregion
    }
}
