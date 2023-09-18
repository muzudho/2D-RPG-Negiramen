namespace _2D_RPG_Negiramen.Models.Drawing
{
    using Microsoft.Maui.Graphics;
    using System.Diagnostics;

    /// <summary>
    ///     <pre>
    ///         😁 タイル上のカーソル
    /// 
    ///         📖 [.net MAUI: how to draw on canvas](https://stackoverflow.com/questions/71001039/net-maui-how-to-draw-on-canvas)
    ///     </pre>
    /// </summary>
    internal class CroppedCursor : BindableObject, IDrawable
    {
        // - パブリック束縛可能プロパティ

        #region 束縛可能プロパティ（切抜きカーソルの線の半分の太さ）
        /// <summary>
        ///     切抜きカーソルの線の半分の太さ
        /// </summary>
        public ThicknessOfLine TileCursor_HalfThicknessOfLine
        {
            get => (ThicknessOfLine)GetValue(TileCursor_HalfThicknessOfLineProperty);
            set => SetValue(TileCursor_HalfThicknessOfLineProperty, value);
        }

        /// <summary>
        ///     <see cref="TileCursor_HalfThicknessOfLine"/> に対応
        /// </summary>
        public static BindableProperty TileCursor_HalfThicknessOfLineProperty = BindableProperty.Create(
            // プロパティ名
            propertyName: nameof(TileCursor_HalfThicknessOfLine),
            // 返却型
            returnType: typeof(ThicknessOfLine),
            // これを含んでいるクラス
            declaringType: typeof(TilesetGrid),
            defaultValue: ThicknessOfLine.Min);
        #endregion

        #region 束縛可能プロパティ（［切抜きカーソル］ズーム済み　関連）
        /// <summary>
        ///     ［切抜きカーソル］ズーム済みのサイズ
        ///     
        ///     <list type="bullet">
        ///         <item>切抜きカーソルの線の太さを含まない</item>
        ///     </list>
        /// </summary>
        public Geometric.SizeFloat CroppedCursorSize
        {
            get => (Geometric.SizeFloat)GetValue(CroppedCursorSizeProperty);
            set => SetValue(CroppedCursorSizeProperty, value);
        }

        /// <summary>
        ///     <see cref="CroppedCursorSize"/> に対応
        /// </summary>
        public static BindableProperty CroppedCursorSizeProperty = BindableProperty.Create(
            // プロパティ名
            propertyName: nameof(CroppedCursorSize),
            // 返却型
            returnType: typeof(Geometric.SizeFloat),
            // これを含んでいるクラス
            declaringType: typeof(TilesetGrid),
            defaultValue: Geometric.SizeFloat.Zero);
        #endregion

        #region 束縛可能プロパティ（ポインティング・デバイス押下中か？）
        /// <summary>
        ///     ポインティング・デバイス押下中か？
        ///     
        ///     <list type="bullet">
        ///         <item>マウスじゃないと思うけど</item>
        ///     </list>
        /// </summary>
        public bool IsMouseDragging
        {
            get => (bool)GetValue(IsMouseDraggingProperty);
            set => SetValue(IsMouseDraggingProperty, value);
        }

        /// <summary>
        ///     ポインティング・デバイス押下中か？
        /// </summary>
        public static BindableProperty IsMouseDraggingProperty = BindableProperty.Create(
            // プロパティ名
            propertyName: nameof(IsMouseDragging),
            // 返却型
            returnType: typeof(bool),
            // これを含んでいるクラス
            declaringType: typeof(TilesetGrid));
        #endregion

        // - パブリック・メソッド

        /// <summary>
        ///     描画
        /// </summary>
        /// <param name="canvas">キャンバス</param>
        /// <param name="dirtyRect">位置とサイズ</param>
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (this.CroppedCursorSize.IsEmpty)
            {
                // カーソルが無いケース
                // Trace.WriteLine($"[TileCursor Draw] 切抜きカーソルに大きさが無いから描画しない");
                return;
            }

            //try
            //{
            //    Trace.WriteLine($"[TileCursor Draw] this.IsMouseDragging: {this.IsMouseDragging}, this.HalfThicknessOfTileCursorLine.AsInt: {this.HalfThicknessOfTileCursorLine.AsInt}, this.WorkingSelectedTileSize: {this.WorkingSelectedTileSize.Dump()}");
            //}
            //catch
            //{
            //    return;
            //}

            // 線の色
            if (this.IsMouseDragging)
            {
                // 選択中
                canvas.StrokeColor = new Color(0, 0, 255, 95);
                // Trace.WriteLine("[TileCursor Draw] 半透明の青いカーソル");
            }
            else
            {
                // 確定時
                canvas.StrokeColor = Colors.Blue;
                // Trace.WriteLine("[TileCursor Draw] 青いカーソル");
            }


            // タイル・カーソルの線の太さの半分
            int halfThicknessOfLineAsInt = this.TileCursor_HalfThicknessOfLine.AsInt;

            // タイル・カーソルの線の太さ
            var thickness = new Models.ThicknessOfLine(2 * halfThicknessOfLineAsInt);
            canvas.StrokeSize = thickness.AsInt;

            // キャンバス・サイズいっぱいにタイル・カーソルを描画
            canvas.DrawRectangle(new Rect(
                // タイル・カーソルの位置を調整するのは、キャンバス自体の位置を動かすこと
                // 端が切れないように、線の太さの半分をずらして描画
                halfThicknessOfLineAsInt,
                halfThicknessOfLineAsInt,
                // 境界線上ではなく、境界線に外接するように描くために、線の太さの半分をずらして描画
                this.CroppedCursorSize.Width.AsFloat + thickness.AsInt,
                this.CroppedCursorSize.Height.AsFloat + thickness.AsInt));
        }
    }
}
