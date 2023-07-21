namespace _2D_RPG_Negiramen.ViewModels
{
    using _2D_RPG_Negiramen.Models;
    using CommunityToolkit.Mvvm.ComponentModel;
    using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;

    /// <summary>
    ///     タイル１件分の記録
    ///     
    ///     <list type="bullet">
    ///         <item>ミュータブル</item>
    ///     </list>
    /// </summary>
    internal class TileRecordViewModel : ObservableObject
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="tileRecord">タイル</param>
        /// <param name="workingRect">ズーム後の位置とサイズ</param>
        /// <returns></returns>
        public static TileRecordViewModel FromModel(
            TileRecord tileRecord,
            TheGeometric.RectangleInt workingRect)
        {
            return new TileRecordViewModel()
            {
                Id = tileRecord.Id,
                SourceRectangle = tileRecord.Rectangle,
                WorkingRectangle = workingRect,
                Comment = tileRecord.Comment,
                LogicalDelete = tileRecord.LogicalDelete,
            };
        }

        /// <summary>
        ///     生成
        /// </summary>
        public TileRecordViewModel()
        {
            this.Id = Models.TileId.Empty;
            this.SourceRectangle = TheGeometric.RectangleInt.Empty;
            this.WorkingRectangle = TheGeometric.RectangleInt.Empty;
            this.Comment = Models.Comment.Empty;
            this.LogicalDelete = Models.LogicalDelete.False;
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
        internal Models.TileId Id { get; set; }
        #endregion

        #region プロパティ（矩形　関連）
        /// <summary>
        ///     矩形（元データ）
        /// </summary>
        internal TheGeometric.RectangleInt SourceRectangle { get; set; }

        /// <summary>
        ///     矩形（ズーム後）
        /// </summary>
        internal TheGeometric.RectangleInt WorkingRectangle { get; set; }
        #endregion

        #region プロパティ（コメント）
        /// <summary>
        ///     コメント
        /// </summary>
        internal Models.Comment Comment { get; set; }
        #endregion

        #region プロパティ（論理削除）
        /// <summary>
        ///     論理削除
        ///     
        ///     <list type="bullet">
        ///         <item>しないなら 0、 するなら 1</item>
        ///     </list>
        /// </summary>
        internal Models.LogicalDelete LogicalDelete { get; set; }
        #endregion

        // - インターナル・メソッド

        #region メソッド（ダンプ）
        /// <summary>
        ///     ダンプ
        /// </summary>
        /// <returns></returns>
        internal string Dump()
        {
            return $"Id:{this.Id.AsBASE64}, SourceRect:{this.SourceRectangle.Dump()}, WorkingRect:{this.WorkingRectangle.Dump()}, Comment:{this.Comment.AsStr}";
        }
        #endregion
    }
}
