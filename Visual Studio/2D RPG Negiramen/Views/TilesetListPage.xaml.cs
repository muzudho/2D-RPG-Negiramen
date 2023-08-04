﻿namespace _2D_RPG_Negiramen.Views;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.FileEntries;
using _2D_RPG_Negiramen.ViewModels;
using SkiaSharp;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

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
        // TODO ここで横幅を取得する方法が分からない
        //Trace.WriteLine($"[TilesetListPage.xaml.cs TilesetListPage] this.WidthRequest: {this.WidthRequest}, this.MaximumWidthRequest: {this.MaximumWidthRequest}, this.MinimumWidthRequest: {this.MinimumWidthRequest}, this.Width: {this.Width}");

        //Window window = this.GetParentWindow();

        //Trace.WriteLine($"[TilesetListPage.xaml.cs TilesetListPage] window.Width: {window.Width}, window.MaximumWidth: {window.MaximumWidth}, this.MinimumWidth: {window.MinimumWidth}");

        //// セル・サイズ（固定幅）
        double cellWidth = 128.0f;
        int cellColumns = (int)(App.WidthForCollectionView / cellWidth);
        // int cellColumns = (int)(this.Width / cellWidth);
        // int cellColumns = Random.Shared.Next(5, 8);
        // int cellColumns = 4;

        this.BindingContext = new TilesetListPageViewModel(
            itemsLayout: new GridItemsLayout(
                span: cellColumns,
                orientation: ItemsLayoutOrientation.Vertical));

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

    // - プライベート静的メソッド

    /// <summary>
    ///     タイルセット画像をネギラーメンに取り込んだ後の処理を自動で行う
    ///     
    ///     <list type="bullet">
    ///         <item>画像ファイルを縮小して（サムネイル画像を作り）、キャッシュ・フォルダーへコピーする</item>
    ///     </list>
    /// </summary>
    async void FollowAutomaticallyAsync(
        string originalPngPathAsStr)
    {
        ITilesetListPageViewModel context = this.TilesetListPageVM;

        // 画像ファイルの名前は UUID という想定
        var uuid = System.IO.Path.GetFileNameWithoutExtension(originalPngPathAsStr);

        // TODO TOML があれば読込む。無ければ新規作成
        string tomlPathAsStr = System.IO.Path.Join(
            System.IO.Path.GetDirectoryName(originalPngPathAsStr),
            $"{uuid}.toml");
        if (System.IO.File.Exists(tomlPathAsStr))
        {
            // TODO ★ Tileset 設定 TOML ファイル読込
        }
        else
        {
            // TODO TOML 新規作成
        }

        try
        {
            // サムネイル画像出力先ディレクトリー
            var thumbnailOutputFolder = App.CacheFolder.YourCircleFolder.YourWorkFolder.ImagesFolder.TilesetFolder.ImagesTilesetsThumbnailsFolder;
            // ディレクトリーが無ければ作成する
            thumbnailOutputFolder.CreateThisDirectoryIfItDoesNotExist();

            // タイルセット元画像のプロパティーズ
            var tilesetImageProperties = await TilesetImageProperties.ReadAsync(
                originalPngPathAsStr: originalPngPathAsStr);

            // サムネイル画像のプロパティーズ
            var tilesetThumbnailImageProperties = TilesetThumbnailImageProperties.Create(
                originalPngPathAsStr: originalPngPathAsStr,
                originalWidth: tilesetImageProperties.Width,
                originalHeight: tilesetImageProperties.Height,
                outputFolder: thumbnailOutputFolder);

            //
            // サムネイル書出し
            // ================
            //
            {
                // ビットマップ作成
                SKBitmap thumbnailBitmap = TilesetThumbnailImageHelper.CreateBitmap(
                    originalBitmap: tilesetImageProperties.Bitmap,
                    tilesetThumbnailImageProperties: tilesetThumbnailImageProperties);

                // 画像書出し
                TilesetThumbnailImageHelper.WriteImage(
                    thumbnailPathAsStr: tilesetThumbnailImageProperties.PathAsStr,
                    thumbnailBitmap: thumbnailBitmap);
            }

            context.EnqueueTilesetRecordVM(new TilesetRecordViewModel(
            // UUID文字列
                uuidAsStr: uuid,
                // PNG元画像のファイルパス文字列
                filePathAsStr: originalPngPathAsStr,
                // PNG元画像の横幅
                widthAsInt: tilesetImageProperties.Width,
                // PNG元画像の縦幅
                heightAsInt: tilesetImageProperties.Height,
                // サムネイル画像へのファイルパス文字列
                thumbnailFilePathAsStr: tilesetThumbnailImageProperties.PathAsStr,
                // サムネイル画像の横幅
                thumbnailWidthAsInt: tilesetThumbnailImageProperties.Width,
                // サムネイル画像の縦幅
                thumbnailHeightAsInt: tilesetThumbnailImageProperties.Height,
                // 画面に表示する画像タイトル
                title: "たいとる１"));
        }
        catch (Exception ex)
        {
            // TODO エラー対応どうする？
            Trace.WriteLine(ex);
        }
    }

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
        // ビューモデルの取得
        // ==================
        //
        ITilesetListPageViewModel context = this.TilesetListPageVM;

        {
            TilesetListPage page = (TilesetListPage)sender;

            // セル・サイズ（固定幅）
            double cellWidth = 128.0f;
            int cellColumns = (int)(page.Width / cellWidth);

            context.ItemsLayout = new GridItemsLayout(cellColumns, ItemsLayoutOrientation.Vertical);
        }

        ////
        //// スターターキット設定の読込
        //// ==========================
        ////
        //StarterKitConfiguration starterKitConfiguration = App.GetOrLoadStarterKitConfiguration();

        // タイルセット画像が入っているフォルダを取得
        var tilesetFolder = App.GetOrLoadProjectConfiguration().UnityAssetsFolder.YourCircleFolder.YourWorkFolder.AutoGeneratedFolder.ImagesFolder.TilesetsFolder;

        var taskList = new List<Task>();

        // フォルダの中の PNG画像ファイルを一覧
        foreach (var originalPngPathAsStr in System.IO.Directory.GetFiles(tilesetFolder.Path.AsStr, "*.png"))
        {
            Trace.WriteLine($"[TilesetListPage.xaml.cs ContentPage_Loaded] path: [{originalPngPathAsStr}]");

            // 画像ファイルを縮小して（サムネイル画像を作り）、キャッシュ・フォルダーへコピーしたい
            var task = Task.Run(() => this.FollowAutomaticallyAsync(
                originalPngPathAsStr: originalPngPathAsStr));

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

        await Shell.Current.GoToAsync("//HomePage");
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    private void ContentPage_SizeChanged(object sender, EventArgs e)
    {
        //ITilesetListPageViewModel context = (ITilesetListPageViewModel)this.BindingContext;

        //TilesetListPage page = (TilesetListPage)sender;


        //// セル・サイズ（固定幅）
        //double cellWidth = 128.0f;
        //// double cellHeight = 148.0f;

        //int cellColumns = (int)(page.Width / cellWidth);
        //// int cellRows = (int)(page.Height / cellHeight);

        //// Trace.WriteLine($"コンテント・ページ・サイズ変更 sender: {sender.GetType().FullName} cellColumns: {cellColumns}, Width: {page.Width}, Height: {page.Height}, WidthRequest: {page.WidthRequest}, HeightRequest: {page.HeightRequest}");

        //// this.TilesetListPageVM.ItemsLayout = new GridItemsLayout(cellColumns, ItemsLayoutOrientation.Vertical);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    private void CollectionView_SizeChanged(object sender, EventArgs e)
    {
        var context = this.TilesetListPageVM;

        CollectionView view = (CollectionView)sender;

        // セル・サイズ（固定幅）
        double cellWidth = 128.0f;
        int cellColumns = (int)(view.Width / cellWidth);

        context.ItemsLayout = new GridItemsLayout(cellColumns, ItemsLayoutOrientation.Vertical);

        //GridItemsLayout layout = (GridItemsLayout)view.ItemsLayout;
        //layout.Span = cellColumns;
        //view.ItemsLayout = null;
        //view.ItemsLayout = layout;
        //// view.ItemsLayout = new GridItemsLayout(cellColumns, ItemsLayoutOrientation.Vertical);
        //// view.Opacity = view.Opacity - 0.01d;
        //view.RotateTo(5.0, length:10000);

        //Trace.WriteLine($"コレクション・ビュー・サイズ変更 sender: {sender.GetType().FullName}, cellColumns: {cellColumns}, Width: {view.Width}, Height: {view.Height} layout.Span: {layout.Span}");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ITilesetListPageViewModel context = (ITilesetListPageViewModel)this.BindingContext;

        CollectionView view = (CollectionView)sender;

        // Trace.WriteLine($"[TilesetListPage.xaml.cs CollectionView_SelectionChanged] 選択変更 e.PreviousSelection: {e.PreviousSelection.GetType().FullName} e.CurrentSelection: {e.CurrentSelection.GetType().FullName}");
        // [TilesetListPage.xaml.cs CollectionView_SelectionChanged] 選択変更 e.PreviousSelection: System.Collections.Generic.List`1[[System.Object, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]] e.CurrentSelection: System.Collections.Generic.List`1[[System.Object, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]

        TilesetRecordViewModel record = (TilesetRecordViewModel)view.SelectedItem;

        // 選択ファイル・ステム
        context.SelectedFileStem = System.IO.Path.GetFileNameWithoutExtension(record.FilePathAsStr);

        if (UUIDHelper.IsMatch(context.SelectedFileStem))
        {
            // UUID だ
            context.IsEnabledTileCropButton = true;
            context.IsEnabledRenameFileNameToUUIDButton = false;
        }
        else
        {
            // UUID ではない
            context.IsEnabledTileCropButton = false;
            context.IsEnabledRenameFileNameToUUIDButton = true;
        }

        // 選択ファイル拡張子
        context.SelectedFileExtension = System.IO.Path.GetExtension(record.FilePathAsStr);
    }

    /// <summary>
    ///     ［タイル切抜き］ボタン・クリック時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void TileCropButton_Clicked(object sender, EventArgs e)
    {
        var shellNavigationState = new ShellNavigationState("//TileCropPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await CodeBehindHelper.ReadyGoToNext(
            onOk: async () =>
            {
                // ユニティのアセット・フォルダへのパス
                var unityAssetsFolderPathAsStr = App.GetOrLoadProjectConfiguration().UnityAssetsFolder.Path.AsStr;

                // TODO ★ ファイル名の拡張子抜き
                var fileStem = "E7911DAD-15AC-44F4-A95D-74AB940A19FB";

                // タイルセット画像ファイル・パス
                var tilesetImageFile = App.GetOrLoadProjectConfiguration().UnityAssetsFolder.YourCircleFolder.YourWorkFolder.AutoGeneratedFolder.ImagesFolder.TilesetsFolder.CreateTilesetPng(fileStem);

                // タイルセットCSVファイル・パス
                var tilesetSettingsFile = App.GetOrLoadProjectConfiguration().UnityAssetsFolder.YourCircleFolder.YourWorkFolder.AutoGeneratedFolder.DataFolder.CsvFolder.TilesetsFolder.CreateTilesetCsv(fileStem);

                await Shell.Current.GoToAsync(
                    state: shellNavigationState,
                    parameters: new Dictionary<string, object>
                    {
                        [key: "TilesetImageFile"] = tilesetImageFile,
                        [key: "TilesetSettingsFile"] = tilesetSettingsFile,
                    });
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await CodeBehindHelper.GoToConfigurationPage(this, shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }

    /// <summary>
    ///     ［ファイル・ステムをＵＵＩＤに変更する］ボタン・クリック時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    private void RenameFileNameToUUIDButton_Clicked(object sender, EventArgs e)
    {

    }

    /// <summary>
    ///     ［インポート］ボタン・クリック時
    ///     
    ///     <list type="bullet">
    ///         <item>📖 [jfversluis　＞　MauiFilePickerSample](https://github.com/jfversluis/MauiFilePickerSample)</item>
    ///     </list>
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void ImportButton_Clicked(object sender, EventArgs e)
    {
        var context = this.TilesetListPageVM;

        // For custom file types            
        var customFileType =
            new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                 //{ DevicePlatform.iOS, new[] { "com.adobe.pdf" } }, // or general UTType values
                 //{ DevicePlatform.Android, new[] { "application/pdf" } },
                 { DevicePlatform.WinUI, new[] { ".png" } },
                 //{ DevicePlatform.Tizen, new[] { "*/*" } },
                 //{ DevicePlatform.macOS, new[] { "pdf"} }, // or general UTType values
        	});

        var results = await FilePicker.PickMultipleAsync(new PickOptions
        {
            FileTypes = customFileType,
            // ↓ Picker Tilte is not working
            // PickerTitle = "タイルセット画像を開く",
        });

        foreach (var result in results)
        {
            // ファイルの読取ストリームを開くところまでしてくれる
            // var stream = await result.OpenReadAsync();

            // 画像コントロールへ、画像データをセット
            // myImage.Source = ImageSource.FromStream(() => stream);

            // ダイアログボックスのようなものを表示する
            // なんか初回は　ボタンがしばらくフリーズしていて押せない？
            // await DisplayAlert("You picked...", result.FileName, "OK");

            // ファイル・ステムが UUID かどうか区別したい
            if (UUIDHelper.IsMatch(System.IO.Path.GetFileNameWithoutExtension(result.FileName)))
            {
                // TOOD 読込む
                Trace.WriteLine($"[TilesetListPage ImportButton_Clicked] 画像ファイル［{result.FileName}］を読込む");
            }
            else
            {
                // UUID を発行
                var uuid = new UUID();

                var tilesetPngLocation = App.GetOrLoadProjectConfiguration().UnityAssetsFolder.YourCircleFolder.YourWorkFolder.AutoGeneratedFolder.ImagesFolder.TilesetsFolder.CreateTilesetPng(uuid.AsStr);
                var newBasename = System.IO.Path.GetFileName(tilesetPngLocation.Path.AsStr);

                // ファイル名を変更していいか、確認する
                string title = "You picked...";
                string message = $"画像ファイル［{result.FullPath}］は、ネギラーメンの中では［{newBasename}］というファイル名にします。";

                bool isOk = await DisplayAlert(title, message, "OK", "Cancel");

                if (isOk)
                {
                    // ファイル・コピー
                    Trace.WriteLine($"[TilesetListPage ImportButton_Clicked] result.FullPath: TODO Copy ［{result.FullPath}］ to ［{tilesetPngLocation.Path.AsStr}］");
                    File.Copy(
                        sourceFileName: result.FullPath,
                        destFileName: tilesetPngLocation.Path.AsStr);

                    // 画像ファイルを縮小して（サムネイル画像を作り）、キャッシュ・フォルダーへコピーしたい
                    await Task.Run(() => this.FollowAutomaticallyAsync(
                        originalPngPathAsStr: tilesetPngLocation.Path.AsStr));
                }
                else
                {

                }
            }
        }
    }
}