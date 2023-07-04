using _2D_RPG_Negiramen.Models.Files;
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

    // - プライベート・イベント・ハンドラー

    /// <summary>
    /// タップ時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        // 反転
        IsPointingDevicePressed = !IsPointingDevicePressed;

        if (IsPointingDevicePressed)
        {
            //
            // 疑似マウス・ダウン
            // ==================
            //

            // タップした位置
            PointingDeviceCurrentPoint = PointingDeviceStartPoint = new Models.Point(
                new Models.X((int)e.GetPosition((Element)sender).Value.X),
                new Models.Y((int)e.GetPosition((Element)sender).Value.Y));
            // Trace.WriteLine($"[TilePaletteEditPage TapGestureRecognizer_Tapped] tapped x:{PointingDeviceStartPoint.X.AsInt} y:{PointingDeviceStartPoint.Y.AsInt}");

            // タイル・カーソルの矩形
            var cursorRectangle = Models.CoordinateHelper.GetCursorRectangle(PointingDeviceStartPoint, PointingDeviceCurrentPoint);
            // Trace.WriteLine($"[TilePaletteEditPage TapGestureRecognizer_Tapped] cursorRectangle x:{cursorRectangle.Point.X.AsInt} y:{cursorRectangle.Point.Y.AsInt} width:{cursorRectangle.Size.Width.AsInt} height:{cursorRectangle.Size.Height.AsInt}");

            //
            // 計算値の反映
            // ============
            //
            TilePaletteEditPageViewModel context = (TilePaletteEditPageViewModel)this.BindingContext;
            context.TileCursorXAsInt = cursorRectangle.Point.X.AsInt;
            context.TileCursorYAsInt = cursorRectangle.Point.Y.AsInt;
            context.TileCursorWidthAsInt = cursorRectangle.Size.Width.AsInt;
            context.TileCursorHeightAsInt = cursorRectangle.Size.Height.AsInt;
        }
        else
        {
            //
            // 疑似マウス・アップ
            // ==================
            //

            // ポイントしている位置
            PointingDeviceCurrentPoint = new Models.Point(
                new Models.X((int)e.GetPosition((Element)sender).Value.X),
                new Models.Y((int)e.GetPosition((Element)sender).Value.Y));
            // Trace.WriteLine($"[TilePaletteEditPage PointerGestureRecognizer_PointerExited] exited x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

            // タイル・カーソルの矩形
            var cursorRectangle = Models.CoordinateHelper.GetCursorRectangle(PointingDeviceStartPoint, PointingDeviceCurrentPoint);
            // Trace.WriteLine($"[TilePaletteEditPage PointerGestureRecognizer_PointerExited] cursorRectangle x:{cursorRectangle.Point.X.AsInt} y:{cursorRectangle.Point.Y.AsInt} width:{cursorRectangle.Size.Width.AsInt} height:{cursorRectangle.Size.Height.AsInt}");

            //
            // 計算値の反映
            // ============
            //
            TilePaletteEditPageViewModel context = (TilePaletteEditPageViewModel)this.BindingContext;
            context.TileCursorXAsInt = cursorRectangle.Point.X.AsInt;
            context.TileCursorYAsInt = cursorRectangle.Point.Y.AsInt;
            context.TileCursorWidthAsInt = cursorRectangle.Size.Width.AsInt;
            context.TileCursorHeightAsInt = cursorRectangle.Size.Height.AsInt;
        }
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
            //
            // 疑似マウス・ドラッグ
            // ====================
            //

            // ポイントしている位置
            PointingDeviceCurrentPoint = new Models.Point(
                new Models.X((int)e.GetPosition((Element)sender).Value.X),
                new Models.Y((int)e.GetPosition((Element)sender).Value.Y));
            // Trace.WriteLine($"[TilePaletteEditPage PointerGestureRecognizer_PointerMoved] moved x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

            // タイル・カーソルの矩形
            var cursorRectangle = Models.CoordinateHelper.GetCursorRectangle(PointingDeviceStartPoint, PointingDeviceCurrentPoint);
            // Trace.WriteLine($"[TilePaletteEditPage PointerGestureRecognizer_PointerMoved] cursorRectangle x:{cursorRectangle.Point.X.AsInt} y:{cursorRectangle.Point.Y.AsInt} width:{cursorRectangle.Size.Width.AsInt} height:{cursorRectangle.Size.Height.AsInt}");

            //
            // 計算値の反映
            // ============
            //
            TilePaletteEditPageViewModel context = (TilePaletteEditPageViewModel)this.BindingContext;
            context.TileCursorXAsInt = cursorRectangle.Point.X.AsInt;
            context.TileCursorYAsInt = cursorRectangle.Point.Y.AsInt;
            context.TileCursorWidthAsInt = cursorRectangle.Size.Width.AsInt;
            context.TileCursorHeightAsInt = cursorRectangle.Size.Height.AsInt;
        }
    }

    /// <summary>
    ///     ［追加］ボタン・クリック時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    private void AddsButton_Clicked(object sender, EventArgs e)
    {
        //
        // 設定ファイルの編集
        // ==================
        //
        TilePaletteEditPageViewModel context = (TilePaletteEditPageViewModel)this.BindingContext;

        context.TileSetSettings.Add(
            id: new Models.TileId(9999), // TODO とりあえず 9999 で。後で直す
            rect: new Models.Rectangle(
                point: new Models.Point(
                    x: new Models.X(context.TileCursorXAsInt),
                    y: new Models.Y(context.TileCursorYAsInt)),
                size: new Models.Size(
                    width: new Models.Width(context.TileCursorWidthAsInt),
                    height: new Models.Height(context.TileCursorHeightAsInt))),
            comment: new Models.Comment(context.CommentAsStr));

        //
        // 設定ファイルの保存
        // ==================
        //
        if (context.TileSetSettings.SaveCSV(context.TileSetCSVFilePath))
        {
            // 保存成功
        }
        else
        {
            // TODO 保存失敗時のエラー対応
        }
    }

    /// <summary>
    /// ページ読込完了時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        //
        // 設定ファイルの読込
        // ==================
        //
        TilePaletteEditPageViewModel context = (TilePaletteEditPageViewModel)this.BindingContext;
        if (Models.Files.TileSetSettings.LoadCSV(context.TileSetCSVFilePath, out Models.Files.TileSetSettings tileSetSettings))
        {
            context.TileSetSettings = tileSetSettings;
        }
    }
}