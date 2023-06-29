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

    /// <summary>
    /// カーソル移動時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    private void PointerGestureRecognizer_PointerMoved(object sender, PointerEventArgs e)
    {
        //Trace.WriteLine("[TilePalettePage PointerGestureRecognizer_PointerMoved]");

        TilePalettePageViewModel context = (TilePalettePageViewModel)this.BindingContext;
        Image image = (Image)sender;

        context.PointingXOnImageAsInt = (int)e.GetPosition(image).Value.X;
        context.PointingYOnImageAsInt = (int)e.GetPosition(image).Value.Y;

        //Trace.WriteLine($"[TilePalettePage PointerGestureRecognizer_PointerMoved] image.X = {image.X}");
        //Trace.WriteLine($"[TilePalettePage PointerGestureRecognizer_PointerMoved] image.Y = {image.Y}");

        context.PointingXOnWindowAsInt = context.PointingXOnImageAsInt + (int)image.X;
        context.PointingYOnWindowAsInt = context.PointingYOnImageAsInt + (int)image.Y;
    }

    /// <summary>
    /// タップ時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        int tappedX = (int)e.GetPosition((Element)sender).Value.X;
        int tappedY = (int)e.GetPosition((Element)sender).Value.Y;
        Trace.WriteLine($"[TilePalettePage TapGestureRecognizer_Tapped] tapped x:{tappedX} y:{tappedY}");

        TilePalettePageViewModel context = (TilePalettePageViewModel)this.BindingContext;
        Image image = (Image)sender;

        context.TappedXOnImageAsInt = tappedX;
        context.TappedYOnImageAsInt = tappedY;

        // タイル・カーソルの座標を算出
        int tileSize = 32;
        int cursorX = tappedX / tileSize * tileSize;
        int cursorY = tappedY / tileSize * tileSize;
        Trace.WriteLine($"[TilePalettePage TapGestureRecognizer_Tapped] cursor x:{cursorX} y:{cursorY}");

        context.TileCursorXOnWindowAsInt = cursorX;
        context.TileCursorYOnWindowAsInt = cursorY;
    }
}