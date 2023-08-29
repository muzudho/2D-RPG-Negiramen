namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

/// <summary>
///     各部屋
///     
///     <list type="bullet">
///         <item>各部屋（ページの各要素）を持つ</item>
///     </list>
/// </summary>
internal class ItsRooms
{
    internal ItsRooms(ItsCorridor corridor)
    {
        this.IndoorCultureInfo = new InnerCultureInfo(corridor, corridor);
    }

    /// <summary>文化情報</summary>
    internal InnerCultureInfo IndoorCultureInfo { get; }
}
