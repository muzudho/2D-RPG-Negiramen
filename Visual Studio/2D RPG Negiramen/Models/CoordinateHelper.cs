namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     座標ヘルパー
    /// </summary>
    static class CoordinateHelper
    {
        /// <summary>
        ///     <pre>
        ///         タップした座標を、タイル・カーソルの座標へ変換する
        ///     
        ///         時間の経過や、回転運動を扱うわけでもないから、整数として扱う
        ///     </pre>
        /// </summary>
        /// <param name="tapped">タップした座標</param>
        /// <param name="gridTile">グリッド・タイルのサイズ</param>
        internal static Models.Point TranslateTappedPointToTileCursorPoint(
            Models.Point tapped,
            Models.Size gridTile)
        {
            // 下図の 0 が原点、 1 がグリッドの左上座標とする
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
                new Models.X(tapped.X.AsInt - App.WorkingGridLeftTop.X.AsInt),
                new Models.Y(tapped.Y.AsInt - App.WorkingGridLeftTop.Y.AsInt));

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

            // 下図の 0 が原点、 1 がグリッドの左上座標とする
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

            // タイル・カーソルの座標を返却
            return tapped;
        }
    }
}
