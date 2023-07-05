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
            if (App.SelectingOnPointingDevice)
            {
                // 選択中
                canvas.StrokeColor = new Color(0, 0, 255, 127);
            }
            else
            {
                // 確定時
                // canvas.StrokeColor = Colors.Red;
                canvas.StrokeColor = Colors.Blue;
            }


            // タイル・カーソルの線の太さの半分
            int halfThicknessOfLineAsInt = App.HalfThicknessOfTileCursorLine.AsInt;

            // タイル・カーソルの線の太さ
            Models.ThicknessOfLine thickness = new Models.ThicknessOfLine(2 * halfThicknessOfLineAsInt);
            canvas.StrokeSize = thickness.AsInt;

            // タイル・カーソルのサイズ
            Models.Size tileCursorSize = App.WorkingTileCursorSize;

            // キャンバス・サイズいっぱいにタイル・カーソルを描画
            canvas.DrawRectangle(new Rect(
                // タイル・カーソルの位置を調整するのは、キャンバス自体の位置を動かすこと
                // 端が切れないように、線の太さの半分をずらして描画
                halfThicknessOfLineAsInt,
                halfThicknessOfLineAsInt,
                // 境界線上ではなく、境界線に外接するように描くために、線の太さの半分をずらして描画
                tileCursorSize.Width.AsInt + thickness.AsInt,
                tileCursorSize.Height.AsInt + thickness.AsInt));
        }
    }
}
