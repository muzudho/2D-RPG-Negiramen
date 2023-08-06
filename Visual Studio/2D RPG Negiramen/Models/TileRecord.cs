namespace _2D_RPG_Negiramen.Models
{
    using _2D_RPG_Negiramen.Coding;

    /// <summary>
    ///     タイル１件分の記録
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///     </list>
    /// </summary>
    class TileRecord
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        public TileRecord()
            : this(Models.TileIdOrEmpty.Empty,
                   Geometric.RectangleInt.Empty,
                   Models.TileTitle.Empty,
                   Models.LogicalDelete.False)
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="id">Ｉｄ</param>
        /// <param name="rect">レクタングル</param>
        /// <param name="title">タイル・タイトル</param>
        /// <param name="logicalDelete">論理削除か？</param>
        internal TileRecord(
            Models.TileIdOrEmpty id,
            Geometric.RectangleInt rect,
            Models.TileTitle title,
            Models.LogicalDelete logicalDelete)
        {
            this.Id = id ?? throw new ArgumentNullException(nameof(id));
            this.Rectangle = rect ?? throw new ArgumentNullException(nameof(rect));
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.LogicalDelete = logicalDelete ?? throw new ArgumentNullException(nameof(logicalDelete));
        }
        #endregion

        // - インターナル静的プロパティ

        #region プロパティ（空オブジェクト）
        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static TileRecord Empty = new();
        #endregion

        #region プロパティ（空オブジェクト）
        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static Option<TileRecord> EmptyOption = new(TileRecord.Empty);
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
        internal Models.TileIdOrEmpty Id { get; }
        #endregion

        #region プロパティ（矩形）
        /// <summary>
        ///     矩形
        /// </summary>
        internal Geometric.RectangleInt Rectangle { get; }
        #endregion

        #region プロパティ（タイトル）
        /// <summary>
        ///     タイトル
        /// </summary>
        internal Models.TileTitle Title { get; }
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
            return $"Id:{this.Id.AsBASE64}, Rect:{this.Rectangle.Dump()}, Title:{this.Title.AsStr}";
        }
        #endregion
    }
}
