namespace _2D_RPG_Negiramen.Models.Drawing
{
    using Microsoft.Maui.Graphics;

    /// <summary>
    ///     😁 タイル・セット用のグリッド
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

            // グリッドの線の太さの半分
            var gridLineHalfThin = 1;
            // グリッドの線の太さ
            var gridLineThin = 2 * gridLineHalfThin;
            canvas.StrokeSize = gridLineThin;

            // マージン
            var marginLeft = 0;
            var marginTop = 0;

            // タイル・サイズ
            var tileWidth = 32;
            var tileHeight = 32;

            // 画像サイズ
            var imageWidth = (int)dirtyRect.Width;
            var imageHeight = (int)dirtyRect.Height;

            // 縦線を引いていこう
            int y1 = marginTop + gridLineHalfThin;
            int y2 = imageHeight + marginTop + gridLineHalfThin;
            for (var x = gridLineHalfThin; x < imageWidth+gridLineThin; x += tileWidth)
            {
                canvas.DrawLine(x, y1, x, y2);
            }

            // 横線を引いていこう
            int x1 = marginLeft + gridLineHalfThin;
            int x2 = imageHeight + marginLeft + gridLineHalfThin;
            for (var y = gridLineHalfThin; y < imageHeight + gridLineThin; y += tileHeight)
            {
                canvas.DrawLine(x1, y, x2, y);
            }
        }
    }
}
