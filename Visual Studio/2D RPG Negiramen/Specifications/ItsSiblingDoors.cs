namespace _2D_RPG_Negiramen.Specifications;

using _2D_RPG_Negiramen.Specifications.TileCropPage;

/// <summary>
///     兄弟ドア
/// </summary>
internal class ItsSiblingDoors
{
    internal ItsSiblingDoors(ItsCorridor corridor)
    {
        this.Corridor = corridor;
    }

    /// <summary>廊下</summary>
    ItsCorridor Corridor { get; }

    /// <summary>ズーム</summary>
    internal InnerZoom Zoom
    {
        get
        {
            return this.Corridor.RoomsideDoors.Zoom;
        }
    }
}
