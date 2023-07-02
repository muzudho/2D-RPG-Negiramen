using _2D_RPG_Negiramen.ViewModels;
using System.Diagnostics;

namespace _2D_RPG_Negiramen.Views;

/// <summary>
///     😁 タイル・パレット編集ページ
/// </summary>
public partial class TilePaletteEditPage : ContentPage
{
	public TilePaletteEditPage()
	{
		InitializeComponent();
	}

    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    /// <summary>
    /// タップ時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        // Image image = (Image)sender;

        // タップした座標
        int tappedX = (int)e.GetPosition((Element)sender).Value.X;
        int tappedY = (int)e.GetPosition((Element)sender).Value.Y;
        Trace.WriteLine($"[TilePalettePage TapGestureRecognizer_Tapped] tapped x:{tappedX} y:{tappedY}");

        // タイル・サイズ
        int tileSize = 32;

        var tileCursorPoint = Models.CoordinateHelper.TranslateTappedPointToTileCursorPoint(
            tappedX,
            tappedY,
            tileSize,
            tileSize);

        // 計算値の反映
        TilePaletteEditPageViewModel context = (TilePaletteEditPageViewModel)this.BindingContext;
        //context.TappedXOnImageAsInt = tappedX;
        //context.TappedYOnImageAsInt = tappedY;
        context.TileCursorXOnWindowAsInt = tileCursorPoint.X.AsInt;
        context.TileCursorYOnWindowAsInt = tileCursorPoint.Y.AsInt;
    }
}