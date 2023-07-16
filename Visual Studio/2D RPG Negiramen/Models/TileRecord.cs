namespace _2D_RPG_Negiramen.Models
{
    using _2D_RPG_Negiramen.Coding;

    /// <summary>
    ///     タイル１件分の記録
    /// </summary>
    class TileRecord
    {
        // - インターナル静的プロパティ

        #region プロパティ（空オブジェクト）
        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static TileRecord Empty = new TileRecord();
        #endregion

        #region プロパティ（空オブジェクト）
        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static Option<TileRecord> EmptyOption = new Option<TileRecord>(TileRecord.Empty);
        #endregion

        // - その他

        #region その他（生成）
        /// <summary>
        ///     生成
        /// </summary>
        public TileRecord()
            : this(Models.TileId.Empty,
                   Models.Rectangle.Empty,
                   Models.Comment.Empty,
                   Models.LogicalDelete.False)
        {
        }
        #endregion

        #region その他（生成）
        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="rectangle">レクタングル</param>
        internal TileRecord(
            Models.TileId id,
            Models.Rectangle rectangle,
            Models.Comment comment,
            Models.LogicalDelete logicalDelete)
        {
            this.Id = id ?? throw new ArgumentNullException(nameof(id));
            this.Rectangle = rectangle ?? throw new ArgumentNullException(nameof(rectangle));
            this.Comment = comment ?? throw new ArgumentNullException(nameof(comment));
            this.LogicalDelete = logicalDelete ?? throw new ArgumentNullException(nameof(logicalDelete));
        }
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（Ｉｄ）
        /// <summary>
        ///     Ｉｄ
        ///     
        ///     <list type="bullet">
        ///         <item>0 は `MA==` だが、これは空文字として表示する</item>
        ///     </list>
        /// </summary>
        internal Models.TileId Id { get; }
        #endregion

        #region プロパティ（矩形）
        /// <summary>
        ///     矩形
        /// </summary>
        internal Models.Rectangle Rectangle { get; }
        #endregion

        #region プロパティ（コメント）
        /// <summary>
        ///     コメント
        /// </summary>
        internal Models.Comment Comment { get; }
        #endregion

        #region プロパティ（論理削除）
        /// <summary>
        ///     論理削除
        ///     
        ///     <list type="bullet">
        ///         <item>しないなら 0、 するなら 1</item>
        ///     </list>
        /// </summary>
        internal Models.LogicalDelete LogicalDelete { get; }
        #endregion

        // - インターナル・メソッド

        #region メソッド（ダンプ）
        /// <summary>
        ///     ダンプ
        /// </summary>
        /// <returns></returns>
        internal string Dump()
        {
            return $"Id:{this.Id.AsBASE64}, Rect:{this.Rectangle.Dump()}, Comment:{this.Comment.AsStr}";
        }
        #endregion
    }
}
