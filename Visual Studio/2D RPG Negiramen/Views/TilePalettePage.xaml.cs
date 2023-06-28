using _2D_RPG_Negiramen.ViewModels;
using System.Diagnostics;

namespace _2D_RPG_Negiramen.Views;

public partial class TilePalettePage : ContentPage
{
    // - ÇªÇÃëº

    /// <summary>
    /// ê∂ê¨
    /// </summary>
	public TilePalettePage()
	{
		InitializeComponent();
	}

    // - ÉÅÉ\ÉbÉh

    private void PointerGestureRecognizer_PointerMoved(object sender, PointerEventArgs e)
    {
        Trace.WriteLine("[TilePalettePage PointerGestureRecognizer_PointerMoved]");
        //Trace.WriteLine($"[TilePalettePage PointerGestureRecognizer_PointerMoved] e.GetPosition((Element)s).Value.X = {e.GetPosition((Element)sender).Value.X}");
        //Trace.WriteLine($"[TilePalettePage PointerGestureRecognizer_PointerMoved] e.GetPosition((Element)s).Value.Y = {e.GetPosition((Element)sender).Value.Y}");
        //Trace.WriteLine($"[TilePalettePage PointerGestureRecognizer_PointerMoved] this.BindingContext.GetType().Name = {this.BindingContext.GetType().Name}");
        Trace.WriteLine($"[TilePalettePage PointerGestureRecognizer_PointerMoved] sender.GetType().Name = {sender.GetType().Name}");

        TilePalettePageViewModel context = (TilePalettePageViewModel)this.BindingContext;

        Image image = (Image)sender;
        context.XAsInt = (int)e.GetPosition(image).Value.X;
        context.YAsInt = (int)e.GetPosition(image).Value.Y;
    }
}