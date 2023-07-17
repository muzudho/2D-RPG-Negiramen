namespace _2D_RPG_Negiramen.Views;

using _2D_RPG_Negiramen.Coding;
using _2D_RPG_Negiramen.Models.FileEntries;
using _2D_RPG_Negiramen.ViewModels;
using System.IO;
using TheGraphics = Microsoft.Maui.Graphics;

#if IOS || ANDROID || MACCATALYST
using Microsoft.Maui.Graphics.Platform;
#elif WINDOWS
using Microsoft.Maui.Graphics.Win2D;
using System.Diagnostics;
using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Coding;
using System.Net;
using SkiaSharp;
using _2D_RPG_Negiramen.FeatSkia;
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

    #region プロパティ（ポインティング・デバイス押下開始位置）
    /// <summary>
    ///     ポインティング・デバイス押下開始位置
    /// </summary>
    Models.Point PointingDeviceStartPoint { get; set; }
    #endregion

    #region プロパティ（ポインティング・デバイス現在位置）
    /// <summary>
    ///     ポインティング・デバイス現在位置
    /// </summary>
    Models.Point PointingDeviceCurrentPoint { get; set; }
    #endregion

    // - プライベート・メソッド

    #region メソッド（タイル・フォームの表示更新）
    /// <summary>
    /// タイル・フォームの表示更新
    /// </summary>
    void RefreshTileForm()
    {
        TileCropPageViewModel context = (TileCropPageViewModel)this.BindingContext;

        // ポインティング・デバイスの２箇所のタップ位置から、タイルの矩形を算出
        var rect = Models.CoordinateHelper.GetCursorRectangle(
            startPoint: PointingDeviceStartPoint,
            endPoint: PointingDeviceCurrentPoint,
            gridLeftTop: context.GridPhase,
            gridTile: context.GridTileSize);


        //
        // 計算値の反映
        // ============
        //
        // Trace.WriteLine($"[TileCropPage.xaml.cs RefreshTileForm] context.SelectingOnPointingDevice: {context.SelectingOnPointingDevice}, context.HalfThicknessOfTileCursorLine.AsInt: {context.HalfThicknessOfTileCursorLine.AsInt}, rect x:{rect.Point.X.AsInt} y:{rect.Point.Y.AsInt} width:{rect.Size.Width.AsInt} height:{rect.Size.Height.AsInt}");

        context.SelectedTileRectangle = rect;

        //
        // タイルが登録済みか？
        // ====================
        //
        if (context.TilesetSettings.TryGetByRectangle(
            rect: context.SelectedTileRectangle,
            out Models.TileRecord record))
        {
            // Trace.WriteLine($"[TileCropPage.xml.cs TapGestureRecognizer_Tapped] タイルは登録済みだ。 Id:{record.Id.AsInt}, X:{record.Rectangle.Point.X.AsInt}, Y:{record.Rectangle.Point.Y.AsInt}, Width:{record.Rectangle.Size.Width.AsInt}, Height:{record.Rectangle.Size.Height.AsInt}, Comment:{record.Comment.AsStr}");

            //
            // データ表示
            // ==========
            //

            // TODO 追加ボタンを、上書きボタンへラベル変更
            // TODO 削除ボタン活性化

            // 選択中のタイルを設定
            context.SelectedTileOption = new Option<Models.TileRecord>(record);
        }
        else
        {
            // Trace.WriteLine("[TileCropPage.xml.cs TapGestureRecognizer_Tapped] 未登録のタイルだ");

            //
            // 空欄にする
            // ==========
            //

            // TODO 追加ボタン活性化
            // TODO 削除ボタン不活性化

            // 選択中のタイルの矩形だけ維持し、タイル・コードと、コメントを空欄にする
            context.SelectedTileOption = new Option<Models.TileRecord>(new Models.TileRecord(
                id: Models.TileId.Empty,
                rectangle: context.SelectedTileRectangle,
                comment: Models.Comment.Empty,
                logicalDelete: Models.LogicalDelete.False));
        }
    }
    #endregion

    // - プライベート・イベントハンドラ

    #region イベントハンドラ（ページ読込完了時）
    /// <summary>
    ///     ページ読込完了時
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
        // ビューモデルの取得
        // ==================
        //
        TileCropPageViewModel context = (TileCropPageViewModel)this.BindingContext;

        //
        // タイル設定ファイルの読込
        // ========================
        //
        if (Models.FileEntries.TilesetSettings.LoadCSV(context.TilesetSettingsFile, out Models.FileEntries.TilesetSettings tileSetSettings))
        {
            context.TilesetSettings = tileSetSettings;

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
        // 作業用のタイルセット画像ファイルへのパスを取得
        //
        var workingTilesetImagefilePathAsStr = userConfiguration.WorkingTilesetImageFile.Path.AsStr;

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
                    Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] image.GetType().FullName: {image.GetType().FullName}");
                    // [TileCropPage.xaml.cs ContentPage_Loaded] image.GetType().FullName: Microsoft.Maui.Graphics.Win2D.W2DImage
                    // W2DImage にはアクセスできない保護レベル

                    //
                    // 作業中のタイルセット画像の保存
                    //
                    if (image != null)
                    {
                        // 書出先（ウィンドウズ・ローカルＰＣ）
                        using (Stream outputFileStream = System.IO.File.Open(workingTilesetImagefilePathAsStr, FileMode.OpenOrCreate))
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
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        await inputFileStream.CopyToAsync(memStream);
                        memStream.Seek(0, SeekOrigin.Begin);

                        // 元画像
                        context.TilesetSourceBitmap = SkiaSharp.SKBitmap.Decode(memStream);

                        // 複製
                        context.TilesetWorkingBitmap = SkiaSharp.SKBitmap.FromImage(SkiaSharp.SKImage.FromBitmap(context.TilesetSourceBitmap));

                        // 画像処理（明度を下げる）
                        FeatSkia.ReduceBrightness.DoItInPlace(context.TilesetWorkingBitmap);
                    };

                    // 再描画
                    skiaView1.InvalidateSurface();
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

    #region イベントハンドラ（別ページから、このページに訪れたときに呼び出される）
    /// <summary>
    ///     別ページから、このページに訪れたときに呼び出される
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void thisContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        var context = this.TileCropPageVM;

        context.ReactOnVisited();
    }
    #endregion

    #region イベントハンドラ（［ホーム］ボタン・クリック時）
    /// <summary>
    ///     ［ホーム］ボタン・クリック時
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await PolicyOfView.ReactOnPushed((Button)sender);

        await Shell.Current.GoToAsync("//MainPage");
    }
    #endregion

    #region イベントハンドラ（タップ時）
    /// <summary>
    ///     タップ時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        TileCropPageViewModel context = (TileCropPageViewModel)this.BindingContext;

        // 反転
        context.SelectingOnPointingDevice = !context.SelectingOnPointingDevice;

        if (context.SelectingOnPointingDevice)
        {
            //
            // 疑似マウス・ダウン
            // ==================
            //
            Trace.WriteLine("[TileCropPage.xml.cs TapGestureRecognizer_Tapped] 疑似マウス・ダウン");

            // ポイントしている位置
            PointingDeviceCurrentPoint = PointingDeviceStartPoint = new Models.Point(
                new Models.X((int)e.GetPosition((Element)sender).Value.X),
                new Models.Y((int)e.GetPosition((Element)sender).Value.Y));
            // Trace.WriteLine($"[TileCropPage TapGestureRecognizer_Tapped] tapped x:{PointingDeviceStartPoint.X.AsInt} y:{PointingDeviceStartPoint.Y.AsInt}");

            // タイル・フォームの表示更新
            RefreshTileForm();
        }
        else
        {
            //
            // 疑似マウス・アップ
            // ==================
            //

            Trace.WriteLine("[TileCropPage.xml.cs TapGestureRecognizer_Tapped] 疑似マウス・アップ");

            // ポイントしている位置
            PointingDeviceCurrentPoint = new Models.Point(
                new Models.X((int)e.GetPosition((Element)sender).Value.X),
                new Models.Y((int)e.GetPosition((Element)sender).Value.Y));
            // Trace.WriteLine($"[TileCropPage PointerGestureRecognizer_PointerExited] exited x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

            // タイル・フォームの表示更新
            RefreshTileForm();
        }
    }
    #endregion

    #region イベントハンドラ（マウスカーソル移動時）
    /// <summary>
    ///     マウスカーソル移動時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    private void PointerGestureRecognizer_PointerMoved(object sender, PointerEventArgs e)
    {
        TileCropPageViewModel context = (TileCropPageViewModel)this.BindingContext;

        if (context.SelectingOnPointingDevice)
        {
            //
            // 疑似マウス・ドラッグ
            // ====================
            //

            // ポイントしている位置
            PointingDeviceCurrentPoint = new Models.Point(
                new Models.X((int)e.GetPosition((Element)sender).Value.X),
                new Models.Y((int)e.GetPosition((Element)sender).Value.Y));
            // Trace.WriteLine($"[TileCropPage PointerGestureRecognizer_PointerMoved] moved x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

            // タイル・フォームの表示更新
            RefreshTileForm();
        }
    }
    #endregion

    #region イベントハンドラ（表面の描画時）
    /// <summary>
    ///     表面の描画時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    private void skiaView1_PaintSurface(object sender, SkiaSharp.Views.Maui.SKPaintSurfaceEventArgs e)
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

    #region イベントハンドラ（［追加］ボタン・クリック時）
    /// <summary>
    ///     ［追加］ボタン・クリック時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    async void AddsButton_Clicked(object sender, EventArgs e)
    {
        await PolicyOfView.ReactOnPushed((Button)sender);

        TileCropPageViewModel context = (TileCropPageViewModel)this.BindingContext;

        Models.LogicalDelete logicalDelete;
        if (context.SelectedTileOption.TryGetValue(out var record))
        {
            logicalDelete = record.LogicalDelete;
        }
        else
        {
            logicalDelete = Models.LogicalDelete.False;
        }

        //
        // 設定ファイルの編集
        // ==================
        //
        context.TilesetSettings.Add(
            // 新しいＩｄを追加
            id: context.TilesetSettings.UsableId,
            rect: new Models.Rectangle(
                point: new Models.Point(
                    x: new Models.X(context.SelectedTileLeftAsInt),
                    y: new Models.Y(context.SelectedTileTopAsInt)),
                size: new Models.Size(
                    width: new Models.Width(context.SelectedTileWidthAsInt),
                    height: new Models.Height(context.SelectedTileHeightAsInt))),
            comment: new Models.Comment(context.SelectedTileCommentAsStr),
            logicalDelete: logicalDelete,
            onTileIdUpdated: () =>
            {
                // ビューの再描画（レコードの追加により、タイルＩｄが更新されるので）
                context.NotifyTileIdChange();
            });

        //
        // 設定ファイルの保存
        // ==================
        //
        if (context.TilesetSettings.SaveCSV(context.TilesetSettingsFile))
        {
            // 保存成功
        }
        else
        {
            // TODO 保存失敗時のエラー対応
        }
    }
    #endregion

    #region イベントハンドラ（［削除］ボタン・クリック時）
    /// <summary>
    ///     ［削除］ボタン・クリック時
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    async void DeletesButton_Clicked(object sender, EventArgs e)
    {
        await PolicyOfView.ReactOnPushed((Button)sender);

        TileCropPageViewModel context = (TileCropPageViewModel)this.BindingContext;

        Models.LogicalDelete logicalDelete;
        if (context.SelectedTileOption.TryGetValue(out var record))
        {
            logicalDelete = record.LogicalDelete;
        }
        else
        {
            logicalDelete = Models.LogicalDelete.False;
        }

        //
        // 設定ファイルの編集
        // ==================
        //
        context.TilesetSettings.DeleteLogical(
            // 現在選択中のＩｄ
            id: context.SelectedTileId);

        //
        // 設定ファイルの保存
        // ==================
        //
        if (context.TilesetSettings.SaveCSV(context.TilesetSettingsFile))
        {
            // 保存成功
        }
        else
        {
            // TODO 保存失敗時のエラー対応
        }
    }
    #endregion

    #region イベントハンドラ（ボタンにマウスカーソル進入時）
    /// <summary>
    ///     ボタンにマウスカーソル進入時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    private void Button_PointerGestureRecognizer_PointerEntered(object sender, PointerEventArgs e)
    {
        PolicyOfView.ReactOnMouseEntered((Button)sender);
    }
    #endregion

    #region イベントハンドラ（ボタンからマウスカーソル退出時）
    /// <summary>
    ///     ボタンからマウスカーソル退出時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    private void Button_PointerGestureRecognizer_PointerExited(object sender, PointerEventArgs e)
    {
        PolicyOfView.ReactOnMouseLeaved((Button)sender);
    }
    #endregion

    #region イベントハンドラ（ロケール変更時）
    /// <summary>
    ///     ロケール変更時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    private void LocalePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var context = this.TileCropPageVM;
        context.RefreshByLocaleChanged();
    }
    #endregion
}
