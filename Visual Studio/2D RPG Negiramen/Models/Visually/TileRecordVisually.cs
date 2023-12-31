﻿namespace _2D_RPG_Negiramen.Models.Visually
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
                Title = tileRecord.Title,
                LogicalDelete = tileRecord.LogicalDelete,
            };

#if DEBUG
            Trace.WriteLine($"[TileRecordVisually.cs FromModel] tileVisually.Dump(): {tileVisually.Dump()}, hint: {hint}");
#else
            Trace.WriteLine($"[TileRecordVisually.cs FromModel] tileVisually.Dump(): {tileVisually.Dump()}");
#endif

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

                // ［作業画像］の矩形再計算
                this.RefreshWorkingRectangle();
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
        internal TileTitle Title { get; set; } = TileTitle.Empty;
        #endregion

        #region プロパティ（論理削除）
        /// <summary>
        ///     論理削除
        ///     
        ///     <list type="bullet">
        ///         <item>しないなら 0、 するなら 1</item>
        ///     </list>
        /// </summary>
        internal LogicalDelete LogicalDelete
        {
            get => this.logicalDelete;
            set
            {
                if (this.logicalDelete == value)
                    return;

                this.logicalDelete = value;
                Trace.WriteLine($"[TileRecordVisually.cs LogicalDelete] this.logicalDelete.AsBool: {this.logicalDelete.AsBool}");
            }
        }
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
                this.RefreshWorkingRectangle();
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
            return $"Id: {Id.AsBASE64}, IsNone: {this.IsNone}, SourceRect: {SourceRectangle.Dump()}, WorkingRect: {WorkingRectangle.Dump()}, Title: {Title.AsStr}, LogicalDelete: {LogicalDelete.AsBool}";
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
            LogicalDelete? logicalDelete = null,
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

            if (!(logicalDelete is null))
            {
                this.LogicalDelete = logicalDelete;
            }

            if(!(zoom is null))
            {
                this.Zoom = zoom;
            }

#if DEBUG
            Trace.WriteLine($"[TileRecordVisually.cs FromModel] this.Dump(): {this.Dump()}, hint: {hint}");
#else
            Trace.WriteLine($"[TileRecordVisually.cs FromModel] this.Dump(): {this.Dump()}");
#endif
        }

        // - プライベート・フィールド

        Zoom zoom = Zoom.IdentityElement;
        TheGeometric.RectangleInt sourceRectangle = RectangleInt.Empty;
        TheGeometric.RectangleFloat workingRectangle = RectangleFloat.Empty;
        LogicalDelete logicalDelete = LogicalDelete.False;

        // - プライベート・メソッド

        #region メソッド（［作業画像］の矩形再計算）
        /// <summary>
        ///     ［作業画像］の矩形再計算
        /// </summary>
        void RefreshWorkingRectangle()
        {
            this.workingRectangle = new RectangleFloat(
                location: new PointFloat(
                    x: this.SourceRectangle.Location.X.ToFloat(),
                    y: this.SourceRectangle.Location.Y.ToFloat()),
                size: new SizeFloat(
                    width: this.SourceRectangle.Size.Width.ToFloat(),
                    height: this.SourceRectangle.Size.Height.ToFloat())).Multiplicate(this.Zoom);
        }
        #endregion
    }
}
