namespace _2D_RPG_Negiramen.Models.Drawing
{
    using Microsoft.Maui.Graphics;


    /// <summary>
    ///     <pre>
    ///         😁 タイル上のカーソル
    /// 
    ///         📖 [.net MAUI: how to draw on canvas](https://stackoverflow.com/questions/71001039/net-maui-how-to-draw-on-canvas)
    ///     </pre>
    /// </summary>
    internal class TileCursor : IDrawable
    {
        /// <summary>
        ///     描画
        /// </summary>
        /// <param name="canvas">キャンバス</param>
        /// <param name="dirtyRect">矩形</param>
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // 線の色
            canvas.StrokeColor = Colors.Red;

            // タイル・カーソルの線の太さの半分
            int halfThicknessOfLineAsInt = App.HalfThicknessOfTileCursorLine.AsInt;

            // タイル・カーソルの線の太さ
            Models.ThicknessOfLine thickness = new Models.ThicknessOfLine(2 * halfThicknessOfLineAsInt);
            canvas.StrokeSize = thickness.AsInt;

            // 線の太さを考えて位置とサイズ指定
            canvas.DrawRectangle(new Rect(
                halfThicknessOfLineAsInt,
                halfThicknessOfLineAsInt,
                32 + thickness.AsInt,
                32 + thickness.AsInt));
        }
    }
}
