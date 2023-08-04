namespace _2D_RPG_Negiramen.Models
{
    using _2D_RPG_Negiramen.Models.FileEntries.Locations.Cache;

    /// <summary>
    ///     😁 タイルセット・サムネイル画像プロパティーズ
    /// </summary>
    class TilesetThumbnailImageProperties
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     作成
        /// </summary>
        /// <param name="originalPngPathAsStr">元画像のファイルパス文字列</param>
        /// <param name="originalWidth">元画像の横幅</param>
        /// <param name="originalHeight">元画像の縦幅</param>
        /// <param name="outputFolder">サムネイル画像出力先フォルダ</param>
        /// <returns></returns>
        internal static TilesetThumbnailImageProperties Create(
            string originalPngPathAsStr,
            int originalWidth,
            int originalHeight,
            ImagesTilesetsThumbnailsFolder outputFolder)
        {
            int width;
            int height;

            // TODO サムネイル画像のサイズをここで決めるのはおかしい
            int longLength = Math.Max(originalWidth, originalHeight);
            // int shortLength = Math.Min(originalWidth, originalHeight);
            // 長い方が 128 より大きければ縮める
            if (128 < longLength)
            {
                float rate = (float)longLength / 128.0f;
                width = (int)(originalWidth / rate);
                height = (int)(originalHeight / rate);
            }
            else
            {
                width = originalWidth;
                height = originalHeight;
            }

            var originalFileStem = System.IO.Path.GetFileNameWithoutExtension(originalPngPathAsStr);

            return new TilesetThumbnailImageProperties(
                pathAsStr: outputFolder.CreateTilesetThumbnailPng(originalFileStem).Path.AsStr,
                width: width,
                height: height);
        }

        /// <summary>
        ///     生成
        /// </summary>
        TilesetThumbnailImageProperties(
            string pathAsStr,
            int width,
            int height)
        {
            this.PathAsStr = pathAsStr;
            this.Width = width;
            this.Height = height;
        }
        #endregion

        // - インターナル・プロパティ

        /// <summary>
        ///     ファイルパス文字列
        /// </summary>
        internal string PathAsStr { get; }

        /// <summary>
        ///     横幅
        /// </summary>
        internal int Width { get; }

        /// <summary>
        ///     縦幅
        /// </summary>
        internal int Height { get; }
    }
}
