namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     座標ヘルパー
    /// </summary>
    static class CoordinateHelper
    {
        // - インターナル・メソッド

        /// <summary>
        /// カーソルの矩形を算出
        /// </summary>
        internal static Models.Rectangle GetCursorRectangle(
            Models.Point startPoint,
            Models.Point endPoint)
        {
            // タイル・カーソルの始点Ｂ位置
            var begin = Models.CoordinateHelper.TranslateTappedPointToTileCursorPoint(
                tapped: startPoint,
                gridLeftTop: App.WorkingGridLeftTop,
                gridTile: App.WorkingGridTileSize);

            // タイル・カーソルの終点Ｅ位置
            var end = Models.CoordinateHelper.TranslateTappedPointToTileCursorPoint(
                tapped: endPoint,
                gridLeftTop: App.WorkingGridLeftTop,
                gridTile: App.WorkingGridTileSize);

            Models.Rectangle rect;

            // 始点Ｓと終点Ｅの位置関係
            if (end.X < begin.X && end.Y < begin.Y)
            {
                //
                //  ┌─┬─┐
                //  │Ｅ│　│
                //  ├─┼─┤
                //  │　│Ｂ│
                //  └─┴─┘
                //
                rect = new Models.Rectangle(
                    end,
                    new Models.Size(
                        new Models.Width(begin.X.AsInt - end.X.AsInt + App.WorkingGridTileSize.Width.AsInt),
                        new Models.Height(begin.Y.AsInt - end.Y.AsInt + App.WorkingGridTileSize.Height.AsInt)));
            }
            else if (begin.X <= end.X && end.Y < begin.Y)
            {
                //
                //  ┌─┬─┐
                //  │　│Ｅ│
                //  ├─┼─┤
                //  │Ｂ│　│
                //  └─┴─┘
                //
                rect = new Models.Rectangle(
                    new Models.Point(begin.X,end.Y),
                    new Models.Size(
                        new Models.Width(end.X.AsInt - begin.X.AsInt + App.WorkingGridTileSize.Width.AsInt),
                        new Models.Height(begin.Y.AsInt - end.Y.AsInt + App.WorkingGridTileSize.Height.AsInt)));
            }
            else
            {
                // その他
                rect = new Models.Rectangle(
                    begin,
                    new Models.Size(
                        new Models.Width(end.X.AsInt - begin.X.AsInt + App.WorkingGridTileSize.Width.AsInt),
                        new Models.Height(end.Y.AsInt - begin.Y.AsInt + App.WorkingGridTileSize.Height.AsInt)));
            }


            return rect;
        }

        /// <summary>
        ///     <pre>
        ///         タップした位置を、タイル・カーソルの位置へ変換する
        ///     
        ///         時間の経過や、回転運動を扱うわけでもないから、整数として扱う
        ///     </pre>
        /// </summary>
        /// <param name="tapped">タップした位置</param>
        /// <param name="gridTile">グリッド・タイルのサイズ</param>
        internal static Models.Point TranslateTappedPointToTileCursorPoint(
            Models.Point tapped,
            Models.Point gridLeftTop,
            Models.Size gridTile)
        {
            // 下図の 0 が原点、 1 がグリッドの左上位置とする
            //
            //  0 ---- +
            //  |      |
            //  |      |
            //  + ---- 1 ---- + ---- +
            //         |      |      |
            //         |      |      |
            //         + ---- + ---- +
            //         |      |      |
            //         |      |      |
            //         + ---- + ---- +
            //
            // ここで、 1 が無い状態にする
            tapped = new Models.Point(
                new Models.X(tapped.X.AsInt - gridLeftTop.X.AsInt),
                new Models.Y(tapped.Y.AsInt - gridLeftTop.Y.AsInt));

            // 下図の 0 が原点、 1 が任意の点、 2 が任意の点が含まれるタイルの左上隅だとする
            //
            //  0 ---- + ---- +
            //  |      |      |
            //  |      |      |
            //  + ---- 2 ---- +
            //  |      |      |
            //  |      |  1   |
            //  + ---- + ---- +
            //
            // ここで、 1 を、 2 へ丸める
            tapped = new Models.Point(
                new Models.X(tapped.X.AsInt / gridTile.Width.AsInt * gridTile.Width.AsInt),
                new Models.Y(tapped.Y.AsInt / gridTile.Height.AsInt * gridTile.Height.AsInt));

            // 下図の 0 が原点、 1 がグリッドの左上位置とする
            //
            //  0 ---- +
            //  |      |
            //  |      |
            //  + ---- 1 ---- + ---- +
            //         |      |      |
            //         |      |      |
            //         + ---- + ---- +
            //         |      |      |
            //         |      |      |
            //         + ---- + ---- +
            //
            // ここで、 1 が有る状態にする
            tapped = new Models.Point(
                new Models.X(tapped.X.AsInt + App.WorkingGridLeftTop.X.AsInt),
                new Models.Y(tapped.Y.AsInt + App.WorkingGridLeftTop.Y.AsInt));

            // タイル・カーソルの位置を返却
            return tapped;
        }
    }
}
