namespace _2D_RPG_Negiramen.Views;

using _2D_RPG_Negiramen.ViewModels;
using Microsoft.Maui.Controls;
using System.Diagnostics;

/// <summary>
///     😁 タイル・パレット・ページ
/// </summary>
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
        Point pointerPosition = e.GetPosition((Element)sender) ?? Point.Zero;

        context.OnPointedMove(image, pointerPosition);
    }

    /// <summary>
    /// タップ時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        // Image image = (Image)sender;
        TilePalettePageViewModel context = (TilePalettePageViewModel)this.BindingContext;

        Point tappedPosition = e.GetPosition((Element)sender) ?? Point.Zero;

        context.OnTapped(tappedPosition);
    }
}