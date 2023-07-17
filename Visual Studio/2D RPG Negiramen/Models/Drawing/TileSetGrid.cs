﻿namespace _2D_RPG_Negiramen.Models.Drawing
{
    using Microsoft.Maui.Graphics;
    using System.Diagnostics;

    /// <summary>
    ///     😁 タイルセット用のグリッド
    /// </summary>
    internal class TilesetGrid : BindableObject, IDrawable
    {
        // - パブリック束縛可能プロパティ

        #region 束縛可能プロパティ（グリッドの線の太さの半分）
        /// <summary>
        /// グリッドの線の太さの半分
        /// </summary>
        public int HalfThicknessOfGridLineAsInt
        {
            get => (int)GetValue(HalfThicknessOfGridLineAsIntProperty);
            set => SetValue(HalfThicknessOfGridLineAsIntProperty, value);
        }

        /// <summary>
        /// グリッドの線の太さの半分
        /// </summary>
        public static BindableProperty HalfThicknessOfGridLineAsIntProperty = BindableProperty.Create(
            // プロパティ名
            propertyName: nameof(HalfThicknessOfGridLineAsInt),
            // 返却型
            returnType: typeof(int),
            // これを含んでいるクラス
            declaringType: typeof(TilesetGrid));
        #endregion

        #region 束縛可能プロパティ（グリッド位相の左上表示位置）
        /// <summary>
        ///     グリッド位相の左上表示位置
        /// </summary>
        public Models.Point GridPhase
        {
            get => (Models.Point)GetValue(GridPhaseProperty);
            set => SetValue(GridPhaseProperty, value);
        }

        /// <summary>
        /// グリッド位相の左上表示位置
        /// </summary>
        public static BindableProperty GridPhaseProperty = BindableProperty.Create(
            // プロパティ名
            propertyName: nameof(GridPhase),
            // 返却型
            returnType: typeof(Models.Point),
            // これを含んでいるクラス
            declaringType: typeof(TilesetGrid),
            // ヌルだと不具合が出る
            defaultValue: Models.Point.Empty);
        #endregion

        #region 束縛可能プロパティ（グリッド・タイル・サイズ）
        /// <summary>
        ///     グリッド・タイル・サイズ
        /// </summary>
        public Models.Size GridTileSize
        {
            get => (Models.Size)GetValue(GridTileSizeProperty);
            set => SetValue(GridTileSizeProperty, value);
        }

        /// <summary>
        ///     グリッド・タイル・サイズ
        /// </summary>
        public static BindableProperty GridTileSizeProperty = BindableProperty.Create(
            // プロパティ名
            propertyName: nameof(GridTileSize),
            // 返却型
            returnType: typeof(Models.Size),
            // これを含んでいるクラス
            declaringType: typeof(TilesetGrid),
            // ヌルだと不具合が出る
            defaultValue: Models.Size.Empty);
        #endregion

        // - パブリック・メソッド

        /// <summary>
        ///     描画
        /// </summary>
        /// <param name="canvas">キャンバス</param>
        /// <param name="dirtyRect">矩形</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            Trace.WriteLine($"[TilesetGrid Draw] this.HalfThicknessOfGridLineAsInt: {this.HalfThicknessOfGridLineAsInt}, this.SourceGridPhase: {this.GridPhase.Dump()}, this.SourceGridTileSize: {this.GridTileSize.Dump()}");

            // 線の色
            canvas.StrokeColor = new Color(255, 0, 0, 127);

            // グリッドの線の太さの半分
            int halfThicknessOfLineAsInt = this.HalfThicknessOfGridLineAsInt;

            // グリッドの線の太さ
            Models.ThicknessOfLine lineThickness = new Models.ThicknessOfLine(2 * halfThicknessOfLineAsInt);
            canvas.StrokeSize = lineThickness.AsInt;

            // グリッド位相の左上表示位置
            int gridPhaseLeftAsInt = this.GridPhase.X.AsInt;
            int gridPhaseTopAsInt = this.GridPhase.Y.AsInt;

            // グリッド・タイル・サイズ
            Models.Size gridTileSize = this.GridTileSize;

            // キャンバス・サイズ
            var canvasWidth = (int)dirtyRect.Width;
            var canvasHeight = (int)dirtyRect.Height;

            //
            // 縦線を引いていこう
            // ==================
            //
            // 線に太さが無いと無限ループするので、防止
            if (0 < gridTileSize.Width.AsInt)
            {
                int y1 = halfThicknessOfLineAsInt + gridPhaseTopAsInt;
                int y2 = canvasHeight + halfThicknessOfLineAsInt + gridPhaseTopAsInt;
                for (var x = halfThicknessOfLineAsInt + gridPhaseLeftAsInt; x < canvasWidth + lineThickness.AsInt + gridPhaseLeftAsInt; x += gridTileSize.Width.AsInt)
                {
                    canvas.DrawLine(x, y1, x, y2);
                }
            }

            //
            // 横線を引いていこう
            // ==================
            //
            // 線に太さが無いと無限ループするので、防止
            if (0 < gridTileSize.Height.AsInt)
            {
                int x1 = halfThicknessOfLineAsInt + gridPhaseLeftAsInt;

                // CANCEL CODE: 横幅が偶数なら横幅を +1、奇数なら横幅を -1 するという TRICK CODE が別の箇所にあるので、
                //              imageWidth は +1 したり、 -1 したり振動している。これはつらい。
                //              そこで、右辺にもグリッドの線があるから　端まで線を引かなくていいことを利用し
                //              右辺の線の手前まで線を引くようにする
                int x2 = canvasWidth - halfThicknessOfLineAsInt + gridPhaseLeftAsInt;

                for (var y = halfThicknessOfLineAsInt + gridPhaseTopAsInt; y < canvasHeight + lineThickness.AsInt + gridPhaseTopAsInt; y += gridTileSize.Height.AsInt)
                {
                    canvas.DrawLine(x1, y, x2, y);
                }
            }
        }
    }
}
