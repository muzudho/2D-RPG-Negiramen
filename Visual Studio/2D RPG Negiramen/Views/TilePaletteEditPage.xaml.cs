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
        IsPointingDevicePressed = true;

        // ポイントしている位置
        PointingDeviceStartPoint = new Models.Point(
            new Models.X((int)e.GetPosition((Element)sender).Value.X),
            new Models.Y((int)e.GetPosition((Element)sender).Value.Y));
        Trace.WriteLine($"[TilePaletteEditPage PointerGestureRecognizer_PointerEntered] entered x:{PointingDeviceStartPoint.X.AsInt} y:{PointingDeviceStartPoint.Y.AsInt}");

        // タイル・カーソルの位置
        var tileCursor = Models.CoordinateHelper.TranslateTappedPointToTileCursorPoint(
            tapped: PointingDeviceStartPoint,
            gridTile: App.WorkingGridTileSize);
    }

    /// <summary>
    /// マウスカーソル移動時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    private void PointerGestureRecognizer_PointerMoved(object sender, PointerEventArgs e)
    {
        if (IsPointingDevicePressed)
        {
            // ポイントしている位置
            PointingDeviceCurrentPoint = new Models.Point(
                new Models.X((int)e.GetPosition((Element)sender).Value.X),
                new Models.Y((int)e.GetPosition((Element)sender).Value.Y));
            Trace.WriteLine($"[TilePaletteEditPage PointerGestureRecognizer_PointerMoved] moved x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

            // タイル・カーソルの位置
            var tileCursor = Models.CoordinateHelper.TranslateTappedPointToTileCursorPoint(
                tapped: PointingDeviceCurrentPoint,
                gridTile: App.WorkingGridTileSize);
        }
    }

    /// <summary>
    /// マウスボタン・リリース時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    private void PointerGestureRecognizer_PointerExited(object sender, PointerEventArgs e)
    {
        IsPointingDevicePressed = false;

        // ポイントしている位置
        PointingDeviceCurrentPoint = new Models.Point(
            new Models.X((int)e.GetPosition((Element)sender).Value.X),
            new Models.Y((int)e.GetPosition((Element)sender).Value.Y));
        Trace.WriteLine($"[TilePaletteEditPage PointerGestureRecognizer_PointerExited] exited x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

        // タイル・カーソルの位置
        var tileCursor = Models.CoordinateHelper.TranslateTappedPointToTileCursorPoint(
            tapped: PointingDeviceCurrentPoint,
            gridTile: App.WorkingGridTileSize);
    }

    // - プライベート・プロパティー

    /// <summary>
    /// ポインティング・デバイス押下中か？
    /// </summary>
    bool IsPointingDevicePressed { get; set; }

    /// <summary>
    /// ポインティング・デバイス押下開始位置
    /// </summary>
    Models.Point PointingDeviceStartPoint { get; set; }

    /// <summary>
    /// ポインティング・デバイス押下現在位置
    /// </summary>
    Models.Point PointingDeviceCurrentPoint { get; set; }

    // - プライベート・メソッド

    /// <summary>
    /// カーソルの矩形を算出
    /// </summary>
    void LetCursorRectangle()
    {

    }
}