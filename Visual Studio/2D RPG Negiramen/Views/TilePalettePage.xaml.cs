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
    void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        // Image image = (Image)sender;

        // タップした座標
        var tapped = new Models.Point(
            new Models.X((int)e.GetPosition((Element)sender).Value.X),
            new Models.Y((int)e.GetPosition((Element)sender).Value.Y));
        Trace.WriteLine($"[TilePalettePage TapGestureRecognizer_Tapped] tapped x:{tapped.X.AsInt} y:{tapped.Y.AsInt}");

        // タイル・カーソルの座標
        var tileCursor = Models.CoordinateHelper.TranslateTappedPointToTileCursorPoint(
            tapped: tapped,
            gridTile: new Models.Size(new Models.Width(32), new Models.Height(32)));

        //
        // 計算値の反映
        // ============
        //
        TilePalettePageViewModel context = (TilePalettePageViewModel)this.BindingContext;
        context.TappedXOnImageAsInt = tapped.X.AsInt;
        context.TappedYOnImageAsInt = tapped.Y.AsInt;
        context.TileCursorXAsInt = tileCursor.X.AsInt;
        context.TileCursorYAsInt = tileCursor.Y.AsInt;
    }
}