namespace _2D_RPG_Negiramen.Views;

using _2D_RPG_Negiramen.Models.FileEntries;
using _2D_RPG_Negiramen.ViewModels;
using System.IO;
using System.Diagnostics;
using TheGraphics = Microsoft.Maui.Graphics;

#if IOS || ANDROID || MACCATALYST
using Microsoft.Maui.Graphics.Platform;
using _2D_RPG_Negiramen.Models.Geometric;
#elif WINDOWS
using Microsoft.Maui.Graphics.Win2D;
using _2D_RPG_Negiramen.Models;
using System.Net;
using SkiaSharp;
using _2D_RPG_Negiramen.FeatSkia;
using _2D_RPG_Negiramen.Models.Geometric;
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
    Models.Geometric.PointFloat PointingDeviceStartPoint { get; set; }
    #endregion

    #region プロパティ（ポインティング・デバイス現在位置）
    /// <summary>
    ///     ポインティング・デバイス現在位置
    /// </summary>
    Models.Geometric.PointFloat PointingDeviceCurrentPoint { get; set; }
    #endregion

    // - プライベート・メソッド

    #region メソッド（タイル・フォームの表示更新）
    /// <summary>
    /// タイル・フォームの表示更新
    /// </summary>
    void RefreshTileForm()
    {
        TileCropPageViewModel context = (TileCropPageViewModel)this.BindingContext;

        //
        // ポインティング・デバイスの２箇所のタップ位置から、タイルの矩形を算出
        // ====================================================================
        //

        // ズームしたまま
        RectangleFloat workingRect = Models.CoordinateHelper.GetCursorRectangle(
            startPoint: PointingDeviceStartPoint,
            endPoint: PointingDeviceCurrentPoint,
            gridLeftTop: context.WorkingGridPhase,
            gridTile: context.WorkingGridUnit);

        // ズームを除去
        RectangleInt sourceRect = new RectangleInt(
            location: new PointInt(
                x: new XInt((int)(workingRect.Location.X.AsFloat / context.ZoomAsFloat)),
                y: new YInt((int)(workingRect.Location.Y.AsFloat / context.ZoomAsFloat))),
            size: new SizeInt(
                width: new WidthInt((int)(workingRect.Size.Width.AsFloat / context.ZoomAsFloat)),
                height: new HeightInt((int)(workingRect.Size.Height.AsFloat / context.ZoomAsFloat))));

        //
        // 計算値の反映
        // ============
        //
        // Trace.WriteLine($"[TileCropPage.xaml.cs RefreshTileForm] context.IsMouseDragging: {context.IsMouseDragging}, context.HalfThicknessOfTileCursorLine.AsInt: {context.HalfThicknessOfTileCursorLine.AsInt}, rect x:{rect.Point.X.AsInt} y:{rect.Point.Y.AsInt} width:{rect.Size.Width.AsInt} height:{rect.Size.Height.AsInt}");

        context.SourceCroppedCursorRect = sourceRect;

        //
        // 登録済みのタイルと被っていないか判定
        // ====================================
        //
        //      - （軽くない処理）
        //
        context.RecalculateBetweenCroppedCursorAndRegisteredTile();

        //
        // 切抜きカーソル更新
        // ==================
        //
        context.InvalidateCroppedCursor();

        // （切抜きカーソル更新後）［追加／上書き］ボタン再描画
        context.InvalidateAddsButton();

        // （切抜きカーソル更新後）［削除］ボタン活性化
        context.InvalidateDeletesButton();
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
        if (TilesetSettingsViewModel.LoadCSV(context.TilesetSettingsFile, out TilesetSettingsViewModel tileSetSettingsVM))
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

    #region イベントハンドラ（別ページから、このページに訪れたときに呼び出される）
    /// <summary>
    ///     別ページから、このページに訪れたときに呼び出される
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void thisContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
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
    void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
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
            Trace.WriteLine("[TileCropPage.xml.cs TapGestureRecognizer_Tapped] 疑似マウス・ダウン");

            // ポイントしている位置
            PointingDeviceCurrentPoint = PointingDeviceStartPoint = new Models.Geometric.PointFloat(
                new Models.Geometric.XFloat((float)tappedPoint.Value.X),
                new Models.Geometric.YFloat((float)tappedPoint.Value.Y));
            // Trace.WriteLine($"[TileCropPage TapGestureRecognizer_Tapped] tapped x:{PointingDeviceStartPoint.X.AsInt} y:{PointingDeviceStartPoint.Y.AsInt}");

            // タイル・フォームの表示更新
            RefreshTileForm();
            context.TrickRefreshCanvasOfTileCursor(codePlace: "[TileCropPage.xml.cs TapGestureRecognizer_Tapped 疑似マウスダウン]");
        }
        else
        {
            //
            // 疑似マウス・アップ
            // ==================
            //

            Trace.WriteLine("[TileCropPage.xml.cs TapGestureRecognizer_Tapped] 疑似マウス・アップ");

            // ポイントしている位置
            PointingDeviceCurrentPoint = new Models.Geometric.PointFloat(
                new Models.Geometric.XFloat((float)tappedPoint.Value.X),
                new Models.Geometric.YFloat((float)tappedPoint.Value.Y));
            // Trace.WriteLine($"[TileCropPage PointerGestureRecognizer_PointerExited] exited x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

            // タイル・フォームの表示更新
            RefreshTileForm();
            context.TrickRefreshCanvasOfTileCursor(codePlace: "[TileCropPage.xml.cs TapGestureRecognizer_Tapped 疑似マウスアップ]");
        }
    }
    #endregion

    #region イベントハンドラ（マウスカーソル移動時）
    /// <summary>
    ///     マウスカーソル移動時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    void PointerGestureRecognizer_PointerMoved(object sender, PointerEventArgs e)
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
            PointingDeviceCurrentPoint = new Models.Geometric.PointFloat(
                new Models.Geometric.XFloat((float)tappedPoint.Value.X),
                new Models.Geometric.YFloat((float)tappedPoint.Value.Y));
            // Trace.WriteLine($"[TileCropPage PointerGestureRecognizer_PointerMoved] moved x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

            // タイル・フォームの表示更新
            RefreshTileForm();
            context.TrickRefreshCanvasOfTileCursor(codePlace: "[TileCropPage.xml.cs PointerGestureRecognizer_PointerMoved 疑似マウスドラッグ]");
        }
    }
    #endregion

    #region イベントハンドラ（表面の描画時）
    /// <summary>
    ///     表面の描画時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    void skiaView1_PaintSurface(object sender, SkiaSharp.Views.Maui.SKPaintSurfaceEventArgs e)
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

        //
        // 切抜きカーソルの中身
        //
        TileRecordViewModel tileRecordViewModel;

        // 切抜きカーソルの中身を取得
        if (context.SelectedTileVMOption.TryGetValue(out TileRecordViewModel? tileRecordViewModelOrNull))
        {
            tileRecordViewModel = tileRecordViewModelOrNull ?? throw new NullReferenceException(nameof(tileRecordViewModelOrNull));
        }
        else
        {
            // 空カーソルなら、ここに来ない（何もしない）
            throw new InvalidOperationException("[TileCropPage.xaml.cs AddsButton_Clicked] cropped cursor is nothing");
        }

        if (context.SelectedTileId == Models.TileId.Empty)
        {
            // Ｉｄが無いということは、新規作成だ

            // 新しいＩｄを追加
            context.SelectedTileId = context.TilesetSettingsVM.UsableId;

            context.TilesetSettingsVM.IncreaseUsableId();

            // ビューの再描画（タイルＩｄ更新）
            context.NotifyTileIdChange();

            // タイルを新規登録（ダミー値。あとですぐ上書きする）
            context.TilesetSettingsVM.AddTile(
                id: context.SelectedTileId,
                rect: RectangleInt.Empty,
                workingRect: RectangleFloat.Empty,
                comment: Models.Comment.Empty,
                logicalDelete: Models.LogicalDelete.False);
        }

        //
        // この時点で、タイルは必ず登録されている
        //

        // タイルＩｄを使って、タイル・レコードを取得、その内容を上書き
        if (context.TilesetSettingsVM.TryGetTileById(context.SelectedTileId, out TileRecordViewModel? tileRecordVMOrNull))
        {
            TileRecordViewModel recordVM = tileRecordVMOrNull ?? throw new NullReferenceException(nameof(tileRecordVMOrNull));

            recordVM.SourceRectangle = context.SourceCroppedCursorRect;
            recordVM.WorkingRectangle = context.SourceCroppedCursorRect.Do(context.Zoom);
            recordVM.Comment = new Models.Comment(context.SelectedTileCommentAsStr);
            recordVM.LogicalDelete = Models.LogicalDelete.False;
        }

        //
        // 設定ファイルの保存
        // ==================
        //
        if (context.TilesetSettingsVM.SaveCSV(context.TilesetSettingsFile))
        {
            // 保存成功
        }
        else
        {
            // TODO 保存失敗時のエラー対応
        }

        //
        // カラーマップの再描画
        // ====================
        //
        this.coloredMapGraphicsView1.Invalidate();
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

        //
        // 設定ファイルの編集
        // ==================
        //
        //      - 選択中のタイルを論理削除
        //
        if (context.TilesetSettingsVM.DeleteLogical(
            // 現在選択中のタイルのＩｄ
            id: context.SelectedTileId))
        {
            // タイルセット設定ビューモデルに変更あり
            context.InvalidateTilesetSettingsVM();
        }

        Trace.WriteLine($"[TileCropPage.xml.cs DeletesButton_Clicked] タイルを論理削除 context.SelectedTileId: [{context.SelectedTileId.AsBASE64}]");

        //
        // 設定ファイルの保存
        // ==================
        //
        if (context.TilesetSettingsVM.SaveCSV(context.TilesetSettingsFile))
        {
            // 保存成功
        }
        else
        {
            // TODO 保存失敗時のエラー対応
        }

        //
        // カラーマップの再描画
        // ====================
        //
        this.coloredMapGraphicsView1.Invalidate();
    }
    #endregion

    #region イベントハンドラ（ボタンにマウスカーソル進入時）
    /// <summary>
    ///     ボタンにマウスカーソル進入時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    void Button_PointerGestureRecognizer_PointerEntered(object sender, PointerEventArgs e)
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
    void Button_PointerGestureRecognizer_PointerExited(object sender, PointerEventArgs e)
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
    void LocalePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var context = this.TileCropPageVM;
        context.InvalidateLocale();
    }
    #endregion

    #region イベントハンドラ（ズーム変更時）
    /// <summary>
    ///     ズーム変更時
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
}
