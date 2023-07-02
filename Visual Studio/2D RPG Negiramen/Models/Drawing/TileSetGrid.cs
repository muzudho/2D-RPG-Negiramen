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
            int paddingLeftAsInt = App.WorkingGridLeftTop.X.AsInt;
            int paddingTopAsInt = App.WorkingGridLeftTop.Y.AsInt;

            // グリッド・タイル・サイズ
            Models.Size gridTileSize = App.WorkingGridTileSize;

            // キャンバス・サイズ
            var canvasWidth = (int)dirtyRect.Width;
            var canvasHeight = (int)dirtyRect.Height;

            //
            // 縦線を引いていこう
            // ==================
            //
            int y1 = halfThicknessOfLineAsInt + paddingTopAsInt;
            int y2 = canvasHeight + halfThicknessOfLineAsInt + paddingTopAsInt;
            for (var x = halfThicknessOfLineAsInt + paddingLeftAsInt; x < canvasWidth + lineThickness.AsInt + paddingLeftAsInt; x += gridTileSize.Width.AsInt)
            {
                canvas.DrawLine(x, y1, x, y2);
            }

            //
            // 横線を引いていこう
            // ==================
            //
            int x1 = halfThicknessOfLineAsInt + paddingLeftAsInt;

            // CANCEL CODE: 横幅が偶数なら横幅を +1、奇数なら横幅を -1 するという TRICK CODE が別の箇所にあるので、
            //              imageWidth は +1 したり、 -1 したり振動している。これはつらい。
            //              そこで、右辺にもグリッドの線があるから　端まで線を引かなくていいことを利用し
            //              右辺の線の手前まで線を引くようにする
            int x2 = canvasWidth - halfThicknessOfLineAsInt + paddingLeftAsInt;

            for (var y = halfThicknessOfLineAsInt + paddingTopAsInt; y < canvasHeight + lineThickness.AsInt + paddingTopAsInt; y += gridTileSize.Height.AsInt)
            {
                canvas.DrawLine(x1, y, x2, y);
            }
        }
    }
}
