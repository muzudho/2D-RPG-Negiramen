namespace _2D_RPG_Negiramen.Models.Visually
{
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.Geometric;
    using CommunityToolkit.Mvvm.ComponentModel;

    /// <summary>
    ///     😁 タイル１件分の画面向け記録
    ///     
    ///     <list type="bullet">
    ///         <item>ミュータブル</item>
    ///         <item>元画像の横幅、縦幅が 1未満 のとき、 None （存在しないもの）として扱う</item>
    ///     </list>
    /// </summary>
    internal class TileRecordVisually : ObservableObject
    {
        // - その他

        #region その他（生成　関連）
        internal static TileRecordVisually CreateEmpty() => new TileRecordVisually(TileRecord.Empty);

        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="tileRecord">タイル</param>
        /// <returns></returns>
        public static TileRecordVisually FromModel(
            TileRecord tileRecord
#if DEBUG
            , string hint
#endif
            )
        {
            var tileVisually = new TileRecordVisually(
                tileRecord: tileRecord);

            //#if DEBUG
            //            Trace.WriteLine($"[TileRecordVisually.cs FromModel] tileVisually.Dump(): {tileVisually.Dump()}, hint: {hint}");
            //#else
            //            Trace.WriteLine($"[TileRecordVisually.cs FromModel] tileVisually.Dump(): {tileVisually.Dump()}");
            //#endif

            return tileVisually;
        }

        /// <summary>
        ///     生成
        /// </summary>
        TileRecordVisually(TileRecord tileRecord)
        {
            this.TileRecord = tileRecord;
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
        internal TileIdOrEmpty Id => this.TileRecord.Id;
        #endregion

        internal TileRecord TileRecord { get; private set; }

        #region プロパティ（タイトル）
        /// <summary>
        ///     タイトル
        /// </summary>
        internal TileTitle Title => this.TileRecord.Title;
        #endregion

        #region プロパティ（サイズが無いか？）
        /// <summary>
        ///     サイズが無いか？
        /// </summary>
        internal bool Rectangle_IsNotNormal => this.TileRecord.Rectangle_IsNotNormal;
        #endregion

        // - インターナル・メソッド

        #region メソッド（ダンプ）
        /// <summary>
        ///     ダンプ
        /// </summary>
        /// <returns></returns>
        internal string Dump(Zoom zoom)
        {
            return $"Id: {Id.AsBASE64}, IsNone: {this.Rectangle_IsNotNormal}, SourceRect: {this.TileRecord.Rectangle.Dump()}, WorkingRect: {TileRecordHelper.GetRefreshWorkingRectangle(
                tileRecord: this.TileRecord,
                zoom: zoom).Dump()}, Title: {Title.AsStr}";
        }
        #endregion

        internal void SetTitle(TileTitle title)
        {
            this.TileRecord = new TileRecord(
                id: this.Id,
                rect: this.TileRecord.Rectangle,
                title: title);
        }

        internal void SetId(TileIdOrEmpty id)
        {
            this.TileRecord = new TileRecord(
                id: id,
                rect: this.TileRecord.Rectangle,
                title: this.Title);
        }

        internal void SetRectangle(RectangleInt rect)
        {
            this.TileRecord = new TileRecord(
                id: this.Id,
                rect: rect,
                title: this.Title);
        }
    }
}
