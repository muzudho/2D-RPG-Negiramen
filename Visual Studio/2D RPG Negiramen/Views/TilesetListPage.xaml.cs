﻿namespace _2D_RPG_Negiramen.Views;

using _2D_RPG_Negiramen.Models.FileEntries;
using _2D_RPG_Negiramen.ViewModels;
using Microsoft.Maui.Graphics.Win2D;
using System.Diagnostics;
using System.Threading.Tasks;
using TheGraphics = Microsoft.Maui.Graphics;

/// <summary>
///     😁 タイルセット一覧ページ
/// </summary>
public partial class TilesetListPage : ContentPage
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    public TilesetListPage()
    {
        InitializeComponent();
    }
    #endregion

    // - パブリック・プロパティ

    #region プロパティ（ビューモデル）
    /// <summary>
    ///     ビューモデル
    /// </summary>
    public ITilesetListPageViewModel TilesetListPageVM => (ITilesetListPageViewModel)this.BindingContext;
    #endregion

    // - プライベート・イベントハンドラ

    #region イベントハンドラ（ページ読込完了時）
    /// <summary>
    ///     ページ読込完了時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        //
        // ユーザー設定の読込
        // ==================
        //
        UserConfiguration userConfiguration = App.GetOrLoadUserConfiguration();

        //
        // ビューモデルの取得
        // ==================
        //
        TilesetListPageViewModel context = (TilesetListPageViewModel)this.BindingContext;

        // タイルセット画像が入っているフォルダを取得
        var tilesetFolder = App.GetOrLoadConfiguration().UnityAssetsFolder.YourCircleNameFolder.YourWorkNameFolder.AutoGeneratedFolder.ImagesFolder.TilesetsFolder;

        List<Task> taskList = new List<Task>();

        // フォルダの中の PNG画像ファイルを一覧
        foreach (var pathAsStr in System.IO.Directory.GetFiles(tilesetFolder.Path.AsStr, "*.png"))
        {
            Trace.WriteLine($"[TilesetListPage.xaml.cs ContentPage_Loaded] path: [{pathAsStr}]");

            //
            // TODO 画像ファイルを縮小して（サムネイル画像を作り）、キャッシュ・フォルダーへコピーしたい
            //
            var task = Task.Run(() =>
            {
                try
                {
                    // タイルセット画像読込
                    using (Stream inputFileStream = System.IO.File.OpenRead(pathAsStr))
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
                            var folder = App.CacheFolder.YourCircleNameFolder.YourWorkNameFolder.ImagesFolder.TilesetFolder.ImagesTilesetsThumbnailsFolder;
                            folder.CreateThisDirectoryIfItDoesNotExist();

                            // 書出先（ウィンドウズ・ローカルＰＣ）
                            var fileStem = System.IO.Path.GetFileNameWithoutExtension(pathAsStr);
                            using (Stream outputFileStream = System.IO.File.Open(
                                path: folder.CreateTilesetThumbnailPng(fileStem).Path.AsStr,
                                mode: FileMode.OpenOrCreate))
                            {
                                image.Save(outputFileStream);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // TODO エラー対応どうする？
                    Trace.WriteLine(ex);
                }
            });

            taskList.Add(task);
        }

        Task.WaitAll(taskList.ToArray());
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
        // ＸＡＭＬではなく、Ｃ＃で動的に翻訳を行っている場合のための変更通知
        var context = this.TilesetListPageVM;
        context.InvalidateLocale();
    }
    #endregion
}