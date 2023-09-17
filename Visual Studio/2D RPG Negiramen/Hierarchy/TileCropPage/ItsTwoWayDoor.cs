namespace _2D_RPG_Negiramen.Hierarchy.TileCropPage;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.Geometric;
using _2D_RPG_Negiramen.Models.Visually;
using _2D_RPG_Negiramen.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Maui.Controls;
using System.Diagnostics;
using TheFileEntryLocations = Models.FileEntries.Locations;
using TheGraphics = Microsoft.Maui.Graphics;
using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;
using System.Globalization;

#if IOS || ANDROID || MACCATALYST
    using Microsoft.Maui.Graphics.Platform;
#elif WINDOWS
using Microsoft.Maui.Graphics.Win2D;
using System.Net;
#endif

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
