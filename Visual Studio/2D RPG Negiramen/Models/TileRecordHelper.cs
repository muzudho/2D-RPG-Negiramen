namespace _2D_RPG_Negiramen.Models;

using _2D_RPG_Negiramen.Models.Geometric;

class TileRecordHelper
{
    // - インターナル・メソッド

    #region メソッド（［作業画像］の矩形再計算）
    /// <summary>
    ///     ［作業画像］の矩形再計算
    /// </summary>
    internal RectangleFloat GetRefreshWorkingRectangle(TileRecord tileRecord, Zoom zoom)
    {
        return new RectangleFloat(
            location: new PointFloat(
                x: tileRecord.Rectangle.Location.X.ToFloat(),
                y: tileRecord.Rectangle.Location.Y.ToFloat()),
            size: new SizeFloat(
                width: tileRecord.Rectangle.Size.Width.ToFloat(),
                height: tileRecord.Rectangle.Size.Height.ToFloat())).Multiplicate(zoom);
    }
    #endregion
}
