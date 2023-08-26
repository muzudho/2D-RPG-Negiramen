namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

/// <summary>
///     グリッド単位
/// </summary>
internal class GridUnit
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="owner"></param>
    internal GridUnit(ItsSpec owner)
    {
        this.Owner = owner;
    }
    #endregion

    // - インターナル・プロパティ

    /// <summary>
    ///     ［元画像グリッド］の単位
    /// </summary>
    internal Models.Geometric.SizeInt SourceValue { get; set; } = new(new Models.Geometric.WidthInt(32), new Models.Geometric.HeightInt(32));

    // - プライベート・プロパティ

    ItsSpec Owner { get; }
}
