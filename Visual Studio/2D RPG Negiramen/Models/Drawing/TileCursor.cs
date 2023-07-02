﻿namespace _2D_RPG_Negiramen.Models.Drawing
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

            // グリッド・タイル・サイズ
            Models.Size gridTileSize = App.WorkingGridTileSize;

            // キャンバス・サイズいっぱいにタイル・カーソルを描画
            canvas.DrawRectangle(new Rect(
                // タイル・カーソルの位置を調整するのは、キャンバス自体の位置を動かすこと
                // 端が切れないように、線の太さの半分をずらして描画
                halfThicknessOfLineAsInt,
                halfThicknessOfLineAsInt,
                // 境界線上ではなく、境界線に外接するように描くために、線の太さの半分をずらして描画
                gridTileSize.Width.AsInt + thickness.AsInt,
                gridTileSize.Height.AsInt + thickness.AsInt));
        }
    }
}
