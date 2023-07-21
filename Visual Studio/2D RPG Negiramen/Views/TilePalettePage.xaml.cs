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
        TilePalettePageViewModel context = (TilePalettePageViewModel)this.BindingContext;

        Point? tappedPosition = e.GetPosition((Element)sender) ?? Point.Zero;

        // タップした位置
        var tapped = new Models.Geometric.PointFloat(
            new Models.Geometric.XFloat((float)tappedPosition.Value.X),
            new Models.Geometric.YFloat((float)tappedPosition.Value.Y));
        Trace.WriteLine($"[TilePalettePage TapGestureRecognizer_Tapped] tapped x:{tapped.X.AsFloat} y:{tapped.Y.AsFloat}");

        // タイル・カーソルの位置
        var tileCursor = Models.CoordinateHelper.TranslateTappedPointToTileCursorPoint(
            tapped: tapped,
            gridLeftTop: context.WorkingGridLeftTop,
            gridTile: new Models.Geometric.SizeFloat(new Models.Geometric.WidthFloat(32), new Models.Geometric.HeightFloat(32)));

        //
        // 計算値の反映
        // ============
        //
        context.TappedXOnImageAsInt = (int)tapped.X.AsFloat;
        context.TappedYOnImageAsInt = (int)tapped.Y.AsFloat;
        context.SourceCroppedCursorLeftAsInt = (int)tileCursor.X.AsFloat;
        context.SourceCroppedCursorTopAsInt = (int)tileCursor.Y.AsFloat;
    }
}