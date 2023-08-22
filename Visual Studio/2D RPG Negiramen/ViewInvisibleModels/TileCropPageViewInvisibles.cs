namespace _2D_RPG_Negiramen.ViewInvisibleModels
{
    /// <summary>
    ///     見えないモデル
    ///     
    ///     <list type="bullet">
    ///         <item>ミュータブル</item>
    ///     </list>
    /// </summary>
    class TileCropPageViewInvisibleModel
    {
        // - インターナル・プロパティ

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

        // - プライベート・フィールド

        #region フィールド（［タイルセット元画像］　関連）
        /// <summary>
        ///     ［タイルセット元画像］サイズ
        /// </summary>
        Models.Geometric.SizeInt tilesetSourceImageSize = Models.Geometric.SizeInt.Empty;
        #endregion
    }
}
