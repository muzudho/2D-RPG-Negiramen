namespace _2D_RPG_Negiramen.Models
{
    using _2D_RPG_Negiramen.Coding;

    /// <summary>
    ///     タイル１件分の記録
    /// </summary>
    class TileRecord
    {
        // - インターナル静的プロパティ

        internal static TileRecord Empty = new TileRecord();

        internal static Option<TileRecord> EmptyOption = new Option<TileRecord>(TileRecord.Empty);

        // - インターナル・プロパティ

        /// <summary>
        ///     Ｉｄ
        ///     
        ///     <list type="bullet">
        ///         <item>0 は `MA==` だが、これは空文字として表示する</item>
        ///     </list>
        /// </summary>
        internal Models.TileId Id { get; }

        /// <summary>
        ///     矩形
        /// </summary>
        internal Models.Rectangle Rectangle { get; }

        /// <summary>
        ///     コメント
        /// </summary>
        internal Models.Comment Comment { get; }

        // - その他

        /// <summary>
        ///     生成
        /// </summary>
        public TileRecord()
            : this(Models.TileId.Empty,
                   Models.Rectangle.Empty,
                   Models.Comment.Empty)
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="rectangle">レクタングル</param>
        internal TileRecord(
            Models.TileId id,
            Models.Rectangle rectangle,
            Models.Comment comment)
        {
            this.Id = id;
            this.Rectangle = rectangle;
            this.Comment = comment;
        }
    }
}
