namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     😁 タイルセット・レコード・ビューモデル
    ///     
    ///     <list type="bullet">
    ///         <item>ミュータブル</item>
    ///     </list>
    /// </summary>
    class TilesetRecordViewModel
    {
        // - その他

        internal TilesetRecordViewModel(
            string uuidAsStr,
            string filePathAsStr,
            int widthAsInt,
            int heightAsInt,
            string thumbnailFilePathAsStr,
            int thumbnailWidthAsInt,
            int thumbnailHeightAsInt,
            string name)
        {
            this.UuidAsStr = uuidAsStr;
            this.FilePathAsStr = filePathAsStr;
            this.WidthAsInt = widthAsInt;
            this.HeightAsInt = heightAsInt;
            this.ThumbnailFilePathAsStr = thumbnailFilePathAsStr;
            this.ThumbnailWidthAsInt = thumbnailWidthAsInt;
            this.ThumbnailHeightAsInt = thumbnailHeightAsInt;
            this.NameAsStr = name;
        }

        // - パブリック・プロパティ

        /// <summary>
        ///     UUID
        /// </summary>
        public string UuidAsStr { get; }

        /// <summary>
        ///     ファイルパス
        /// </summary>
        public string FilePathAsStr { get; }

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
        ///     名前
        ///     
        ///     <list type="bullet">
        ///         <item>実質、コメントのようなもの</item>
        ///     </list>
        /// </summary>
        public string NameAsStr { get; }
    }
}
