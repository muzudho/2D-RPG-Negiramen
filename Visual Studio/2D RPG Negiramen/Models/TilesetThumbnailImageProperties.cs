namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     😁 タイルセット・サムネイル画像プロパティーズ
    /// </summary>
    class TilesetThumbnailImageProperties
    {
        // - その他

        #region その他（生成　関連）
        internal static TilesetThumbnailImageProperties Create(
            int originalWidth,
            int originalHeight)
        {
            int thumbnailWidth;
            int thumbnailHeight;

            // TODO サムネイル画像のサイズをここで決めるのはおかしい
            int longLength = Math.Max(originalWidth, originalHeight);
            // int shortLength = Math.Min(originalWidth, originalHeight);
            // 長い方が 128 より大きければ縮める
            if (128 < longLength)
            {
                float rate = (float)longLength / 128.0f;
                thumbnailWidth = (int)(originalWidth / rate);
                thumbnailHeight = (int)(originalHeight / rate);
            }
            else
            {
                thumbnailWidth = originalWidth;
                thumbnailHeight = originalHeight;
            }

            return new TilesetThumbnailImageProperties(
                thumbnailWidth: thumbnailWidth,
                thumbnailHeight: thumbnailHeight);
        }

        /// <summary>
        ///     生成
        /// </summary>
        TilesetThumbnailImageProperties(
            int thumbnailWidth,
            int thumbnailHeight)
        {
            this.ThumbnailWidth = thumbnailWidth;
            this.ThumbnailHeight = thumbnailHeight;
        }
        #endregion

        // - インターナル・プロパティ

        /// <summary>
        ///     サムネイル画像の横幅
        /// </summary>
        internal int ThumbnailWidth;

        /// <summary>
        ///     サムネイル画像の縦幅
        /// </summary>
        internal int ThumbnailHeight;
    }
}
