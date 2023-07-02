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
            // タイル・カーソルの座標を算出
            return new Models.Point(
                new Models.X(tapped.X.AsInt / gridTile.Width.AsInt * gridTile.Width.AsInt),
                new Models.Y(tapped.Y.AsInt / gridTile.Height.AsInt * gridTile.Height.AsInt));
        }
    }
}
