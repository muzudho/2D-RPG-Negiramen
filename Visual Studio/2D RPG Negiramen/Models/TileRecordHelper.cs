namespace _2D_RPG_Negiramen.Models;

using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;

/// <summary>
///     ヘルパー
/// </summary>
static class TileRecordHelper
{
    // - インターナル静的メソッド

    /// <summary>
    ///     矩形の置換
    /// </summary>
    /// <param name="source">元オブジェクト</param>
    /// <param name="rect">タイトル</param>
    /// <returns></returns>
    internal static TileRecord ReplaceRectangle(TileRecord source, TheGeometric.RectangleInt rect)
    {
        return new TileRecord(
            id: source.Id,
            rect: rect,
            title: source.Title);
    }

    /// <summary>
    ///     Ｉｄの置換
    /// </summary>
    /// <param name="source">元オブジェクト</param>
    /// <param name="rect">タイトル</param>
    /// <returns></returns>
    internal static TileRecord ReplaceId(TileRecord source, TileIdOrEmpty id)
    {
        return new TileRecord(
            id: id,
            rect: source.Rectangle,
            title: source.Title);
    }

    /// <summary>
    ///     タイトルの置換
    /// </summary>
    /// <param name="source">元オブジェクト</param>
    /// <param name="rect">タイトル</param>
    /// <returns></returns>
    internal static TileRecord ReplaceTitle(TileRecord source, TileTitle title)
    {
        return new TileRecord(
            id: source.Id,
            rect: source.Rectangle,
            title: title);
    }

    #region メソッド（［作業画像］の矩形再計算）
    /// <summary>
    ///     ［作業画像］の矩形再計算
    /// </summary>
    internal static TheGeometric.RectangleFloat GetRefreshWorkingRectangle(TileRecord tileRecord, TheGeometric.Zoom zoom)
    {
        return new TheGeometric.RectangleFloat(
            location: new TheGeometric.PointFloat(
                x: tileRecord.Rectangle.Location.X.ToFloat(),
                y: tileRecord.Rectangle.Location.Y.ToFloat()),
            size: new TheGeometric.SizeFloat(
                width: tileRecord.Rectangle.Size.Width.ToFloat(),
                height: tileRecord.Rectangle.Size.Height.ToFloat())).Multiplicate(zoom);
    }
    #endregion

    #region メソッド（ダンプ）
    /// <summary>
    ///     ダンプ
    /// </summary>
    /// <returns></returns>
    internal static string Dump(TileRecord tileRecord, TheGeometric.Zoom zoom)
    {
        return $"{tileRecord.Dump()}, WorkingRect: {TileRecordHelper.GetRefreshWorkingRectangle(
            tileRecord: tileRecord,
            zoom: zoom).Dump()}";
    }
    #endregion
}
