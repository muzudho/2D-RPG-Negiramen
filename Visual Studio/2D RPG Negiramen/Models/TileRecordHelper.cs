namespace _2D_RPG_Negiramen.Models;

using _2D_RPG_Negiramen.Models.Geometric;

/// <summary>
///     ヘルパー
/// </summary>
static class TileRecordHelper
{
    // - インターナル静的メソッド

    #region メソッド（［作業画像］の矩形再計算）
    /// <summary>
    ///     ［作業画像］の矩形再計算
    /// </summary>
    internal static RectangleFloat GetRefreshWorkingRectangle(TileRecord tileRecord, Zoom zoom)
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

    #region メソッド（ダンプ）
    /// <summary>
    ///     ダンプ
    /// </summary>
    /// <returns></returns>
    internal static string Dump(TileRecord tileRecord, Zoom zoom)
    {
        return $"{tileRecord.Dump()}, WorkingRect: {TileRecordHelper.GetRefreshWorkingRectangle(
            tileRecord: tileRecord,
            zoom: zoom).Dump()}";
    }
    #endregion
}
