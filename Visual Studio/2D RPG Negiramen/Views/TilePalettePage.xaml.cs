using _2D_RPG_Negiramen.ViewModels;
using Microsoft.Maui.Controls;
using System.Diagnostics;

namespace _2D_RPG_Negiramen.Views;

public partial class TilePalettePage : ContentPage
{
    // - その他

    /// <summary>
    /// 生成
    /// </summary>
	public TilePalettePage()
	{
		InitializeComponent();
	}

    // - メソッド

    private void PointerGestureRecognizer_PointerMoved(object sender, PointerEventArgs e)
    {
        Trace.WriteLine("[TilePalettePage PointerGestureRecognizer_PointerMoved]");

        TilePalettePageViewModel context = (TilePalettePageViewModel)this.BindingContext;

        Image image = (Image)sender;

        context.PointingXOnImageAsInt = (int)e.GetPosition(image).Value.X;
        context.PointingYOnImageAsInt = (int)e.GetPosition(image).Value.Y;

        Trace.WriteLine($"[TilePalettePage PointerGestureRecognizer_PointerMoved] image.X = {image.X}");
        Trace.WriteLine($"[TilePalettePage PointerGestureRecognizer_PointerMoved] image.Y = {image.Y}");

        context.PointingXOnWindowAsInt = context.PointingXOnImageAsInt + (int)image.X;
        context.PointingYOnWindowAsInt = context.PointingYOnImageAsInt + (int)image.Y;
    }
}