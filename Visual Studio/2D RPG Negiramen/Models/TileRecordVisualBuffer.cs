namespace _2D_RPG_Negiramen.Models
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using TheGeometric = Geometric;

    /// <summary>
    ///     タイル１件分の画面向けの記録
    ///     
    ///     <list type="bullet">
    ///         <item>ミュータブル</item>
    ///         <item>元画像の横幅、縦幅が 1未満 のとき、 None （存在しないもの）として扱う</item>
    ///     </list>
    /// </summary>
    internal class TileRecordVisualBuffer : ObservableObject
    {
        // - その他

        #region その他（生成　関連）
        internal static TileRecordVisualBuffer CreateEmpty() => new TileRecordVisualBuffer();

        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="tileRecord">タイル</param>
        /// <param name="workingRect">ズーム後の位置とサイズ</param>
        /// <returns></returns>
        public static TileRecordVisualBuffer FromModel(
            TileRecord tileRecord,
            TheGeometric.RectangleFloat workingRect)
        {
            return new TileRecordVisualBuffer()
            {
                Id = tileRecord.Id,
                SourceRectangle = tileRecord.Rectangle,
                WorkingRectangle = workingRect,
                Title = tileRecord.Title,
                LogicalDelete = tileRecord.LogicalDelete,
            };
        }

        /// <summary>
        ///     生成
        /// </summary>
        public TileRecordVisualBuffer()
        {
            Id = TileIdOrEmpty.Empty;
            SourceRectangle = TheGeometric.RectangleInt.Empty;
            WorkingRectangle = TheGeometric.RectangleFloat.Empty;
            Title = TileTitle.Empty;
            LogicalDelete = LogicalDelete.False;
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
        internal TileIdOrEmpty Id { get; set; }
        #endregion

        #region プロパティ（矩形　関連）
        /// <summary>
        ///     矩形（元データ）
        /// </summary>
        internal TheGeometric.RectangleInt SourceRectangle { get; set; }

        /// <summary>
        ///     矩形（ズーム後）
        /// </summary>
        internal TheGeometric.RectangleFloat WorkingRectangle { get; set; }
        #endregion

        #region プロパティ（タイトル）
        /// <summary>
        ///     タイトル
        /// </summary>
        internal TileTitle Title { get; set; }
        #endregion

        #region プロパティ（論理削除）
        /// <summary>
        ///     論理削除
        ///     
        ///     <list type="bullet">
        ///         <item>しないなら 0、 するなら 1</item>
        ///     </list>
        /// </summary>
        internal LogicalDelete LogicalDelete { get; set; }
        #endregion

        #region プロパティ（サイズが無いか？）
        /// <summary>
        ///     サイズが無いか？
        /// </summary>
        internal bool IsNone => (this.SourceRectangle.RightAsInt - this.SourceRectangle.LeftAsInt < 1) && (this.SourceRectangle.BottomAsInt - this.SourceRectangle.TopAsInt < 1);
        #endregion

        // - インターナル・メソッド

        #region メソッド（ダンプ）
        /// <summary>
        ///     ダンプ
        /// </summary>
        /// <returns></returns>
        internal string Dump()
        {
            return $"Id:{Id.AsBASE64}, SourceRect:{SourceRectangle.Dump()}, WorkingRect:{WorkingRectangle.Dump()}, Title:{Title.AsStr}, LogicalDelete: {LogicalDelete.AsInt}";
        }
        #endregion
    }
}
