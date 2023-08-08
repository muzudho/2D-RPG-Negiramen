namespace _2D_RPG_Negiramen.Views;

using _2D_RPG_Negiramen.ViewModels;
using System.IO;
using System.Diagnostics;
using TheGraphics = Microsoft.Maui.Graphics;

#if IOS || ANDROID || MACCATALYST
using Microsoft.Maui.Graphics.Platform;
using _2D_RPG_Negiramen.Models.Geometric;
using _2D_RPG_Negiramen.VisualModels;
using _2D_RPG_Negiramen.Models.Visually;
#elif WINDOWS
using Microsoft.Maui.Graphics.Win2D;
using _2D_RPG_Negiramen.Models;
using System.Net;
using SkiaSharp;
using _2D_RPG_Negiramen.FeatSkia;
using _2D_RPG_Negiramen.Models.Geometric;
using _2D_RPG_Negiramen.VisualModels;
using _2D_RPG_Negiramen.Models.Visually;
#endif

/// <summary>
///     😁 タイル切抜き編集ページ
/// </summary>
public partial class TileCropPage : ContentPage
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    public TileCropPage()
    {
        InitializeComponent();
    }
    #endregion

    // - パブリック・プロパティ

    #region プロパティ（ビューモデル）
    /// <summary>
    ///     ビューモデル
    /// </summary>
    public ITileCropPageViewModel TileCropPageVM => (ITileCropPageViewModel)this.BindingContext;
    #endregion

    // - プライベート・プロパティ

    // - プライベート・メソッド

    // - プライベート・イベントハンドラ

    #region イベントハンドラ（ページ新規生成時）
    /// <summary>
    ///     ページ新規生成時
    ///     
    ///     <list type="bullet">
    ///         <item>訪問時ではないことに注意</item>
    ///     </list>
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    void ContentPage_Loaded(object sender, EventArgs e)
    {
        Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] ページ新規作成時");
    }
    #endregion

    #region イベントハンドラ（別ページから、このページに訪れたときに呼び出される）
    /// <summary>
    ///     別ページから、このページに訪れたときに呼び出される
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void thisContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] ページ来訪時");

        //
        // ビューモデルの取得
        // ==================
        //
        TileCropPageViewModel context = (TileCropPageViewModel)this.BindingContext;

        context.ReactOnVisited();

        //
        // タイル設定ファイルの読込
        // ========================
        //
        if (TilesetDatatableVisually.LoadCSV(context.TilesetSettingsFile, out TilesetDatatableVisually tileSetSettingsVM))
        {
            context.TilesetSettingsVM = tileSetSettingsVM;

#if DEBUG
            // ファイルの整合性チェック（重い処理）
            if (context.TilesetSettingsVM.IsValid())
            {
                Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] ファイルの内容は妥当　File: {context.TilesetSettingsFile.Path.AsStr}");
            }
            else
            {
                Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] ファイルの内容に異常あり　File: {context.TilesetSettingsFile.Path.AsStr}");
            }
#endif

            //// 登録タイルのデバッグ出力
            //foreach (var record in context.TilesetSettings.RecordList)
            //{
            //    Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] Record: {record.Dump()}");
            //}
        }

        //
        // タイルセット画像ファイルへのパスを取得
        //
        var tilesetImageFilePathAsStr = context.TilesetImageFilePathAsStr;

        //
        // タイルセット画像の読込、作業中タイルセット画像の書出
        // ====================================================
        //
        var task = Task.Run(async () =>
        {
            try
            {
                // タイルセット読込（読込元：　ウィンドウズ・ローカルＰＣ）
                using (Stream inputFileStream = System.IO.File.OpenRead(tilesetImageFilePathAsStr))
                {
#if IOS || ANDROID || MACCATALYST
                    // PlatformImage isn't currently supported on Windows.
                    
                    TheGraphics.IImage image = PlatformImage.FromStream(inputFileStream);
#elif WINDOWS
                    TheGraphics.IImage image = new W2DImageLoadingService().FromStream(inputFileStream);
#endif

                    //
                    // 作業中のタイルセット画像の保存
                    //
                    if (image != null)
                    {
                        // ディレクトリーが無ければ作成する
                        var folder = App.CacheFolder.YourCircleFolder.YourWorkFolder.ImagesFolder;
                        folder.CreateThisDirectoryIfItDoesNotExist();

                        // 書出先（ウィンドウズ・ローカルＰＣ）
                        using (Stream outputFileStream = System.IO.File.Open(folder.WorkingTilesetPng.Path.AsStr, FileMode.OpenOrCreate))
                        {
                            image.Save(outputFileStream);
                        }
                    }

                    // 作業中のタイルセット画像の再描画
                    context.RefreshWorkingTilesetImage();
                }
            }
            catch (Exception ex)
            {
                // TODO エラー対応どうする？
            }

            // ↓ SkiaSharp の流儀
            try
            {
                // タイルセット読込（読込元：　ウィンドウズ・ローカルＰＣ）
                using (Stream inputFileStream = System.IO.File.OpenRead(tilesetImageFilePathAsStr))
                {
                    // ↓ １つのストリームが使えるのは、１回切り
                    using (var memStream = new MemoryStream())
                    {
                        await inputFileStream.CopyToAsync(memStream);
                        memStream.Seek(0, SeekOrigin.Begin);

                        // 元画像
                        context.SetTilesetSourceBitmap(SkiaSharp.SKBitmap.Decode(memStream));

                        // 複製
                        context.TilesetWorkingBitmap = SkiaSharp.SKBitmap.FromImage(SkiaSharp.SKImage.FromBitmap(context.TilesetSourceBitmap));

                        // 画像処理（明度を下げる）
                        FeatSkia.ReduceBrightness.DoItInPlace(context.TilesetWorkingBitmap);
                    };

                    // 再描画
                    this.skiaTilesetCanvas1.InvalidateSurface();
                }
            }
            catch (Exception ex)
            {
                // TODO エラー対応どうする？
            }
        });

        Task.WaitAll(new Task[] { task });
    }
    #endregion

    #region イベントハンドラ（ロケール変更時）
    /// <summary>
    ///     ロケール変更時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    void LocalePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        // ＸＡＭＬではなく、Ｃ＃で動的に翻訳を行っている場合のための変更通知
        var context = this.TileCropPageVM;
        context.InvalidateLocale();
    }
    #endregion

    #region イベントハンドラ（［タイルセット一覧］ボタン　関連）
    /// <summary>
    ///     ［タイルセット一覧］ボタン・クリック時
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    async void TilesetListButton_Clicked(object sender, EventArgs e)
    {
        await PolicyOfView.ReactOnPushed((Button)sender);

        await Shell.Current.GoToAsync("//TilesetListPage");
    }
    #endregion

    #region イベントハンドラ（［タイルセット］画像　関連）
    /// <summary>
    ///     ［タイルセット］画像タップ時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    void TilesetImage_OnTapped(object sender, TappedEventArgs e)
    {
        TileCropPageViewModel context = (TileCropPageViewModel)this.BindingContext;

        // 反転
        context.IsMouseDragging = !context.IsMouseDragging;

        Point? tappedPoint = e.GetPosition((Element)sender) ?? Point.Zero;

        if (context.IsMouseDragging)
        {
            //
            // 疑似マウス・ダウン
            // ==================
            //
            Trace.WriteLine("[TileCropPage.xml.cs TileImage_OnTapped] 疑似マウス・ダウン");

            // ポイントしている位置
            context.PointingDeviceCurrentPoint = context.PointingDeviceStartPoint = new Models.Geometric.PointFloat(
                new Models.Geometric.XFloat((float)tappedPoint.Value.X),
                new Models.Geometric.YFloat((float)tappedPoint.Value.Y));
            // Trace.WriteLine($"[TileCropPage TileImage_OnTapped] tapped x:{PointingDeviceStartPoint.X.AsInt} y:{PointingDeviceStartPoint.Y.AsInt}");

            // タイル・フォームの表示更新
            context.RefreshTileForm();

            context.TrickRefreshCanvasOfTileCursor(codePlace: "[TileCropPage.xml.cs TileImage_OnTapped 疑似マウスダウン]");
        }
        else
        {
            //
            // 疑似マウス・アップ
            // ==================
            //

            Trace.WriteLine("[TileCropPage.xml.cs TileImage_OnTapped] 疑似マウス・アップ");

            // ポイントしている位置
            context.PointingDeviceCurrentPoint = new Models.Geometric.PointFloat(
                new Models.Geometric.XFloat((float)tappedPoint.Value.X),
                new Models.Geometric.YFloat((float)tappedPoint.Value.Y));
            // Trace.WriteLine($"[TileCropPage PointerGestureRecognizer_PointerExited] exited x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

            // タイル・フォームの表示更新
            context.RefreshTileForm();

            context.TrickRefreshCanvasOfTileCursor(codePlace: "[TileCropPage.xml.cs TileImage_OnTapped 疑似マウスアップ]");
        }
    }

    /// <summary>
    ///     ［タイルセット］画像ポインター移動時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    void TilesetImage_OnPointerMoved(object sender, PointerEventArgs e)
    {
        TileCropPageViewModel context = (TileCropPageViewModel)this.BindingContext;

        if (context.IsMouseDragging)
        {
            //
            // 疑似マウス・ドラッグ
            // ====================
            //

            Point? tappedPoint = e.GetPosition((Element)sender) ?? Point.Zero;

            // ポイントしている位置
            context.PointingDeviceCurrentPoint = new Models.Geometric.PointFloat(
                new Models.Geometric.XFloat((float)tappedPoint.Value.X),
                new Models.Geometric.YFloat((float)tappedPoint.Value.Y));
            // Trace.WriteLine($"[TileCropPage PointerGestureRecognizer_PointerMoved] moved x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

            // タイル・フォームの表示更新
            context.RefreshTileForm();

            context.TrickRefreshCanvasOfTileCursor(codePlace: "[TileCropPage.xml.cs PointerGestureRecognizer_PointerMoved 疑似マウスドラッグ]");
        }
    }

    /// <summary>
    ///     ［タイルセット］画像表面の描画時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    void TilesetImageSkiaView_PaintSurface(object sender, SkiaSharp.Views.Maui.SKPaintSurfaceEventArgs e)
    {
        var bindingContext = this.TileCropPageVM;

        // 画像描画
        if (!bindingContext.TilesetWorkingBitmap.IsNull)
        {
            // the the canvas and properties
            var canvas = e.Surface.Canvas;

            canvas.DrawImage(
                image: SkiaSharp.SKImage.FromBitmap(bindingContext.TilesetWorkingBitmap),
                p: new SkiaSharp.SKPoint());
        }
    }
    #endregion

    #region イベントハンドラ（［追加］ボタン　関連）
    /// <summary>
    ///     ［追加］ボタン・クリック時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    async void AddsButton_Clicked(object sender, EventArgs e)
    {
        await PolicyOfView.ReactOnPushed((Button)sender);

        TileCropPageViewModel context = (TileCropPageViewModel)this.BindingContext;

        if (context.CroppedCursorPointedTileIdOrEmpty == Models.TileIdOrEmpty.Empty)
        {
            // Ｉｄが空欄
            // ［追加］（新規作成）だ

            // 登録タイル追加
            context.AddRegisteredTile();
        }
        else
        {
            // TODO 上書き機能削除
        }
    }
    #endregion

    #region イベントハンドラ（［削除］ボタン　関連）
    /// <summary>
    ///     ［削除］ボタン・クリック時
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    async void DeletesButton_Clicked(object sender, EventArgs e)
    {
        await PolicyOfView.ReactOnPushed((Button)sender);

        TileCropPageViewModel context = (TileCropPageViewModel)this.BindingContext;

        // 登録タイル削除
        context.RemoveRegisteredTile();
    }
    #endregion

    #region イベントハンドラ（［ズーム］テキスト変更時）
    /// <summary>
    ///     ［ズーム］テキスト変更時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    void ZoomEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        // 再描画（タイルセット画像）
        if (this.skiaTilesetCanvas1 != null)
        {
            this.skiaTilesetCanvas1.InvalidateSurface();
        }

        // 再描画（グリッド画像）
        if (this.gridView1 != null)
        {
            this.gridView1.Invalidate();
        }

        // 再描画（切抜きカーソル）
        if (this.croppedCursor1 != null)
        {
            this.croppedCursor1.Invalidate();
        }
    }
    #endregion

    #region イベントハンドラ（［アンドゥ］ボタン・クリック時）
    /// <summary>
    ///     ［アンドゥ］ボタン・クリック時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    private void UndoImageButton_Clicked(object sender, EventArgs e)
    {
        App.History.Undo();
    }
    #endregion

    #region イベントハンドラ（［リドゥ］ボタン・クリック時）
    /// <summary>
    ///     ［リドゥ］ボタン・クリック時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    private void RedoImageButton_Clicked(object sender, EventArgs e)
    {
        App.History.Redo();
    }
    #endregion

    #region イベントハンドラ（［タイル・タイトル］エンター入力時）
    /// <summary>
    ///     ［タイル・タイトル］エンター入力時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    private void TileTitleEntry_Completed(object sender, EventArgs e)
    {
        Entry entry = (Entry)sender;

        TileCropPageViewModel context = (TileCropPageViewModel)this.BindingContext;

        Trace.WriteLine($"[TileCropPage.xaml.cs TileTitleEntry_Completed] entry.Text: {entry.Text}");

        context.OverwriteRegisteredTile();
    }
    #endregion
}
