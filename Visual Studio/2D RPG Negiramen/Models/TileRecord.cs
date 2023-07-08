namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     タイル１件分の記録
    /// </summary>
    class TileRecord
    {
        // - パブリック・プロパティ

        /// <summary>
        ///     Ｉｄ
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
