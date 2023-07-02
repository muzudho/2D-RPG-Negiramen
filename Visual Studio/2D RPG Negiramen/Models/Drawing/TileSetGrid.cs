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
            int halfThicknessOfLineAsInt = App.HalfThicknessOfGridLine.AsInt;

            // グリッドの線の太さ
            Models.ThicknessOfLine lineThickness = new Models.ThicknessOfLine(2 * halfThicknessOfLineAsInt);
            canvas.StrokeSize = lineThickness.AsInt;

            // グリッド全体の左上表示位置
            var paddingLeft = App.WorkingGridLeftTop.X.AsInt;
            var paddingTop = App.WorkingGridLeftTop.Y.AsInt;

            // グリッド・タイル・サイズ
            Models.Size gridTileSize = App.WorkingGridTileSize;

            // 画像サイズ
            var imageWidth = (int)dirtyRect.Width;
            var imageHeight = (int)dirtyRect.Height;

            // 縦線を引いていこう
            int y1 = halfThicknessOfLineAsInt + paddingTop;
            int y2 = imageHeight + halfThicknessOfLineAsInt + paddingTop;
            for (var x = halfThicknessOfLineAsInt + paddingLeft; x < imageWidth + lineThickness.AsInt + paddingLeft; x += gridTileSize.Width.AsInt)
            {
                canvas.DrawLine(x, y1, x, y2);
            }

            // 横線を引いていこう
            int x1 = halfThicknessOfLineAsInt + paddingLeft;

            // CANCEL CODE: 横幅が偶数なら横幅を +1、奇数なら横幅を -1 するという TRICK CODE が別の箇所にあるので、
            //              imageWidth は +1 したり、 -1 したり振動している。これはつらい。
            //              そこで、右辺にもグリッドの線があるから　端まで線を引かなくていいことを利用し
            //              右辺の線の手前まで線を引くようにする
            int x2 = imageWidth - halfThicknessOfLineAsInt + paddingLeft;

            for (var y = halfThicknessOfLineAsInt + paddingTop; y < imageHeight + lineThickness.AsInt + paddingTop; y += gridTileSize.Height.AsInt)
            {
                canvas.DrawLine(x1, y, x2, y);
            }
        }
    }
}
