namespace _2D_RPG_Negiramen.Views;

using _2D_RPG_Negiramen.Models.FileEntries;
using _2D_RPG_Negiramen.ViewModels;
using System.IO;
using TheGraphics = Microsoft.Maui.Graphics;

#if IOS || ANDROID || MACCATALYST
using Microsoft.Maui.Graphics.Platform;
#elif WINDOWS
using Microsoft.Maui.Graphics.Win2D;
#endif

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
        App.SelectingOnPointingDevice = !App.SelectingOnPointingDevice;

        if (App.SelectingOnPointingDevice)
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

            // タイル・カーソルのキャンバスの再描画
            context.RefreshCanvasOfTileCursor();
        }
    }

    /// <summary>
    /// マウスカーソル移動時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    private void PointerGestureRecognizer_PointerMoved(object sender, PointerEventArgs e)
    {
        if (App.SelectingOnPointingDevice)
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
            id: context.TileSetSettings.UsableId,
            rect: new Models.Rectangle(
                point: new Models.Point(
                    x: new Models.X(context.TileCursorXAsInt),
                    y: new Models.Y(context.TileCursorYAsInt)),
                size: new Models.Size(
                    width: new Models.Width(context.TileCursorWidthAsInt),
                    height: new Models.Height(context.TileCursorHeightAsInt))),
            comment: new Models.Comment(context.CommentAsStr),
            onTileIdUpdated: () =>
            {
                // ビューの再描画（レコードの追加により、タイルＩｄが更新されるので）
                context.RefreshTileId();
            });

        //
        // 設定ファイルの保存
        // ==================
        //
        if (context.TileSetSettings.SaveCSV(context.TileSetSettingsFile))
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
    void ContentPage_Loaded(object sender, EventArgs e)
    {
        //
        // ユーザー設定の読込
        // ==========================
        //
        UserConfiguration userConfiguration = App.GetOrLoadUserConfiguration();

        //
        // 設定ファイルの読込
        // ==================
        //
        TilePaletteEditPageViewModel context = (TilePaletteEditPageViewModel)this.BindingContext;
        if (Models.FileEntries.TileSetSettings.LoadCSV(context.TileSetSettingsFile, out Models.FileEntries.TileSetSettings tileSetSettings))
        {
            context.TileSetSettings = tileSetSettings;
        }

        //
        // TODO タイル・セット・キャンバス画像ファイルへのパスを取得
        //
        var tileSetImageFilePathAsStr = context.TileSetImageFilePathAsStr;

        //
        // TODO 作業用のタイル・セット・キャンバス画像ファイルへのパスを取得
        //
        var workingTileSetCanvasImagefilePathAsStr = userConfiguration.WorkingTileSetCanvasImageFile.Path.AsStr;

        //
        // タイル・セット・キャンバス画像の読込
        //
        var task = Task.Run(() =>
        {
            try
            {
                // 読込元（ウィンドウズ・ローカルＰＣ）
                using (Stream inputFileStream = System.IO.File.OpenRead(tileSetImageFilePathAsStr))
                {
#if IOS || ANDROID || MACCATALYST
                    // PlatformImage isn't currently supported on Windows.
                    TheGraphics.IImage image = PlatformImage.FromStream(inputFileStream);
#elif WINDOWS
                    TheGraphics.IImage image = new W2DImageLoadingService().FromStream(inputFileStream);
#endif

                    //
                    // 作業中のタイル・セット・キャンバス画像の保存
                    //
                    if (image != null)
                    {
                        // 書出先（ウィンドウズ・ローカルＰＣ）
                        using (Stream outputFileStream = System.IO.File.Open(workingTileSetCanvasImagefilePathAsStr, FileMode.OpenOrCreate))
                        {
                            image.Save(outputFileStream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO エラー対応どうする？
            }
        });

        Task.WaitAll(new Task[] { task });
    }
}