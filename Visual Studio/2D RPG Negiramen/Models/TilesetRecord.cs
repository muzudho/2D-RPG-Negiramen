namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     😁 タイルセット・レコード
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///     </list>
    /// </summary>
    class TilesetRecord
    {
        // - その他

        internal TilesetRecord(
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

        // - インターナル・プロパティ

        /// <summary>
        ///     UUID
        /// </summary>
        internal string UuidAsStr { get; }

        /// <summary>
        ///     ファイルパス
        /// </summary>
        internal string FilePathAsStr { get; }

        /// <summary>
        ///     横幅
        /// </summary>
        internal int WidthAsInt { get; }

        /// <summary>
        ///     縦幅
        /// </summary>
        internal int HeightAsInt { get; }

        /// <summary>
        ///     サムネイルのファイルパス
        /// </summary>
        internal string ThumbnailFilePathAsStr { get; }

        /// <summary>
        ///     サムネイルの横幅
        /// </summary>
        internal int ThumbnailWidthAsInt { get; }

        /// <summary>
        ///     サムネイルの縦幅
        /// </summary>
        internal int ThumbnailHeightAsInt { get; }

        /// <summary>
        ///     名前
        ///     
        ///     <list type="bullet">
        ///         <item>実質、コメントのようなもの</item>
        ///     </list>
        /// </summary>
        internal string NameAsStr { get; }
    }
}
