using System.Diagnostics;

namespace _2D_RPG_Negiramen.Views;

public partial class TilePalettePage : ContentPage
{
	public TilePalettePage()
	{
		InitializeComponent();
	}

    private void PointerGestureRecognizer_PointerMoved(object sender, PointerEventArgs e)
    {
        Trace.WriteLine("[TilePalettePage PointerGestureRecognizer_PointerMoved]");
        Trace.WriteLine($"[TilePalettePage PointerGestureRecognizer_PointerMoved] e.GetPosition((Element)s).Value.X = {e.GetPosition((Element)sender).Value.X}");
        Trace.WriteLine($"[TilePalettePage PointerGestureRecognizer_PointerMoved] e.GetPosition((Element)s).Value.Y = {e.GetPosition((Element)sender).Value.Y}");
    }
}