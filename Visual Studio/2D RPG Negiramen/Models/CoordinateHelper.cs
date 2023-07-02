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
        internal static Models.Point TranslateTappedPointToTileCursorPoint(
            int tappedX,
            int tappedY,
            int gridTileWidth,
            int gridTileHeight)
        {
            // タイル・カーソルの座標を算出
            return new Models.Point(
                new Models.X(tappedX / gridTileWidth * gridTileWidth),
                new Models.Y(tappedY / gridTileHeight * gridTileHeight));
        }
    }
}
