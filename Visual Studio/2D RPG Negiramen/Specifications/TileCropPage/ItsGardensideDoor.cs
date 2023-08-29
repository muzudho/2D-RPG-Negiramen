namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.ViewModels;

/// <summary>
///     屋外側ドア
/// </summary>
internal class ItsGardensideDoor
{
    internal ItsGardensideDoor(ItsCorridor corridor)
    {
        this.Corridor = corridor;
    }

    /// <summary>廊下</summary>
    ItsCorridor Corridor { get; }

    /// <summary>全体ページ・ビューモデル</summary>
    internal TileCropPageViewModel OutdoorPageVM => this.Corridor.OutdoorPageVM

}
