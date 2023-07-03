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

        // タップした位置
        var tapped = new Models.Point(
            new Models.X((int)e.GetPosition((Element)sender).Value.X),
            new Models.Y((int)e.GetPosition((Element)sender).Value.Y));
        Trace.WriteLine($"[TilePaletteEditPage TapGestureRecognizer_Tapped] tapped x:{tapped.X.AsInt} y:{tapped.Y.AsInt}");

        // タイル・カーソルの位置
        var tileCursor = Models.CoordinateHelper.TranslateTappedPointToTileCursorPoint(
            tapped: tapped,
            gridTile: App.WorkingGridTileSize);

        //
        // 計算値の反映
        // ============
        //
        TilePaletteEditPageViewModel context = (TilePaletteEditPageViewModel)this.BindingContext;
        //context.TappedXOnImageAsInt = tappedX;
        //context.TappedYOnImageAsInt = tappedY;
        context.TileCursorXAsInt = tileCursor.X.AsInt;
        context.TileCursorYAsInt = tileCursor.Y.AsInt;
    }

    /// <summary>
    /// マウス押下時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    private void PointerGestureRecognizer_PointerEntered(object sender, PointerEventArgs e)
    {
        // ポイントしている位置
        var point = new Models.Point(
            new Models.X((int)e.GetPosition((Element)sender).Value.X),
            new Models.Y((int)e.GetPosition((Element)sender).Value.Y));
        Trace.WriteLine($"[TilePaletteEditPage PointerGestureRecognizer_PointerEntered] entered x:{point.X.AsInt} y:{point.Y.AsInt}");
    }

    /// <summary>
    /// マウスカーソル移動時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    private void PointerGestureRecognizer_PointerMoved(object sender, PointerEventArgs e)
    {
        // ポイントしている位置
        var point = new Models.Point(
            new Models.X((int)e.GetPosition((Element)sender).Value.X),
            new Models.Y((int)e.GetPosition((Element)sender).Value.Y));
        Trace.WriteLine($"[TilePaletteEditPage PointerGestureRecognizer_PointerMoved] moved x:{point.X.AsInt} y:{point.Y.AsInt}");
    }

    /// <summary>
    /// マウスボタン・リリース時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    private void PointerGestureRecognizer_PointerExited(object sender, PointerEventArgs e)
    {
        // ポイントしている位置
        var point = new Models.Point(
            new Models.X((int)e.GetPosition((Element)sender).Value.X),
            new Models.Y((int)e.GetPosition((Element)sender).Value.Y));
        Trace.WriteLine($"[TilePaletteEditPage PointerGestureRecognizer_PointerExited] exited x:{point.X.AsInt} y:{point.Y.AsInt}");
    }
}