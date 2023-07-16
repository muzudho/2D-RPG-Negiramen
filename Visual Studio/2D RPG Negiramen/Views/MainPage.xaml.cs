namespace _2D_RPG_Negiramen.Views;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.ViewModels;
using CommunityToolkit.Maui.Views;
using System.Diagnostics;

/// <summary>
///     😁 メイン・ページ
/// </summary>
public partial class MainPage : ContentPage
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    public MainPage()
    {
        InitializeComponent();
    }
    #endregion

    // - パブリック・プロパティ

    #region プロパティ（ローカライゼーション資源管理インスタンス）
    /// <summary>
    ///     ローカライゼーション資源管理インスタンス
    /// </summary>
    public LocalizationResourceManager LocalizationResourceManager
        => LocalizationResourceManager.Instance;
    #endregion

    // - プライベート・メソッド

    #region メソッド（環境が構成ファイル通りか判定する）
    /// <summary>
    ///     環境が構成ファイル通りか判定する
    ///     
    ///     <list type="bullet">
    ///         <item>構成ファイルの設定は、ユーザーは苦手とするだろうから、必要となるまで設定を要求しないようにする仕掛け</item>
    ///         <item>📖 [同期メソッドを非同期メソッドに変換する（ex. Action → Func＜Task＞）](https://qiita.com/mxProject/items/81ba8dd331484717ee01)</item>
    ///     </list>
    /// </summary>
    /// <paramref name="onNotYetConfiguration">構成ファイル通りだ</paramref>
    /// <paramref name="onNotYetConfiguration">構成ファイル通りではない</paramref>
    async Task ReadyGoToNext(
        Func<Task> onOk,
        Func<Task> onNotYetConfiguration)
    {
        // 構成を取得
        var configuration = App.GetOrLoadConfiguration();

        // 構成通り準備できているなら、そのまま画面遷移する
        if (configuration.IsReady())
        {
            await onOk();
        }
        // そうでなければ、初期構成を要求
        else
        {
            await onNotYetConfiguration();
            // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
        }
    }
    #endregion

    #region メソッド（構成ページへ移動）
    /// <summary>
    ///     <pre>
    ///         構成ページへ移動
    ///         
    ///         本来の移動先をグローバル変数へ記憶して、構成ページへ移動。
    ///         構成が終わったら、一旦構成ページから戻ったあと、本来の移動先へ遷移
    ///     </pre>
    /// </summary>
    /// <param name="shellNavigationState">本来の移動先</param>
    async Task GoToConfigurationPage(ShellNavigationState shellNavigationState)
    {
        App.NextPage.Push(shellNavigationState);
        await Navigation.PushAsync(new StartupConfigurationPage());
        // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
    }
    #endregion

    // - プライベート・イベントハンドラ

    #region イベントハンドラ（［マップを作る］ボタン押下時）
    /// <summary>
    /// ［マップを作る］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void CreateMapViewBtn_Clicked(object sender, EventArgs e)
    {
        var shellNavigationState = new ShellNavigationState("//MapExplorerPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await ReadyGoToNext(
            onOk: async () =>
            {
                await Shell.Current.GoToAsync(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await GoToConfigurationPage(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }
    #endregion

    #region イベントハンドラ（［戦闘を作る］ボタン押下時）
    /// <summary>
    /// ［戦闘を作る］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void CreateBattleBtn_Clicked(object sender, EventArgs e)
    {
        var shellNavigationState = new ShellNavigationState("//CreateBattleViewPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await ReadyGoToNext(
            onOk: async () =>
            {
                await Shell.Current.GoToAsync(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await GoToConfigurationPage(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }
    #endregion

    #region イベントハンドラ（［メニューを作る］ボタン押下時）
    /// <summary>
    /// ［メニューを作る］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void CreateMenuViewBtn_Clicked(object sender, EventArgs e)
    {
        var shellNavigationState = new ShellNavigationState("//CreateMenuViewPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await ReadyGoToNext(
            onOk: async () =>
            {
                await Shell.Current.GoToAsync(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await GoToConfigurationPage(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }
    #endregion

    #region イベントハンドラ（［会話画面を作る］ボタン押下時）
    /// <summary>
    /// ［会話画面を作る］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void CreateTalkingBtn_Clicked(object sender, EventArgs e)
    {
        var shellNavigationState = new ShellNavigationState("//CreateTalkingViewPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await ReadyGoToNext(
            onOk: async () =>
            {
                await Shell.Current.GoToAsync(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await GoToConfigurationPage(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }
    #endregion

    #region イベントハンドラ（［プレイヤー・キャラクターを編集］ボタン押下時）
    /// <summary>
    /// ［プレイヤー・キャラクターを編集］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void EditPlayerCharacterBtn_Clicked(object sender, EventArgs e)
    {
        var shellNavigationState = new ShellNavigationState("//EditPlayerCharacterPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await ReadyGoToNext(
            onOk: async () =>
            {
                await Shell.Current.GoToAsync(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await GoToConfigurationPage(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }
    #endregion

    #region イベントハンドラ（［モンスターを編集］ボタン押下時）
    /// <summary>
    /// ［モンスターを編集］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void CreateMonsterBtn_Clicked(object sender, EventArgs e)
    {
        var shellNavigationState = new ShellNavigationState("//EditMonsterPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await ReadyGoToNext(
            onOk: async () =>
            {
                await Shell.Current.GoToAsync(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await GoToConfigurationPage(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }
    #endregion

    #region イベントハンドラ（［モンスター・グループを編集］ボタン押下時）
    /// <summary>
    /// ［モンスター・グループを編集］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void EditMonsterGroupBtn_Clicked(object sender, EventArgs e)
    {
        var shellNavigationState = new ShellNavigationState("//EditMonsterGroupPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await ReadyGoToNext(
            onOk: async () =>
            {
                await Shell.Current.GoToAsync(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await GoToConfigurationPage(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }
    #endregion

    #region イベントハンドラ（［アイテムを編集］ボタン押下時）
    /// <summary>
    /// ［アイテムを編集］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void EditItemBtn_Clicked(object sender, EventArgs e)
    {
        var shellNavigationState = new ShellNavigationState("//EditItemPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await ReadyGoToNext(
            onOk: async () =>
            {
                await Shell.Current.GoToAsync(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await GoToConfigurationPage(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }
    #endregion

    #region イベントハンドラ（［話しを編集］ボタン押下時）
    /// <summary>
    /// ［話しを編集］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void EditorStoryBtn_Clicked(object sender, EventArgs e)
    {
        var shellNavigationState = new ShellNavigationState("//EditStoryPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await ReadyGoToNext(
            onOk: async () =>
            {
                await Shell.Current.GoToAsync(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await GoToConfigurationPage(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }
    #endregion

    #region イベントハンドラ（［初期設定］ボタン押下時）
    /// <summary>
    /// ［初期設定］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void StartupConfigurationBtn_Clicked(object sender, EventArgs e)
    {
        // 必ず、初期設定を要求
        // 戻り先はホーム
        App.NextPage.Push(new ShellNavigationState("//MainPage"));
        await Navigation.PushAsync(new StartupConfigurationPage());
        // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
    }
    #endregion

    #region イベントハンドラ（［ウィンドウ表示テスト］ボタン押下時）
    /// <summary>
    /// ［ウィンドウ表示テスト］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void TestShowWindowButton_Clicked(object sender, EventArgs e)
    {
        /*
        var secondWindow = new Window
        {
            Page = new StartupConfigurationPage
            {
                // ...
            },
            Width = 1200,
            Height = 400,
        };

        Application.Current.OpenWindow(secondWindow);
        */

        await Navigation.PushAsync(new StartupConfigurationPage());
    }
    #endregion

    #region イベントハンドラ（［ポップアップ練習］ボタン押下時）
    /// <summary>
    /// ［ポップアップ練習］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    void PopupPracticeButton_Clicked(object sender, EventArgs e)
    {
        var popup = new PopupPractice();

        this.ShowPopup(popup);
    }
    #endregion

    #region イベントハンドラ（［タイル・パレット表示］ボタン押下時）
    /// <summary>
    /// ［タイル・パレット表示］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    void ShowTilePalette_Clicked(object sender, EventArgs e)
    {
        var secondWindow = new Window
        {
            Page = new TilePalettePage
            {
                // ...
            },
            Width = 600,
            Height = 300,
        };

        Application.Current.OpenWindow(secondWindow);
    }
    #endregion

    #region イベントハンドラ（［マップ描画ページ表示］ボタン押下時）
    /// <summary>
    /// ［マップ描画ページ表示］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    void MapDrawingButton_Clicked(object sender, EventArgs e)
    {
        var secondWindow = new Window
        {
            Page = new MapDrawingPage
            {
                // ...
            },
            Width = 600,
            Height = 300,
        };

        Application.Current.OpenWindow(secondWindow);
    }
    #endregion

    #region イベントハンドラ（［タイル・パレット編集］ボタン押下時）
    /// <summary>
    /// ［タイル・パレット編集］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void TilePaletteEditButton_Clicked(object sender, EventArgs e)
    {
        // Trace.WriteLine($"[MainPage.xaml.cs TilePaletteEditButton_Clicked] sender.GetType().FullName: {sender.GetType().FullName}");
        // [MainPage.xaml.cs TilePaletteEditButton_Clicked] sender.GetType().FullName: Microsoft.Maui.Controls.Button

        await ButtonAnimationHelper.DoIt((Button)sender);

        var shellNavigationState = new ShellNavigationState("//TilePaletteEditPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await ReadyGoToNext(
            onOk: async () =>
            {
                // ユニティのアセット・フォルダーへのパス
                var unityAssetsFolderPathAsStr = App.GetOrLoadConfiguration().UnityAssetsFolder.Path.AsStr;

                // ファイル名の拡張子抜き
                var fileStem = "map-tile-format-8x19";

                // タイル・セット画像ファイル・パス
                var tileSetImageFile = new Models.FileEntries.Locations.TileSetImageFile(
                    pathSource: FileEntryPathSource.FromString(System.IO.Path.Combine(unityAssetsFolderPathAsStr,
                                                                                      $"Doujin Circle Negiramen/Negiramen Quest/Auto Generated/Images/Tile Set/{fileStem}.png")),
                    convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                replaceSeparators: true));

                // タイル・セットCSVファイル・パス
                var tileSetSettingsFile = new Models.FileEntries.Locations.TileSetSettingsFile(
                    pathSource: FileEntryPathSource.FromString(System.IO.Path.Combine(unityAssetsFolderPathAsStr,
                                                                                      $"Doujin Circle Negiramen/Negiramen Quest/Auto Generated/Data/CSV/Tile Set/{fileStem}.csv")),
                    convert: (pathSource)=>FileEntryPath.From(pathSource,
                                                              replaceSeparators: true));

                // タイル・セット画像の縦横幅
                var tileSetSize = Models.FileEntries.PNGHelper.GetImageSize(tileSetImageFile);

                // グリッドの線の幅（初期値）
                ThicknessOfLine gridLineThickness = new ThicknessOfLine(2);

                await Shell.Current.GoToAsync(
                    state: shellNavigationState,
                    parameters: new Dictionary<string, object>
                    {
                        [key: "TileSetImageFile"] = tileSetImageFile,
                        [key: "TileSetSettingsFile"] = tileSetSettingsFile,
                        [key: "ImageSize"] = tileSetSize,
                        // グリッドの線の太さを 2px と想定しているので、グリッドの線が画像の端っこで切れないように、グリッドの内部的キャンバス・サイズを 2px 広げる
                        [key: "GridCanvasSize"] = new Models.Size(new Models.Width(tileSetSize.Width.AsInt + gridLineThickness.AsInt), new Models.Height(tileSetSize.Height.AsInt + gridLineThickness.AsInt)),
                        [key: "GridLeftTop"] = new Models.Point(new Models.X(0), new Models.Y(0)),
                        [key: "GridTileSize"] = new Models.Size(new Models.Width(32), new Models.Height(32)),
                    });
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await GoToConfigurationPage(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }
    #endregion
}
