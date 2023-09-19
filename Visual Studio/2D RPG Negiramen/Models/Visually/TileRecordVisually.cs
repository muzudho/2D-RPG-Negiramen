namespace _2D_RPG_Negiramen.Models.Visually
{
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.Geometric;
    using CommunityToolkit.Mvvm.ComponentModel;
    using System.Diagnostics;
    using TheGeometric = Geometric;

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
                tileRecord: tileRecord)
            {
                Id = tileRecord.Id,
                Title = tileRecord.Title
            };

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
        internal TileIdOrEmpty Id { get; set; } = TileIdOrEmpty.Empty;
        #endregion

        internal TileRecord TileRecord { get; private set; }

        #region プロパティ（タイトル）
        /// <summary>
        ///     タイトル
        /// </summary>
        internal TileTitle Title { get; set; } = TileTitle.Empty;
        #endregion

        #region プロパティ（サイズが無いか？）
        /// <summary>
        ///     サイズが無いか？
        /// </summary>
        internal bool IsNone => this.TileRecord.Rectangle.RightAsInt - this.TileRecord.Rectangle.LeftAsInt < 1 && this.TileRecord.Rectangle.BottomAsInt - this.TileRecord.Rectangle.TopAsInt < 1;
        #endregion

        // - インターナル・メソッド

        #region メソッド（ダンプ）
        /// <summary>
        ///     ダンプ
        /// </summary>
        /// <returns></returns>
        internal string Dump(Zoom zoom)
        {
            return $"Id: {Id.AsBASE64}, IsNone: {this.IsNone}, SourceRect: {this.TileRecord.Rectangle.Dump()}, WorkingRect: {this.GetRefreshWorkingRectangle(
                zoom: zoom).Dump()}, Title: {Title.AsStr}";
        }
        #endregion

        internal void SetRectangle(RectangleInt rect)
        {
            Debug.Assert(rect != null);

            this.TileRecord = new TileRecord(
                id: this.Id,
                rect: rect,
                title: this.Title);
        }

        /// <summary>
        ///     差分更新
        /// </summary>
        /// <param name="tileRecord">タイル</param>
        /// <returns></returns>
        internal void UpdateByDifference(
#if DEBUG
            string hint,
#endif
            TileIdOrEmpty? tileIdOrEmpty = null,
            RectangleInt? rectangleInt = null,
            TileTitle? tileTitle = null)
        {
            if (!(tileIdOrEmpty is null))
            {
                this.Id = tileIdOrEmpty;
            }

            this.SetRectangle(rectangleInt);

            if (!(tileTitle is null))
            {
                this.Title = tileTitle;
            }

            //#if DEBUG
            //            Trace.WriteLine($"[TileRecordVisually.cs FromModel] this.Dump(): {this.Dump()}, hint: {hint}");
            //#else
            //            Trace.WriteLine($"[TileRecordVisually.cs FromModel] this.Dump(): {this.Dump()}");
            //#endif
        }

        #region メソッド（［作業画像］の矩形再計算）
        /// <summary>
        ///     ［作業画像］の矩形再計算
        /// </summary>
        internal RectangleFloat GetRefreshWorkingRectangle(Zoom zoom)
        {
            return new RectangleFloat(
                location: new PointFloat(
                    x: this.TileRecord.Rectangle.Location.X.ToFloat(),
                    y: this.TileRecord.Rectangle.Location.Y.ToFloat()),
                size: new SizeFloat(
                    width: this.TileRecord.Rectangle.Size.Width.ToFloat(),
                    height: this.TileRecord.Rectangle.Size.Height.ToFloat())).Multiplicate(zoom);
        }
        #endregion
    }
}
