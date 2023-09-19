namespace _2D_RPG_Negiramen.Models.Visually
{
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.Geometric;
    using CommunityToolkit.Mvvm.ComponentModel;
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
        internal static TileRecordVisually CreateEmpty() => new TileRecordVisually();

        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="tileRecord">タイル</param>
        /// <returns></returns>
        public static TileRecordVisually FromModel(
            TileRecord tileRecord,
            Zoom zoom
#if DEBUG
            , string hint
#endif
            )
        {
            var tileVisually = new TileRecordVisually()
            {
                Id = tileRecord.Id,
                SourceRectangle = tileRecord.Rectangle,
                Zoom = zoom,
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
        TileRecordVisually()
        {
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

        #region プロパティ（［元画像］　関連）
        /// <summary>
        ///     ［元画像］の矩形
        /// </summary>
        internal TheGeometric.RectangleInt SourceRectangle
        {
            get => this.sourceRectangle;
            set
            {
                if (this.sourceRectangle == value)
                    return;

                this.sourceRectangle = value;
            }
        }
        #endregion

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
        internal bool IsNone => SourceRectangle.RightAsInt - SourceRectangle.LeftAsInt < 1 && SourceRectangle.BottomAsInt - SourceRectangle.TopAsInt < 1;
        #endregion

        #region プロパティ（ズーム）
        /// <summary>
        ///     ズーム
        /// </summary>
        internal Zoom Zoom
        {
            get => this.zoom;
            set
            {
                if (this.zoom == value)
                    return;

                this.zoom = value;
            }
        }
        #endregion

        // - インターナル・メソッド

        #region メソッド（ダンプ）
        /// <summary>
        ///     ダンプ
        /// </summary>
        /// <returns></returns>
        internal string Dump()
        {
            return $"Id: {Id.AsBASE64}, IsNone: {this.IsNone}, SourceRect: {SourceRectangle.Dump()}, WorkingRect: {this.GetRefreshWorkingRectangle().Dump()}, Title: {Title.AsStr}";
        }
        #endregion

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
            TileTitle? tileTitle = null,
            Zoom? zoom = null)
        {
            if (!(tileIdOrEmpty is null))
            {
                this.Id = tileIdOrEmpty;
            }

            if (!(rectangleInt is null))
            {
                this.SourceRectangle = rectangleInt;
            }

            if (!(tileTitle is null))
            {
                this.Title = tileTitle;
            }

            if(!(zoom is null))
            {
                this.Zoom = zoom;
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
        internal RectangleFloat GetRefreshWorkingRectangle()
        {
            return new RectangleFloat(
                location: new PointFloat(
                    x: this.SourceRectangle.Location.X.ToFloat(),
                    y: this.SourceRectangle.Location.Y.ToFloat()),
                size: new SizeFloat(
                    width: this.SourceRectangle.Size.Width.ToFloat(),
                    height: this.SourceRectangle.Size.Height.ToFloat())).Multiplicate(this.Zoom);
        }
        #endregion

        // - プライベート・フィールド

        Zoom zoom = Zoom.IdentityElement;
        TheGeometric.RectangleInt sourceRectangle = RectangleInt.Empty;
    }
}
