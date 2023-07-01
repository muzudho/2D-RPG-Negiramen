/// <summary>
///     😁 描画
/// </summary>
namespace _2D_RPG_Negiramen.Models.Drawing
{
    using Microsoft.Maui.Graphics;

    /// <summary>
    /// タイル・セット用のグリッド
    /// </summary>
    internal class TileSetGrid : IDrawable
    {
        /// <summary>
        ///     描画
        /// </summary>
        /// <param name="canvas">キャンバス</param>
        /// <param name="dirtyRect">矩形</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // 線の色
            canvas.StrokeColor = new Color(255, 0, 0, 127);

            // マージン
            var marginLeft = 0;
            var marginTop = 0;

            // タイル・サイズ
            var tileWidth = 32;
            var tileHeight = 32;

            // 画像サイズ
            var imageWidth = 64;
            var imageHeight = 64;

            // 線の太さの半分
            var halfThin = 1;
            // 線の太さ
            var thin = 2 * halfThin;
            canvas.StrokeSize = thin;

            // 縦線を引いていこう
            int y1 = marginTop + halfThin;
            int y2 = imageHeight + marginTop + halfThin;
            for (var x = halfThin; x < imageWidth+thin; x += tileWidth)
            {
                canvas.DrawLine(x, y1, x, y2);
            }

            // 横線を引いていこう
            int x1 = marginLeft + halfThin;
            int x2 = imageHeight + marginLeft + halfThin;
            for (var y = halfThin; y < imageHeight + thin; y += tileHeight)
            {
                canvas.DrawLine(x1, y, x2, y);
            }
        }
    }
}
