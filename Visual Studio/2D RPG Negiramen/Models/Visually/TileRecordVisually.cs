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
            Zoom zoom)
        {
            return new TileRecordVisually()
            {
                Id = tileRecord.Id,
                SourceRectangle = tileRecord.Rectangle,
                Zoom = zoom,
                Title = tileRecord.Title,
                LogicalDelete = tileRecord.LogicalDelete,
            };
        }

        /// <summary>
        ///     生成
        /// </summary>
        public TileRecordVisually()
        {
            Id = TileIdOrEmpty.Empty;
            SourceRectangle = TheGeometric.RectangleInt.Empty;
            Title = TileTitle.Empty;
            LogicalDelete = LogicalDelete.False;
            // Zoom = Zoom.IdentityElement;
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

                // ［作業画像］の矩形再計算
                this.workingRectangle = new RectangleFloat(
                    location: new PointFloat(
                        x: new XFloat(this.Zoom.AsFloat * this.SourceRectangle.Location.X.AsInt),
                        y: new YFloat(this.Zoom.AsFloat * this.SourceRectangle.Location.Y.AsInt)),
                    size: new SizeFloat(
                        width: new WidthFloat(this.Zoom.AsFloat * this.SourceRectangle.Size.Width.AsInt),
                        height: new HeightFloat(this.Zoom.AsFloat * this.SourceRectangle.Size.Height.AsInt)));
            }
        }
        #endregion

        #region プロパティ（［作業画像］　関連）
        /// <summary>
        ///     ［作業画像］の矩形（ズーム後）
        ///     
        ///     TODO ★ WorkingRectangle を止めて、ズームに置き換えたい
        /// </summary>
        internal TheGeometric.RectangleFloat WorkingRectangle => this.workingRectangle;
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

                // ［作業画像］の矩形再計算
                this.workingRectangle = new RectangleFloat(
                    location: new PointFloat(
                        x: new XFloat(this.Zoom.AsFloat * this.SourceRectangle.Location.X.AsInt),
                        y: new YFloat(this.Zoom.AsFloat * this.SourceRectangle.Location.Y.AsInt)),
                    size: new SizeFloat(
                        width: new WidthFloat(this.Zoom.AsFloat * this.SourceRectangle.Size.Width.AsInt),
                        height: new HeightFloat(this.Zoom.AsFloat * this.SourceRectangle.Size.Height.AsInt)));
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
            return $"Id:{Id.AsBASE64}, SourceRect:{SourceRectangle.Dump()}, WorkingRect:{WorkingRectangle.Dump()}, Title:{Title.AsStr}, LogicalDelete: {LogicalDelete.AsInt}";
        }
        #endregion

        // - プライベート・フィールド

        Zoom zoom = Zoom.IdentityElement;
        TheGeometric.RectangleInt sourceRectangle = RectangleInt.Empty;
        TheGeometric.RectangleFloat workingRectangle = RectangleFloat.Empty;
    }
}
