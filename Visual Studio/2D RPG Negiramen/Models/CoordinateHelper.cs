namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     座標ヘルパー
    /// </summary>
    static class CoordinateHelper
    {
        // - インターナル・メソッド

        /// <summary>
        ///     ポインティング・デバイスの２箇所のタップ位置から、タイルの矩形を算出
        /// </summary>
        internal static Geometric.RectangleFloat GetCursorRectangle(
            Geometric.PointFloat startPoint,
            Geometric.PointFloat endPoint,
            Geometric.PointFloat gridLeftTop,
            Geometric.SizeFloat gridTile)
        {
            // タイル・カーソルの始点Ｂ位置
            var begin = Models.CoordinateHelper.TranslateTappedPointToTileCursorPoint(
                tapped: startPoint,
                gridLeftTop: gridLeftTop,
                gridTile: gridTile);

            // タイル・カーソルの終点Ｅ位置
            var end = Models.CoordinateHelper.TranslateTappedPointToTileCursorPoint(
                tapped: endPoint,
                gridLeftTop: gridLeftTop,
                gridTile: gridTile);

            Geometric.RectangleFloat rect;

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
                rect = new Models.Geometric.RectangleFloat(
                    end,
                    new Models.Geometric.SizeFloat(
                        new Models.Geometric.WidthFloat(begin.X.AsFloat - end.X.AsFloat + gridTile.Width.AsFloat),
                        new Models.Geometric.HeightFloat(begin.Y.AsFloat - end.Y.AsFloat + gridTile.Height.AsFloat)));
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
                rect = new Models.Geometric.RectangleFloat(
                    new Models.Geometric.PointFloat(begin.X, end.Y),
                    new Models.Geometric.SizeFloat(
                        new Models.Geometric.WidthFloat(end.X.AsFloat - begin.X.AsFloat + gridTile.Width.AsFloat),
                        new Models.Geometric.HeightFloat(begin.Y.AsFloat - end.Y.AsFloat + gridTile.Height.AsFloat)));
            }
            else if (end.X <= begin.X && begin.Y <= end.Y)
            {
                //
                //  ┌─┬─┐
                //  │　│Ｂ│
                //  ├─┼─┤
                //  │Ｅ│　│
                //  └─┴─┘
                //
                rect = new Models.Geometric.RectangleFloat(
                    new Models.Geometric.PointFloat(end.X, begin.Y),
                    new Models.Geometric.SizeFloat(
                        new Models.Geometric.WidthFloat(begin.X.AsFloat - end.X.AsFloat + gridTile.Width.AsFloat),
                        new Models.Geometric.HeightFloat(end.Y.AsFloat - begin.Y.AsFloat + gridTile.Height.AsFloat)));
            }
            else
            {
                // その他
                rect = new Models.Geometric.RectangleFloat(
                    begin,
                    new Models.Geometric.SizeFloat(
                        new Models.Geometric.WidthFloat(end.X.AsFloat - begin.X.AsFloat + gridTile.Width.AsFloat),
                        new Models.Geometric.HeightFloat(end.Y.AsFloat - begin.Y.AsFloat + gridTile.Height.AsFloat)));
            }

            return rect;
        }

        ///// <summary>
        /////     ポインティング・デバイスの２箇所のタップ位置から、タイルの矩形を算出
        ///// </summary>
        //internal static Geometric.RectangleInt GetCursorRectangle(
        //    Geometric.PointInt startPoint,
        //    Geometric.PointInt endPoint,
        //    Geometric.PointInt gridLeftTop,
        //    Geometric.SizeInt gridTile)
        //{
        //    // タイル・カーソルの始点Ｂ位置
        //    var begin = Models.CoordinateHelper.TranslateTappedPointToTileCursorPoint(
        //        tapped: startPoint,
        //        gridLeftTop: gridLeftTop,
        //        gridTile: gridTile);

        //    // タイル・カーソルの終点Ｅ位置
        //    var end = Models.CoordinateHelper.TranslateTappedPointToTileCursorPoint(
        //        tapped: endPoint,
        //        gridLeftTop: gridLeftTop,
        //        gridTile: gridTile);

        //    Geometric.RectangleInt rect;

        //    // 始点Ｓと終点Ｅの位置関係
        //    if (end.X < begin.X && end.Y < begin.Y)
        //    {
        //        //
        //        //  ┌─┬─┐
        //        //  │Ｅ│　│
        //        //  ├─┼─┤
        //        //  │　│Ｂ│
        //        //  └─┴─┘
        //        //
        //        rect = new Models.Geometric.RectangleInt(
        //            end,
        //            new Models.Geometric.SizeInt(
        //                new Models.Geometric.WidthInt(begin.X.AsInt - end.X.AsInt + gridTile.Width.AsInt),
        //                new Models.Geometric.HeightInt(begin.Y.AsInt - end.Y.AsInt + gridTile.Height.AsInt)));
        //    }
        //    else if (begin.X <= end.X && end.Y < begin.Y)
        //    {
        //        //
        //        //  ┌─┬─┐
        //        //  │　│Ｅ│
        //        //  ├─┼─┤
        //        //  │Ｂ│　│
        //        //  └─┴─┘
        //        //
        //        rect = new Models.Geometric.RectangleInt(
        //            new Models.Geometric.PointInt(begin.X,end.Y),
        //            new Models.Geometric.SizeInt(
        //                new Models.Geometric.WidthInt(end.X.AsInt - begin.X.AsInt + gridTile.Width.AsInt),
        //                new Models.Geometric.HeightInt(begin.Y.AsInt - end.Y.AsInt + gridTile.Height.AsInt)));
        //    }
        //    else if (end.X <= begin.X && begin.Y <= end.Y)
        //    {
        //        //
        //        //  ┌─┬─┐
        //        //  │　│Ｂ│
        //        //  ├─┼─┤
        //        //  │Ｅ│　│
        //        //  └─┴─┘
        //        //
        //        rect = new Models.Geometric.RectangleInt(
        //            new Models.Geometric.PointInt(end.X, begin.Y),
        //            new Models.Geometric.SizeInt(
        //                new Models.Geometric.WidthInt(begin.X.AsInt - end.X.AsInt + gridTile.Width.AsInt),
        //                new Models.Geometric.HeightInt(end.Y.AsInt - begin.Y.AsInt + gridTile.Height.AsInt)));
        //    }
        //    else
        //    {
        //        // その他
        //        rect = new Models.Geometric.RectangleInt(
        //            begin,
        //            new Models.Geometric.SizeInt(
        //                new Models.Geometric.WidthInt(end.X.AsInt - begin.X.AsInt + gridTile.Width.AsInt),
        //                new Models.Geometric.HeightInt(end.Y.AsInt - begin.Y.AsInt + gridTile.Height.AsInt)));
        //    }


        //    return rect;
        //}

        /// <summary>
        ///     <pre>
        ///         タップした位置を、タイル・カーソルの位置へ変換する
        ///     
        ///         時間の経過や、回転運動を扱うわけでもないから、整数として扱う
        ///     </pre>
        /// </summary>
        /// <param name="tapped">タップした位置</param>
        /// <param name="gridTile">グリッド・タイルのサイズ</param>
        internal static Geometric.PointFloat TranslateTappedPointToTileCursorPoint(
            Geometric.PointFloat tapped,
            Geometric.PointFloat gridLeftTop,
            Geometric.SizeFloat gridTile)
        {
            // 下図の A が原点、 B がグリッドの左上位置とする
            //
            //  A ---- +
            //  |      |
            //  |      |
            //  + ---- B ---- + ---- +
            //         |      |      |
            //         |      |      |
            //         + ---- + ---- +
            //         |      |      |
            //         |      |      |
            //         + ---- + ---- +
            //
            // ここで、 B が無い状態にする
            tapped = new Models.Geometric.PointFloat(
                new Models.Geometric.XFloat(tapped.X.AsFloat - gridLeftTop.X.AsFloat),
                new Models.Geometric.YFloat(tapped.Y.AsFloat - gridLeftTop.Y.AsFloat));

            // 下図の A が原点、 B が任意の点、 C が任意の点が含まれるタイルの左上隅だとする
            //
            //  A ---- + ---- +
            //  |      |      |
            //  |      |      |
            //  + ---- C ---- +
            //  |      |      |
            //  |      |  B   |
            //  + ---- + ---- +
            //
            // ここで、 B を、 C へ丸める
            tapped = new Models.Geometric.PointFloat(
                new Models.Geometric.XFloat((int)(tapped.X.AsFloat / gridTile.Width.AsFloat) * gridTile.Width.AsFloat),
                new Models.Geometric.YFloat((int)(tapped.Y.AsFloat / gridTile.Height.AsFloat) * gridTile.Height.AsFloat));

            // 下図の A が原点、 B がグリッドの左上位置とする
            //
            //  A ---- +
            //  |      |
            //  |      |
            //  + ---- B ---- + ---- +
            //         |      |      |
            //         |      |      |
            //         + ---- + ---- +
            //         |      |      |
            //         |      |      |
            //         + ---- + ---- +
            //
            // ここで、 B が有る状態にする
            tapped = new Models.Geometric.PointFloat(
                new Models.Geometric.XFloat(tapped.X.AsFloat + gridLeftTop.X.AsFloat),
                new Models.Geometric.YFloat(tapped.Y.AsFloat + gridLeftTop.Y.AsFloat));

            // タイル・カーソルの位置を返却
            return tapped;
        }

        ///// <summary>
        /////     <pre>
        /////         タップした位置を、タイル・カーソルの位置へ変換する
        /////     
        /////         時間の経過や、回転運動を扱うわけでもないから、整数として扱う
        /////     </pre>
        ///// </summary>
        ///// <param name="tapped">タップした位置</param>
        ///// <param name="gridTile">グリッド・タイルのサイズ</param>
        //internal static Geometric.PointInt TranslateTappedPointToTileCursorPoint(
        //    Geometric.PointInt tapped,
        //    Geometric.PointInt gridLeftTop,
        //    Geometric.SizeInt gridTile)
        //{
        //    // 下図の 0 が原点、 1 がグリッドの左上位置とする
        //    //
        //    //  0 ---- +
        //    //  |      |
        //    //  |      |
        //    //  + ---- 1 ---- + ---- +
        //    //         |      |      |
        //    //         |      |      |
        //    //         + ---- + ---- +
        //    //         |      |      |
        //    //         |      |      |
        //    //         + ---- + ---- +
        //    //
        //    // ここで、 1 が無い状態にする
        //    tapped = new Models.Geometric.PointInt(
        //        new Models.Geometric.XInt(tapped.X.AsInt - gridLeftTop.X.AsInt),
        //        new Models.Geometric.YInt(tapped.Y.AsInt - gridLeftTop.Y.AsInt));

        //    // 下図の 0 が原点、 1 が任意の点、 2 が任意の点が含まれるタイルの左上隅だとする
        //    //
        //    //  0 ---- + ---- +
        //    //  |      |      |
        //    //  |      |      |
        //    //  + ---- 2 ---- +
        //    //  |      |      |
        //    //  |      |  1   |
        //    //  + ---- + ---- +
        //    //
        //    // ここで、 1 を、 2 へ丸める
        //    tapped = new Models.Geometric.PointInt(
        //        new Models.Geometric.XInt(tapped.X.AsInt / gridTile.Width.AsInt * gridTile.Width.AsInt),
        //        new Models.Geometric.YInt(tapped.Y.AsInt / gridTile.Height.AsInt * gridTile.Height.AsInt));

        //    // 下図の 0 が原点、 1 がグリッドの左上位置とする
        //    //
        //    //  0 ---- +
        //    //  |      |
        //    //  |      |
        //    //  + ---- 1 ---- + ---- +
        //    //         |      |      |
        //    //         |      |      |
        //    //         + ---- + ---- +
        //    //         |      |      |
        //    //         |      |      |
        //    //         + ---- + ---- +
        //    //
        //    // ここで、 1 が有る状態にする
        //    tapped = new Models.Geometric.PointInt(
        //        new Models.Geometric.XInt(tapped.X.AsInt + gridLeftTop.X.AsInt),
        //        new Models.Geometric.YInt(tapped.Y.AsInt + gridLeftTop.Y.AsInt));

        //    // タイル・カーソルの位置を返却
        //    return tapped;
        //}
    }
}
