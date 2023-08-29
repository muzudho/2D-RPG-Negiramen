namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

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
}
