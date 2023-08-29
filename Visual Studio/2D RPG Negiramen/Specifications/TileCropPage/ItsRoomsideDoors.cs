namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.Models.Geometric;

/// <summary>
///     各部屋
///     
///     <list type="bullet">
///         <item>各部屋（ページの各要素）を持つ</item>
///     </list>
/// </summary>
internal class ItsRoomsideDoors
{
    internal ItsRoomsideDoors(ItsCorridor corridor)
    {
        this.IndoorCultureInfo = new InnerCultureInfo(corridor, corridor);
        this.Zoom = new InnerZoom(corridor, corridor);
    }

    /// <summary>文化情報</summary>
    internal InnerCultureInfo IndoorCultureInfo { get; }

    /// <summary>ズーム</summary>
    internal InnerZoom Zoom { get; }

    public Zoom ZoomValue
    {
        get
        {
            return this.Zoom.Value;
        }
        set
        {
            this.Zoom.Value = value;
        }
    }
}
