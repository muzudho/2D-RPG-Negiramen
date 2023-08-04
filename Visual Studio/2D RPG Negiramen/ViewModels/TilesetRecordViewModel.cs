namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     😁 タイルセット・レコード・ビューモデル
    ///     
    ///     <list type="bullet">
    ///         <item>ミュータブル</item>
    ///     </list>
    /// </summary>
    public class TilesetRecordViewModel
    {
        // - その他

        #region その他（生成）
        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="uuidAsStr"></param>
        /// <param name="pngFilePathAsStr">PNG元画像のファイルパス文字列</param>
        /// <param name="widthAsInt"></param>
        /// <param name="heightAsInt"></param>
        /// <param name="thumbnailFilePathAsStr"></param>
        /// <param name="thumbnailWidthAsInt"></param>
        /// <param name="thumbnailHeightAsInt"></param>
        /// <param name="title"></param>
        internal TilesetRecordViewModel(
            string uuidAsStr,
            string pngFilePathAsStr,
            int widthAsInt,
            int heightAsInt,
            string thumbnailFilePathAsStr,
            int thumbnailWidthAsInt,
            int thumbnailHeightAsInt,
            string title)
        {
            this.UuidAsStr = uuidAsStr;
            this.PngFilePathAsStr = pngFilePathAsStr;
            this.WidthAsInt = widthAsInt;
            this.HeightAsInt = heightAsInt;
            this.ThumbnailFilePathAsStr = thumbnailFilePathAsStr;
            this.ThumbnailWidthAsInt = thumbnailWidthAsInt;
            this.ThumbnailHeightAsInt = thumbnailHeightAsInt;
            this.TitleAsStr = title;
        }
        #endregion

        // - パブリック・プロパティ

        /// <summary>
        ///     UUID
        /// </summary>
        public string UuidAsStr { get; }

        /// <summary>
        ///     PNG元画像のファイルパス文字列
        /// </summary>
        public string PngFilePathAsStr { get; }

        /// <summary>
        ///     横幅
        /// </summary>
        public int WidthAsInt { get; }

        /// <summary>
        ///     縦幅
        /// </summary>
        public int HeightAsInt { get; }

        /// <summary>
        ///     サムネイルのファイルパス
        /// </summary>
        public string ThumbnailFilePathAsStr { get; }

        /// <summary>
        ///     サムネイルの横幅
        /// </summary>
        public int ThumbnailWidthAsInt { get; }

        /// <summary>
        ///     サムネイルの縦幅
        /// </summary>
        public int ThumbnailHeightAsInt { get; }

        /// <summary>
        ///     タイトル
        ///     
        ///     <list type="bullet">
        ///         <item>実質、コメントのようなもの</item>
        ///     </list>
        /// </summary>
        public string TitleAsStr { get; }
    }
}
