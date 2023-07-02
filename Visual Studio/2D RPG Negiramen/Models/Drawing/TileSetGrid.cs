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
            var halfThicknessOfGridLine = App.HalfThicknessOfGridLine;

            // グリッドの線の太さ
            Models.ThicknessOfLine gridLineThickness = new Models.ThicknessOfLine(2 * halfThicknessOfGridLine.AsInt);
            canvas.StrokeSize = gridLineThickness.AsInt;

            // グリッド全体の左上表示位置
            var paddingLeft = App.WorkingGridLeftTop.X.AsInt;
            var paddingTop = App.WorkingGridLeftTop.Y.AsInt;

            // グリッド・タイル・サイズ
            Models.Size gridTileSize = App.WorkingGridTileSize;

            // 画像サイズ
            var imageWidth = (int)dirtyRect.Width;
            var imageHeight = (int)dirtyRect.Height;

            // 縦線を引いていこう
            int y1 = halfThicknessOfGridLine.AsInt + paddingTop;
            int y2 = imageHeight + halfThicknessOfGridLine.AsInt + paddingTop;
            for (var x = halfThicknessOfGridLine.AsInt + paddingLeft; x < imageWidth + gridLineThickness.AsInt + paddingLeft; x += gridTileSize.Width.AsInt)
            {
                canvas.DrawLine(x, y1, x, y2);
            }

            // 横線を引いていこう
            int x1 = halfThicknessOfGridLine.AsInt + paddingLeft;

            // CANCEL CODE: 横幅が偶数なら横幅を +1、奇数なら横幅を -1 するという TRICK CODE が別の箇所にあるので、
            //              imageWidth は +1 したり、 -1 したり振動している。これはつらい。
            //              そこで、右辺にもグリッドの線があるから　端まで線を引かなくていいことを利用し
            //              右辺の線の手前まで線を引くようにする
            int x2 = imageWidth - halfThicknessOfGridLine.AsInt + paddingLeft;

            for (var y = halfThicknessOfGridLine.AsInt + paddingTop; y < imageHeight + gridLineThickness.AsInt + paddingTop; y += gridTileSize.Height.AsInt)
            {
                canvas.DrawLine(x1, y, x2, y);
            }
        }
    }
}
