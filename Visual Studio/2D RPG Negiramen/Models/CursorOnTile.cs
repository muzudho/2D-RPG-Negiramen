namespace _2D_RPG_Negiramen.Models
{
    using Microsoft.Maui.Graphics;


    /// <summary>
    ///     <pre>
    ///         タイル上のカーソル
    /// 
    ///         📖 [.net MAUI: how to draw on canvas](https://stackoverflow.com/questions/71001039/net-maui-how-to-draw-on-canvas)
    ///     </pre>
    /// </summary>
    internal class CursorOnTile : IDrawable
    {
        /// <summary>
        /// 図形描画
        /// </summary>
        /// <param name="canvas">キャンバス</param>
        /// <param name="dirtyRect">矩形</param>
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 6;
            canvas.DrawLine(10, 10, 90, 100);
        }
    }
}
