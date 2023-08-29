namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

/// <summary>
///     双方向ドア
/// </summary>
class ItsTwoWayDoor
{
    internal ItsTwoWayDoor(ItsCorridor corridor)
    {
        this.Corridor = corridor;
    }

    /// <summary>廊下</summary>
    ItsCorridor Corridor { get; }
}
