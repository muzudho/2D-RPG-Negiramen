namespace _2D_RPG_Negiramen.Views;

using _2D_RPG_Negiramen.ViewModels;
using System.Diagnostics;
using _2D_RPG_Negiramen.ViewHistory.TileCropPage;
using _2D_RPG_Negiramen.Models;

#if IOS || ANDROID || MACCATALYST
using Microsoft.Maui.Graphics.Platform;
using _2D_RPG_Negiramen.Models.Geometric;
using _2D_RPG_Negiramen.Models.Visually;
#elif WINDOWS
using Microsoft.Maui.Graphics.Win2D;
using System.Net;
using SkiaSharp;
using _2D_RPG_Negiramen.FeatSkia;
using _2D_RPG_Negiramen.Models.Geometric;
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
    void thisContentPage_NavigatedTo(object sender, NavigatedToEventArgs e) => ((TileCropPageViewModel)this.BindingContext).OnNavigatedTo(this.skiaTilesetCanvas1);
    #endregion

    #region イベントハンドラ（ロケール変更時）
    /// <summary>
    ///     ロケール変更時
    ///     
    ///     <list type="bullet">
    ///         <item>ＸＡＭＬではなく、Ｃ＃で動的に翻訳を行っている場合のための変更通知</item>
    ///     </list>
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    void LocalePicker_SelectedIndexChanged(object sender, EventArgs e) => ((TileCropPageViewModel)this.BindingContext).Corridor.InvalidateByLocale();
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
    void TilesetImage_OnTapped(object sender, TappedEventArgs e) => ((TileCropPageViewModel)this.BindingContext).OnTilesetImageTapped(
            tappedPoint: e.GetPosition((Element)sender) ?? Point.Zero);

    /// <summary>
    ///     ［タイルセット］画像ポインター移動時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    void TilesetImage_OnPointerMoved(object sender, PointerEventArgs e) => ((TileCropPageViewModel)this.BindingContext).OnTilesetImagePointerMove(
            tappedPoint: e.GetPosition((Element)sender) ?? Point.Zero);

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

        ((TileCropPageViewModel)this.BindingContext).OnAddsButtonClicked();
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
        context.Corridor.RoomsideDoors.DeletesButton.RemoveTile(
            doRemoveRegisteredTIle: (TileIdOrEmpty tileIdOrEmpty) =>
            {
                App.History.Do(new RemoveRegisteredTileProcessing(
                    gardensideDoor: context.Corridor.GardensideDoor,    // 権限を委譲
                    tileIdOrEmpty: tileIdOrEmpty));

                ((TileCropPageViewModel)this.BindingContext).InvalidateForHistory();
            });
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
    private void UndoImageButton_Clicked(object sender, EventArgs e) => App.History.Undo();
    #endregion

    #region イベントハンドラ（［リドゥ］ボタン・クリック時）
    /// <summary>
    ///     ［リドゥ］ボタン・クリック時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    private void RedoImageButton_Clicked(object sender, EventArgs e) => App.History.Redo();
    #endregion

    #region イベントハンドラ（［タイル・タイトル］エンター入力時）
    /// <summary>
    ///     ［タイル・タイトル］エンター入力時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    private void TileTitleEntry_Completed(object sender, EventArgs e)
    {
        ((TileCropPageViewModel)this.BindingContext).OverwriteTile();
    }
    #endregion
}
